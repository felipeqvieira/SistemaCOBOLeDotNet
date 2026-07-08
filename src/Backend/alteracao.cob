      *>
      *> PROGRAMA: ALTERACAO.COB
      *> ATUALIZA CADASTRO UTILIZANDO ARQUIVO TEMPORARIO
      *> 
       IDENTIFICATION DIVISION.
       PROGRAM-ID. alteracao.

       ENVIRONMENT DIVISION.
       INPUT-OUTPUT SECTION.
       FILE-CONTROL.
           SELECT ARQ-CLIENTES ASSIGN TO "CLIENTES.DAT"
                  ORGANIZATION IS LINE SEQUENTIAL.
           SELECT ARQ-TEMP     ASSIGN TO "CLIENTES.TMP"
                  ORGANIZATION IS LINE SEQUENTIAL.

       DATA DIVISION.
       FILE SECTION.
       FD  ARQ-CLIENTES.
       01  REG-ARQ-CLIENTE.
           05 ARQ-CODIGO    PIC X(09).
           05 ARQ-NOME      PIC X(30).
           05 ARQ-TELEFONE  PIC X(15).
           05 ARQ-EMAIL     PIC X(40).

       FD  ARQ-TEMP.
       01  REG-ARQ-TEMP     PIC X(94).

       WORKING-STORAGE SECTION.
       01  SINALIZADORES.
           05 FIM-DO-ARQUIVO  PIC X(01) VALUE "N".
       01  WS-REGISTRO-AUX.
           05 WS-CODIGO       PIC X(09).
           05 WS-NOME         PIC X(30).
           05 WS-TELEFONE     PIC X(15).
           05 WS-EMAIL        PIC X(40).

       LINKAGE SECTION.
       COPY "REGISTRO.cpy".

       PROCEDURE DIVISION USING REGISTRO-CLIENTE.
       MAIN-PROCEDURE.
           MOVE "N" TO FIM-DO-ARQUIVO
           MOVE "01" TO REG-STATUS

           OPEN INPUT ARQ-CLIENTES
           OPEN OUTPUT ARQ-TEMP
    
           PERFORM UNTIL FIM-DO-ARQUIVO = "S"
               READ ARQ-CLIENTES
                   AT END
                       MOVE "S" TO FIM-DO-ARQUIVO
                   NOT AT END
      *> Transfere para a estrutura auxiliar de trabalho
                       MOVE ARQ-CODIGO   TO WS-CODIGO
                       MOVE ARQ-NOME     TO WS-NOME
                       MOVE ARQ-TELEFONE TO WS-TELEFONE
                       MOVE ARQ-EMAIL    TO WS-EMAIL
                
                       IF ARQ-CODIGO = REG-CODIGO
                           MOVE REG-TELEFONE TO WS-TELEFONE
                           MOVE REG-EMAIL    TO WS-EMAIL
                           MOVE "00"         TO REG-STATUS
                       END-IF
                
      *> Grava o registro atualizado ou mantido no arquivo temporario
                       WRITE REG-ARQ-TEMP FROM WS-REGISTRO-AUX
               END-READ
           END-PERFORM

           CLOSE ARQ-CLIENTES
           CLOSE ARQ-TEMP

      *> Realiza a substituicao fisica caso o codigo tenha sido processado
           IF REG-STATUS = "00"
               CALL "SYSTEM" USING "cmd /c copy /y CLIENTES.TMP CLIENTES.DAT >nul && del CLIENTES.TMP"
           ELSE
               CALL "SYSTEM" USING "cmd /c del CLIENTES.TMP"
           END-IF

           EXIT PROGRAM.
    