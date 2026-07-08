# Documento de Arquitetura

## Sistema de Gestão de Clientes — Cooperativa Financeira Alfa

### Modernização de Sistema Legado (Integração .NET ↔ COBOL)

  

---

  

## Arquitetura Escolhida

  

O sistema adota uma **arquitetura híbrida de integração direta em processo (in-process bridge)**, na qual uma aplicação desktop em **C# / Windows Forms (.NET, `net10.0-windows`)** consome, via **P/Invoke**, um conjunto de programas legados escritos em **COBOL (GnuCOBOL)**, compilados como bibliotecas dinâmicas (`.dll`). Não há processo COBOL separado nem chamada de rede: o runtime do GnuCOBOL (`libcob-4`) é carregado **dentro do próprio processo .NET**, e cada operação de negócio é uma chamada de função nativa síncrona.

  

```

┌─────────────────────────────────────────────────────────────┐

│                    APLICAÇÃO .NET (WinForms)                 │

│                                                                │

│   Program.cs  ──inicia──▶  FormMain.cs (UI, 2 abas)           │

│                                   │                           │

│                                   ▼                           │

│                         CobolService.cs (camada de serviço)   │

│                    [DllImport] P/Invoke, Cdecl                │

└───────────────────────────────┬────────────────────────────────┘

                                 │  buffer de 96 bytes (in/out)

                                 ▼

┌─────────────────────────────────────────────────────────────┐

│                 RUNTIME NATIVO GnuCOBOL (libcob-4)             │

│                                                                │

│   consulta.dll   alteracao.dll   exclusao.dll   inclusao.dll  │

│        │               │              │              │        │

│        └───────────────┴──────┬───────┴──────────────┘        │

│                                ▼                              │

│                      CLIENTES.DAT (LINE SEQUENTIAL)            │

└─────────────────────────────────────────────────────────────┘

```

  

A arquitetura é organizada em três camadas nítidas:

  

1. **Camada de Apresentação (.NET/WinForms):** `Program.cs` e `FormMain.cs`. Responsável exclusivamente por captura de entrada, validação de formato (ex.: código com 9 dígitos) e apresentação de resultado. Não conhece bytes, offsets nem DLLs.

2. **Camada de Serviço/Integração (.NET):** `CobolService.cs`. Único ponto do sistema que conhece o layout binário do contrato, monta e higieniza o buffer, declara as assinaturas `[DllImport]` e traduz o retorno cru em um objeto de domínio (`ClienteModel`).

3. **Camada Legada (COBOL):** `consulta.cob`, `alteracao.cob`, `exclusao.cob`, `inclusao.cob`. Cada operação de negócio é um programa COBOL independente, compilado isoladamente em uma DLL, que recebe e devolve a mesma estrutura de dados (`REGISTRO.cpy`) e opera sobre o arquivo físico `CLIENTES.DAT`.

  

---

  

## Justificativas Técnicas

  

| Decisão | O que foi escolhido | Por que |

|---|---|---|---|

| **Canal de comunicação** | P/Invoke chamando DLLs COBOL compiladas, com `libcob-4` carregado no próprio processo .NET | Menor latência (chamada de função em memória, sem serialização nem I/O de rede) e menor superfície de infraestrutura — não exige processo adicional, porta, nem protocolo |

| **Contrato de dados** | Buffer binário fixo de 96 bytes, campos `PIC X` de tamanho fixo com padding em espaços (ver Documento de Estrutura Compartilhada) | Corresponde de forma direta e sem ambiguidade ao layout de memória que o COBOL já manipula nativamente (`WORKING-STORAGE`/`LINKAGE SECTION`), eliminando a necessidade de um parser em qualquer uma das pontas |

| **Organização do arquivo de dados** | `CLIENTES.DAT` como arquivo `LINE SEQUENTIAL`, com escrita/exclusão via arquivo temporário (`CLIENTES.TMP`) e substituição física (`CALL "SYSTEM"`) | Simplicidade de manutenção e depuração (arquivo texto legível), suficiente para o volume de dados do projeto acadêmico |

| **Sinalização de erro entre as camadas** | Código de status de 2 caracteres (`REG-STATUS`: `"00"` sucesso, `"01"` não encontrado, `"02"` duplicidade, `"03"` nome obrigatório vazio) embutido no próprio contrato de dados | Mantém o tratamento de erro dentro do mesmo canal síncrono já existente, sem exigir um segundo mecanismo de transporte de exceção entre um runtime gerenciado (.NET) e um runtime nativo (COBOL) |

