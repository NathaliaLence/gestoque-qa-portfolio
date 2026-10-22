-- ============================================================
-- Gestoque · estrutura do banco (SQL Server / LocalDB)
-- Script original do projeto, com duas correções para que ele rode:
--   1. Faltava uma vírgula entre a chave primária e a restrição
--      de e-mail único na tabela [usuario].
--   2. Havia uma chave estrangeira para [usuario].[id_perfil],
--      coluna que não existe (o vínculo é feito por [perfil_usuario]).
-- ============================================================


CREATE TABLE [grupo_produto] (
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [nvarchar](50) NULL,
	[ativo] [bit] NULL,
	CONSTRAINT [PK_grupo_produto] PRIMARY KEY ([id])
)
GO
CREATE TABLE [unidade_medida] (
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](30) NOT NULL,
	[sigla] [varchar](3) NOT NULL,
	[ativo] [bit] NOT NULL,
	CONSTRAINT [PK_unidade_medida] PRIMARY KEY ([id])
)
GO
CREATE TABLE [usuario] (
	[id] [int] IDENTITY(1,1) NOT NULL,
	[login] [nvarchar](50) NOT NULL,
	[senha] [nvarchar](32) NOT NULL,
	[nome] [nvarchar](100) NOT NULL,
	[email] [nvarchar](150) NOT NULL,

	CONSTRAINT [PK_usuario] PRIMARY KEY ([id]),
	CONSTRAINT [unique_usuario_email] UNIQUE ([email])
)
GO
CREATE TABLE [marca_produto] (
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NOT NULL,
	[ativo] [bit] NOT NULL,
	CONSTRAINT [PK_marca_produto] PRIMARY KEY ([id])
)
GO
CREATE TABLE [local_armazenamento] (
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NOT NULL,
	[ativo] [bit] NOT NULL,
	CONSTRAINT [PK_local_armazenamento] PRIMARY KEY ([id])
)
GO
CREATE TABLE [perfil] (
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [nvarchar](20) NULL,
	[ativo] [bit] NULL,
	CONSTRAINT [PK_perfil] PRIMARY KEY ([id])
)
GO
CREATE TABLE [perfil_usuario] (
	[id_perfil] [int] NOT NULL,
	[id_usuario] [int] NOT NULL,
 CONSTRAINT [PK_perfil_usuario] PRIMARY KEY ([id_perfil], [id_usuario])
)
GO
CREATE TABLE [fornecedor] (
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](60) NOT NULL,
	[razao_social] [varchar](100) NULL,
	[num_documento] [varchar](20) NULL,
	[tipo] [int] NOT NULL,
	[telefone] [varchar](20) NOT NULL,
	[contato] [varchar](60) NOT NULL,
	[logradouro] [varchar](100) NOT NULL,
	[numero] [varchar](20) NOT NULL,
	[complemento] [varchar](100) NULL,
	[cep] [varchar](10) NULL,
	[estado] [varchar](60) NOT NULL,
	[cidade] [varchar](60) NOT NULL,
	[ativo] [bit] NOT NULL,
	CONSTRAINT [PK_fornecedor] PRIMARY KEY ([id])
)
GO
CREATE TABLE [produto] (
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [varchar](10) NOT NULL,
	[nome] [varchar](50) NOT NULL,
	[preco_custo] [decimal](7, 2) NOT NULL,
	[preco_venda] [decimal](7, 2) NOT NULL,
	[quant_estoque] [int] NOT NULL,
	[id_unidade_medida] [int] NOT NULL,
	[id_grupo] [int] NOT NULL,
	[id_marca] [int] NOT NULL,
	[id_fornecedor] [int] NOT NULL,
	[id_local_armazenamento] [int] NOT NULL,
	[ativo] [bit] NOT NULL,
 CONSTRAINT [PK_produto] PRIMARY KEY ([id])
)
GO
CREATE TABLE [entrada_produto] (
	[id] [int] IDENTITY(1,1) NOT NULL,
	[numero] [varchar](10) NOT NULL,
	[data] [datetime] NOT NULL,
	[id_produto] [int] NOT NULL,
	[quant] [int] NOT NULL,
 CONSTRAINT [PK_entrada_produto] PRIMARY KEY ([id])
)
GO
CREATE TABLE [saida_produto] (
	[id] [int] IDENTITY(1,1) NOT NULL,
	[numero] [varchar](10) NOT NULL,
	[data] [datetime] NOT NULL,
	[id_produto] [int] NOT NULL,
	[quant] [int] NOT NULL,
 CONSTRAINT [PK_saida_produto] PRIMARY KEY ([id]) 
)
GO
CREATE TABLE [inventario_estoque] (
	[id] [int] IDENTITY(1,1) NOT NULL,
	[data] [datetime] NOT NULL,
	[id_produto] [int] NOT NULL,
	[quant_estoque] [int] NOT NULL,
	[quant_inventario] [int] NOT NULL,
	[motivo] [varchar](100),
 CONSTRAINT [PK_inventario_estoque] PRIMARY KEY ([id]) 
)
GO

ALTER TABLE [perfil_usuario] WITH CHECK ADD FOREIGN KEY([id_perfil]) REFERENCES [perfil] ([id])
GO
ALTER TABLE [perfil_usuario] WITH CHECK ADD FOREIGN KEY([id_usuario]) REFERENCES [usuario] ([id])
GO
ALTER TABLE [produto] WITH CHECK ADD FOREIGN KEY([id_fornecedor]) REFERENCES [fornecedor] ([id])
GO
ALTER TABLE [produto] WITH CHECK ADD FOREIGN KEY([id_grupo]) REFERENCES [grupo_produto] ([id])
GO
ALTER TABLE [produto] WITH CHECK ADD FOREIGN KEY([id_local_armazenamento]) REFERENCES [local_armazenamento] ([id])
GO
ALTER TABLE [produto] WITH CHECK ADD FOREIGN KEY([id_marca]) REFERENCES [marca_produto] ([id])
GO
ALTER TABLE [produto] WITH CHECK ADD FOREIGN KEY([id_unidade_medida]) REFERENCES [unidade_medida] ([id])
GO
CREATE SEQUENCE [SEC_entrada_produto] AS [int] START WITH 1 INCREMENT BY 1
GO
CREATE SEQUENCE [SEC_saida_produto] AS [int] START WITH 1 INCREMENT BY 1
GO
ALTER TABLE [inventario_estoque] WITH CHECK ADD FOREIGN KEY([id_produto]) REFERENCES [produto] ([id])
GO
