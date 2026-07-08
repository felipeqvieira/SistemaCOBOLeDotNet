# Plano de Testes
## Sistema de Gestão de Clientes — Cooperativa Financeira Alfa
---

## 1. Estratégia de Testes

| Camada | Instrumento | Cobre |
|---|---|---|
| **Testes Automatizados Unitários/Funcionais** | `test_consulta.cob`, `test_escrita.cob`, `test_exclusao.cob`, `test_inclusao.cob` | Cada operação de negócio isolada, incluindo caminho feliz e cenários de erro |
| **Teste Automatizado de Regressão E2E** | `regressao.cob` | O ciclo completo (Cadastrar → Consultar → Alterar → Consultar → Excluir → Consultar) mais 2 cenários adversos cruzados |
| **Testes Funcionais Manuais** | Roteiro manual pela GUI (Seção 5 deste documento) | Usabilidade e integração real do P/Invoke a partir da interface gráfica (não coberta pelos testes COBOL, que chamam os programas diretamente via `CALL`) |

Esta combinação atende aos três requisitos do enunciado: **casos de teste funcionais** (Seções 3, 4 e 5), **resultado esperado de cada cenário** (coluna "Comportamento Esperado" em cada tabela) e **evidências de execução** (campos `📎 Evidência` a preencher). A automação em COBOL (`test_*.cob` + `regressao.cob`) é o mecanismo que garante que **alterações futuras não comprometam funcionalidades já implementadas** — basta recompilar e reexecutar toda a suíte após qualquer mudança no legado.

---

## 2. Pré-requisitos e Ordem de Execução

### 2.1 Compilação
Cada programa de negócio precisa ser compilado como **módulo chamável** (não como executável principal), pois os testes o invocam via `CALL "nome_do_programa"`. Os arquivos de teste, por sua vez, são compilados como executáveis principais:

```bash
# Compilar os módulos de negócio (uma vez só)
cobc -m -free consulta.cob
cobc -m -free alteracao.cob
cobc -m -free exclusao.cob
cobc -m -free inclusao.cob

# Compilar cada suíte de teste como executável
cobc -x -free test_consulta.cob  -o test_consulta.exe
cobc -x -free test_escrita.cob   -o test_escrita.exe
cobc -x -free test_exclusao.cob  -o test_exclusao.exe
cobc -x -free test_inclusao.cob  -o test_inclusao.exe
cobc -x -free regressao.cob      -o regressao.exe
```

> Os arquivos gerados (`.dll`/`.so` dos módulos) precisam estar na mesma pasta dos executáveis de teste no momento da execução, junto com `CLIENTES.DAT`.

### 2.2 Backup Obrigatório de `CLIENTES.DAT` Antes de Começar
Os testes **gravam de verdade** no arquivo físico — não são simulações. O arquivo original enviado contém apenas um registro-base:

```
000000001JOAO DA SILVA ALFA            11999998888    joao.silva@alfa.coop
```

Faça uma cópia de segurança antes de rodar qualquer teste, para poder restaurar o estado inicial depois (por exemplo, para repetir a suíte completa ou para os testes manuais da GUI):

```bash
copy CLIENTES.DAT CLIENTES.DAT.backup
```

### 2.3 Ordem de Execução Recomendada
Como os testes compartilham o mesmo arquivo físico e alguns são **destrutivos**, a ordem abaixo evita que um teste invalide o pré-requisito de dados de outro:

| Ordem | Suíte | Motivo da posição |
|---|---|---|
| 1º | `test_consulta.exe` | Apenas leitura; depende do registro-base `000000001` existir intacto |
| 2º | `test_escrita.exe` | Modifica telefone/e-mail de `000000001`, mas **não remove** o registro — não quebra os testes seguintes |
| 3º | `test_inclusao.exe` | Opera sobre um código próprio (`000000002`), independente dos demais |
| 4º | `regressao.exe` | Opera sobre um código próprio (`000000003`) e é autocontido (inclui e reinclui o mesmo código ao final) |
| 5º | `test_exclusao.exe` | **Deve ser o último**, pois remove definitivamente o registro `000000001` do arquivo, do qual `test_consulta.exe` e `test_escrita.exe` dependem |

Após rodar a suíte completa, restaure o backup (`copy CLIENTES.DAT.backup CLIENTES.DAT`) antes de iniciar os testes manuais da GUI (Seção 5), para que a demonstração comece a partir de um estado limpo e conhecido.