| **Interface do usuário final** | Windows Forms, com `TabControl` de duas abas ("Consulta & Exclusão" e "Cadastro & Alteração") | Simplicidade de interop com chamadas nativas bloqueantes (modelo de thread único de UI mais simples de raciocinar que o modelo de bindings do WPF) e curva de implementação mais rápida para o escopo do projeto |

| **Separação de responsabilidades no .NET** | Extração da lógica de P/Invoke e manipulação de bytes para uma classe de serviço estática (`CobolService`, namespace `ProjetoFinal.Core`), independente do formulário | Evita que o code-behind da tela (`FormMain.cs`) precise conhecer detalhes de baixo nível (offsets, encoding, DLLs), tornando a UI substituível sem tocar na integração |

| **Estratégia de automação de testes** | Suíte de testes automatizados escrita em **COBOL** (`test_consulta.cob`, `test_escrita.cob`, `test_exclusao.cob`, `test_inclusao.cob`) mais uma suíte de regressão E2E (`regressao.cob`) que invoca os quatro programas via `CALL` | Testa o comportamento real do legado no mesmo ambiente de execução em que ele roda em produção, incluindo efeitos colaterais físicos no arquivo |

  

---

  

## Fluxo de Execução da Solução

  

O fluxo abaixo descreve o caminho de uma operação de negócio típica, do clique do usuário até a persistência física no legado.

  

### 1 Inicialização da Aplicação

1. `Program.cs` define o modo de thread único (`[STAThread]`), habilita estilos visuais modernos e suporte a alta densidade de pixels, e dispara `Application.Run(new FormMain())`.

2. Na primeira operação de negócio disparada pelo usuário, `CobolService.InicializarRuntime()` é chamado internamente: ele ajusta a variável de ambiente `PATH` (necessária para localizar as DLLs do MSYS2/GnuCOBOL no Windows) e executa `CobInit(0, IntPtr.Zero)`, inicializando a engine `libcob-4` uma única vez por execução do programa (controlado pela flag estática `_isInitialized`).

  

### 2. Fluxo de Consulta (operação de leitura, base para as demais)

1. O usuário digita um código de 9 dígitos no campo `txtCodigo`; o evento `TextChanged` valida o formato em tempo real e só habilita o botão `btnConsultar` quando o código é válido.

2. Ao clicar em "Buscar Cliente", `FormMain.BtnConsultar_Click` chama `CobolService.Consultar(codigo)`.

3. `CobolService` monta um buffer de 96 bytes preenchido com espaços (`CriarBufferBase`), copia o código nos 9 primeiros bytes, e chama `ConsultarClienteNative(buffer)` — o P/Invoke para `consulta.dll`.

4. Dentro do COBOL, `consulta.cob` abre `CLIENTES.DAT`, percorre sequencialmente todos os registros (`PERFORM UNTIL FIM-DO-ARQUIVO = "S"`) comparando `ARQ-CODIGO` com `REG-CODIGO`; ao encontrar, copia nome/telefone/e-mail para a estrutura de retorno e marca `REG-STATUS = "00"`. Caso o laço termine sem encontrar (`AT END`), o status permanece `"01"` (valor default definido no início do programa).

5. O buffer retorna para o .NET; `CobolService` lê os bytes das posições 94-95 (status) e, se `"00"`, monta um `ClienteModel` com os campos decodificados via `Encoding.ASCII.GetString`.

6. `FormMain` exibe os dados no `groupResultados` (sucesso) ou exibe um `MessageBox` de aviso (status `"01"`).

  

### 3. Fluxo de Alteração (reaproveita a consulta como pré-carregamento)

1. O usuário informa o código na aba "Cadastro & Alteração" e clica em "Carregar Dados" (`BtnAltCarregar_Click`), que **reutiliza `CobolService.Consultar`** para pré-popular os campos de nome (bloqueado), telefone e e-mail, habilitando a edição apenas se o cliente for encontrado.

2. Ao clicar em "Salvar Alterações de Contato", `CobolService.Alterar(codigo, telefone, email)` monta novamente o buffer, higieniza posicionalmente as fatias de telefone/e-mail (bytes 39-93) e chama `alteracao.dll`.

3. Em `alteracao.cob`, o programa lê `CLIENTES.DAT` registro a registro, copiando cada um para uma estrutura auxiliar (`WS-REGISTRO-AUX`); ao encontrar o código correspondente, sobrescreve telefone e e-mail na estrutura auxiliar antes de gravá-la em `CLIENTES.TMP`. Ao final, se algum registro foi alterado (`REG-STATUS = "00"`), o arquivo temporário substitui fisicamente o original via `CALL "SYSTEM"`; caso contrário, o temporário é apenas descartado.

  

