--Rhythm's gonna get you--

--For this project, we will model and create a database. We are starting a record label company, and we need a place to store our bands, albums, and eventually songs. 
--You are creating a console app that stores our information in a database.

--Objectives:
--Practice working with SQL
--Practice working with ORMs (EF Core)
--Create a console that allows a user to store and manage the company's bands, albums, and (eventually) songs.

--Top Tips:
--Although in reality an album could be done by more than one band, our system will just have an album involving one band. 
--That is, an album belongs to one band.

--Explorer Mode:

--Create a database that stores Albums, Bands, and Songs. 
--They should have the following properties, use your best judgment for types. 
--Also include the foreign keys when making your CREATE TABLE statements. 
--HINT: You might have to create your tables in a specific order

CREATE TABLE "Albums" (
  "Id"  SERIAL PRIMARY KEY,
  "Title"  TEXT,
  "IsExplicit"  BOOLEAN,
  "ReleaseDate"  DATE,
  "BandId" INTEGER NULL REFERENCES "Bands" ("Id")
);

CREATE TABLE "Bands" (
  "Id"  SERIAL PRIMARY KEY,
  "Name"   TEXT,
  "CountryOfOrigin"  TEXT,
  "NumberOfMembers"  INT,
  "Website"  TEXT,
  "Style"   TEXT,
  "IsSigned"   BOOLEAN,
  "ContactName"   TEXT,
  "ContactPhoneNumber"  TEXT  
);

CREATE TABLE "Songs" (
  "Id"  SERIAL PRIMARY KEY,
  "Title"  TEXT,
  "Duration"  TIME,
  "TrackNumber"  INT,
  "AlbumId" INTEGER NULL REFERENCES "Albums" ("Id")
  );

-------------------------------------------------------------
-- CREATE TABLE "BandMembers" (
--   "Id"       SERIAL PRIMARY KEY,
--   "BandId"  INTEGER NULL REFERENCES "Bands" ("Id"),
--   "MusicianId"  INTEGER NULL REFERENCES "Musicians" ("Id")
--   "WhenJoined"  DATE, -- Interesting, because number of members should have to update 
--   "WhenLeft"  DATE    -- When a value is assigned here, look into
-- );
-------------------------------------------------------------
-- CREATE TABLE "Musicians" (
--   "Id"  SERIAL PRIMARY KEY,
--   "Name"  TEXT,
--   "Age"  TIME,
--   "MusicianPhoneNumber"  TEXT
--   );
-------------------------------------------------------------
  

--A Band has many Albums and an Album belongs to one Band. 
--An Album has many Songs and a Song belongs to one Album.

--Create a menu system that shows the following options to the user until they choose to quit your program =>

--Add a new band
--View all the bands
--Add an album for a band
--Add a song to an album
--Let a band go (update isSigned to false)
--Resign a band (update isSigned to true)
--Prompt for a band name and view all their albums
--View all albums ordered by ReleaseDate
--View all bands that are signed
--View all bands that are not signed
--Quit the program

--Adventure Mode:

--Track the individual members of a band. Since musicians play in several different groups, 
--create a new table called Musicians and make it a many to many relationships with a Band.

--Add the following menu options:

--View albums in a genre
--View all members of a band

--Epic Mode:

--Add another entity that you feel would benefit the system. 
--Update your ERD, tables, and user interface to support it.
