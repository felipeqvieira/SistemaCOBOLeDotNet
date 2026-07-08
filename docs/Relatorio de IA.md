# 1. Prompt: Cronograma do Projeto

Atue como um Gerente de Projetos de Software especialista em arquiteturas híbridas envolvendo sistemas legados. Seu objetivo é estruturar um cronograma detalhado de exatamente 5 dias para a execução do projeto de "Modernização de um Sistema Legado" da Cooperativa Financeira Alfa[cite: 1].


### 🚨 REGRA CRÍTICA DE EXECUÇÃO (MODO SEQUENCIAL)

* Você deve gerar APENAS o planejamento detalhado do **Dia 1** nesta primeira resposta.

* Pare completamente a execução após o Dia 1 e aguarde a minha confirmação expressa ("Aprovado" ou com feedbacks) para prosseguir.

* Você somente avançará para os dias seguintes (Dia 2, Dia 3, Dia 4 e Dia 5) um por um, mantendo estritamente essa dinâmica de pausa e validação sequencial.


### 📋 DIRETRIZES DE DETALHAMENTO DIÁRIO

Para o dia apresentado, você deve detalhar:

1. **Objetivo Central do Dia**: O foco principal do progresso técnico ou analítico.

2. **Atividades e Tarefas**: Divisão clara dos passos operacionais a serem executados pela equipe.

3. **Documentos e Entregáveis Vinculados**: Indicação exata de quais artefatos oficiais serão criados ou atualizados no dia, mapeados a partir dos seguintes itens: Código-fonte, Documento de Arquitetura, Estrutura Compartilhada, Plano de Testes, Testes Automatizados e Relatório de Utilização de IA.

4. **Objetivo de Cada Documento**: A finalidade técnica e o valor prático de cada documento gerado.

5. **Decisões Críticas do Período**: Itens de tomada de decisão sobre comunicação entre componentes, armazenamento de dados, organização estrutural do projeto e integração tecnológica (.NET e COBOL).


### 🎯 ESCOPO E REQUISITOS DO PROJETO A SEGUIR

* **Tecnologias**: Uso obrigatório de ambiente .NET e COBOL.

* **Funcionalidades**: Consulta de cliente por código, exibição de dados cadastrais (Código, Nome, Telefone, E-mail), alteração de telefone e e-mail, tratamento para cliente não existente e persistência das modificações.

* **Testes**: Planejamento de testes funcionais (cenários, resultados esperados e evidências) combinado com o desenvolvimento de testes automatizados para as rotinas principais.

* **Uso de IA**: Previsão de espaço para documentar as interações com ferramentas de IA, detalhando o prompt, objetivo, resposta, análise crítica e o impacto real gerado no projeto.


Por favor, inicie apresentando exclusivamente o planejamento para o **Dia 1** e aguarde meu comando para prosseguir. 

## 1.1: Resposta da I.A.

### DIA 1: Fundações de Engenharia e Mitigação de Riscos do Canal
* **Objetivo:** Estabelecer a conectividade básica entre .NET e COBOL através de um "Spike Técnico" (Prova de Conceito "Hello World") antes de fechar o contrato final[cite: 2, 3].
* **Critério Go/No-Go:** O spike deve completar um round-trip de envio e retorno de strings sem truncamento de dados[cite: 3]. Se falhar em até N tentativas lógicas, ativar o Plano B (comunicação alternativa via arquivos físicos de tamanho fixo)[cite: 3].
* **Tarefas:** Criar o esqueleto do Spike; Definir o layout e a documentação formal da Estrutura Compartilhada com padding[cite: 1, 3]; Iniciar a matriz do Plano de Testes[cite: 1]; Registrar o Prompt 1 no Relatório de IA[cite: 1].
* **Entregáveis do Dia:** Protótipo do Spike (Código), Documento de Arquitetura Inicial (com Plano B), Especificação Textual e Código da Estrutura Compartilhada, Plano de Testes Inicial e Relatório de IA (Prompt 1)[cite: 1, 3].

### DIA 2: Desenvolvimento Híbrido e Integração da Consulta (E2E)
* **Objetivo:** Implementar e acoplar de forma incremental o fluxo completo de consulta de cliente por código, evitando integrações tardias[cite: 2, 3].
* **Tarefas:** Codificar o programa COBOL para receber o código por parâmetro, varrer o arquivo de dados e retornar as informações ou o erro de "cliente não encontrado"[cite: 1]; Construir a interface visual no .NET para busca[cite: 1]; Integrar incrementalmente o .NET chamando a consulta no COBOL pelo canal real[cite: 2]; Desenvolver a suíte de testes automatizados lógicos no COBOL e o primeiro script de smoke test automatizado do canal de leitura[cite: 2, 3]; Registrar o Prompt 2 no Relatório de IA[cite: 1].
* **Entregáveis do Dia:** Código-fonte (Consulta COBOL e Tela .NET), Projeto de Testes Automatizados (Consulta e Smoke do Canal), Atualização do Plano de Testes e Relatório de IA (Prompt 2)[cite: 1, 2].

