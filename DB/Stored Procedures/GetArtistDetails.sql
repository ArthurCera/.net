CREATE PROC GetArtistDetails
	@artistID int
AS
BEGIN
	select * from Artist where @artistID = artistID;
	select * from Album where @artistID = artistID;
	select * from Song where @artistID = artistID;
END
