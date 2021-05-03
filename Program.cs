using System;
using System.Globalization;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
/*
PEDAC:
Problem => 

--Model and create a database for a record label company that stores our bands, albums, and songs. 
--Create a console app that allows a user to store and manage the company's bands, albums, and songs.

--Create a menu system that shows the following options to the user 
--until they choose to quit your program =>
--(1)Add a new band- 
--(2)View all the bands- could make this a sorted list too
--(3)Add an album for a band-
--(4)Add a song to an album-
--(5)Let a band go (update isSigned to false)-
--(6)Resign a band (update isSigned to true)-
--(7)Prompt for a band name and view all their albums-
--(8)View all albums ordered by ReleaseDate-
--(9)View all bands that are signed-
--(10)View all bands that are not signed-

--(11)View albums in a genre----------
--(12)View all members of a band------------------
--(13)Add musician to band-----------------
--(14)Remove musician from band-------------
--(15)View all bands that a musician is a member of------------
--()View a sorted list of musicians
--(16)View band's past concerts--------------
--(17)View band's upcoming concerts--------------
--(18)View number of fans at a concert----------------
--(Q)uit



Examples => 


Data Structures => 


Algorithm =>


Code =>


--------------------------------
Database-first Approach: 
--Build database with SQL => Then, build and connect program to the database (ORMs (EF Core) && C#)

Top Tips:
--A Band has many Albums and an Album belongs to one Band. 
--An Album has many Songs and a Song belongs to one Album.
--Also include the foreign keys when making your CREATE TABLE statements. 
--You might have to create your tables in a specific order


TO-DO:
- Fill out description functions for classes 
  and any other internal functions that may be needed
- Start writing functions for menu options using Linq stmts to manipulate SQL database
- Flesh out some examples
- 
*/
namespace RhythmsGonnaGetYou
{
    class Band
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }
        // public int NumberOfMembers { get; set; } ==>> handled by BandMembers List
        public string Website { get; set; }
        public string Style { get; set; }
        public bool IsSigned { get; set; }
        public string ContactName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public List<Album> Albums { get; set; }
        public List<Concert> Concerts { get; set; }
        public List<BandMember> BandMembers { get; set; }
        public void Description()
        {
            Console.WriteLine($"Band Name: {Name}, Country of Origin: {CountryOfOrigin}");
            Console.WriteLine($"Website: {Website}, Style: {Style}, Signed to Record Label: {IsSigned}");
            Console.WriteLine($"Contact Name: {ContactName}, Contact Phone Number: {ContactPhoneNumber}");
        }
    }
    class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public bool IsExplicit { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int BandId { get; set; }
        public Band Band { get; set; }
        public List<Song> Songs { get; set; }
        public void Description()
        {
            Console.WriteLine($"Album Title: {Title}, Genre: {Genre}, Explicit: {IsExplicit.ToString().ToUpper()}, Release Date: {ReleaseDate}");
        }
    }
    class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public int TrackNumber { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public void Description()
        {
            Console.WriteLine($"Song Title: {Title}, Track Number: {TrackNumber}, Length of Song: {Duration}");
        }
    }
    class Concert
    {
        public int Id { get; set; }
        public DateTime WhenPerformed { get; set; }
        public string WherePerformed { get; set; }
        public int NumberOfPeopleAtConcert { get; set; }
        public int BandId { get; set; }
        public Band Band { get; set; }
        public void Description()
        {
            Console.WriteLine($"Performed At: {WherePerformed}, Performed For: {NumberOfPeopleAtConcert} people, Date of Performance: {WhenPerformed}");
        }
    }
    class Musician
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string MusicianPhoneNumber { get; set; }
        public string Instrument { get; set; }
        public List<BandMember> BandMembers { get; set; }
        public void Description()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}, Phone Number: {MusicianPhoneNumber}, Instrument: {Instrument}");
        }
        public void ListCurrentBands()
        {
            Console.WriteLine();
            Console.WriteLine("Listing Musician's Current Bands:");
            Console.WriteLine();
            var count = 1;
            foreach (var bandMember in BandMembers.Where(bandMember => bandMember.CurrentMember == true))
            {
                Console.Write($"{count}: ");
                Console.WriteLine($"Band Name: {bandMember.Band.Name}");
                count += 1;
            }
            Console.WriteLine();
        }
        public void ListPastBands()
        {
            Console.WriteLine();
            Console.WriteLine("Listing Musician's Former Bands:");
            Console.WriteLine();
            var count = 1;
            foreach (var bandMember in BandMembers.Where(bandMember => bandMember.CurrentMember == false))
            {
                Console.Write($"{count}: ");
                Console.WriteLine($"Band Name: {bandMember.Band.Name}");
                count += 1;
            }
            Console.WriteLine();
        }
    }

    class BandMember
    {
        public int Id { get; set; }
        public int BandId { get; set; }
        public int MusicianId { get; set; }
        public bool CurrentMember { get; set; }
        public Band Band { get; set; }
        public Musician Musician { get; set; }
        public void AddMusicianBackToBand()
        {
            Console.WriteLine("Reinstating musician in band");
            CurrentMember = true;
        }
        public void RemoveMusicianFromBand()
        {
            Console.WriteLine("Removing musician from band");
            CurrentMember = false;
        }
    }
    class RecordLabelContext : DbContext
    {
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<BandMember> BandMembers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            // optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseNpgsql("server=localhost;database=RecordLabelDatabase"); // Connects to Db
        }
    }
    // class LabelDatabase
    // {




    //     }
    // }

    class Program
    {
        // static Band SelectABand() >>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Why didn't Band object it returned work in other functions??
        // {
        //     var context = new RecordLabelContext();
        //     Console.Clear();
        //     var selectingBand = true;
        //     var selectedBand = new Band();
        //     while (selectingBand)
        //     {
        //         Console.Write("Please select a band (Type the band's name then press Enter): ");
        //         var bandSelection = Console.ReadLine();
        //         if (context.Bands.FirstOrDefault(band => band.Name == bandSelection) != null)
        //         {
        //             selectedBand = context.Bands.FirstOrDefault(band => band.Name == bandSelection);
        //             selectingBand = false;
        //         }
        //         else
        //         {
        //             Console.WriteLine();
        //             Console.WriteLine("There is no band by that name in the database");
        //             Console.WriteLine();
        //             Console.WriteLine("Please try again");
        //         }
        //     }
        //     return selectedBand;
        // }
        static Musician SelectAMusician()
        {
            var context = new RecordLabelContext();
            Console.Clear();
            var selectingMusician = true;
            var selectingCounter = 0;
            var selectedMusician = new Musician();
            while (selectingMusician)
            {
                Console.Write("Please select a musician (Type the musician's name then press Enter): ");
                var musicianSelection = Console.ReadLine();
                if (context.Musicians.FirstOrDefault(musician => musician.Name == musicianSelection) != null)
                {
                    selectedMusician = context.Musicians.FirstOrDefault(musician => musician.Name == musicianSelection);
                    selectingMusician = false;
                }
                else if (selectingCounter > 3)
                {
                    Console.WriteLine();
                    Console.WriteLine("There is no musician by that name in the database");
                    Console.WriteLine();
                    Console.WriteLine("Quitting to menu");
                    selectingMusician = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("There is no band by that name in the database");
                    Console.WriteLine();
                    Console.WriteLine("Please try again");
                    selectingCounter += 1;
                }
            }
            return selectedMusician;
        }
        // static Album SelectAnAlbum()
        // {
        //     var context = new RecordLabelContext();
        //     Console.Clear();
        //     var selectingAlbum = true;
        //     var selectingCounter = 0;
        //     var selectedAlbum = new Album();
        //     while (selectingAlbum)
        //     {
        //         Console.Write("Please select an album (Type the album's title then press Enter): ");
        //         var albumSelection = Console.ReadLine();
        //         if (context.Albums.FirstOrDefault(album => album.Title == albumSelection) != null)
        //         {
        //             selectedAlbum = context.Albums.FirstOrDefault(album => album.Title == albumSelection);
        //             selectingAlbum = false;
        //         }
        //         else if (selectingCounter > 3)
        //         {
        //             Console.WriteLine();
        //             Console.WriteLine("There is no album by that name in the database");
        //             Console.WriteLine();
        //             Console.WriteLine("Quitting to menu");
        //             selectingAlbum = false;
        //         }
        //         else
        //         {
        //             Console.WriteLine();
        //             Console.WriteLine("There is no album by that name in the database");
        //             Console.WriteLine();
        //             Console.WriteLine("Please try again");
        //             selectingCounter += 1;
        //         }
        //     }
        //     return selectedAlbum;
        // }

        // static BandMember SelectABandMember(DbContext context)
        // {
        //     using (var context = new RecordLabelContext())
        //     {
        //         Console.Clear();
        //         var selectingBandMember = true;
        //         var selectingCounter = 0;
        //         var selectedBand = new Band();
        //         while (selectingBand)
        //         {
        //             Console.Write("Please select a band (Type the band's name then press Enter): ");
        //             var bandSelection = Console.ReadLine();
        //             if (context.Bands.FirstOrDefault(band => band.Name == bandSelection) != null)
        //             {
        //                 selectedBand = context.Bands.FirstOrDefault(band => band.Name == bandSelection);
        //                 selectingBand = false;
        //             }
        //             else if (selectingCounter > 3)
        //             {
        //                 Console.WriteLine();
        //                 Console.WriteLine("There is no band by that name in the database");
        //                 Console.WriteLine();
        //                 Console.WriteLine("Quitting to menu");
        //                 selectingBand = false;
        //             }
        //             else
        //             {
        //                 Console.WriteLine();
        //                 Console.WriteLine("There is no band by that name in the database");
        //                 Console.WriteLine();
        //                 Console.WriteLine("Please try again");
        //                 selectingCounter += 1;
        //             }
        //         }
        //         return selectedBand;
        //     }

        // static void ViewAllSignedBands(DbContext context)
        // {

        // }
        // static void ViewAllUnsignedBands(DbContext context)
        // {

        // }
        static void ViewAlbumsSongs()
        {
            var context = new RecordLabelContext();
            Console.Clear();
            var selectingAlbum = true;
            var selectedAlbum = new Album();
            while (selectingAlbum)
            {
                Console.Write("Please select an album (Type the album's title then press Enter): ");
                var albumSelection = Console.ReadLine();
                if (context.Albums.FirstOrDefault(album => album.Title == albumSelection) != null)
                {
                    selectedAlbum = context.Albums.FirstOrDefault(album => album.Title == albumSelection);
                    selectingAlbum = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("There is no album by that name in the database");
                    Console.WriteLine();
                    Console.WriteLine("Please try again");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Listing Album's Songs:");
            Console.WriteLine();
            var count = 1;
            foreach (var song in context.Songs.Include(song => song.Album).Where(song => song.Album.Id == selectedAlbum.Id))
            {
                Console.Write($"{count}: ");
                song.Description();
                Console.WriteLine();
                count += 1;
            }
        }
        static void ViewAllBands()
        {
            var context = new RecordLabelContext();
            Console.Clear();
            Console.WriteLine();
            foreach (var band in context.Bands)
            {
                band.Description();
                Console.WriteLine();
            }
        }
        static void ViewAllBandsThatAreSigned()
        {
            var context = new RecordLabelContext();
            Console.Clear();
            foreach (var band in context.Bands.Where(band => band.IsSigned == true))
            {
                band.Description();
            }
        }
        static void ViewAllUnsignedBands()
        {
            var context = new RecordLabelContext();
            Console.Clear();
            foreach (var band in context.Bands.Where(band => band.IsSigned == false))
            {
                band.Description();
            }
        }

        static void ViewAlbumsInAGenre()
        {
            var context = new RecordLabelContext();
            Console.Clear();
            Console.WriteLine("Album genre: ");
            var genreSelection = Console.ReadLine();
            var counterForGenreDisplay = 1;
            foreach (var album in context.Albums.Where(album => album.Genre == genreSelection))
            {
                Console.Write($"{counterForGenreDisplay}: ");
                album.Description();
                counterForGenreDisplay += 1;
            }
        }
        static void ViewAlbumsOrderedByReleaseDate()
        {
            var context = new RecordLabelContext();
            Console.Clear();
            var counterForOrderedDisplay = 1;
            Console.WriteLine();
            foreach (var album in context.Albums.OrderBy(album => album.ReleaseDate))
            {
                Console.Write($"{counterForOrderedDisplay}: ");
                album.Description();
                counterForOrderedDisplay += 1;
            }
        }
        static void ViewAllBandsAlbums()
        {
            var context = new RecordLabelContext();
            Console.Clear();
            var selectingBand = true;
            var bandSelectionDisplayAlbums = new Band();
            while (selectingBand)
            {
                Console.Write("Please select a band (Type the band's name then press Enter): ");
                var bandSelection = Console.ReadLine();
                if (context.Bands.FirstOrDefault(band => band.Name == bandSelection) != null)
                {
                    bandSelectionDisplayAlbums = context.Bands.FirstOrDefault(band => band.Name == bandSelection);
                    selectingBand = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("There is no band by that name in the database");
                    Console.WriteLine();
                    Console.WriteLine("Please try again");
                }
            }
            foreach (var album in context.Albums.Include(album => album.Band).Where(album => album.Band == bandSelectionDisplayAlbums))
            {
                Console.WriteLine();
                album.Description();
            }
        }
        static void ViewConcerts()
        {
            var context = new RecordLabelContext();
            Console.Clear();
            var bandSelectionConcerts = new Band();
            var selectingBandConcerts = true;
            while (selectingBandConcerts)
            {
                Console.Write("Please select a band to assign the album to (Type the band's name then press Enter): ");
                var bandSelection = Console.ReadLine();
                if (context.Bands.FirstOrDefault(band => band.Name == bandSelection) != null)
                {
                    bandSelectionConcerts = context.Bands.FirstOrDefault(band => band.Name == bandSelection);
                    selectingBandConcerts = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("There is no band by that name in the database");
                    Console.WriteLine();
                    Console.WriteLine("Please try again");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Listing Band's Concerts:");
            Console.WriteLine();
            var count = 1;
            foreach (var concert in context.Concerts.Include(concert => concert.Band).Where(concert => concert.Band == bandSelectionConcerts))
            {
                Console.Write($"{count}: ");
                concert.Description();
                count += 1;
            }
            Console.WriteLine();
        }
        static void ViewAllMusicians()
        {
            var context = new RecordLabelContext();
            Console.Clear();
            Console.WriteLine();
            foreach (var musician in context.Musicians.OrderBy(musician => musician.Name))
            {
                musician.Description();
                Console.WriteLine();
            }
        }
        static void CreateBand()
        {
            Console.Clear();
            var context = new RecordLabelContext();
            Band newBand = new Band();
            Console.Write("Type the new band's name and press Enter: ");
            newBand.Name = Console.ReadLine();
            Console.Write("Type the new band's country of origin and press Enter: ");
            newBand.CountryOfOrigin = Console.ReadLine();
            Console.Write("Type the URL of the new band's website and press Enter: ");
            newBand.Website = Console.ReadLine();
            Console.Write("Type the new band's music style and press Enter: ");
            newBand.Style = Console.ReadLine();
            Console.Write("If the band is signed, type true and press Enter. If the band isn't signed, type false and press Enter: ");
            var isSignedInput = Console.ReadLine().ToLower();
            newBand.IsSigned = bool.Parse(isSignedInput);
            Console.Write("Type the name of the new band's primary contact and press Enter: ");
            newBand.ContactName = Console.ReadLine();
            Console.Write("Type the phone number of the new band's contact and press Enter: ");
            newBand.ContactPhoneNumber = Console.ReadLine();
            context.Bands.Add(newBand);
            context.SaveChanges();
            Console.WriteLine();
            newBand.Description();
            Console.WriteLine("Added to the database.");
            Console.WriteLine();
        }
        private static void LetBandGo()
        {
            var context = new RecordLabelContext();
            Console.Clear();
            var selectingBand = true;
            var selectedBandToLetGo = new Band();
            while (selectingBand)
            {
                Console.Write("Please select a band (Type the band's name then press Enter): ");
                var bandSelection = Console.ReadLine();
                if (context.Bands.FirstOrDefault(band => band.Name == bandSelection) != null)
                {
                    selectedBandToLetGo = context.Bands.FirstOrDefault(band => band.Name == bandSelection);
                    selectingBand = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("There is no band by that name in the database");
                    Console.WriteLine();
                    Console.WriteLine("Please try again");
                }
            }
            selectedBandToLetGo.IsSigned = false;
            Console.WriteLine();
            Console.WriteLine("Band no longer signed to label");
            context.SaveChanges();
        }
        private static void ResignBand()
        {
            var context = new RecordLabelContext();
            Console.Clear();
            var selectingBand = true;
            var selectedBandToResign = new Band();
            while (selectingBand)
            {
                Console.Write("Please select a band (Type the band's name then press Enter): ");
                var bandSelection = Console.ReadLine();
                if (context.Bands.FirstOrDefault(band => band.Name == bandSelection) != null)
                {
                    selectedBandToResign = context.Bands.FirstOrDefault(band => band.Name == bandSelection);
                    selectingBand = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("There is no band by that name in the database");
                    Console.WriteLine();
                    Console.WriteLine("Please try again");
                }
            }
            selectedBandToResign.IsSigned = true;
            Console.WriteLine();
            Console.WriteLine("Band resigned to label");
            context.SaveChanges();
        }
        static void CreateMusician()
        {
            var context = new RecordLabelContext();
            Musician newMusician = new Musician();
            Console.Write("Type the new musician's name and press Enter: ");
            newMusician.Name = Console.ReadLine();
            Console.Write("Type the new musician's age and press Enter: ");
            var newMusicianAge = Console.ReadLine();
            newMusician.Age = int.Parse(newMusicianAge);
            Console.Write("Type the new musician's phone number and press Enter: ");
            newMusician.MusicianPhoneNumber = Console.ReadLine();
            Console.Write("Type the new musician's instrument and press Enter: ");
            newMusician.Instrument = Console.ReadLine();
            context.Musicians.Add(newMusician);
            context.SaveChanges();
            Console.WriteLine();
            newMusician.Description();
            Console.WriteLine("Added to the database.");
            Console.WriteLine();
        }
        static BandMember CreateBandMember(DbContext context) // adds musician to band they've never been a part of
        {
            BandMember newBandMember = new BandMember();
            return newBandMember;
        }
        static void CreateSong()
        {
            Console.Clear();
            var context = new RecordLabelContext();
            Song newSong = new Song();
            Console.Write("Type the new song's title and press Enter: ");
            newSong.Title = Console.ReadLine();
            Console.Write("Type the duration of the new song in the following format, 00:02:30 (2 minutes and 30 seconds), and press Enter: ");
            var newSongDuration = Console.ReadLine();
            newSong.Duration = TimeSpan.Parse(newSongDuration);
            Console.Write("Type the track number of the new song and press Enter: ");
            var newSongTrackNumber = Console.ReadLine();
            newSong.TrackNumber = int.Parse(newSongTrackNumber);
            var selectingAlbum = true;
            var albumForSong = new Album();
            while (selectingAlbum)
            {
                Console.Write("Please select an album (Type the album's title then press Enter): ");
                var albumSelection = Console.ReadLine();
                if (context.Albums.FirstOrDefault(album => album.Title == albumSelection) != null)
                {
                    albumForSong = context.Albums.FirstOrDefault(album => album.Title == albumSelection);
                    selectingAlbum = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("There is no album by that name in the database");
                    Console.WriteLine();
                    Console.WriteLine("Please try again");
                }
            }
            Console.WriteLine($"{newSong.Title} assigned to {albumForSong.Title}");
            newSong.AlbumId = albumForSong.Id;
            context.Songs.Add(newSong);
            context.SaveChanges();
            Console.WriteLine();
            newSong.Description();
            Console.WriteLine("Added to the database.");
            Console.WriteLine();
        }
        static void CreateAlbum()
        {
            Console.Clear();
            var context = new RecordLabelContext();
            Album newAlbum = new Album();
            Console.Write("Type the new album's title and press Enter: ");
            newAlbum.Title = Console.ReadLine();
            Console.Write("Type the new album's genre and press Enter: ");
            newAlbum.Genre = Console.ReadLine();
            Console.Write("Type true and press Enter if the new album is explicit. Type false and press Enter if the new album is not explicit: ");
            var newAlbumExplicit = Console.ReadLine().ToLower();
            newAlbum.IsExplicit = bool.Parse(newAlbumExplicit);
            Console.Write("Type the new album's release date in the following format, 01/01/2000, and press Enter: ");
            var newAlbumReleaseDate = Console.ReadLine();
            newAlbum.ReleaseDate = DateTime.Parse(newAlbumReleaseDate);
            var selectingBand = true;
            var bandForAlbum = new Band();
            while (selectingBand)
            {
                Console.Write("Please select a band to assign the album to (Type the band's name then press Enter): ");
                var bandSelection = Console.ReadLine();
                if (context.Bands.FirstOrDefault(band => band.Name == bandSelection) != null)
                {
                    bandForAlbum = context.Bands.FirstOrDefault(band => band.Name == bandSelection);
                    selectingBand = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("There is no band by that name in the database");
                    Console.WriteLine();
                    Console.WriteLine("Please try again");
                }
            }
            Console.WriteLine($"{newAlbum.Title} assigned to {bandForAlbum.Name}");
            newAlbum.BandId = bandForAlbum.Id;
            context.Albums.Add(newAlbum);
            context.SaveChanges();
            Console.WriteLine();
            newAlbum.Description();
            Console.WriteLine("Added to the database.");
            Console.WriteLine();
        }
        static void ViewCurrentOrPastBandMembers()
        {
            var context = new RecordLabelContext();
            Console.Clear();
            var selectingBand = true;
            var bandSelectionPastOrCurrent = new Band();
            while (selectingBand)
            {
                Console.Write("Please select a band (Type the band's name then press Enter): ");
                var bandSelection = Console.ReadLine();
                if (context.Bands.FirstOrDefault(band => band.Name == bandSelection) != null)
                {
                    bandSelectionPastOrCurrent = context.Bands.FirstOrDefault(band => band.Name == bandSelection);
                    selectingBand = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("There is no band by that name in the database");
                    Console.WriteLine();
                    Console.WriteLine("Please try again");
                }
            }
            var keepAskingPastOrCurrent = true;
            while (keepAskingPastOrCurrent)
            {
                Console.Write("View (p)ast members or (c)urrent members (press p or c then press Enter): ");
                var pastOrCurrentChoice = Console.ReadLine().ToLower();
                if (pastOrCurrentChoice == "p")
                {
                    Console.Clear();
                    var pastBandMembers = context.BandMembers.Include(bandMember => bandMember.Musician).Where(bandMember => bandMember.BandId == bandSelectionPastOrCurrent.Id && bandMember.CurrentMember == false);
                    foreach (var bandMember in pastBandMembers)
                    {
                        bandMember.Musician.Description();
                        keepAskingPastOrCurrent = false;
                    }
                }
                else if (pastOrCurrentChoice == "c")
                {
                    var currentBandMembers = context.BandMembers.Include(bandMember => bandMember.Musician).Where(bandMember => bandMember.BandId == bandSelectionPastOrCurrent.Id && bandMember.CurrentMember == true);
                    foreach (var bandMember in currentBandMembers)
                    {
                        bandMember.Musician.Description();
                        keepAskingPastOrCurrent = false;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Try again");
                    Console.WriteLine();
                }
            }
        }
        static void Greeting()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("Welcome to Suncoast Records Database Management Application");
            Console.WriteLine("-----------------------------------------------------------");
        }
        static string Menu()
        {
            Console.WriteLine();
            Console.WriteLine("What do you want to do?");
            Console.WriteLine();
            Console.WriteLine("(1) View all the bands                   (7) View current or past members of a band              (13) Add a song to an album");
            Console.WriteLine("");
            Console.WriteLine("(2) View all bands that are signed       (8) View a band's concerts                              (14) View songs on an album");
            Console.WriteLine("");
            Console.WriteLine("(3) View all bands that are not signed   (9) View albums in a genre                              (15) View all musicians");
            Console.WriteLine("");
            Console.WriteLine("(4) Add a new band                       (10) View all albums ordered by ReleaseDate             (16) Add musician to band");
            Console.WriteLine("");
            Console.WriteLine("(5) Let a band go                        (11) Prompt for a band name and view all their albums   (17) Remove musician from band");
            Console.WriteLine("");
            Console.WriteLine("(6) Resign a band                        (12) Add an album for a band                            (18) Create new musician");
            Console.WriteLine("");
            Console.WriteLine("(19) View all bands that a musician      (Q) Quit");
            Console.WriteLine("     is or has been a member of");
            Console.WriteLine();
            Console.Write("Select one of the options in parentheses and press Enter: ");
            var choice = Console.ReadLine().ToUpper();
            return choice;
        }
        // --(1)Add a new band-X
        // --(2)View all the bands-X
        // --(3)Add an album for a band-X
        // --(4)Add a song to an album-X
        // --(5)Let a band go (update isSigned to false)-X
        // --(6)Resign a band (update isSigned to true)-X
        // --(7)Prompt for a band name and view all their albums-X
        // --(8)View all albums ordered by ReleaseDate-X
        // --(9)View all bands that are signed-X
        // --(10)View all bands that are not signed-X
        static void Main(string[] args)
        {
            var applicationOpen = true;
            var counter = 0;
            Greeting();
            while (applicationOpen)
            {
                var menuSelection = Menu();
                switch (menuSelection)
                {
                    case "1": // DONE
                        ViewAllBands();
                        break;
                    case "2": // DONE
                        ViewAllBandsThatAreSigned();
                        break;
                    case "3": // DONE
                        ViewAllUnsignedBands();
                        break;
                    case "4": // DONE
                        CreateBand();
                        break;
                    case "5": // DONE
                        LetBandGo();
                        break;
                    case "6": // DONE
                        ResignBand();
                        break;
                    case "7": // DONE
                        ViewCurrentOrPastBandMembers();
                        break;
                    case "8": // DONE
                        ViewConcerts();
                        break;
                    case "9": // DONE
                        ViewAlbumsInAGenre();
                        break;
                    case "10": // DONE
                        ViewAlbumsOrderedByReleaseDate();
                        break;
                    case "11": // DONE
                        ViewAllBandsAlbums();
                        break;
                    case "12": // DONE
                        CreateAlbum();
                        break;
                    case "13": // DONE
                        CreateSong();
                        break;
                    case "14": // DONE
                        ViewAlbumsSongs();
                        break;
                    case "15": // DONE
                        ViewAllMusicians();
                        break;
                    // case "16":
                    //     Console.Clear();

                    //     var selectedBandMemberToBeAddedToBand = SelectABandMember();
                    //     if (selectedBandMemberToBeAddedToBand != null)
                    //     {
                    //         selectedBandMemberToBeAddedToBand.AddMusicianBackToBand();
                    //     }
                    //     else
                    //     {
                    //         context.BandMembers.Add(CreateBandMember(context));
                    //     }
                    //     context.SaveChanges();
                    //     break;
                    // case "17":
                    //     Console.Clear();
                    //     var selectedBandMemberToBeRemovedFromBand = SelectABandMember();
                    //     if (selectedBandMemberToBeRemovedFromBand != null && selectedBandMemberToBeRemovedFromBand.CurrentMember == true)
                    //     {
                    //         selectedBandMemberToBeRemovedFromBand.RemoveMusicianFromBand();
                    //     }
                    //     else if (selectedBandMemberToBeRemovedFromBand != null && selectedBandMemberToBeRemovedFromBand.CurrentMember == false)
                    //     {
                    //         Console.WriteLine("Musician already removed from band");
                    //     }
                    //     else
                    //     {
                    //         Console.WriteLine("No musician by that name has ever been part of the band");
                    //     }
                    //     context.SaveChanges();
                    //     break;
                    // case "18":
                    //     Console.Clear();
                    //     context.Musicians.Add(CreateMusician(context));
                    //     context.SaveChanges();
                    //     break;
                    // case "19":
                    //     Console.Clear();
                    //     var selectedMusicianForBandsDisplay = SelectAMusician(context);
                    //     var counterForBandDisplay = 1;
                    //     Console.WriteLine();
                    //     Console.WriteLine("Displaying musician's bands");
                    //     Console.WriteLine();
                    //     foreach (var bandMember in selectedMusicianForBandsDisplay.BandMembers)
                    //     {
                    //         Console.Write($"{counterForBandDisplay}: ");
                    //         Console.WriteLine($"{bandMember.Band.Name}");
                    //         counterForBandDisplay += 1;
                    //     }
                    //     break;
                    case "Q":
                        Console.Clear();
                        Console.WriteLine("Closing application");
                        Console.WriteLine("");
                        applicationOpen = false;
                        break;
                    default:
                        Console.Clear();
                        counter += 1;
                        if (counter > 3)
                        {
                            Console.WriteLine("Nothing selected");
                            Console.WriteLine();
                            Console.WriteLine("Closing application");
                            Console.WriteLine("");
                            applicationOpen = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Select an option and press Enter");
                            Console.WriteLine("");
                        }
                        break;
                }
            }
            Console.WriteLine("Goodbye");
            Console.WriteLine();
        }
    }
}