### DIA 3: Escrita Cadastral, Paridade de Testes e Checkpoint de IA
* **Objetivo:** Desenvolver a funcionalidade de alteração e persistência de dados no legado, estabelecendo a paridade nos testes automatizados do canal de comunicação[cite: 1, 3].
* **Tarefas:** Programar a rotina COBOL de alteração e escrita física (*REWRITE*) de telefone e e-mail[cite: 1]; Atualizar a UI do .NET com campos de edição[cite: 1]; Acoplar incrementalmente o fluxo de escrita ponta a ponta pelo canal real[cite: 2]; Estender o smoke test automatizado no COBOL para cobrir de forma robusta e computadorizada a integridade do canal real no fluxo de escrita (paridade com o Dia 2)[cite: 3]; Realizar um checkpoint de auditoria no Relatório de IA para verificar os blocos de Prompts 1 e 2[cite: 2, 3].
* **Entregáveis do Dia:** Código-fonte (Escrita/Persistência COBOL e UI .NET atualizada), Testes Automatizados expandidos para escrita no canal, Plano de Testes e Relatório de IA auditado[cite: 1, 2, 3].

### DIA 4: Lapidação de Usabilidade, Testes de Regressão E2E e Evidências
* **Objetivo:** Refinar os aspectos de usabilidade da aplicação moderna, executar testes automatizados completos de regressão pelo canal integrado e realizar a coleta antecipada de evidências técnicas[cite: 2, 3].
* **Tarefas:** Lapidar elementos visuais, mensagens de alerta e feedbacks na camada .NET (Atendimento ao requisito de facilitação de uso)[cite: 1, 2]; Rodar a suíte unificada de testes de regressão automatizados no COBOL simulando operações combinadas pelo canal físico[cite: 1, 3]; Capturar logs, retornos e telas reais para compor as evidências finais do Plano de Testes[cite: 1, 2, 3]; Realizar e catalogar a terceira interação mínima com a IA (Prompt 3)[cite: 1].
* **Entregáveis do Dia:** Código-fonte refinado (.NET/COBOL), Testes Automatizados consolidados, Plano de Testes preenchido com as evidências de execução e Relatório de IA completo (3 prompts)[cite: 1, 2, 3].

### DIA 5: Estabilização (Hardening), Revisão Documental e Ensaio Técnico
* **Objetivo:** Dedicar o período exclusivamente à estabilização dos artefatos, auditorias de conformidade regulamentar e preparação para defesa técnica do projeto[cite: 2, 3].
* **Tarefas:** Sanar desvios cosméticos residuais; Auditar o Documento de Arquitetura e a Estrutura Compartilhada contra o código final[cite: 1, 3]; Homologar o fechamento do Relatório de IA[cite: 1]; Conduzir uma simulação de banca para alinhar as justificativas de engenharia e escolhas arquiteturais[cite: 1, 3].
* **Entregáveis do Dia:** Pacote final contendo os 6 entregáveis obrigatórios revisados (.NET/COBOL Código, Documento de Arquitetura, Documento e Código da Estrutura Compartilhada, Plano de Testes com Evidências coletadas, Projeto de Testes Automatizados e Relatório de Utilização de IA)[cite: 1, 3].


## 1.2: Análise e Impacto

O prompt foi utilizado para ter um direcionamento de como o projeto poderia ser dividido e cumpriu bem esse fim. Apesar de não ser ideal, já que não apresenta um espaço para possíveis falhas e apresenta um número significativo de entregáveis por dia. No entanto, escolhi o número de cinco dias de forma estratégica, prevendo possíveis ausências como as citadas.

No fim, a ideia de ter um dia para entender melhor como a integração .NET e COBOL funcionava com um protótipo foi muito boa. Posso dizer que a resposta da I.A. teve um impacto positivo, já que influenciou a divisão de tarefas do projeto.

---

# 2. Prompt: Cronograma para novas funcionalidades

O cronograma em "Projeto Final.md" em anexo, foi utilizado como base para criar os seguintes arquivos em anexo.

Gostaria de adicionar novas funcionalidades:

1. Deletar um cliente existente;
2. Adicionar um cliente novo;
3. Interface front-end para o projeto.

Cada funcionalidade nova (1 e 2) adiciona um novo conjunto de testes. A terceira funcionalidade elimina o uso do terminal como interface do projeto e portanto acredito que é a mais complexa, sendo deixada por último.

Seu objetivo é elaborar um cronograma completo (adicionando novos dias) a fim de ter um documento que consiga completar o desenvolvimento do programa de forma satisfatória com as novas demandas acima.

## 2.1: Resposta da I.A.

# Cronograma Estendido — Extensão de Escopo (Exclusão, Cadastro e Interface Gráfica)

> Este documento é um **complemento** ao `Projeto_Final.md` original. Os Dias 1 a 5 permanecem inalterados e já homologados. A partir daqui, o cronograma é estendido com os Dias 6 a 13 para cobrir as três novas demandas: **(1) Exclusão de cliente**, **(2) Cadastro de novo cliente** e **(3) Interface gráfica (fim do terminal)**.
> 

---

## DIA 6: Exclusão de Cliente (Delete) — Backend Legado e Integração E2E

### 1. Objetivo Central do Dia

Implementar de ponta a ponta a funcionalidade de exclusão de cliente, reaproveitando a técnica de reescrita física via arquivo temporário já validada no Dia 3 para o fluxo de alteração — já que o arquivo `CLIENTES.DAT` é `LINE SEQUENTIAL` e não suporta `DELETE` posicional nativo, exigindo a leitura integral do arquivo, a omissão do registro-alvo e a regravação do restante.

### 2. Atividades e Tarefas

