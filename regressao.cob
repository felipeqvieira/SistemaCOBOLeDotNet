      *> 
      *> PROGRAMA: REGRESSAO.COB
      *> SUITE UNIFICADA DE REGRESSÃO DO CRUD COMPLETO
      *> 
       IDENTIFICATION DIVISION.
       PROGRAM-ID. regressao.

       DATA DIVISION.
       WORKING-STORAGE SECTION.
       COPY "REGISTRO.cpy".

       PROCEDURE DIVISION.
       MAIN-PROCEDURE.
           DISPLAY "--- INICIANDO SUITE DE REGRESSEO UNIFICADA (CRUD) ---"

           *> Cadastrar um cliente novo ("000000003")
           MOVE SPACES TO REGISTRO-CLIENTE
           MOVE "000000003" TO REG-CODIGO
           MOVE "CARLOS GOMES ALFA" TO REG-NOME
           MOVE "11966665555" TO REG-TELEFONE
           MOVE "carlos.gomes@alfa.coop" TO REG-EMAIL
           CALL "inclusao" USING REGISTRO-CLIENTE
    
           IF REG-STATUS = "00"
               DISPLAY "[PASS] Passo 1: Cadastro inicial de cliente inedito concluido."
           ELSE
               DISPLAY "[FAIL] Passo 1: Falha no cadastro inicial de teste."
           END-IF.

           *> Consultar o cliente cadastrado para aferir o estado
           MOVE SPACES TO REGISTRO-CLIENTE
           MOVE "000000003" TO REG-CODIGO
           CALL "consulta" USING REGISTRO-CLIENTE
    
           IF REG-STATUS = "00" AND REG-NOME(1:12) = "CARLOS GOMES"
               DISPLAY "[PASS] Passo 2: Consulta de confirmacao de escrita executada."
           ELSE
               DISPLAY "[FAIL] Passo 2: Falha na consulta de confirmacao de dados."
           END-IF.

           *> Alterar o telefone e email do cliente inserido
           MOVE "11955554444" TO REG-TELEFONE
           MOVE "carlos.novo@alfa.coop" TO REG-EMAIL
           CALL "alteracao" USING REGISTRO-CLIENTE
    
           IF REG-STATUS = "00"
               DISPLAY "[PASS] Passo 3: Alteracao cadastral em lote processada."
           ELSE
               DISPLAY "[FAIL] Passo 3: Falha na alteracao de dados cadastrais."
           END-IF.

           *> Consultar novamente para conferir a mudanca fisica
           MOVE SPACES TO REGISTRO-CLIENTE
           MOVE "000000003" TO REG-CODIGO
           CALL "consulta" USING REGISTRO-CLIENTE
    
           IF REG-STATUS = "00" AND REG-TELEFONE(1:11) = "11955554444"
               DISPLAY "[PASS] Passo 4: Verificacao da alteracao pos-gravacao de teste."
           ELSE
               DISPLAY "[FAIL] Passo 4: Inconsistencia detectada na releitura da alteracao."
           END-IF.

           *> Excluir o cliente de teste da base
           MOVE SPACES TO REGISTRO-CLIENTE
           MOVE "000000003" TO REG-CODIGO
           CALL "exclusao" USING REGISTRO-CLIENTE
    
           IF REG-STATUS = "00"
               DISPLAY "[PASS] Passo 5: Exclusao filtrada do registro efetuada."
           ELSE
               DISPLAY "[FAIL] Passo 5: Falha ao remover o cliente da base."
           END-IF.

           *> Consultar o registro excluido (Deve retornar 01)
           MOVE SPACES TO REGISTRO-CLIENTE
           MOVE "000000003" TO REG-CODIGO
           CALL "consulta" USING REGISTRO-CLIENTE
    
           IF REG-STATUS = "01"
               DISPLAY "[PASS] Passo 6: Confirmacao de ausencia pos-exclusao homologada."
           ELSE
               DISPLAY "[FAIL] Passo 6: Registro ainda localizado pos-exclusao."
           END-IF.

           *> CENARIO ADVERSO 1 - Tentar alterar cliente excluido
           MOVE "1122223333" TO REG-TELEFONE
           MOVE "erro@alfa.coop" TO REG-EMAIL
           CALL "alteracao" USING REGISTRO-CLIENTE
    
           IF REG-STATUS = "01"
               DISPLAY "[PASS] Cenario Adverso 1: Rejeicao de alteracao em excluido validada."
           ELSE
               DISPLAY "[FAIL] Cenario Adverso 1: Modificacao indevida de chave inexistente."
           END-IF.

           *> CENARIO ADVERSO 2 - Cadastrar codigo que foi recem-excluido
           MOVE SPACES TO REGISTRO-CLIENTE
           MOVE "000000003" TO REG-CODIGO
           MOVE "REINCLUIDO ALFA" TO REG-NOME
           CALL "inclusao" USING REGISTRO-CLIENTE
    
           IF REG-STATUS = "00"
               DISPLAY "[PASS] Cenario Adverso 2: Re-inclusao de codigo liberado aceita."
           ELSE
               DISPLAY "[FAIL] Cenario Adverso 2: Falha ao re-incluir codigo disponível."
           END-IF.

           DISPLAY "--- FIM DA EXECUCEO DA SUITE DE REGRESSEO ---"
           STOP RUN.
