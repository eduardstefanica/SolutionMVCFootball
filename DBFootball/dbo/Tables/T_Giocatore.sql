CREATE TABLE [dbo].[T_Giocatore] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Nome]        NVARCHAR (50) NULL,
    [Eta]         INT           NULL,
    [IdSquadra]   INT           NULL,
    [Nazionalita] NVARCHAR (50) NULL,
    CONSTRAINT [PK_T_Giocatore] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_T_Giocatore_T_Squadra] FOREIGN KEY ([IdSquadra]) REFERENCES [dbo].[T_Squadra] ([ID])
);

