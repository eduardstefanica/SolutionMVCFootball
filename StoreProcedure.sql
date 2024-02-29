-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetPlayersInLeague
    @LeagueName NVARCHAR(50)
AS
BEGIN
    SELECT 
        T_Giocatore.Nome AS NomeGiocatore,
        T_Giocatore.Eta AS EtaGiocatore,
        T_Squadra.Nome AS NomeSquadra,
        T_Campionato.Nome AS NomeCampionato
    FROM 
        T_Giocatore
    INNER JOIN 
        T_Squadra ON T_Giocatore.IdSquadra = T_Squadra.ID
    INNER JOIN 
        T_Campionato ON T_Squadra.IdCampionato = T_Campionato.ID
    WHERE
        T_Campionato.Nome = @LeagueName;
END;
GO
EXEC GetPlayersInLeague 'La Liga';