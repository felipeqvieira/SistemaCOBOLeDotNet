# Documento de Estrutura Compartilhada

## Contrato de Dados para Integração .NET ↔ COBOL

  

---

  

## Visão Geral do Contrato

  

A integração entre a camada .NET (`CobolService.cs`) e os programas COBOL (`consulta.cob`, `alteracao.cob`, `exclusao.cob`, `inclusao.cob`) é feita através de um **buffer binário de tamanho fixo, de 96 bytes**, transmitido por referência via P/Invoke a cada chamada nativa. Esse buffer é a representação em memória, no lado .NET, exatamente do mesmo layout que o copybook `REGISTRO.cpy` define no lado COBOL, na `LINKAGE SECTION` de cada programa:

  

```cobol

        01  REGISTRO-CLIENTE.

            05 REG-CODIGO    PIC X(09).

            05 REG-NOME      PIC X(30).

            05 REG-TELEFONE  PIC X(15).

            05 REG-EMAIL     PIC X(40).

            05 REG-STATUS    PIC X(02).

```

  

Por ser um único copybook (`COPY "REGISTRO.cpy"`) compartilhado entre os quatro programas COBOL, qualquer alteração de layout é propagada automaticamente para todos os pontos de integração no lado COBOL — o único ponto que precisa ser mantido manualmente em sincronia é a montagem do buffer em `CobolService.cs`, no lado .NET, já que C# não lê copybooks diretamente.

  

---

  

## Especificação de Campos (Buffer de Integração — 96 bytes)

  

| # | Campo | Offset (byte inicial) | Tamanho (bytes) | Tipo COBOL | Tipo .NET equivalente | Regra de Padding |

|---|---|---|---|---|---|---|

| 1 | `REG-CODIGO` | 0 | 9 | `PIC X(09)` alfanumérico | `string` (ASCII) | Preenchido à esquerda com o código numérico; completado com **espaços em branco** à direita até 9 posições |

| 2 | `REG-NOME` | 9 | 30 | `PIC X(30)` alfanumérico | `string` (ASCII) | Texto alinhado à esquerda; completado com **espaços em branco** à direita até 30 posições; truncado se exceder |

| 3 | `REG-TELEFONE` | 39 | 15 | `PIC X(15)` alfanumérico | `string` (ASCII) | Texto alinhado à esquerda; completado com **espaços em branco** à direita até 15 posições; truncado se exceder |

| 4 | `REG-EMAIL` | 54 | 40 | `PIC X(40)` alfanumérico | `string` (ASCII) | Texto alinhado à esquerda; completado com **espaços em branco** à direita até 40 posições; truncado se exceder |

| 5 | `REG-STATUS` | 94 | 2 | `PIC X(02)` alfanumérico | `string` (ASCII) | Sempre um código de 2 dígitos numéricos como texto (`"00"`, `"01"`, `"02"`, `"03"`); é o único campo **exclusivamente de saída** (não deve ser preenchido pelo .NET antes da chamada) |

  

**Tamanho total do contrato: 96 bytes** (9 + 30 + 15 + 40 + 2).

  

Convenções gerais de padding:

- Todos os campos são alfanuméricos (`PIC X`), codificados em **ASCII**, sem terminador nulo — o tamanho é sempre fixo e conhecido por ambas as pontas, nunca delimitado por caractere de controle.

- O caractere de padding padrão é o **espaço em branco (`0x20`)**, tanto na inicialização do buffer no .NET (`Array.Fill(buffer, (byte)' ')`) quanto na inicialização de campos no COBOL (`MOVE SPACES TO ...`).

- Strings mais curtas que o campo são preenchidas à direita com espaços; strings mais longas são truncadas na cópia (`Math.Min(bytes.Length, tamanhoDoCampo)` no .NET).

- Ao ler o retorno, o lado .NET remove os espaços de padding com `TrimEnd()` antes de exibir os dados ao usuário.

  

---

  