- **Tarefa 1 (Programa COBOL de Exclusão):** Criar `exclusao.cob`, seguindo o padrão de `alteracao.cob` — abrir `ARQ-CLIENTES` para leitura e `CLIENTES.TMP` para escrita, copiar todo registro cujo `ARQ-CODIGO` for diferente do `REG-CODIGO` recebido, e simplesmente **não gravar** o registro que corresponder ao código informado.
- **Tarefa 2 (Sinalização de Status):** Definir `REG-STATUS = "00"` quando o registro-alvo for localizado e removido, e `"01"` quando o código não existir no arquivo (reaproveitando a mesma semântica de status já usada na consulta/alteração).
- **Tarefa 3 (Substituição Física Condicional):** Reaproveitar a lógica de `CALL "SYSTEM"` para promover `CLIENTES.TMP` a `CLIENTES.DAT` **somente** se o status for "00"; caso contrário, descartar o temporário sem tocar no arquivo original.
- **Tarefa 4 (Integração .NET):** Adicionar a opção "4. Excluir Cliente" ao menu do `Program.cs`, com `DllImport` para `exclusao.dll`, reaproveitando o buffer de 96 bytes já definido (apenas os 9 bytes de código são necessários como entrada).
- **Tarefa 5 (Confirmação de Segurança):** Implementar uma confirmação explícita ("Tem certeza? S/N") antes de disparar a chamada COBOL, mitigando exclusões acidentais — requisito de usabilidade específico desta operação irreversível.
- **Tarefa 6 (Testes Automatizados COBOL):** Criar `test_exclusao.cob` cobrindo: exclusão de cliente existente (registro efetivamente ausente do arquivo após a operação), tentativa de exclusão de código inexistente (arquivo permanece intacto) e verificação de que os demais registros não são afetados (contagem de linhas antes/depois).
- **Tarefa 7 (Uso de IA):** Registrar o Prompt 4 no Relatório de IA, documentando a decisão de reaproveitar o padrão arquivo-temporário em vez de migrar para organização indexada.

### 3. Documentos e Entregáveis Vinculados

- **Código-fonte** (`exclusao.cob`, `exclusao.dll` e opção de exclusão no `Program.cs`)
- **Testes Automatizados** (`test_exclusao.cob`)
- **Plano de Testes** (novos cenários de exclusão: sucesso, código inexistente, integridade dos demais registros)
- **Relatório de Utilização de IA** (Prompt 4)

### 4. Objetivo de Cada Documento

- **Código-fonte:** Materializar a remoção física e definitiva de um registro do legado sem comprometer os demais.
- **Testes Automatizados:** Garantir de forma computadorizada que a exclusão é seletiva e que o arquivo permanece íntegro em cenários de erro.
- **Plano de Testes:** Expandir a matriz de critérios de aceitação para cobrir uma operação destrutiva e irreversível.
- **Relatório de Utilização de IA:** Registrar criticamente a decisão de reaproveitamento arquitetural em vez de reescrita.

### 5. Decisões Críticas do Período

- **Armazenamento de Dados:** Confirmação de que o padrão arquivo-temporário se mantém adequado mesmo com uma nova operação destrutiva, evitando dívida técnica de migrar prematuramente para arquivo indexado.
- **Integração Tecnológica:** Validação de que o buffer de 96 bytes já definido no Dia 1 comporta a operação de exclusão sem necessidade de alteração de layout (apenas o código é usado como entrada).
- **Usabilidade:** Necessidade de uma etapa de confirmação explícita no .NET, único ponto do sistema com ação irreversível.

### ✅ Checklist de Validação do Dia 6

- [ ] `exclusao.cob` compila e remove corretamente um registro por código
- [ ] Arquivo `CLIENTES.DAT` permanece inalterado quando o código não existe
- [ ] Opção "Excluir Cliente" funcional no menu do `Program.cs`, com confirmação S/N
- [ ] `test_exclusao.cob` cobre sucesso, falha e integridade dos demais registros
- [ ] Plano de Testes atualizado e Prompt 4 registrado

**Pausar e aguardar validação humana ("Aprovado" ou feedback) antes de prosseguir para o Dia 7.**

---

## DIA 7: Cadastro de Novo Cliente (Create) e Correção Retroativa do Contrato

### 1. Objetivo Central do Dia

Implementar a inclusão de novos clientes, introduzindo a primeira regra de negócio de **validação de duplicidade** do projeto (código já existente), o que exige revisitar formalmente a Estrutura Compartilhada definida no Dia 1.

### 2. Atividades e Tarefas

- **Tarefa 1 (Correção Retroativa do Contrato):** Atualizar a especificação textual da Estrutura Compartilhada (tabela de campos do Dia 1) para documentar o novo valor de negócio do `REG-STATUS`: `"00"` sucesso, `"01"` não encontrado/código inexistente (contexto consulta/alteração/exclusão), `"02"` código já cadastrado (contexto exclusivo de inclusão). O copybook `REGISTRO.cpy` não muda em tamanho, apenas a documentação de domínio de valores.
- **Tarefa 2 (Programa COBOL de Inclusão):** Criar `inclusao.cob`, que varre `ARQ-CLIENTES` verificando se o `REG-CODIGO` recebido já existe; em caso positivo, retorna `"02"` sem gravar; caso negativo, reabre o arquivo em modo `EXTEND` (ou grava via arquivo temporário, mantendo o padrão dos demais programas) e grava o novo registro completo (código, nome, telefone, e-mail), retornando `"00"`.
- **Tarefa 3 (Validação de Campos Obrigatórios):** Definir e codificar a regra de que nome não pode ser vazio na inclusão (diferente da alteração, que só edita telefone/e-mail).
- **Tarefa 4 (Integração .NET):** Adicionar a opção "5. Cadastrar Novo Cliente" ao `Program.cs`, coletando código (9 dígitos), nome, telefone e e-mail, aplicando a mesma técnica de padding/higienização posicional já usada na alteração, e tratando os três status de retorno (`00`, `01` não se aplica aqui, `02`) com mensagens distintas — atenção especial para não reutilizar cegamente a mensagem de "cliente não encontrado" da consulta.
- **Tarefa 5 (Testes Automatizados COBOL):** Criar `test_inclusao.cob` cobrindo: inclusão de código novo (sucesso e presença física do registro), tentativa de inclusão de código duplicado (status "02" e arquivo inalterado), e inclusão com nome vazio (rejeição).
- **Tarefa 6 (Uso de IA):** Registrar o Prompt 5, documentando a extração da regra de duplicidade e a decisão de introduzir um terceiro status de negócio.