---

## 3. Casos de Teste Automatizados — Por Operação

### 3.1 Consulta (`test_consulta.cob`)

| ID | Cenário | Entrada | Comportamento Esperado | Status Esperado |
|---|---|---|---|---|
| TC-01 | Busca de cliente cadastrado | `REG-CODIGO = "000000001"` | Localiza o registro e retorna nome iniciando em `"JOAO DA SILVA"` | `"00"` | 
| TC-02 | Busca de cliente inexistente | `REG-CODIGO = "999999999"` | Varre o arquivo inteiro sem localizar e sinaliza ausência | `"01"` |

Evidência: 
![Testes - Consulta](docs/evidencias/test_consulta.png)

### 3.2 Alteração / Escrita (`test_escrita.cob`)

| ID | Cenário | Entrada | Comportamento Esperado | Status Esperado |
|---|---|---|---|---|
| TE-01 | Alteração de registro existente | `REG-CODIGO = "000000001"`, telefone `"11988887777"`, e-mail `"novo@alfa.coop"` | Localiza a linha, sobrescreve os campos via padrão arquivo-temporário e substitui `CLIENTES.DAT` | `"00"` |
| TE-02 | Confirmação de persistência física | `REG-CODIGO = "000000001"` (nova chamada a `consulta`) | Releitura confirma que o telefone gravado é exatamente `"11988887777"`, provando que a escrita foi física e não apenas em memória | `"00"` |

Evidência: 
![Testes - Escrita](docs/evidencias/test_escrita.png)

### 3.3 Exclusão (`test_exclusao.cob`)

| ID | Cenário | Entrada | Comportamento Esperado | Status Esperado |
|---|---|---|---|---|
| TX-01 | Remoção de código inexistente | `REG-CODIGO = "999999999"` | Varre o arquivo, não encontra e descarta o `CLIENTES.TMP` sem alterar a base | `"01"` |
| TX-02 | Remoção de cliente cadastrado | `REG-CODIGO = "000000001"` | Omite o registro no arquivo temporário e substitui fisicamente `CLIENTES.DAT` | `"00"` |
| TX-03 | Confirmação de ausência física pós-exclusão | `REG-CODIGO = "000000001"` (nova chamada a `consulta`) | Releitura confirma que o registro não existe mais fisicamente no arquivo | `"01"` |

Evidência: 
![Testes - Escrita](docs/evidencias/test_escrita.png)

### 3.4 Inclusão (`test_inclusao.cob`)

| ID | Cenário | Entrada | Comportamento Esperado | Status Esperado | 📎 Evidência |
|---|---|---|---|---|---|
| TI-01 | Rejeição de nome vazio | `REG-CODIGO = "000000002"`, `REG-NOME = SPACES` | Interrompe o processamento antes de gravar qualquer linha | `"03"` | *(colar print do console com `[PASS] Cenario 1`)* |
| TI-02 | Inclusão de cliente inédito | `REG-CODIGO = "000000002"`, `REG-NOME = "MARIA SOUZA ALFA"`, telefone e e-mail preenchidos | Grava novo registro de 94 bytes ao final do arquivo via `OPEN EXTEND` | `"00"` | *(colar print do console com `[PASS] Cenario 2`)* |
| TI-03 | Bloqueio de código duplicado | `REG-CODIGO = "000000002"` (já cadastrado no cenário anterior) | Identifica a chave já existente e rejeita a gravação | `"02"` | *(colar print do console com `[PASS] Cenario 3`)* |

Evidência: 
![Testes - Inclusão](docs/evidencias/test_inclusao.png)

---

## 4. Teste de Regressão Consolidado (`regressao.cob`)

Este teste encadeia as quatro operações sobre um único cliente fictício (`REG-CODIGO = "000000003"`, `"CARLOS GOMES ALFA"`), garantindo que uma operação não corrompe o estado usado pela seguinte, e fecha com dois cenários adversos.

