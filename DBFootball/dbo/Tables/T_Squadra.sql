CREATE TABLE [dbo].[T_Squadra] (
    [ID]             INT           IDENTITY (1, 1) NOT NULL,
    [IdCampionato]   INT           NULL,
    [Nome]           NVARCHAR (50) NULL,
    [AnnoFondazione] INT           NULL,
    CONSTRAINT [PK_T_Squadra] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_T_Squadra_T_Campionato] FOREIGN KEY ([IdCampionato]) REFERENCES [dbo].[T_Campionato] ([ID])
);