### 3. Documentos e Entregáveis Vinculados

- **Código-fonte** (`inclusao.cob`, `inclusao.dll` e opção de cadastro no `Program.cs`)
- **Estrutura Compartilhada** (documento textual **atualizado retroativamente** com o domínio de valores de `REG-STATUS`)
- **Testes Automatizados** (`test_inclusao.cob`)
- **Plano de Testes** (novos cenários: inclusão simples, duplicidade, campo obrigatório vazio)
- **Relatório de Utilização de IA** (Prompt 5)

### 4. Objetivo de Cada Documento

- **Código-fonte:** Habilitar o crescimento da base de clientes de forma controlada e validada.
- **Estrutura Compartilhada:** Manter o contrato como fonte única da verdade, agora contemplando o domínio completo de status de negócio usados pelo sistema.
- **Testes Automatizados:** Assegurar que a regra de duplicidade é a barreira efetiva contra inconsistência de dados.
- **Plano de Testes:** Registrar formalmente que a evolução do contrato foi coberta por evidência de teste antes de seguir adiante.

### 5. Decisões Críticas do Período

- **Comunicação entre Componentes:** Reforço de que o mesmo buffer de 96 bytes definido no Dia 1 é suficiente para a operação de inclusão, sem necessidade de renegociar o layout físico — apenas o significado do campo de status foi estendido.
- **Armazenamento de Dados:** Definição de que a checagem de duplicidade é feita por varredura sequencial completa (mesmo padrão de consulta), aceitável dado o volume de dados do projeto; registrar isso como limitação conhecida (não é um índice único de banco de dados).
- **Organização Estrutural do Projeto:** Padronização de que todo programa COBOL que grava no arquivo segue o mesmo padrão arquivo-temporário + substituição condicional, agora usado por três programas (alteração, exclusão, inclusão).

### ✅ Checklist de Validação do Dia 7

- [ ] Documento da Estrutura Compartilhada atualizado com os três valores de `REG-STATUS`
- [ ] `inclusao.cob` grava clientes novos e rejeita códigos duplicados
- [ ] Opção "Cadastrar Novo Cliente" funcional no `Program.cs`, distinguindo mensagens de sucesso/duplicidade
- [ ] `test_inclusao.cob` cobre sucesso, duplicidade e nome vazio
- [ ] Plano de Testes atualizado e Prompt 5 registrado

**Pausar e aguardar validação humana antes de prosseguir para o Dia 8.**

---

## DIA 8: Consolidação do CRUD Completo — Regressão E2E e Paridade de Testes

### 1. Objetivo Central do Dia

Fechar o ciclo de funcionalidades de dados com uma bateria de regressão fim-a-fim cobrindo as quatro operações (Consultar, Cadastrar, Alterar, Excluir) operando em conjunto pelo canal real, replicando para o CRUD completo o mesmo rigor de paridade de testes já exigido entre os Dias 2 e 3.

### 2. Atividades e Tarefas

- **Tarefa 1 (Suíte Unificada de Regressão):** Estender o smoke test automatizado do canal físico para exercitar uma sequência realista: cadastrar → consultar → alterar → consultar → excluir → consultar (confirmando ausência), validando que uma operação não corrompe o estado usado pela seguinte.
- **Tarefa 2 (Testes de Concorrência de Cenário):** Validar o comportamento do sistema diante de sequências adversas, como excluir um cliente e, em seguida, tentar alterá-lo (deve retornar "01"), ou cadastrar um código recém-excluído (deve ser aceito normalmente).
- **Tarefa 3 (Revisão de Usabilidade do Menu):** Consolidar o menu do .NET agora com 5 opções (Consultar, Alterar, Cadastrar, Excluir, Sair), revisando textos, atalhos e mensagens para manter consistência de tom entre as cinco operações.
- **Tarefa 4 (Coleta de Evidências):** Capturar logs e telas reais da suíte de regressão consolidada para o Plano de Testes.
- **Tarefa 5 (Uso de IA):** Registrar o Prompt 6, referente à elaboração dos cenários de regressão cruzada entre as quatro operações.

### 3. Documentos e Entregáveis Vinculados

- **Testes Automatizados** (suíte de regressão consolidada cobrindo as 4 operações)
- **Código-fonte** (menu do `Program.cs` revisado e consolidado)
- **Plano de Testes** (evidências da regressão cruzada)
- **Relatório de Utilização de IA** (Prompt 6)

### 4. Objetivo de Cada Documento

- **Testes Automatizados:** Certificar que o CRUD completo funciona de forma coesa e não apenas isoladamente por operação.
- **Plano de Testes:** Consolidar formalmente que os requisitos funcionais completos do sistema (consulta, cadastro, alteração, exclusão) foram atendidos com evidência real.

