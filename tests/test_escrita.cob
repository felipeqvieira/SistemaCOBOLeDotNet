      *>
      *> PROGRAMA: TEST_ESCRITA.COB
      *> TESTE AUTOMATIZADO DO COMPORTAMENTO DO CANAL DE ESCRITA
      *>
       IDENTIFICATION DIVISION.
       PROGRAM-ID. test-escrita.

       DATA DIVISION.
       WORKING-STORAGE SECTION.
       COPY "REGISTRO.cpy".

       PROCEDURE DIVISION.
       MAIN-PROCEDURE.
           DISPLAY "--- INICIANDO SUITE DE TESTES AUTOMATIZADOS (ESCRITA) ---"

      *> Execucao da alteracao cadastral em registro existente
           MOVE SPACES TO REGISTRO-CLIENTE
           MOVE "000000001"       TO REG-CODIGO
           MOVE "11988887777"     TO REG-TELEFONE
           MOVE "novo@alfa.coop"  TO REG-EMAIL
    
           CALL "alteracao" USING REGISTRO-CLIENTE
    
           IF REG-STATUS = "00"
               DISPLAY "[PASS] Cenario 1: Comando REWRITE executado com sucesso."
           ELSE
               DISPLAY "[FAIL] Cenario 1: Falha ao atualizar dados do registro."
           END-IF.

      *> Releitura para homologacao da persistencia fisica
           MOVE SPACES TO REGISTRO-CLIENTE
           MOVE "000000001" TO REG-CODIGO
    
           CALL "consulta" USING REGISTRO-CLIENTE
    
           IF REG-STATUS = "00" AND REG-TELEFONE(1:11) = "11988887777"
               DISPLAY "[PASS] Cenario 2: Paridade e persistencia de dados homologadas."
           ELSE
               DISPLAY "[FAIL] Cenario 2: Inconsistencia detectada na releitura."
           END-IF.

           DISPLAY "--- FIM DA EXECUCAO DOS TESTES DE ESCRITA ---"
           STOP RUN.
           