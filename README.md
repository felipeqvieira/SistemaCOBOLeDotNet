# Cooperativa Financeira Alfa — Sistema de Gestão de Clientes
### Modernização de Sistema Legado (Integração .NET ↔ COBOL)

Sistema híbrido de gestão de clientes desenvolvido como projeto de modernização de um sistema legado. A interface gráfica em **C# / Windows Forms** consome, via **P/Invoke**, um conjunto de programas de negócio escritos em **COBOL (GnuCOBOL)**, que persistem os dados em um arquivo físico sequencial. O projeto evoluiu de um spike de conectividade em console até uma aplicação gráfica completa com CRUD total sobre a base de clientes.

---

## 🧭 Funcionalidades

| Operação | Descrição |
|---|---|
| **Consultar** | Busca de cliente por código, exibindo Nome, Telefone e E-mail |
| **Cadastrar** | Inclusão de novo cliente, com validação de nome obrigatório e bloqueio de código duplicado |
| **Alterar** | Atualização de Telefone e E-mail de um cliente existente |
| **Excluir** | Remoção definitiva de um cliente, com confirmação explícita na interface |

---

## 🏗️ Arquitetura (Resumo)

```
src/Frontend (FormMain.cs, UI)  ──▶  src/Frontend (CobolService.cs, P/Invoke)  ──▶  libcob-4 + *.dll  ──▶  data/CLIENTES.DAT
                                                                                          ▲
                                                                          src/Backend (consulta/alteracao/exclusao/inclusao.cob)
```

- **Camada de Apresentação:** `Program.cs` (raiz) e `src/Frontend/FormMain.cs` — captura de entrada, validação de formato e exibição de resultado, organizados em duas abas ("Consulta & Exclusão" e "Cadastro & Alteração").
- **Camada de Serviço/Integração:** `src/Frontend/CobolService.cs` — único ponto que conhece o layout binário do contrato, monta/higieniza o buffer de 96 bytes e traduz o código de status em um `ClienteModel`.
- **Camada Legada:** `src/Backend/consulta.cob`, `alteracao.cob`, `exclusao.cob`, `inclusao.cob` — cada operação de negócio é um programa COBOL independente, compilado como DLL, operando sobre `data/CLIENTES.DAT` (organização `LINE SEQUENTIAL`).

📄 Detalhamento completo, justificativas técnicas e fluxo de execução passo a passo: **[`docs/Documento de Arquitetura.md`](./docs/Documento%20de%20Arquitetura.md)**

---

## 🔗 Estrutura Compartilhada (Contrato de Dados)

A comunicação entre .NET e COBOL usa um buffer fixo de 96 bytes, espelhando o copybook `src/Backend/REGISTRO.cpy`:

| Campo | Offset | Tamanho | Tipo |
|---|---|---|---|
| `REG-CODIGO` | 0 | 9 | `PIC X(09)` |
| `REG-NOME` | 9 | 30 | `PIC X(30)` |
| `REG-TELEFONE` | 39 | 15 | `PIC X(15)` |
| `REG-EMAIL` | 54 | 40 | `PIC X(40)` |
| `REG-STATUS` | 94 | 2 | `PIC X(02)` (`00` sucesso · `01` não encontrado · `02` duplicado · `03` nome vazio) |

📄 Especificação completa, regras de padding e diferença entre o layout de integração e o layout físico persistido: **[`docs/Estrutura Compartilhada.md`](./docs/Estrutura%20Compartilhada.md)**

---

## 📁 Estrutura do Repositório

```
├── data/
│   └── CLIENTES.DAT                    # Arquivo de dados (LINE SEQUENTIAL)
│
├── docs/
│   ├── evidencias/                     # Prints/logs de execução dos testes
│   ├── Documento de Arquitetura.md     # Arquitetura, justificativas técnicas e fluxo de execução
│   ├── Estrutura Compartilhada.md      # Especificação do contrato de dados
│   ├── Plano de Testes.md              # Casos de teste, resultados esperados e evidências
│   └── Relatorio de IA.md              # Registro dos prompts utilizados ao longo do projeto
│
├── src/
│   ├── Backend/
│   │   ├── consulta.cob                # Busca de cliente por código
│   │   ├── alteracao.cob               # Atualização de telefone/e-mail
│   │   ├── exclusao.cob                # Remoção física de cliente
│   │   ├── inclusao.cob                # Cadastro de novo cliente
│   │   └── REGISTRO.cpy                # Copybook do contrato de dados compartilhado
│   │
│   └── Frontend/
│       ├── bin/, obj/                  # Artefatos de build (gerados, não versionar)
│       ├── CobolService.cs             # Camada de serviço/integração (P/Invoke)
│       ├── FormMain.cs                 # Interface gráfica (WinForms)
│       └── PROJETO-FINAL.csproj        # Projeto .NET (net10.0-windows, WinForms)
│
├── tests/
│   ├── test_consulta.cob               # Teste automatizado: consulta
│   ├── test_escrita.cob                # Teste automatizado: alteração
│   ├── test_exclusao.cob               # Teste automatizado: exclusão
│   ├── test_inclusao.cob               # Teste automatizado: inclusão
│   └── regressao.cob                   # Teste automatizado: regressão E2E do CRUD completo
│
├── Program.cs                          # Ponto de entrada da aplicação WinForms
├── COOPERATIVA-ALFA.slnx               # Solução .NET
└── README.md                           # Este arquivo
```