### 5. Decisões Críticas do Período

- **Armazenamento de Dados:** Confirmação de que o padrão de reescrita via arquivo temporário se sustenta sob uso combinado e sequencial das quatro operações sem degradação ou corrupção do arquivo.
- **Organização Estrutural do Projeto:** Code freeze técnico do backend COBOL (`consulta.cob`, `alteracao.cob`, `exclusao.cob`, `inclusao.cob`) — a partir daqui, mudanças no legado só ocorrem se a interface gráfica (Dias 9-12) revelar alguma falha real.

### ✅ Checklist de Validação do Dia 8

- [ ] Suíte de regressão cruzada (cadastrar→consultar→alterar→excluir→consultar) executada com sucesso
- [ ] Cenários adversos (alterar excluído, recadastrar código excluído) validados
- [ ] Menu do `Program.cs` consolidado com as 5 opções
- [ ] Evidências de regressão anexadas ao Plano de Testes
- [ ] Prompt 6 registrado

**Pausar e aguardar validação humana antes de prosseguir para o Dia 9.**

---

## DIA 9: Arquitetura da Interface Gráfica — Spike de Migração do Terminal

### 1. Objetivo Central do Dia

Antes de reescrever qualquer tela, tratar a migração do console para uma interface gráfica como um **novo spike técnico** (mesmo espírito do Dia 1), pois o `P/Invoke` para `libcob-4` e para as DLLs COBOL precisa continuar funcionando fora de uma aplicação de console, o que pode expor problemas de thread, diretório de trabalho e carregamento de DLL nativa.

### 2. Atividades e Tarefas

- **Tarefa 1 (Seleção do Framework):** Avaliar e decidir entre WPF, Windows Forms ou .NET MAUI para a nova interface, considerando maturidade, curva de aprendizado e compatibilidade com P/Invoke — decisão a ser justificada e registrada no Documento de Arquitetura.
- **Tarefa 2 (Spike de Integração Gráfica):** Criar um projeto mínimo na tecnologia escolhida com um único botão que chama `ConsultarCliente` via P/Invoke, validando que a engine COBOL inicializa corretamente fora do contexto de console (reaproveitando o `CobInit`).
- **Tarefa 3 (Critério Go/No-Go da Migração):** Definir o critério de aceite: a chamada P/Invoke a partir da GUI deve retornar os mesmos dados que o console retornava para o mesmo código de cliente, sem travamentos de interface (problema comum quando chamadas nativas bloqueantes rodam na thread de UI).
- **Tarefa 4 (Plano de Migração Incremental):** Desenhar a estratégia de substituição gradual: a lógica de negócio (P/Invoke, tratamento de buffer, higienização) será extraída do `Program.cs` para uma camada de serviço compartilhada, reaproveitada tanto pela GUI quanto pelos testes, evitando duplicação de código de baixo nível de manipulação de bytes.
- **Tarefa 5 (Atualização do Documento de Arquitetura):** Registrar formalmente a decisão de framework, o resultado do spike gráfico e o Plano B (caso a tecnologia escolhida apresente instabilidade com a DLL nativa, pivotar para Windows Forms por sua maior simplicidade de interop).
- **Tarefa 6 (Uso de IA):** Registrar o Prompt 7, documentando a comparação entre frameworks gráficos e riscos de interop nativo.

### 3. Documentos e Entregáveis Vinculados

- **Código-fonte** (protótipo do spike gráfico e camada de serviço extraída)
- **Documento de Arquitetura** (decisão de framework, resultado do spike e Plano B)
- **Plano de Testes** (critério de aceite da migração registrado)
- **Relatório de Utilização de IA** (Prompt 7)

### 4. Objetivo de Cada Documento

- **Código-fonte:** Provar que a integração nativa sobrevive à mudança de paradigma de console para GUI.
- **Documento de Arquitetura:** Justificar tecnicamente a escolha do framework gráfico perante a banca avaliadora.

### 5. Decisões Críticas do Período

- **Integração Tecnológica:** Risco central da migração — chamadas P/Invoke bloqueantes na thread de interface gráfica podem congelar a tela; decisão de usar chamadas assíncronas/`Task.Run` deve ser tomada aqui, não postergada.
- **Organização Estrutural do Projeto:** Separação de responsabilidades entre camada de UI e camada de serviço de integração legada, preparando o terreno para os Dias 10 e 11.

### ✅ Checklist de Validação do Dia 9

- [ ] Framework gráfico escolhido e justificado no Documento de Arquitetura
- [ ] Spike gráfico executa consulta real via P/Invoke sem travar a interface
- [ ] Camada de serviço extraída do `Program.cs` (reutilizável pela GUI)
- [ ] Plano B de framework documentado
- [ ] Prompt 7 registrado

**Pausar e aguardar validação humana antes de prosseguir para o Dia 10.**

---

## DIA 10: Interface Gráfica — Telas de Consulta e Exclusão

### 1. Objetivo Central do Dia

Construir as duas primeiras telas da aplicação gráfica, priorizando as operações de leitura e remoção por serem as de menor complexidade de formulário, consolidando o padrão de tela antes de avançar para os formulários de cadastro/alteração.

### 2. Atividades e Tarefas