| Passo | Cenário | Comportamento Esperado | Status Esperado |
|---|---|---|---|
| 1 | Cadastro inicial do cliente inédito | `CALL "inclusao"` grava o novo cliente | `"00"` |
| 2 | Consulta de confirmação de escrita | `CALL "consulta"` confirma nome `"CARLOS GOMES..."` | `"00"` |
| 3 | Alteração cadastral em lote | `CALL "alteracao"` atualiza telefone/e-mail | `"00"` |
| 4 | Verificação pós-alteração | `CALL "consulta"` confirma telefone `"11955554444"` | `"00"` |
| 5 | Exclusão do cliente de teste | `CALL "exclusao"` remove o registro | `"00"` |
| 6 | Confirmação de ausência pós-exclusão | `CALL "consulta"` não localiza mais o cliente | `"01"` |
| 7 (Adverso 1) | Tentativa de alterar cliente já excluído | `CALL "alteracao"` deve rejeitar por ausência de chave | `"01"` |
| 8 (Adverso 2) | Recadastro do código recém-liberado | `CALL "inclusao"` deve aceitar normalmente, pois a chave está livre | `"00"` |

Evidência: 
![Testes - Regressão](docs/evidencias/regressao.png)

## 5. Testes Funcionais Manuais — Interface Gráfica (WinForms)

Estes cenários não são cobertos pelos testes COBOL acima (que chamam os programas diretamente via `CALL`, sem passar pela interface). Execute-os manualmente após restaurar o backup de `CLIENTES.DAT` no estado original.

| ID | Cenário | Passos na GUI | Comportamento Esperado | 📎 Evidência |
|---|---|---|---|---|
| TG-01 | Inicialização do runtime fora do console | Abrir a aplicação (`PROJETO-FINAL.exe`) | `CobInit` é executado com sucesso na primeira operação, sem travar a interface gráfica | ![Teste - Execução](docs/evidencias/tela_inicial.png) |
| TG-02 | Consulta de cliente existente | Aba "Consulta & Exclusão" → digitar `000000001` → "Buscar Cliente" | Exibe Nome, Telefone e E-mail no `groupResultados` | ![Teste - Consulta](docs/evidencias/consulta_GUI.png) |
| TG-03 | Consulta de cliente inexistente | Digitar `999999999` → "Buscar Cliente" | Exibe `MessageBox` de aviso "Cooperado não localizado..." | ![Teste - Código não encontrado](docs/evidencias/busca_nao_encontrada.png) |
| TG-04 | Validação de formato do código em tempo real | Digitar um código com menos de 9 dígitos | Botão "Buscar Cliente" permanece desabilitado | ![Teste - Execução](docs/evidencias/codigo_menor_9.png) |
| TG-05 | Exclusão com confirmação | A partir de uma consulta bem-sucedida, clicar "Excluir Cliente Definitivamente" | Exibe diálogo de confirmação; ao confirmar, exibe `MessageBox` de sucesso e limpa a tela | ![Teste - Exclusão](docs/evidencias/cliente_removido.png) |
| TG-06 | Cadastro de cliente inédito | Aba "Cadastro & Alteração" → preencher Código, Nome, Telefone, E-mail → "Persistir Novo Cadastro" | Exibe `MessageBox` de sucesso e limpa os campos | *![Teste - Cadastro](docs/evidencias/novo_cliente.png)* |
| TG-07 | Cadastro com código duplicado | Repetir o cadastro do TG-06 com o mesmo código | Exibe `MessageBox` de aviso "...já se encontra atribuído a outro cliente" | ![Teste - Código Duplicado](docs/evidencias/codigo_repetido.png) |
| TG-08 | Cadastro com nome vazio | Preencher apenas o código, deixar Nome em branco → "Persistir Novo Cadastro" | Validação local barra o envio antes de chamar o legado, com `MessageBox` de erro | ![Teste - Cadastro sem nome](docs/evidencias/cadastro_sem_nome.png) |
| TG-09 | Alteração de contato | Aba "Cadastro & Alteração" → "Carregar Dados" com um código existente → editar Telefone/E-mail → "Salvar Alterações de Contato" | Campos de nome vêm bloqueados (`ReadOnly`); alteração é persistida com `MessageBox` de sucesso | ![Teste - Alterar dados](docs/evidencias/contato_atualizado.png) |
| TG-10 | Carregamento de código inexistente para alteração | "Carregar Dados" com um código que não existe | Exibe `MessageBox` de aviso "Cliente não localizado para alteração" e mantém os campos de edição desabilitados | ![Teste - Carregar dados inexistentes](docs/evidencias/carregar_dados_inexistentes.png) |