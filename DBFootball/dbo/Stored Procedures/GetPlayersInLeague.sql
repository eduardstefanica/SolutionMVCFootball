-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetPlayersInLeague]
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
EXEC GetPlayersInLeague 'La Liga';