- **Tarefa 1 (Tela de Consulta):** Implementar a tela com campo de busca por código, botão de pesquisa e área de exibição dos dados (Código, Nome, Telefone, E-mail), substituindo integralmente a saída de texto do console.
- **Tarefa 2 (Tratamento Visual de Erro):** Traduzir o alerta de "cliente não encontrado" para um componente visual claro (ex: mensagem inline ou modal), mantendo o requisito de usabilidade já validado no Dia 2.
- **Tarefa 3 (Tela/Fluxo de Exclusão):** Implementar a exclusão a partir da própria tela de consulta (reaproveitando o cliente já exibido), com um diálogo de confirmação gráfico substituindo o prompt S/N do console.
- **Tarefa 4 (Validação de Entrada em Tempo Real):** Migrar a validação de "9 dígitos numéricos" para feedback imediato no campo de texto (ex: desabilitar botão de busca enquanto inválido), melhorando a usabilidade em relação ao console.
- **Tarefa 5 (Testes de Interface):** Definir e documentar um roteiro manual de teste de interface (já que testes de UI automatizados estão fora do escopo do projeto, conforme definido no Dia 1) cobrindo consulta e exclusão pela GUI.
- **Tarefa 6 (Uso de IA):** Registrar o Prompt 8, referente à tradução dos fluxos de console para componentes gráficos equivalentes.

### 3. Documentos e Entregáveis Vinculados

- **Código-fonte** (telas de Consulta e Exclusão na GUI)
- **Plano de Testes** (roteiro manual de teste de interface para consulta/exclusão)
- **Relatório de Utilização de IA** (Prompt 8)

### 4. Objetivo de Cada Documento

- **Código-fonte:** Entregar as duas primeiras funcionalidades operáveis sem depender do terminal.
- **Plano de Testes:** Formalizar como a ausência de testes automatizados de UI é compensada por roteiro manual estruturado.

### 5. Decisões Críticas do Período

- **Integração Tecnológica e Usabilidade:** Definição do padrão visual de feedback (cores, ícones, mensagens) que será reaproveitado nas telas de cadastro/alteração do Dia 11, garantindo consistência de interface.

### ✅ Checklist de Validação do Dia 10

- [ ] Tela de Consulta funcional, exibindo os 4 campos do cliente
- [ ] Alerta de "cliente não encontrado" com tratamento visual claro
- [ ] Exclusão funcional a partir da tela de consulta, com confirmação gráfica
- [ ] Roteiro manual de teste de interface documentado
- [ ] Prompt 8 registrado

**Pausar e aguardar validação humana antes de prosseguir para o Dia 11.**

---

## DIA 11: Interface Gráfica — Telas de Cadastro e Alteração

### 1. Objetivo Central do Dia

Concluir a migração para GUI implementando os dois formulários mais complexos do sistema — cadastro (todos os campos) e alteração (telefone/e-mail) — eliminando por completo a dependência do terminal como interface do projeto.

### 2. Atividades e Tarefas

- **Tarefa 1 (Tela de Cadastro):** Implementar formulário com os campos Código, Nome, Telefone e E-mail, aplicando validações de preenchimento obrigatório e tratando visualmente os três status possíveis (sucesso, duplicidade, campo inválido).
- **Tarefa 2 (Tela de Alteração):** Implementar formulário de edição pré-carregado a partir dos dados de uma consulta prévia, permitindo editar apenas Telefone e E-mail (mantendo a regra de negócio original), com o mesmo padrão visual de feedback definido no Dia 10.
- **Tarefa 3 (Navegação Unificada):** Consolidar a navegação entre as quatro telas (Consulta, Cadastro, Alteração, Exclusão) em um menu principal ou barra de navegação, substituindo definitivamente o menu numérico do console.
- **Tarefa 4 (Roteiro Manual de Teste de Interface):** Estender o roteiro do Dia 10 para cobrir cadastro e alteração pela GUI.
- **Tarefa 5 (Uso de IA):** Registrar o Prompt 9, referente ao design dos formulários e ao tratamento unificado de status de retorno na camada visual.

### 3. Documentos e Entregáveis Vinculados

- **Código-fonte** (telas de Cadastro e Alteração, navegação unificada — GUI completa e funcional)
- **Plano de Testes** (roteiro manual de teste de interface completo)
- **Relatório de Utilização de IA** (Prompt 9)

### 4. Objetivo de Cada Documento

- **Código-fonte:** Entregar a aplicação gráfica completa, com o terminal definitivamente eliminado como interface de uso.
- **Plano de Testes:** Registrar cobertura manual completa das quatro operações pela interface final.

### 5. Decisões Críticas do Período

- **Organização Estrutural do Projeto:** Congelamento (code freeze) da camada de UI, isolando o ambiente para a bateria de regressão do Dia 12.
- **Integração Tecnológica:** Confirmação final de que a camada de serviço extraída no Dia 9 é suficiente para todas as quatro telas, sem duplicação de lógica de buffer entre elas.

### ✅ Checklist de Validação do Dia 11

- [ ] Tela de Cadastro funcional com tratamento dos 3 status de retorno
- [ ] Tela de Alteração funcional, pré-carregada a partir da consulta
- [ ] Navegação unificada entre as 4 operações, sem uso de terminal
- [ ] Roteiro manual de teste de interface completo
- [ ] Prompt 9 registrado

**Pausar e aguardar validação humana antes de prosseguir para o Dia 12.**

---

## DIA 12: Regressão E2E Completa na GUI, Usabilidade e Evidências Finais

### 1. Objetivo Central do Dia

Repetir, agora pela interface gráfica definitiva, o mesmo rigor de regressão E2E e coleta de evidências já exercitado nos Dias 4 e 8, garantindo que a migração de interface não introduziu regressões funcionais em nenhuma das quatro operações.

