      *> 
      *> PROGRAMA: TEST_CONSULTA.COB
      *> TESTE AUTOMATIZADO DOS CENARIOS DE LEITURA
      *> 
       IDENTIFICATION DIVISION.
       PROGRAM-ID. test-consulta.

       DATA DIVISION.
       WORKING-STORAGE SECTION.
       COPY "REGISTRO.cpy".

       PROCEDURE DIVISION.
       MAIN-PROCEDURE.
           DISPLAY "--- INICIANDO SUITE DE TESTES AUTOMATIZADOS (LEITURA) ---"

       *>  Pesquisa por Cliente Existente
           MOVE SPACES TO REGISTRO-CLIENTE
           MOVE "000000001" TO REG-CODIGO
           CALL "consulta" USING REGISTRO-CLIENTE
    
           IF REG-STATUS = "00" AND REG-NOME(1:13) = "JOAO DA SILVA"
               DISPLAY "[PASS] Cenario 1: Busca de cliente cadastrado com sucesso."
           ELSE
               DISPLAY "[FAIL] Cenario 1: Erro ao buscar cliente cadastrado."
           END-IF.

       *>  Pesquisa por Cliente Inexistente
           MOVE SPACES TO REGISTRO-CLIENTE
           MOVE "999999999" TO REG-CODIGO
           CALL "consulta" USING REGISTRO-CLIENTE
    
           IF REG-STATUS = "01"
               DISPLAY "[PASS] Cenario 2: Tratamento de cliente inexistente validado."
           ELSE
               DISPLAY "[FAIL] Cenario 2: Falha ao sinalizar cliente inexistente."
           END-IF.

           DISPLAY "--- FIM DA EXECUCAO DOS TESTES LOGICOS ---"
           STOP RUN.
