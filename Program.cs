using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
// using Microsoft.Extensions.Logging;
/*
TO-DO:
- Flesh out some examples
- Build in failsafes for decision loops, correct for situations when DbSet == null
- DRY process on selection, code commented out
- Add functionality via different Linq statements
- Sort when not prompted by Name/Title
- Clean up Linq statements
*/
namespace RhythmsGonnaGetYou
{
    class Band
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }
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
    //----------------------------------------------------------------------------------------------------------------
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
    //----------------------------------------------------------------------------------------------------------------
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
    //----------------------------------------------------------------------------------------------------------------
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
    //----------------------------------------------------------------------------------------------------------------
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
    }
    //----------------------------------------------------------------------------------------------------------------  
    class BandMember
    {
        public int Id { get; set; }
        public int BandId { get; set; }
        public int MusicianId { get; set; }
        public bool CurrentMember { get; set; }
        public Band Band { get; set; }
        public Musician Musician { get; set; }
    }
    //----------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------
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

    // class LabelDatabase >>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Why didn't context at outset it work for other functions??
    // {
    // }

    //----------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------
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

        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
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
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        static void AddMusicianToBand()
        {
            var context = new RecordLabelContext();
            Console.Clear();
            var selectingMusician = true;
            var selectedMusician = new Musician();
            var musicianCounter = 0;
            while (selectingMusician)
            {
                Console.Write("Please select a musician (Type the musician's name then press Enter): ");
                var musicianSelection = Console.ReadLine();
                if (context.Musicians.FirstOrDefault(musician => musician.Name == musicianSelection) != null)
                {
                    selectedMusician = context.Musicians.Include(musician => musician.BandMembers).ThenInclude(bandMember => bandMember.Band).FirstOrDefault(musician => musician.Name == musicianSelection);
                    var bandList = selectedMusician.BandMembers;
                    Console.Write("Please select a band (Type the musician's name then press Enter): ");
                    var bandSelection = Console.ReadLine();
                    var selectedBand = bandList.FirstOrDefault(bandMember => bandMember.Band.Name == bandSelection);
                    if (selectedBand != null && selectedBand.CurrentMember == false)
                    {
                        selectedBand.CurrentMember = true;
                        selectingMusician = false;
                        Console.WriteLine("Reinstating musician in band");
                        context.SaveChanges();
                    }
                    else if (selectedBand != null && selectedBand.CurrentMember == true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Musician already member of band");
                        Console.WriteLine();
                        Console.WriteLine("Try again");
                        musicianCounter += 1;
                    }
                    else if (context.Bands.FirstOrDefault(band => band.Name == bandSelection) != null && bandList.FirstOrDefault(bandMember => bandMember.Band.Name == bandSelection) == null)
                    {
                        var newBandMember = new BandMember();
                        newBandMember.BandId = context.Bands.FirstOrDefault(band => band.Name == bandSelection).Id;
                        newBandMember.MusicianId = selectedMusician.Id;
                        newBandMember.CurrentMember = true;
                        Console.WriteLine();
                        Console.WriteLine("Adding musician to band");
                        Console.WriteLine();
                        context.BandMembers.Add(newBandMember);
                        context.SaveChanges();
                        selectingMusician = false;
                    }
                    else if (musicianCounter > 3)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Quitting to menu");
                        selectingMusician = false;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("No band by that name");
                        Console.WriteLine();
                        Console.WriteLine("Try again");
                        musicianCounter += 1;

                    }
                }
                else if (selectedMusician == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("There is no musician by that name in the database");
                    Console.WriteLine();
                    Console.WriteLine("Please try again");
                    musicianCounter += 1;
                }
                else if (musicianCounter > 3)
                {
                    Console.WriteLine();
                    Console.WriteLine("Quitting to menu");
                    selectingMusician = false;
                }
            }
        }
        static void RemoveMusicianFromBand()
        {
            var context = new RecordLabelContext();
            Console.Clear();
            var selectingMusician = true;
            var musicianCounter = 0;
            var selectedMusician = new Musician();
            while (selectingMusician)
            {
                Console.Write("Please select a musician (Type the musician's name then press Enter): ");
                var musicianSelection = Console.ReadLine();
                if (context.Musicians.FirstOrDefault(musician => musician.Name == musicianSelection) != null)
                {
                    selectedMusician = context.Musicians.Include(musician => musician.BandMembers).ThenInclude(bandMember => bandMember.Band).FirstOrDefault(musician => musician.Name == musicianSelection);
                    var bandList = selectedMusician.BandMembers;
                    var selectingBand = true;
                    musicianCounter = 0;
                    var bandCounter = 0;
                    while (selectingBand)
                    {
                        Console.WriteLine();
                        Console.Write("Please select a band (Type the musician's name then press Enter): ");
                        var bandSelection = Console.ReadLine();
                        var selectedBand = bandList.FirstOrDefault(bandMember => bandMember.Band.Name == bandSelection);
                        if (selectedBand != null && selectedBand.CurrentMember == false)
                        {
                            Console.WriteLine("Musician not a member of band");
                            Console.WriteLine();
                            Console.WriteLine("Please try again");
                            bandCounter += 1;
                        }
                        else if (selectedBand != null && selectedBand.CurrentMember == true)
                        {
                            Console.WriteLine("Removing musician from band");
                            selectedBand.CurrentMember = false;
                            selectingMusician = false;
                            selectingBand = false;
                            context.SaveChanges();
                        }
                        else if (bandCounter > 3)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Quitting to menu");
                            selectingBand = false;
                            selectingMusician = false;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("There is no band by that name in the database");
                            Console.WriteLine();
                            Console.WriteLine("Please try again");
                            bandCounter += 1;
                        }
                    }
                }
                else if (musicianCounter > 3)
                {
                    Console.WriteLine();
                    Console.WriteLine("Quitting to menu");
                    selectingMusician = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("There is no musician by that name in the database");
                    Console.WriteLine();
                    Console.WriteLine("Please try again");
                    musicianCounter += 1;
                }
            }
        }
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
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
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        static void ViewMusiciansBands()
        {
            Console.Clear();
            var context = new RecordLabelContext();
            var selectingMusician = true;
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
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("There is no musician by that name in the database");
                    Console.WriteLine();
                    Console.WriteLine("Please try again");
                }
            }
            var counterForBandDisplay = 1;
            Console.WriteLine();
            Console.WriteLine("Displaying musician's bands");

            Console.WriteLine();

            var musiciansBands = context.BandMembers.Include(bandMember => bandMember.Band).Where(bandMember => bandMember.MusicianId == selectedMusician.Id);
            foreach (var bandMember in musiciansBands)
            {
                Console.Write($"{counterForBandDisplay}: ");
                Console.WriteLine($"{bandMember.Band.Name}");
                counterForBandDisplay += 1;
            }
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
            foreach (var band in context.Bands.Include(band => band.BandMembers))
            {
                band.Description();
                Console.WriteLine($"Band Member Count: {band.BandMembers.Count()}");
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
                Console.WriteLine($"Band Member Count: {band.BandMembers.Count()}");
                Console.WriteLine();
            }
        }
        static void ViewAllUnsignedBands()
        {
            var context = new RecordLabelContext();
            Console.Clear();
            foreach (var band in context.Bands.Where(band => band.IsSigned == false))
            {
                band.Description();
                Console.WriteLine($"Band Member Count: {band.BandMembers.Count()}");
                Console.WriteLine();
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
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
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
            Console.WriteLine("(6) Resign a band                        (12) Add an album for a band                            (18) Add new musician");
            Console.WriteLine("");
            Console.WriteLine("(19) View all bands that a musician      (Q) Quit");
            Console.WriteLine("     is or has been a member of");
            Console.WriteLine();
            Console.Write("Select one of the options in parentheses and press Enter: ");
            var choice = Console.ReadLine().ToUpper();
            return choice;
        }
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
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
                    case "1":
                        ViewAllBands();
                        break;
                    case "2":
                        ViewAllBandsThatAreSigned();
                        break;
                    case "3":
                        ViewAllUnsignedBands();
                        break;
                    case "4":
                        CreateBand();
                        break;
                    case "5":
                        LetBandGo();
                        break;
                    case "6":
                        ResignBand();
                        break;
                    case "7":
                        ViewCurrentOrPastBandMembers();
                        break;
                    case "8":
                        ViewConcerts();
                        break;
                    case "9":
                        ViewAlbumsInAGenre();
                        break;
                    case "10":
                        ViewAlbumsOrderedByReleaseDate();
                        break;
                    case "11":
                        ViewAllBandsAlbums();
                        break;
                    case "12":
                        CreateAlbum();
                        break;
                    case "13":
                        CreateSong();
                        break;
                    case "14":
                        ViewAlbumsSongs();
                        break;
                    case "15":
                        ViewAllMusicians();
                        break;
                    case "16":
                        AddMusicianToBand();
                        break;
                    case "17":
                        RemoveMusicianFromBand();
                        break;
                    case "18":
                        CreateMusician();
                        break;
                    case "19":
                        ViewMusiciansBands();
                        break;
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
