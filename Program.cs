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
        public string Description()
        {
            return "";
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
        public List<Song> songs { get; set; }
        public string Description()
        {
            return "";
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
        public string Description()
        {
            return "";
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
        public string Description()
        {
            return "";
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
        public string Description()
        {
            return "";
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
        static string Menu()
        {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine();
            Console.WriteLine("(1) View all the bands");
            Console.WriteLine("(2) View all bands that are signed");
            Console.WriteLine("(3) View all bands that are not signed");
            Console.WriteLine("(4) Add a new band");
            Console.WriteLine("(5) Let a band go (update isSigned to false)");
            Console.WriteLine("(6) Resign a band (update isSigned to true)");
            Console.WriteLine();
            Console.WriteLine("(7) View albums in a genre");
            Console.WriteLine("(8) View all albums ordered by ReleaseDate");
            Console.WriteLine("(9) Prompt for a band name and view all their albums");
            Console.WriteLine("(10) Add an album for a band");
            Console.WriteLine("(11) Add a song to an album");
            Console.WriteLine();
            Console.WriteLine("(12) View all members of a band");
            Console.WriteLine("(13) View current members of a band");
            Console.WriteLine("(14) View past members of a band");
            Console.WriteLine("(15) View all bands that a musician is a member of");
            Console.WriteLine("(16) View all musicians");
            Console.WriteLine("(17) Add musician to band");
            Console.WriteLine("(18) Remove musician from band");
            Console.WriteLine("(19) Create new musician");
            Console.WriteLine();
            Console.WriteLine("(20) View all past concerts");
            Console.WriteLine("(21) View upcoming concerts");
            Console.WriteLine("(22) View number of fans at a concert");
            Console.WriteLine();
            Console.WriteLine("(Q)uit");
            var acceptableInput = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "Q" };
            var choice = Console.ReadLine();
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
                        break;
                    case "5":
                        Console.Clear();
                        break;
                    case "6":
                        Console.Clear();
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
                        break;
                    case "13":
                        Console.Clear();
                        break;
                    case "14":
                        Console.Clear();
                        break;
                    case "15":
                        Console.Clear();
                        break;
                    case "16":
                        Console.Clear();
                        break;
                    case "17":
                        Console.Clear();
                        break;
                    case "18":
                        Console.Clear();
                        break;
                    case "19":
                        Console.Clear();
                        break;
                    case "20":
                        Console.Clear();
                        break;
                    case "22":
                        Console.Clear();
                        break;
                    case "21":
                        Console.Clear();
                        break;
                    case "Q":
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