### 4. Fluxo de Exclusão

1. A partir da tela de Consulta, o usuário aciona "Excluir Cliente Definitivamente" (`BtnExcluir_Click`), que primeiro exige confirmação explícita via `MessageBox` (mitigação de ação irreversível).

2. Confirmado, `CobolService.Excluir(codigo)` chama `exclusao.dll`. O programa `exclusao.cob` segue a mesma técnica de arquivo temporário do fluxo de alteração, mas com uma diferença de propósito: em vez de sobrescrever o registro correspondente, ele simplesmente **não o copia** para `CLIENTES.TMP`, efetivando a remoção física por omissão.

  

### 5. Fluxo de Cadastro (Inclusão)

1. Na aba "Cadastro & Alteração", o usuário preenche código, nome, telefone e e-mail e aciona "Persistir Novo Cadastro" (`BtnCadastrar_Click`), que valida localmente o formato do código e a obrigatoriedade do nome antes de sequer chamar o legado.

2. `CobolService.Incluir(...)` monta o buffer completo e chama `inclusao.dll`. Diferente das demais operações de escrita, `inclusao.cob` **não precisa reescrever o arquivo inteiro**: ele primeiro varre `CLIENTES.DAT` apenas para checar duplicidade de código (`REG-STATUS = "02"` se encontrado) e validar que o nome não é vazio (`REG-STATUS = "03"`); se ambas as checagens passarem, reabre o arquivo em modo `OPEN EXTEND` e grava o novo registro ao final, sem tocar nos registros existentes.

3. O uso de `SELECT OPTIONAL` em `inclusao.cob` garante que o programa funcione mesmo que `CLIENTES.DAT` ainda não exista fisicamente (primeira execução do sistema).

  

### 6. Fluxo de Validação Automatizada (Regressão)

Fora do fluxo interativo do usuário, `regressao.cob` demonstra a integridade do ciclo completo ao encadear, em um único programa de teste, as quatro operações sobre um mesmo código de cliente fictício (cadastrar → consultar → alterar → consultar → excluir → consultar), incluindo dois cenários adversos (tentar alterar um cliente já excluído; recadastrar um código recém-liberado), reaproveitando os próprios programas de produção via `CALL`.

  

---

  

## Componentes Utilizados

  

### 1.Componentes da Aplicação (.NET)

| Componente | Papel |

|---|---|

| `Program.cs` | Ponto de entrada da aplicação WinForms; inicializa o runtime gráfico e dispara a janela principal |

| `FormMain.cs` | Camada de apresentação; duas abas (Consulta/Exclusão e Cadastro/Alteração), captura de entrada e exibição de resultado/erro |

| `CobolService.cs` | Camada de serviço/integração; encapsula P/Invoke, montagem/higienização de buffer e tradução de status em `ClienteModel` |

| `PROJETO-FINAL.csproj` | Projeto WinForms (`net10.0-windows`, `UseWindowsForms=true`, `ImplicitUsings` e `Nullable` desabilitados) |

  

### 2 Componentes do Legado (COBOL)

| Componente | Papel |

|---|---|

| `consulta.cob` | Busca sequencial de cliente por código (operação de leitura, base para pré-carregamento em outras telas) |

| `alteracao.cob` | Atualização de telefone/e-mail via padrão arquivo-temporário |

| `exclusao.cob` | Remoção física de registro via padrão arquivo-temporário (omissão seletiva) |

| `inclusao.cob` | Cadastro de novo cliente via `OPEN EXTEND`, com validação de duplicidade e de nome obrigatório |

| `regressao.cob` | Suíte de regressão E2E que encadeia as quatro operações e dois cenários adversos |

| `test_consulta.cob`, `test_escrita.cob`, `test_exclusao.cob`, `test_inclusao.cob` | Testes automatizados unitários/funcionais por operação |

| `REGISTRO.cpy` | Copybook do contrato de dados compartilhado (ver Documento de Estrutura Compartilhada) |

| `CLIENTES.DAT` | Arquivo de dados físico, organização `LINE SEQUENTIAL` |

| `libcob-4` | Runtime do GnuCOBOL, carregado em processo via `cob_init` |

  

### Ferramentas de Ambiente

- **GnuCOBOL/`cobc`** para compilação dos programas `.cob` em bibliotecas dinâmicas (`.dll`).

- **MSYS2/mingw64** como origem das bibliotecas nativas do GnuCOBOL no Windows, referenciado explicitamente em `CobolService.InicializarRuntime()` via ajuste do `PATH`.

- **.NET SDK** (`net10.0-windows`) para compilação e execução da aplicação WinForms.