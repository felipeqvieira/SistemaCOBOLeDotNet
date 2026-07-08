      *> 
      *> PROGRAMA: SPIKE.COB
      *> SUBPROGRAMA DE TESTE DE CONECTIVIDADE (ROUND-TRIP)
      *> 
       IDENTIFICATION DIVISION.
       PROGRAM-ID. spike.

       DATA DIVISION.
       WORKING-STORAGE SECTION.
       LINKAGE SECTION.
       COPY "REGISTRO.cpy".

       PROCEDURE DIVISION USING REGISTRO-CLIENTE.
       MAIN-PROCEDURE.
           IF REG-CODIGO = "000000001"
               MOVE "ALFA COOPERATIVA" TO REG-NOME
               MOVE "00"               TO REG-STATUS
           ELSE
               MOVE "ERRO"             TO REG-NOME
               MOVE "99"               TO REG-STATUS
           END-IF.
       EXIT PROGRAM.
       