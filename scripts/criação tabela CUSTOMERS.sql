CREATE TABLE [Customers] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](150) NULL,
	[Sobrenome] [varchar](255) NULL,
	[Nascimento] [date] NULL,
	[Limite] [float] NULL,
	[Ativo] [bit] NULL
)

ALTER TABLE [Customers] ADD CONSTRAINT Pk_Customer PRIMARY KEY(Id)