### 2. Atividades e Tarefas

- **Tarefa 1 (Regressão E2E via GUI):** Executar manualmente, pela interface gráfica, a mesma sequência de regressão cruzada definida no Dia 8 (cadastrar → consultar → alterar → consultar → excluir → consultar), confirmando paridade de comportamento com os testes automatizados do canal COBOL.
- **Tarefa 2 (Testes Automatizados COBOL — Confirmação):** Rodar novamente a suíte automatizada consolidada do Dia 8 para confirmar que o backend permanece estável, já que nenhuma alteração de layout ou regra de negócio ocorreu na camada COBOL durante a migração de UI.
- **Tarefa 3 (Lapidação Final de Usabilidade):** Ajustes finos de mensagens, cores, atalhos de teclado e responsividade da interface gráfica.
- **Tarefa 4 (Coleta de Evidências Finais):** Capturar telas reais da GUI para cada uma das quatro operações, compondo a evidência final e definitiva do Plano de Testes.
- **Tarefa 5 (Uso de IA):** Registrar o Prompt 10, referente ao ajuste fino de usabilidade identificado durante a regressão manual.

### 3. Documentos e Entregáveis Vinculados

- **Código-fonte** (GUI lapidada, backend COBOL confirmado estável)
- **Testes Automatizados** (execução de confirmação da suíte consolidada)
- **Plano de Testes** (evidências finais em imagem/telas da GUI para as 4 operações)
- **Relatório de Utilização de IA** (Prompt 10)

### 4. Objetivo de Cada Documento

- **Plano de Testes:** Reunir a evidência definitiva de que o sistema, na sua forma final com interface gráfica, atende a todos os requisitos funcionais do escopo estendido.

### 5. Decisões Críticas do Período

- **Integração Tecnológica e Usabilidade:** Homologação final de que a interface gráfica é equivalente ou superior ao console em clareza e facilidade de uso para o atendente da cooperativa.

### ✅ Checklist de Validação do Dia 12

- [ ] Regressão E2E cruzada validada manualmente na GUI
- [ ] Suíte automatizada COBOL confirmada estável (sem regressão de backend)
- [ ] Evidências finais (telas) coletadas para as 4 operações
- [ ] Prompt 10 registrado

**Pausar e aguardar validação humana antes de prosseguir para o Dia 13.**

---

## DIA 13: Estabilização Final (Hardening), Revisão Documental e Ensaio Técnico

> Este dia substitui e amplia o antigo "Dia 5" do cronograma original, agora como etapa final de um projeto de 13 dias.

### 1. Objetivo Central do Dia

Dedicar o período exclusivamente à estabilização de todos os artefatos do escopo completo (CRUD com 4 operações + interface gráfica), auditoria de conformidade e preparação para defesa técnica.

### 2. Atividades e Tarefas

- **Tarefa 1 (Hardening Geral):** Sanar desvios cosméticos residuais em toda a aplicação gráfica e eventuais pendências de mensagens no backend COBOL.
- **Tarefa 2 (Auditoria Completa):** Auditar o Documento de Arquitetura (incluindo a decisão de framework gráfico do Dia 9) e a Estrutura Compartilhada (incluindo o domínio de status atualizado no Dia 7) contra o código final.
- **Tarefa 3 (Homologação do Relatório de IA):** Certificar que os 10 prompts mínimos (3 originais + 7 da extensão) estão preenchidos sem lacunas analíticas.
- **Tarefa 4 (Empacotamento Final):** Gerar o pacote de entrega organizando código-fonte (.NET GUI + 5 programas COBOL: consulta, alteração, exclusão, inclusão), documentação e testes automatizados.
- **Tarefa 5 (Ensaio Técnico):** Simulação de banca cobrindo especificamente as justificativas das três novas decisões arquiteturais: o padrão arquivo-temporário reaproveitado em exclusão/inclusão, a extensão do domínio de `REG-STATUS`, e a escolha do framework gráfico.

### 3. Documentos e Entregáveis Vinculados

- **Código-fonte** (versão final estável — GUI completa + 5 programas COBOL)
- **Documento de Arquitetura** (versão final revisada, incluindo a migração de interface)
- **Estrutura Compartilhada** (layout final homologado com domínio de status completo)
- **Plano de Testes** (evidências consolidadas de todos os 13 dias)
- **Testes Automatizados** (suíte COBOL completa: consulta, alteração, exclusão, inclusão, regressão)
- **Relatório de Utilização de IA** (10 prompts, documento auditado)

### 4. Objetivo de Cada Documento

- **Código-fonte:** Entregar a solução final — CRUD completo, sem dependência de terminal, pronta para operação.
- **Documento de Arquitetura:** Consolidar a narrativa técnica completa do projeto, do spike de conectividade ao spike de interface gráfica.
- **Relatório de Utilização de IA:** Evidenciar de forma responsável o uso de IA ao longo de todo o ciclo estendido do projeto.

### 5. Decisões Críticas do Período

- **Organização Estrutural do Projeto:** Empacotamento final organizando claramente os artefatos originais (Dias 1-5) e os artefatos da extensão de escopo (Dias 6-12), facilitando a leitura da evolução do projeto pela banca.
- **Integração Tecnológica e Defesa de Solução:** Consolidação dos argumentos técnicos para justificar por que a arquitetura híbrida se manteve estável mesmo diante do crescimento de escopo (2 novas operações de dados + troca completa de interface).

### ✅ Checklist de Validação do Dia 13

