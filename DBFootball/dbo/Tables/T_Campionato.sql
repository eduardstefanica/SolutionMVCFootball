CREATE TABLE [dbo].[T_Campionato] (
    [ID]              INT           IDENTITY (1, 1) NOT NULL,
    [Nome]            NVARCHAR (50) NULL,
    [Paese]           NVARCHAR (50) NULL,
    [NomeFederazione] NVARCHAR (50) NULL,
    [isTopRanking]    BIT           NULL,
    CONSTRAINT [PK_T_Campionato] PRIMARY KEY CLUSTERED ([ID] ASC)
);

