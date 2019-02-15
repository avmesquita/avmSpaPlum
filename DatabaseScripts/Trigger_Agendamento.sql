USE dbo_spaplum
GO

CREATE TABLE [dbo].[TB_HISTORICO_AGENDAMENTO](
	[COD_AGENDAMENTO] [int] NULL,
	[TXT_NOME] [nvarchar](max) NULL,
	[TXT_EMAIL] [nvarchar](max) NULL,
	[TXT_CLUBE_VIP] [nvarchar](max) NULL,
	[DAT_INICIO] [datetime] NULL,
	[DAT_FIM] [datetime] NULL,
	[VAL_VALOR] [decimal](18, 2) NULL,
	[VAL_TAXA_ADICIONAL] [decimal](18, 2) NULL,
	[TIP_MASSAGEM] [int] NULL,
	[NUM_PERIODOS] [int] NULL,
	[TXT_OBSERVACAO] [nvarchar](max) NULL,
	[COD_FILIAL] [int] NULL,
	[COD_SERVICO] [int] NULL,
	[COD_STATUS_AGENDAMENTO] [int] NULL,
	[COD_TERAPEUTA] [int] NULL,
	[COD_TIPO_PAGAMENTO] [int] NULL,
	[TXT_AUTENTICACAO] nvarchar(255) NULL,
	[TXT_OPERACAO] nvarchar(255) NULL
) 
GO

-- =============================================
-- Author:		Andre Mesquita
-- Create date: 20/02/2017
-- Description:	Inclui histórico de alterações 
--              no agendamento
-- =============================================
CREATE TRIGGER dbo.TRG_HISTORICO_AGENDAMENTO
   ON  TB_AGENDAMENTO
   AFTER INSERT,DELETE,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF NOT EXISTS(SELECT * FROM INSERTED)  AND EXISTS(SELECT * FROM DELETED) 
    BEGIN 
	  /* PRINT 'UPDATE' */
	  INSERT INTO TB_HISTORICO_AGENDAMENTO (
               [COD_AGENDAMENTO],
	           [TXT_NOME],
	           [TXT_EMAIL],
	           [TXT_CLUBE_VIP],
	           [DAT_INICIO],
	           [DAT_FIM],
	           [VAL_VALOR],
	           [VAL_TAXA_ADICIONAL],
	           [TIP_MASSAGEM],
	           [NUM_PERIODOS],
	           [TXT_OBSERVACAO],
	           [COD_FILIAL],
	           [COD_SERVICO],
	           [COD_STATUS_AGENDAMENTO],
	           [COD_TERAPEUTA],
	           [COD_TIPO_PAGAMENTO],
	           [TXT_AUTENTICACAO])
	    SELECT [COD_AGENDAMENTO],
	           [TXT_NOME],
	           [TXT_EMAIL],
	           [TXT_CLUBE_VIP],
	           [DAT_INICIO],
	           [DAT_FIM],
	           [VAL_VALOR],
	           [VAL_TAXA_ADICIONAL],
	           [TIP_MASSAGEM],
	           [NUM_PERIODOS],
	           [TXT_OBSERVACAO],
	           [COD_FILIAL],
	           [COD_SERVICO],
	           [COD_STATUS_AGENDAMENTO],
	           [COD_TERAPEUTA],
	           [COD_TIPO_PAGAMENTO],
	           [TXT_AUTENTICACAO],		
			   'INCLUSAO'						
		FROM INSERTED	
	END 
    ELSE IF EXISTS(SELECT * FROM INSERTED)  AND NOT EXISTS(SELECT * FROM DELETED) 
	BEGIN
       /* PRINT 'INSERT' */
	  INSERT INTO TB_HISTORICO_AGENDAMENTO (
               [COD_AGENDAMENTO],
	           [TXT_NOME],
	           [TXT_EMAIL],
	           [TXT_CLUBE_VIP],
	           [DAT_INICIO],
	           [DAT_FIM],
	           [VAL_VALOR],
	           [VAL_TAXA_ADICIONAL],
	           [TIP_MASSAGEM],
	           [NUM_PERIODOS],
	           [TXT_OBSERVACAO],
	           [COD_FILIAL],
	           [COD_SERVICO],
	           [COD_STATUS_AGENDAMENTO],
	           [COD_TERAPEUTA],
	           [COD_TIPO_PAGAMENTO],
	           [TXT_AUTENTICACAO])
	    SELECT [COD_AGENDAMENTO],
	           [TXT_NOME],
	           [TXT_EMAIL],
	           [TXT_CLUBE_VIP],
	           [DAT_INICIO],
	           [DAT_FIM],
	           [VAL_VALOR],
	           [VAL_TAXA_ADICIONAL],
	           [TIP_MASSAGEM],
	           [NUM_PERIODOS],
	           [TXT_OBSERVACAO],
	           [COD_FILIAL],
	           [COD_SERVICO],
	           [COD_STATUS_AGENDAMENTO],
	           [COD_TERAPEUTA],
	           [COD_TIPO_PAGAMENTO],
	           [TXT_AUTENTICACAO],		
			   'ALTERACAO'						
		FROM UPDATED
	END 
    ELSE IF EXISTS(SELECT * FROM DELETED) AND NOT EXISTS(SELECT * FROM INSERTED)
    BEGIN 
	  /* PRINT 'DELETED' */
	  INSERT INTO TB_HISTORICO_AGENDAMENTO (
               [COD_AGENDAMENTO],
	           [TXT_NOME],
	           [TXT_EMAIL],
	           [TXT_CLUBE_VIP],
	           [DAT_INICIO],
	           [DAT_FIM],
	           [VAL_VALOR],
	           [VAL_TAXA_ADICIONAL],
	           [TIP_MASSAGEM],
	           [NUM_PERIODOS],
	           [TXT_OBSERVACAO],
	           [COD_FILIAL],
	           [COD_SERVICO],
	           [COD_STATUS_AGENDAMENTO],
	           [COD_TERAPEUTA],
	           [COD_TIPO_PAGAMENTO],
	           [TXT_AUTENTICACAO])
	    SELECT [COD_AGENDAMENTO],
	           [TXT_NOME],
	           [TXT_EMAIL],
	           [TXT_CLUBE_VIP],
	           [DAT_INICIO],
	           [DAT_FIM],
	           [VAL_VALOR],
	           [VAL_TAXA_ADICIONAL],
	           [TIP_MASSAGEM],
	           [NUM_PERIODOS],
	           [TXT_OBSERVACAO],
	           [COD_FILIAL],
	           [COD_SERVICO],
	           [COD_STATUS_AGENDAMENTO],
	           [COD_TERAPEUTA],
	           [COD_TIPO_PAGAMENTO],
	           [TXT_AUTENTICACAO],		
			   'EXCLUSAO'						
		FROM DELETED	
	END
    ELSE BEGIN 
	  /* PRINT 'NOTHING CHANGED'; */
	  RETURN; 	
	END  -- NOTHING
END
GO
