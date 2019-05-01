/* SCRIPT PARA CRIAR AS ESTRUTURAS PARA EXCLUIR AS TABELAS DE DEV */
use dbo_spaplum
go

CREATE FUNCTION [dbo].[TableExists] 
(
    @TableName VarChar(100)
)  
    RETURNS BIT
AS  
BEGIN 
    DECLARE @TableExists BIT

IF EXISTS(SELECT name FROM sysobjects a
          WHERE a.name =  @TableName
          AND a.xtype = 'U')
    SET @TableExists = 1
ELSE
    SET @TableExists = 0


RETURN @TableExists
END
GO

CREATE procedure [dbo].[SP_APAGA_TABELAS]
AS  
BEGIN  
  --select * from vw_minhas_tabelas   
  IF [dbo].TableExists('AspNetUserClaims') = 1             drop table AspNetUserClaims  
  IF [dbo].TableExists('AspNetUserLogins') = 1             drop table AspNetUserLogins  
  IF [dbo].TableExists('AspNetUserRoles') = 1              drop table AspNetUserRoles  
  IF [dbo].TableExists('AspNetRoles') = 1                  drop table AspNetRoles  
  IF [dbo].TableExists('AspNetUsers') = 1                  drop table AspNetUsers  
  IF [dbo].TableExists('TB_FILIAL_CONFIGURACAO') = 1       drop table TB_FILIAL_CONFIGURACAO  
  IF [dbo].TableExists('TB_PONTO_TERAPEUTA') = 1           drop table TB_PONTO_TERAPEUTA  
  IF [dbo].TableExists('TB_HORARIO_TERAPEUTA') = 1         drop table TB_HORARIO_TERAPEUTA  
  IF [dbo].TableExists('TB_TERAPEUTA_SERVICO') = 1         drop table TB_TERAPEUTA_SERVICO  
  IF [dbo].TableExists('TB_CARRINHO_ITEM') = 1             drop table TB_CARRINHO_ITEM  
  IF [dbo].TableExists('TB_CARRINHO') = 1                  drop table TB_CARRINHO  
  IF [dbo].TableExists('TB_EMAIL') = 1                     drop table TB_EMAIL  
  IF [dbo].TableExists('TB_LOG') = 1                       drop table TB_LOG  
  IF [dbo].TableExists('TB_CHAT_MENSAGEM') = 1             drop table TB_CHAT_MENSAGEM  
  IF [dbo].TableExists('TB_TAREFA') = 1                    drop table TB_TAREFA  
  IF [dbo].TableExists('TB_PROMOCAO_CLIENTE') = 1          drop table TB_PROMOCAO_CLIENTE  
  IF [dbo].TableExists('TB_PROMOCAO') = 1                  drop table TB_PROMOCAO  
  IF [dbo].TableExists('TB_AGENDAMENTO_ITEM_SERVICO') = 1  drop table TB_AGENDAMENTO_ITEM_SERVICO  
  IF [dbo].TableExists('TB_LANCAMENTO') = 1                drop table TB_LANCAMENTO  
  IF [dbo].TableExists('TB_FORNECEDOR') = 1                drop table TB_FORNECEDOR      
  IF [dbo].TableExists('TB_AGENDAMENTO') = 1               drop table TB_AGENDAMENTO  
  IF [dbo].TableExists('TB_TIPO_PAGAMENTO') = 1            drop table TB_TIPO_PAGAMENTO    
  IF [dbo].TableExists('TB_SERVICO') = 1                   drop table TB_SERVICO  
  IF [dbo].TableExists('TB_TERAPEUTA') = 1                 drop table TB_TERAPEUTA  
  IF [dbo].TableExists('TB_FILIAL') = 1                    drop table TB_FILIAL  
  IF [dbo].TableExists('TB_CLIENTE') = 1                   drop table TB_CLIENTE  
  IF [dbo].TableExists('TB_PERFIL_ACESSO') = 1             drop table TB_PERFIL_ACESSO    
  IF [dbo].TableExists('TB_EMPRESA') = 1                   drop table TB_EMPRESA
  IF [dbo].TableExists('TB_PLANO') = 1                     drop table TB_PLANO
  IF [dbo].TableExists('__MigrationHistory') = 1           drop table __MigrationHistory  
END  