## Domínio de Valores do Campo de Status (`REG-STATUS`)

  

O campo de status funciona como o mecanismo de tratamento de erro do contrato — seu significado depende da operação chamada:

  

| Valor | Significado | Programas que o utilizam |

|---|---|---|

| `"00"` | Sucesso — cliente localizado (consulta) ou operação de escrita efetivada (alteração/exclusão/inclusão) | `consulta.cob`, `alteracao.cob`, `exclusao.cob`, `inclusao.cob` |

| `"01"` | Cliente não localizado pelo código informado | `consulta.cob`, `alteracao.cob`, `exclusao.cob` |

| `"02"` | Código já cadastrado (violação de duplicidade) — operação de inclusão rejeitada | `inclusao.cob` |

| `"03"` | Nome do cliente não informado (campo obrigatório vazio) — operação de inclusão rejeitada | `inclusao.cob` |

  

---

  

## Diferença entre o Contrato de Integração e o Layout de Persistência Física

  

É importante não confundir o buffer de 96 bytes (contrato de **integração**, usado apenas em memória entre .NET e COBOL) com o layout do registro **fisicamente gravado** em `CLIENTES.DAT`, que é 2 bytes menor, pois **não inclui o campo de status**:

  

```cobol

       FD  ARQ-CLIENTES.

       01  REG-ARQ-CLIENTE.

           05 ARQ-CODIGO    PIC X(09).

           05 ARQ-NOME      PIC X(30).

           05 ARQ-TELEFONE  PIC X(15).

           05 ARQ-EMAIL     PIC X(40).

```

  

| Camada | Tamanho do registro | Contém `STATUS`? |

|---|---|---|

| Contrato de integração (`REGISTRO.cpy`, buffer .NET ↔ COBOL) | 96 bytes | Sim (2 bytes, apenas de saída) |

| Registro físico em `CLIENTES.DAT` (`REG-ARQ-CLIENTE`) | 94 bytes | Não |

  

Essa distinção existe porque o status é um **artefato de comunicação entre processos/camadas**, sem significado de negócio a ser persistido — um cliente não "tem" um status gravado no arquivo; o status apenas informa o resultado de uma operação específica no momento em que ela ocorre. Os programas de escrita (`alteracao.cob`, `exclusao.cob`) fazem essa "tradução" de layout explicitamente ao copiar campo a campo entre `REG-ARQ-CLIENTE`/`WS-REGISTRO-AUX` (94 bytes, sem status) e `REGISTRO-CLIENTE` (96 bytes, com status).

  

---

  

## Representação Visual do Buffer

  

```

Offset:   0         9                            39              54                                94   96

          │─────────│────────────────────────────│───────────────│────────────────────────────────│────│

Campo:    │ CODIGO  │            NOME            │   TELEFONE    │              EMAIL             │STAT│

Tamanho:  │  9 bytes│          30 bytes          │    15 bytes   │            40 bytes            │2 by│

          └─────────┴────────────────────────────┴───────────────┴────────────────────────────────┴────┘

```

  

## Exemplo Concreto (Cliente de Teste da Suíte de Regressão)

  

Tomando como exemplo o cliente fictício usado em `regressao.cob` (código `"000000003"`, nome `"CARLOS GOMES ALFA"`), o buffer enviado na operação de inclusão seria montado, em bytes ASCII, como:

  

```

  "000000003"    + "CARLOS GOMES ALFA            " + "11966665555    " +  "carlos.gomes@alfa.coop                 " + "  "

└─── 9 bytes ───┘└─────────── 30 bytes ───────────┘└───── 15 bytes ─────┘└──────────────── 40 bytes ─────────────────┘└2 by┘

```

  

Após a chamada a `inclusao.dll`, o campo de status (últimos 2 bytes) é sobrescrito pelo COBOL com `"00"` (sucesso) — os demais 94 bytes permanecem como enviados, pois a operação de inclusão não precisa devolver os dados de volta ao chamador.