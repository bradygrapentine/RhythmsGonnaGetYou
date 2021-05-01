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

--(1)Create a menu system that shows the following options to the user 
--(2)until they choose to quit your program =>
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
        public void ListAlbums()
        {
            Console.WriteLine();
            Console.WriteLine("Listing Band's Albums:");
            Console.WriteLine();
            var count = 1;
            foreach (var album in Albums)
            {
                Console.Write($"{count}: ");
                album.Description();
                count += 1;
            }
            Console.WriteLine();
        }
        public void ListConcerts()
        {
            Console.WriteLine();
            Console.WriteLine("Listing Band's Concerts:");
            Console.WriteLine();
            var count = 1;
            foreach (var concert in Concerts)
            {
                Console.Write($"{count}: ");
                concert.Description();
                count += 1;
            }
            Console.WriteLine();
        }
        public void ListPastBandMembers()
        {
            Console.WriteLine();
            Console.WriteLine("Listing Band's Past Members:");
            Console.WriteLine();
            var count = 1;
            foreach (var bandMember in BandMembers.Where(bandMember => bandMember.CurrentMember == false))
            {
                Console.Write($"{count}: ");
                bandMember.Musician.Description();
                count += 1;
            }
            Console.WriteLine();
        }
        public void ListCurrentBandMembers()
        {
            Console.WriteLine();
            Console.WriteLine("Listing Band's Current Members:");
            Console.WriteLine();
            var count = 1;
            foreach (var bandMember in BandMembers.Where(bandMember => bandMember.CurrentMember == true))
            {
                Console.Write($"{count}: ");
                bandMember.Musician.Description();
                count += 1;
            }
            Console.WriteLine();
        }
        public void LetBandGo()
        {
            Console.WriteLine("Band no longer signed to label");
            IsSigned = false;
        }
        public void ResignBand()
        {
            Console.WriteLine("Band resigned to label");
            IsSigned = true;
        }
    }
    class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public bool IsExplicit { get; set; }
        public int ReleaseDate { get; set; }
        public int BandId { get; set; }
        public Band Band { get; set; }
        public List<Song> Songs { get; set; }
        public void Description()
        {
            Console.WriteLine($"Album Title: {Title}, Band: {Band.Name}, Genre: {Genre}, Explicit: {IsExplicit.ToString().ToUpper()}, Release Date: {ReleaseDate}");
        }
        public void ListSongs()
        {
            Console.WriteLine();
            Console.WriteLine("Listing Album's Songs:");
            Console.WriteLine();
            var count = 1;
            foreach (var song in Songs)
            {
                Console.Write($"{count}: ");
                song.Description();
                count += 1;
            }
            Console.WriteLine();
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
            Console.WriteLine($"Song Title: {Title}, Album: {Album.Title}, Track Number: {TrackNumber}, Length of Song: {Duration}");
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
            Console.WriteLine($"Performing Band: {Band.Name}, Performed At: {WherePerformed}, Performed For: {NumberOfPeopleAtConcert} people, Date of Performance: {WhenPerformed}");
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
        public Band Band { get; set; }
        public Musician Musician { get; set; }
        public bool CurrentMember { get; set; }
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
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseNpgsql("server=localhost;database=RecordLabelDatabase"); // Connects to Db
        }
    }

    class Program
    {
        static Band SelectABand(DbContext context)
        {
            Console.Write("Enter band's name: ");
            var selectedBandName = Console.ReadLine();
            var selectedBand = new Band(); // To prevent error
            return selectedBand;
        }
        static Musician SelectAMusician(DbContext context)
        {
            Console.Write("Enter musician's name: ");
            var selectedMusicianName = Console.ReadLine();
            var selectedMusician = new Musician(); // To prevent error
            return selectedMusician;
        }
        static Album SelectAnAlbum(DbContext context)
        {
            Console.Write("Enter album's name: ");
            var selectedAlbumName = Console.ReadLine();
            var selectedAlbum = new Album(); // To prevent error
            return selectedAlbum;
        }
        static void ViewAllSignedBands(DbContext context)
        {

        }
        static void ViewAllUnsignedBands(DbContext context)
        {

        }
        static void ViewAlbumsInAGenre(DbContext context)
        {

        }
        static void ViewAlbumsOrderedByReleaseDate(DbContext context)
        {

        }
        static void ViewAllMusicians(DbContext context)
        {

        }
        static Band CreateBand(DbContext context)
        {
            Band newBand = new Band();
            return newBand;
        }
        static Musician CreateMusician(DbContext context)
        {
            Musician newMusician = new Musician();
            return newMusician;
        }
        static BandMember CreateBandMember(DbContext context) // adds musician to band they've never been a part of
        {
            BandMember newBandMember = new BandMember();
            return newBandMember;
        }
        static Song CreateSong(DbContext context)
        {
            Song newSong = new Song();
            return newSong;
        }
        static Album CreateAlbum(DbContext context)
        {
            Album newAlbum = new Album();
            return newAlbum;
        }
        static string Menu()
        {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine();
            Console.WriteLine("(1) View all the bands"); // method in RecordLabelDataBase class, DISPLAY
            Console.WriteLine("(2) View all bands that are signed"); // method in RecordLabelDataBase class, DISPLAY
            Console.WriteLine("(3) View all bands that are not signed"); // method in RecordLabelDataBase class, DISPLAY
            Console.WriteLine("(4) Add a new band"); // method in RecordLabelDataBase class, CREATE, SAVE NEEDED
            Console.WriteLine("(5) Let a band go (update isSigned to false)"); // method in Band class, UPDATE, SAVE NEEDED
            Console.WriteLine("(6) Resign a band (update isSigned to true)"); // method in Band class, UPDATE, SAVE NEEDED
            Console.WriteLine("(7) View current or past members of a band"); // method in Band class, DISPLAY
            Console.WriteLine("(8) View a band's past concerts"); // method in Band class, DISPLAY
            Console.WriteLine();
            Console.WriteLine("(9) View albums in a genre"); // method in RecordLabelDataBase class, DISPLAY
            Console.WriteLine("(10) View all albums ordered by ReleaseDate"); // method in RecordLabelDataBase class, DISPLAY
            Console.WriteLine("(11) Prompt for a band name and view all their albums"); // method in Band class, DISPLAY
            Console.WriteLine("(12) Add an album for a band"); // method in RecordLabelDatabase class, CREATE, SAVE NEEDED
            Console.WriteLine("(13) Add a song to an album"); // method in RecordLabelDatabase class, CREATE, SAVE NEEDED
            Console.WriteLine("(14) View songs on an album"); // method in Album class, DISPLAY
            Console.WriteLine();
            Console.WriteLine("(15) View all musicians"); // method in RecordLabelDataBase class, DISPLAY 
            Console.WriteLine("(16) Add musician to band"); // method in BandMember class (if used to be in band), UPDATE, method in RecordLabelDatabase class (if never been part of band), CREATE, SAVE NEEDED
            Console.WriteLine("(17) Remove musician from band"); // method in BandMember class, UPDATE, SAVE NEEDED
            Console.WriteLine("(18) Create new musician"); // method in RecordLabelDatabase class, CREATE, SAVE NEEDED
            Console.WriteLine("(19) View all bands that a musician is or has been a member of"); // method in Musician class, DISPLAY
            Console.WriteLine();
            Console.WriteLine("(Q)uit");
            var choice = Console.ReadLine().ToUpper();
            return choice;
        }
        static void Main(string[] args)
        {
            var context = new RecordLabelContext();
            var applicationOpen = true;
            var counter = 0;
            while (applicationOpen)
            {
                var menuSelection = Menu();
                switch (menuSelection)
                {
                    case "1":
                        Console.Clear();
                        break;
                    case "2":
                        Console.Clear();
                        break;
                    case "3":
                        Console.Clear();
                        break;
                    case "4":
                        Console.Clear();
                        context.Bands.Add(CreateBand(context));
                        context.SaveChanges();
                        break;
                    case "5":
                        Console.Clear();
                        var selectedBandToLetGo = SelectABand(context);
                        selectedBandToLetGo.LetBandGo();
                        context.SaveChanges();
                        break;
                    case "6":
                        Console.Clear();
                        var selectedBandToResign = SelectABand(context);
                        selectedBandToResign.ResignBand();
                        context.SaveChanges();
                        break;
                    case "7":
                        Console.Clear();
                        break;
                    case "8":
                        Console.Clear();
                        break;
                    case "9":
                        Console.Clear();
                        break;
                    case "10":
                        Console.Clear();
                        break;
                    case "11":
                        Console.Clear();
                        break;
                    case "12":
                        Console.Clear();
                        context.Albums.Add(CreateAlbum(context));
                        context.SaveChanges();
                        break;
                    case "13":
                        Console.Clear();
                        context.Songs.Add(CreateSong(context));
                        context.SaveChanges();
                        break;
                    case "14":
                        Console.Clear();
                        break;
                    case "15":
                        Console.Clear();
                        break;
                    case "16":
                        Console.Clear();
                        var selectedBandToAddMusicianTo = SelectABand(context);
                        var selectedMusicianToBeAddedToBand = SelectAMusician(context);
                        var selectedBandMemberToBeAddedToBand = selectedMusicianToBeAddedToBand.BandMembers.FirstOrDefault(bandMember => bandMember.BandId == selectedBandToAddMusicianTo.Id);
                        if ((selectedMusicianToBeAddedToBand.BandMembers.FirstOrDefault(bandMember => bandMember.BandId == selectedBandToAddMusicianTo.Id)) != null)
                        {
                            selectedBandMemberToBeAddedToBand.AddMusicianBackToBand();
                        }
                        else
                        {
                            context.BandMembers.Add(CreateBandMember(context));
                        }
                        context.SaveChanges();
                        break;
                    case "17":  // CLEAN THIS UP
                        Console.Clear();
                        var selectedBandToRemoveMusicianFrom = SelectABand(context);
                        var selectedMusicianToBeRemovedFromBand = SelectAMusician(context);
                        var selectedBandMemberToBeRemovedFromBand = selectedMusicianToBeRemovedFromBand.BandMembers.FirstOrDefault(bandMember => bandMember.BandId == selectedBandToRemoveMusicianFrom.Id);
                        if ((selectedMusicianToBeRemovedFromBand.BandMembers.FirstOrDefault(bandMember => bandMember.BandId == selectedBandToRemoveMusicianFrom.Id && bandMember.CurrentMember == true)) != null)
                        {
                            selectedBandMemberToBeRemovedFromBand.RemoveMusicianFromBand();
                        }
                        else if ((selectedMusicianToBeRemovedFromBand.BandMembers.FirstOrDefault(bandMember => bandMember.BandId == selectedBandToRemoveMusicianFrom.Id && bandMember.CurrentMember == false)) != null)
                        {
                            Console.WriteLine("Musician already removed from band");
                        }
                        else
                        {
                            Console.WriteLine("No musician by that name has ever been part of the band");
                        }
                        context.SaveChanges();
                        break;
                    case "18":
                        Console.Clear();
                        context.Musicians.Add(CreateMusician(context));
                        context.SaveChanges();
                        break;
                    case "19":
                        Console.Clear();
                        break;
                    // case "20":
                    //     Console.Clear();
                    //     break;
                    // case "22":
                    //     Console.Clear();
                    //     break;
                    // case "21":
                    //     Console.Clear();
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
                            Console.WriteLine("Closing application");
                            Console.WriteLine("");
                            applicationOpen = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Press select an option and press Enter");
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



//             var context = new SuncoastMoviesContext();
//             var movies = context.Movies;
//             var movieCount = movies.Count(); // Using Linq on DbSet retrieved from database
//             Console.WriteLine($"There are {movieCount} movies!");
//             var moviesWithRatings = context.Movies // makes a new collection of movies but each movie knows the associated Rating object
//             .Include(movie => movie.Rating). // from our movie, please include the associated rating
//             Include(movie => movie.Roles). // from our movie, please include the associated roles list
//             ThenInclude(role => role.Actor); // THEN for each of the roles, please include associated actor object...join stmts in C# with includes, now we have access to all the data tables
//             foreach (var movie in movies)
//             {
//                 if (movie.Rating == null)
//                 {
//                     Console.WriteLine($"There is an unrated movie named {movie.Title}");
//                 }
//                 else
//                 {
//                     Console.WriteLine($"Movie {movie.Title} - movie.Rating.Description");
//                 }
//             }
//             foreach (var movie in movies)
//             {
//                 if (movie.Rating == null)
//                 {
//                     Console.WriteLine($"{movie.Title} - not rated");
//                 }
//                 else
//                 {
//                     Console.WriteLine($"{movie.Title} - {movie.Rating.Description}");
//                 }
//                 foreach (var role in movie.Roles)
//                 {
//                     Console.WriteLine($" - {role.CharacterName} played by {role.Actor.FullName}");
//                 }
//             }
//             var newMovie = new Movie
//             {
//                 Title = "SpaceBalls",
//                 PrimaryDirector = "Mel Brooks",
//                 Genre = "Comedy",
//                 YearReleased = 1987,
//                 RatingId = 2
//             };
//             context.Movies.Add(newMovie); // DbSet can be treated like a list

//             context.SaveChanges(); // Saves to database, must do this because it's hosted on a different computer, but add to the context (if not done, it will seriously slow down the program because it would have to connect with the database each time)
//             // SaveChanges imparts atomicity
//             var existingMovie = context.Movies.FirstOrDefault(movie => movie.Title == "SpaceBalls");
//             if (existingMovie != null)
//             {
//                 existingMovie.Title = "SpaceBalls - the best movie ever";
//                 context.SaveChanges();
//             }
//             var existingMovie2 = context.Movies.FirstOrDefault(movie => movie.Title == "Cujo");
//             if (existingMovie2 != null)
//             {
//                 context.Movies.Remove(existingMovie2);
//                 context.SaveChanges();
//             }
//         }
//     }
// }