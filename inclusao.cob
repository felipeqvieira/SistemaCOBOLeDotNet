      *>
      *> PROGRAMA: INCLUSAO.COB
      *> CADASTRAR NOVO CLIENTE COM VALIDACAO DE DUPLICIDADE
      *>
       IDENTIFICATION DIVISION.
       PROGRAM-ID. inclusao.

       ENVIRONMENT DIVISION.
       INPUT-OUTPUT SECTION.
       FILE-CONTROL.
      *> O uso de OPTIONAL permite a abertura mesmo se o arquivo for inexistente
           SELECT OPTIONAL ARQ-CLIENTES ASSIGN TO "CLIENTES.DAT"
                  ORGANIZATION IS LINE SEQUENTIAL.

       DATA DIVISION.
       FILE SECTION.
       FD  ARQ-CLIENTES.
       01  REG-ARQ-CLIENTE.
           05 ARQ-CODIGO    PIC X(09).
           05 ARQ-NOME      PIC X(30).
           05 ARQ-TELEFONE  PIC X(15).
           05 ARQ-EMAIL     PIC X(40).

       WORKING-STORAGE SECTION.
       01  SINALIZADORES.
           05 FIM-DO-ARQUIVO  PIC X(01) VALUE "N".

       LINKAGE SECTION.
       COPY "REGISTRO.cpy".

       PROCEDURE DIVISION USING REGISTRO-CLIENTE.
       MAIN-PROCEDURE.
           MOVE "N" TO FIM-DO-ARQUIVO
           MOVE "00" TO REG-STATUS

           IF REG-NOME = SPACES OR REG-NOME = LOW-VALUES
               MOVE "03" TO REG-STATUS
               EXIT PROGRAM
           END-IF

           OPEN INPUT ARQ-CLIENTES
    
           PERFORM UNTIL FIM-DO-ARQUIVO = "S"
               READ ARQ-CLIENTES
            AT END
                MOVE "S" TO FIM-DO-ARQUIVO
            NOT AT END
                IF ARQ-CODIGO = REG-CODIGO
                    MOVE "02" TO REG-STATUS
                    MOVE "S"  TO FIM-DO-ARQUIVO
                END-IF
        END-READ
           END-PERFORM

           CLOSE ARQ-CLIENTES

           IF REG-STATUS = "00"
               OPEN EXTEND ARQ-CLIENTES
               MOVE REG-CODIGO   TO ARQ-CODIGO
               MOVE REG-NOME     TO ARQ-NOME
               MOVE REG-TELEFONE TO ARQ-TELEFONE
               MOVE REG-EMAIL    TO ARQ-EMAIL
               WRITE REG-ARQ-CLIENTE
               CLOSE ARQ-CLIENTES
           END-IF

           EXIT PROGRAM.
           