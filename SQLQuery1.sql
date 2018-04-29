use AnimeDatabase

drop table Anime
drop table Description


Create TABLE Description 
(
	DescriptionID int identity(1, 1) not null
	constraint PK_DescriptionID primary key clustered,
	description varchar (100) not null
)


Create table Anime 
(
 AnimeID int identity(1, 1) not null
	constraint PK_AnimeID primary key clustered,
 AnimeName varchar(100) not null,
 TotalNumberOfEpisodes int null,
 CurrentEpisode int not null
	constraint DF_CurrentEpisode default 1,
 NumberOfSeasons int null 
  constraint DF_NumberOfSeasons default 1,
 AnimeDescriptionID int not null
	constraint FK_AnimeDescriptionID references Description(DescriptionID)
)


SELECT AnimeID, AnimeName, TotalNumberOfEpisodes, CurrentEpisode, Description.DescriptionID descID, description
	FROM Anime inner join Description 
	on Anime.AnimeDescriptionID = Description.DescriptionID
	ORDER By AnimeName

	go
create procedure SearchAnimeByRatingAndName (@RatingID int = null, @Name varchar(100) = null) as
	if @RatingID is null or @Name is Null
		raiserror('Missing Parameter(s)', 16, 1)
	else
		begin
			if @RatingID = 0
				select AnimeID, AnimeName, TotalNumberOfEpisodes, 
						CurrentEpisode, NumberOfSeasons, AnimeDescriptionID
					From Anime
					Where AnimeName like '%'+@Name+'%'
			else
				select AnimeID, AnimeName, TotalNumberOfEpisodes, 
						CurrentEpisode, NumberOfSeasons, AnimeDescriptionID
					From Anime
					Where AnimeName like '%'+@Name+'%' and AnimeDescriptionID = @RatingID;
			if @@ROWCOUNT = 0 
				raiserror('No Anime Matching Description', 16, 1)
		end
go
	Create Procedure FindAllRatings as 
		select DescriptionID, description 
			From Description
		if @@ROWCOUNT = 0 
				raiserror('No Descriptions in existance', 16, 1)
go