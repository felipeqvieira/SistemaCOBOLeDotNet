      *> 
      *> PROGRAMA: TEST_EXCLUSAO.COB
      *> VALIDAR EXCLUSÃO SELETIVA E INTEGRIDADE DE REGISTROS
      *> 
       IDENTIFICATION DIVISION.
       PROGRAM-ID. test-exclusao.

       DATA DIVISION.
       WORKING-STORAGE SECTION.
       COPY "REGISTRO.cpy".

       PROCEDURE DIVISION.
       MAIN-PROCEDURE.
           DISPLAY "--- INICIANDO SUITE DE TESTES AUTOMATIZADOS (EXCLUSAO) ---"

           *> Tentativa de remocao de codigo inexistente
           MOVE SPACES TO REGISTRO-CLIENTE
           MOVE "999999999" TO REG-CODIGO
           CALL "exclusao" USING REGISTRO-CLIENTE
    
           IF REG-STATUS = "01"
               DISPLAY "[PASS] Cenario 1: Rejeicao de codigo inexistente validada."
           ELSE
               DISPLAY "[FAIL] Cenario 1: Erro ao tratar codigo inexistente."
           END-IF.

           *> Execucao da remocao de cliente cadastrado
           MOVE SPACES TO REGISTRO-CLIENTE
           MOVE "000000001" TO REG-CODIGO
           CALL "exclusao" USING REGISTRO-CLIENTE
    
           IF REG-STATUS = "00"
               DISPLAY "[PASS] Cenario 2: Fluxo de exclusao executado com sucesso."
           ELSE
               DISPLAY "[FAIL] Cenario 2: Falha ao excluir cliente cadastrado."
           END-IF.

           *> Releitura para provar a ausencia fisica do registro
           MOVE SPACES TO REGISTRO-CLIENTE
           MOVE "000000001" TO REG-CODIGO
           CALL "consulta" USING REGISTRO-CLIENTE
    
           IF REG-STATUS = "01"
               DISPLAY "[PASS] Cenario 3: Ausencia fisica do cliente homologada."
           ELSE
               DISPLAY "[FAIL] Cenario 3: Registro ainda remanesce na base física."
           END-IF.

           DISPLAY "--- FIM DA EXECUÇÃO DOS TESTES DE REMOÇÃO ---"
           STOP RUN.