- [ ] Código-fonte final estável e congelado (GUI + 5 programas COBOL)
- [ ] Documento de Arquitetura e Estrutura Compartilhada auditados contra o código
- [ ] Relatório de IA com os 10 prompts homologado
- [ ] Pacote final de entrega organizado
- [ ] Ensaio técnico realizado

**Fim do cronograma estendido — pacote pronto para entrega/apresentação.**

---

## Resumo da Extensão de Escopo

|Dia|Foco|Novidade em relação ao original|
|---|---|---|
|1-5|Escopo original (Consulta + Alteração)|Inalterado|
|6|Exclusão de cliente (backend + integração)|Novo|
|7|Cadastro de cliente (backend + correção retroativa do contrato)|Novo|
|8|Regressão E2E do CRUD completo|Novo|
|9|Spike de arquitetura da interface gráfica|Novo|
|10|GUI: telas de Consulta e Exclusão|Novo|
|11|GUI: telas de Cadastro e Alteração|Novo|
|12|Regressão E2E completa na GUI + evidências|Novo|
|13|Estabilização final e ensaio técnico|Substitui o antigo Dia 5|

**Justificativa da ordenação:** as funcionalidades 1 e 2 (exclusão e cadastro) são resolvidas primeiro por serem extensões diretas do padrão já validado (arquivo-temporário), cada uma somando seu próprio conjunto de testes, exatamente como solicitado. A funcionalidade 3 (interface gráfica) foi deixada por último e recebeu 4 dias completos (9 a 12) por eliminar a interface atual por completo e introduzir um novo risco técnico de integração (P/Invoke fora do console), justificando seu próprio spike técnico antes da construção das telas — mesmo raciocínio de mitigação de risco aplicado ao Dia 1 do cronograma original.

## 2.1: Análise e Impacto

Acredito que a utilização do primeiro cronograma em anexo deu base suficiente para um cronograma mais completo, com checklists e atividades mais detalhadas. Apesar disso, o número de dias somados (não especificados no prompt) foi maior que o esperado - e também maior que o prazo do projeto. Ademais, o cronograma continua com os mesmos erros do anterior, sem espaço para corrigir possíveis erros. No entanto, as tarefas e objetivos de cada dia me pareceram suficiente para a implementação das funcionalidades propostas.

Então, utilizei o prompt como base para dividir melhor as tarefas entre meus dias restantes até o prazo do projeto e executá-las ao máximo. No fim, estava com o pensamento de entregar o que foi possível, mas acredito que tenha conseguido completar tudo que era esperado - mesmo que trabalhando até o último dia de entrega.

Portanto, a resposta da I.A. foi muito útil e me deu um grande direcionamento para novas funcionalidades e, principalmente, me deixou mais confiante sobre teorias inicias - que me eram inseguras - sobre como implementá-las.

# 3. Prompt: Dúvida sobre concorrência em sistema local

Como o P/INVOKE é totalmente local, não há como ter dois acessos simultâneos. É verdadeira a afirmação? Nesse caso, a preocupação com concorrência só seria possível caso fosse utilizado, por exemplo, a estratégia de API REST em servidor web. Estou correto?

### Sendo o P/Invoke totalmente local, é possível ter dois acessos simultâneos?

A sua linha de raciocínio sobre a estratégia de API REST está impecável: num servidor web centralizado, múltiplas requisições de atendentes diferentes entram em threads concorrentes, gerando um cenário severo de concorrência pelo arquivo de dados.

Contudo, a sua premissa de que o P/Invoke — por ser local — impede o cenário de concorrência **está incorreta**.

Mesmo que a aplicação rode localmente na máquina do utilizador, existe um cenário muito comum de concorrência chamado **Concorrência Multiprocesso Local**.

**Como o erro de concorrência acontece localmente:** Se o atendente da cooperativa, por distração ou lentidão do computador, clicar duas vezes no ícone do sistema, o Windows iniciará **duas instâncias independentes da interface gráfica (dois processos executáveis `.exe` separados na RAM)**.

Cada um desses dois processos carregará a sua própria `libcob-4.dll` e as suas próprias instâncias isoladas do `CobolService.cs` em memória. Até aí, tudo bem. O problema surge quando ambos tentam interagir com o sistema de arquivos local:

1. O Atendente usa a **Janela A** para alterar o e-mail do cliente `000000001`. O Processo A abre `CLIENTES.DAT` e cria o arquivo físico `CLIENTES.TMP` na raiz do projeto.
    
2. Exatamente no mesmo instante, antes do Processo A terminar, o atendente vai à **Janela B** (que ficou aberta por engano) e tenta consultar ou excluir o mesmo cliente. O Processo B tentará criar ou manipular o **exato mesmo arquivo fixo `CLIENTES.TMP` no mesmo diretório**.
    

**O Resultado:** O Windows bloqueará o segundo processo, disparando um erro fatal de travamento de arquivo (_File Locking_), ou o GnuCOBOL desabará emitindo um _File Status 30 (Permanent File Error)_ ou _Status 42 (File Already Open)_.

## 3.2: Análise e Impacto

Fiquei com uma dúvida sobre a existência de acessos simultâneos no P/INVOKE para saber se era necessário o tratamento desse caso. A resposta da I.A. foi muito didática e clara sobre, me ajudando a entender melhor as consequências da minha escolha e até mesmo a perspectiva de outras estratégias. Diferente dos outros, não tenho o que comentar de negativo e a resposta me ajudou a entender melhor detalhes da implementação seguida no trabalho.