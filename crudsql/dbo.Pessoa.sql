CREATE TABLE [dbo].[Pessoa] (
    [nome]     NCHAR (80)  NULL,
    [endereco] NCHAR (120) NULL,
    [celular]  NCHAR (15)  NULL,
    [email]    NCHAR (80)  NULL,
    PRIMARY KEY CLUSTERED ([nome] ASC)
);