---

## ⚙️ Pré-requisitos

- **.NET SDK** compatível com `net10.0-windows` (`dotnet --version`)
- **GnuCOBOL** (`cobc --version`) — instalado via MSYS2/mingw64 no Windows
- Sistema operacional **Windows** (a interface é Windows Forms e o projeto referencia caminhos MSYS2 nativos)

---

## ▶️ Como Compilar e Executar

> ⚠️ **Atenção — caminho do arquivo de dados:** os programas COBOL referenciam o arquivo pelo literal `SELECT ARQ-CLIENTES ASSIGN TO "CLIENTES.DAT"`, ou seja, procuram `CLIENTES.DAT` no **diretório de trabalho do processo em execução**, não na pasta onde o `.cob`/`.dll` está fisicamente. Como o arquivo agora vive em `data/`, é necessário **copiar `data/CLIENTES.DAT` para a pasta de onde o executável final é rodado** (ver Passo 3).

### 1. Compilar os programas COBOL como bibliotecas dinâmicas
A partir de `src/Backend/`:

```bash
cd src/Backend
cobc -m -free consulta.cob
cobc -m -free alteracao.cob
cobc -m -free exclusao.cob
cobc -m -free inclusao.cob
```

Isso gera `consulta.dll`, `alteracao.dll`, `exclusao.dll` e `inclusao.dll` dentro de `src/Backend/`.

### 2. Compilar a aplicação .NET
A partir da raiz do repositório:

```bash
dotnet build COOPERATIVA-ALFA.slnx
```

### 3. Disponibilizar DLLs e dados para a aplicação
Copie as 4 DLLs geradas em `src/Backend/` e o arquivo `data/CLIENTES.DAT` para a pasta de saída do build (ex.: `src/Frontend/bin/Debug/net10.0-windows/`), garantindo também que o runtime `libcob-4` esteja acessível (o próprio `CobolService.cs` já ajusta o `PATH` para `C:\msys64\mingw64\bin` automaticamente, caso a instalação padrão do MSYS2 seja usada).

### 4. Executar
```bash
dotnet run --project src/Frontend/PROJETO-FINAL.csproj
```

---

## ✅ Testes

O projeto possui suíte de testes automatizados em COBOL (`tests/`, unitários por operação + regressão E2E) e um roteiro de testes funcionais manuais para a interface gráfica. Casos de teste, entradas, resultados esperados, ordem correta de execução (alguns testes são destrutivos) e onde salvar as evidências (`docs/evidencias/`) estão documentados em:

📄 **[`docs/Plano de Testes.md`](./docs/Plano%20de%20Testes.md)**

Resumo rápido de execução (a partir de `tests/`, com o copybook incluído via `-I`):
```bash
cobc -x - free test_consulta.cob -o test_consulta.exe -I ../src/Backend && test_consulta.exe
cobc -x -free test_escrita.cob  -o test_escrita.exe  -I ../src/Backend && test_escrita.exe
cobc -x -free test_inclusao.cob -o test_inclusao.exe -I ../src/Backend && test_inclusao.exe
cobc -x -free regressao.cob     -o regressao.exe     -I ../src/Backend && regressao.exe
cobc -x -free test_exclusao.cob -o test_exclusao.exe -I ../src/Backend && test_exclusao.exe   # executar por último (destrutivo)
```

> ⚠️ Copie `data/CLIENTES.DAT` para dentro de `tests/` antes de rodar (mesmo motivo do aviso da seção anterior) e faça backup do arquivo antes — os testes gravam de fato no arquivo físico.

---

## 📚 Documentação do Projeto

| Documento | Local | Conteúdo |
|---|---|---|
| Documento de Arquitetura | `docs/Documento de Arquitetura.md` | Arquitetura escolhida, justificativas técnicas, fluxo de execução e componentes |
| Estrutura Compartilhada | `docs/Estrutura Compartilhada.md` | Especificação do contrato de dados de integração |
| Plano de Testes | `docs/Plano de Testes.md` | Casos de teste, resultados esperados e evidências de execução |
| Relatório de Utilização de IA | `docs/Relatorio de IA.md` | Registro dos prompts utilizados ao longo do projeto |
| Evidências de Teste | `docs/evidencias/` | Prints e logs reais coletados na execução do Plano de Testes |

---

## 🧩 Status do Projeto

Projeto finalizado, cobrindo o CRUD completo (Consultar, Cadastrar, Alterar, Excluir) sobre a base de clientes, com interface gráfica em Windows Forms e testes automatizados de regressão sobre o canal legado.