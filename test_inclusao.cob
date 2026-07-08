      *>
      *> PROGRAMA: TEST_INCLUSAO.COB
      *> TESTE AUTOMATIZADO DO FLUXO DE CADASTRO / INCLUSÃO
      *>
       IDENTIFICATION DIVISION.
       PROGRAM-ID. test-inclusao.

       DATA DIVISION.
       WORKING-STORAGE SECTION.
       COPY "REGISTRO.cpy".

       PROCEDURE DIVISION.
       MAIN-PROCEDURE.
           DISPLAY "--- INICIANDO SUITE DE TESTES AUTOMATIZADOS (INCLUSAO) ---"

           *> Tentativa de inclusao com nome vazio (Rejeicao)
           MOVE SPACES TO REGISTRO-CLIENTE
           MOVE "000000002" TO REG-CODIGO
           MOVE SPACES      TO REG-NOME
           CALL "inclusao" USING REGISTRO-CLIENTE
    
           IF REG-STATUS = "03"
               DISPLAY "[PASS] Cenario 1: Rejeicao de nome vazio validada."
           ELSE
               DISPLAY "[FAIL] Cenario 1: Falha ao rejeitar nome vazio."
           END-IF.

           *> Inclusao de um novo codigo valido (Sucesso)
           MOVE SPACES TO REGISTRO-CLIENTE
           MOVE "000000002" TO REG-CODIGO
           MOVE "MARIA SOUZA ALFA" TO REG-NOME
           MOVE "11977776666" TO REG-TELEFONE
           MOVE "maria.souza@alfa.coop" TO REG-EMAIL
           CALL "inclusao" USING REGISTRO-CLIENTE
    
           IF REG-STATUS = "00"
               DISPLAY "[PASS] Cenario 2: Inclusao de novo cliente realizada."
           ELSE
               DISPLAY "[FAIL] Cenario 2: Falha ao incluir novo cliente."
           END-IF.

           *> Tentativa de inclusao de codigo duplicado (Bloqueio)
           MOVE SPACES TO REGISTRO-CLIENTE
           MOVE "000000002" TO REG-CODIGO
           MOVE "OUTRO NOME" TO REG-NOME
           CALL "inclusao" USING REGISTRO-CLIENTE
    
           IF REG-STATUS = "02"
               DISPLAY "[PASS] Cenario 3: Bloqueio de codigo duplicado validado."
           ELSE
               DISPLAY "[FAIL] Cenario 3: Falha ao bloquear duplicidade."
           END-IF.

           DISPLAY "--- FIM DA EXECUÇÃO DOS TESTES DE INCLUSÃO ---"
           STOP RUN.
