-------------------------------------------------------------
CREATE TABLE "Bands" (
  "Id"  SERIAL PRIMARY KEY,
  "Name"   TEXT,
  "CountryOfOrigin"  TEXT,
  "Website"  TEXT,
  "Style"   TEXT,
  "IsSigned"   BOOLEAN,
  "ContactName"   TEXT,
  "ContactPhoneNumber"  TEXT  
);
-------------------------------------------------------------
-------------------------------------------------------------
CREATE TABLE "Albums" (
  "Id"  SERIAL PRIMARY KEY,
  "Title"  TEXT,
  "Genre"  TEXT,
  "IsExplicit"  BOOLEAN,
  "ReleaseDate"  DATE,
  "BandId" INTEGER NULL REFERENCES "Bands" ("Id")
);
-------------------------------------------------------------
CREATE TABLE "Songs" (
  "Id"  SERIAL PRIMARY KEY,
  "Title"  TEXT,
  "Duration"  TIME,
  "TrackNumber"  INT,
  "AlbumId" INTEGER NULL REFERENCES "Albums" ("Id")
);
-------------------------------------------------------------
-------------------------------------------------------------
CREATE TABLE "Concerts" (
  "Id"       SERIAL PRIMARY KEY,
  "WhenPerformed"  DATE,
  "WherePerformed"  TEXT,
  "NumberOfPeopleAtConcert"  INT,
  "BandId"  INTEGER NULL REFERENCES "Bands" ("Id")
);
-------------------------------------------------------------
-------------------------------------------------------------
CREATE TABLE "Musicians" (
  "Id"  SERIAL PRIMARY KEY,
  "Name"  TEXT,
  "Age"  INT,
  "MusicianPhoneNumber"  TEXT,
  "Instrument"   TEXT
);
-------------------------------------------------------------
CREATE TABLE "BandMembers" (
  "Id"       SERIAL PRIMARY KEY,
  "BandId"  INTEGER NULL REFERENCES "Bands" ("Id"),
  "MusicianId"  INTEGER NULL REFERENCES "Musicians" ("Id"),
  "CurrentMember" BOOLEAN
);
-------------------------------------------------------------
-------------------------------------------------------------
INSERT INTO "Bands" ("Name", "CountryOfOrigin", "NumberOfMembers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('', '',INT, '', '',BOOLEAN , '', '', );
INSERT INTO "Bands" ("Name", "CountryOfOrigin", "NumberOfMembers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('', '',INT, '', '',BOOLEAN , '', '', );
INSERT INTO "Bands" ("Name", "CountryOfOrigin", "NumberOfMembers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('', '',INT, '', '',BOOLEAN , '', '', );
INSERT INTO "Bands" ("Name", "CountryOfOrigin", "NumberOfMembers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('', '',INT, '', '',BOOLEAN , '', '', );
INSERT INTO "Bands" ("Name", "CountryOfOrigin", "NumberOfMembers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('', '',INT, '', '',BOOLEAN , '', '', );
INSERT INTO "Bands" ("Name", "CountryOfOrigin", "NumberOfMembers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('', '',INT, '', '',BOOLEAN , '', '', );
INSERT INTO "Bands" ("Name", "CountryOfOrigin", "NumberOfMembers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('', '',INT, '', '',BOOLEAN , '', '', );
INSERT INTO "Bands" ("Name", "CountryOfOrigin", "NumberOfMembers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('', '',INT, '', '',BOOLEAN , '', '', );
INSERT INTO "Bands" ("Name", "CountryOfOrigin", "NumberOfMembers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('', '',INT, '', '',BOOLEAN , '', '', );
-------------------------------------------------------------
-------------------------------------------------------------
INSERT INTO "Albums" ("Title", "Genre", "IsExplicit", "ReleaseDate", "BandId")
VALUES ('', '', BOOLEAN, DATE, INT);
INSERT INTO "Albums" ("Title", "Genre", "IsExplicit", "ReleaseDate", "BandId")
VALUES ('', '', BOOLEAN, DATE, INT);
INSERT INTO "Albums" ("Title", "Genre", "IsExplicit", "ReleaseDate", "BandId")
VALUES ('', '', BOOLEAN, DATE, INT);
INSERT INTO "Albums" ("Title", "Genre", "IsExplicit", "ReleaseDate", "BandId")
VALUES ('', '', BOOLEAN, DATE, INT);
INSERT INTO "Albums" ("Title", "Genre", "IsExplicit", "ReleaseDate", "BandId")
VALUES ('', '', BOOLEAN, DATE, INT);
INSERT INTO "Albums" ("Title", "Genre", "IsExplicit", "ReleaseDate", "BandId")
VALUES ('', '', BOOLEAN, DATE, INT);
INSERT INTO "Albums" ("Title", "Genre", "IsExplicit", "ReleaseDate", "BandId")
VALUES ('', '', BOOLEAN, DATE, INT);
INSERT INTO "Albums" ("Title", "Genre", "IsExplicit", "ReleaseDate", "BandId")
VALUES ('', '', BOOLEAN, DATE, INT);
INSERT INTO "Albums" ("Title", "Genre", "IsExplicit", "ReleaseDate", "BandId")
VALUES ('', '', BOOLEAN, DATE, INT);
INSERT INTO "Albums" ("Title", "Genre", "IsExplicit", "ReleaseDate", "BandId")
VALUES ('', '', BOOLEAN, DATE, INT);
INSERT INTO "Albums" ("Title", "Genre", "IsExplicit", "ReleaseDate", "BandId")
VALUES ('', '', BOOLEAN, DATE, INT);
-------------------------------------------------------------
-------------------------------------------------------------
INSERT INTO "Songs" ("Title", "Duration", "TrackNumber", "AlbumId")
VALUES ('', TIME, INT, INT);
INSERT INTO "Songs" ("Title", "Duration", "TrackNumber", "AlbumId")
VALUES ('', TIME, INT, INT);
INSERT INTO "Songs" ("Title", "Duration", "TrackNumber", "AlbumId")
VALUES ('', TIME, INT, INT);
INSERT INTO "Songs" ("Title", "Duration", "TrackNumber", "AlbumId")
VALUES ('', TIME, INT, INT);
INSERT INTO "Songs" ("Title", "Duration", "TrackNumber", "AlbumId")
VALUES ('', TIME, INT, INT);
INSERT INTO "Songs" ("Title", "Duration", "TrackNumber", "AlbumId")
VALUES ('', TIME, INT, INT);
INSERT INTO "Songs" ("Title", "Duration", "TrackNumber", "AlbumId")
VALUES ('', TIME, INT, INT);
INSERT INTO "Songs" ("Title", "Duration", "TrackNumber", "AlbumId")
VALUES ('', TIME, INT, INT);
INSERT INTO "Songs" ("Title", "Duration", "TrackNumber", "AlbumId")
VALUES ('', TIME, INT, INT);
INSERT INTO "Songs" ("Title", "Duration", "TrackNumber", "AlbumId")
VALUES ('', TIME, INT, INT);
INSERT INTO "Songs" ("Title", "Duration", "TrackNumber", "AlbumId")
VALUES ('', TIME, INT, INT);
-------------------------------------------------------------
-------------------------------------------------------------
INSERT INTO "Concerts" ("WhenPerformed", "WherePerformed", "NumberOfPeopleAtConcert", "BandId")
VALUES (DATE, '', INT, INT);
INSERT INTO "Concerts" ("WhenPerformed", "WherePerformed", "NumberOfPeopleAtConcert", "BandId")
VALUES (DATE, '', INT, INT);
INSERT INTO "Concerts" ("WhenPerformed", "WherePerformed", "NumberOfPeopleAtConcert", "BandId")
VALUES (DATE, '', INT, INT);
INSERT INTO "Concerts" ("WhenPerformed", "WherePerformed", "NumberOfPeopleAtConcert", "BandId")
VALUES (DATE, '', INT, INT);
INSERT INTO "Concerts" ("WhenPerformed", "WherePerformed", "NumberOfPeopleAtConcert", "BandId")
VALUES (DATE, '', INT, INT);
INSERT INTO "Concerts" ("WhenPerformed", "WherePerformed", "NumberOfPeopleAtConcert", "BandId")
VALUES (DATE, '', INT, INT);
INSERT INTO "Concerts" ("WhenPerformed", "WherePerformed", "NumberOfPeopleAtConcert", "BandId")
VALUES (DATE, '', INT, INT);
INSERT INTO "Concerts" ("WhenPerformed", "WherePerformed", "NumberOfPeopleAtConcert", "BandId")
VALUES (DATE, '', INT, INT);
INSERT INTO "Concerts" ("WhenPerformed", "WherePerformed", "NumberOfPeopleAtConcert", "BandId")
VALUES (DATE, '', INT, INT);
INSERT INTO "Concerts" ("WhenPerformed", "WherePerformed", "NumberOfPeopleAtConcert", "BandId")
VALUES (DATE, '', INT, INT);
-------------------------------------------------------------
-------------------------------------------------------------
INSERT INTO "Musicians" ("Name", "Age", "MusicianPhoneNumber")
VALUES ('', INT, TEXT);
INSERT INTO "Musicians" ("Name", "Age", "MusicianPhoneNumber")
VALUES ('', INT, TEXT);
INSERT INTO "Musicians" ("Name", "Age", "MusicianPhoneNumber")
VALUES ('', INT, TEXT);
INSERT INTO "Musicians" ("Name", "Age", "MusicianPhoneNumber")
VALUES ('', INT, TEXT);
INSERT INTO "Musicians" ("Name", "Age", "MusicianPhoneNumber")
VALUES ('', INT, TEXT);
INSERT INTO "Musicians" ("Name", "Age", "MusicianPhoneNumber")
VALUES ('', INT, TEXT);
INSERT INTO "Musicians" ("Name", "Age", "MusicianPhoneNumber")
VALUES ('', INT, TEXT);
INSERT INTO "Musicians" ("Name", "Age", "MusicianPhoneNumber")
VALUES ('', INT, TEXT);
INSERT INTO "Musicians" ("Name", "Age", "MusicianPhoneNumber")
VALUES ('', INT, TEXT);
INSERT INTO "Musicians" ("Name", "Age", "MusicianPhoneNumber")
VALUES ('', INT, TEXT);
INSERT INTO "Musicians" ("Name", "Age", "MusicianPhoneNumber")
VALUES ('', INT, TEXT);
INSERT INTO "Musicians" ("Name", "Age", "MusicianPhoneNumber")
VALUES ('', INT, TEXT);
-------------------------------------------------------------
-------------------------------------------------------------
INSERT INTO "BandMembers" ("BandId", "MusicianId")
VALUES (INT, INT);
INSERT INTO "BandMembers" ("BandId", "MusicianId")
VALUES (INT, INT);
INSERT INTO "BandMembers" ("BandId", "MusicianId")
VALUES (INT, INT);
INSERT INTO "BandMembers" ("BandId", "MusicianId")
VALUES (INT, INT);
INSERT INTO "BandMembers" ("BandId", "MusicianId")
VALUES (INT, INT);
INSERT INTO "BandMembers" ("BandId", "MusicianId")
VALUES (INT, INT);
INSERT INTO "BandMembers" ("BandId", "MusicianId")
VALUES (INT, INT);
INSERT INTO "BandMembers" ("BandId", "MusicianId")
VALUES (INT, INT);
INSERT INTO "BandMembers" ("BandId", "MusicianId")
VALUES (INT, INT);
INSERT INTO "BandMembers" ("BandId", "MusicianId")
VALUES (INT, INT);
INSERT INTO "BandMembers" ("BandId", "MusicianId")
VALUES (INT, INT);
INSERT INTO "BandMembers" ("BandId", "MusicianId")
VALUES (INT, INT);
INSERT INTO "BandMembers" ("BandId", "MusicianId")
VALUES (INT, INT);
INSERT INTO "BandMembers" ("BandId", "MusicianId")
VALUES (INT, INT);
-------------------------------------------------------------
-------------------------------------------------------------