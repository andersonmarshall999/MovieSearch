using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using MovieSearch.Models;

namespace MovieSearch.Services
{
    public class FileService : IFileService
    {
        private readonly ILogger<IFileService> _logger;
        private readonly string _movieFileName;
        private readonly string _showFileName;
        private readonly string _videoFileName;

        // these should not be here
        private List<int> MovieIds { get; set; }
        private List<string> MovieTitles { get; set; }
        private List<string> MovieGenres { get; set; }
        private List<int> ShowIds { get; set; }
        private List<string> ShowTitles { get; set; }
        private List<int> ShowSeasons { get; set; }
        private List<int> ShowEpisodes { get; set; }
        private List<string> ShowWriters { get; set; }
        private List<int> VideoIds { get; set; }
        private List<string> VideoTitles { get; set; }
        private List<string> VideoFormats { get; set; }
        private List<int> VideoLengths { get; set; }
        private List<string> VideoRegions { get; set; }

        #region constructors

        // default constructor
        public FileService()
        {
        }

        // constructor
        public FileService(int myInt)
        {
            Console.WriteLine($"constructor value {myInt}");
        }

        public FileService(string myString)
        {
            Console.WriteLine($"constructor value {myString}");
        }

        #endregion

        public FileService(ILogger<IFileService> logger)
        {
            _logger = logger;
            logger.LogInformation("Here is some information");

            _movieFileName = $"{Environment.CurrentDirectory}/movies.csv";
            _showFileName = $"{Environment.CurrentDirectory}/shows.csv";
            _videoFileName = $"{Environment.CurrentDirectory}/videos.csv";

            MovieIds = new List<int>();
            MovieTitles = new List<string>();
            MovieGenres = new List<string>();
            ShowIds = new List<int>();
            ShowTitles = new List<string>();
            ShowSeasons = new List<int>();
            ShowEpisodes = new List<int>();
            ShowWriters = new List<string>();
            VideoIds = new List<int>();
            VideoTitles = new List<string>();
            VideoFormats = new List<string>();
            VideoLengths = new List<int>();
            VideoRegions = new List<string>();
        }
        // TODO: files are written to bin/Debug/net5.0

        public void Read()
        {
            _logger.LogInformation("Reading");
            Console.WriteLine("*** I am reading");
            try
            {
                StreamReader sr = new StreamReader(_movieFileName);
                // first line contains column headers
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    // first look for quote(") in string
                    // this indicates a comma(,) in movie title
                    if (line != null)
                    {
                        int idx = line.IndexOf('"');
                        if (idx == -1)
                        {
                            // no quote = no comma in movie title
                            // movie details are separated with comma(,)
                            string[] movieDetails = line.Split(',');
                            // 1st array element contains movie id
                            MovieIds.Add(int.Parse(movieDetails[0]));
                            // 2nd array element contains movie title
                            MovieTitles.Add(movieDetails[1]);
                            // 3rd array element contains movie genre(s)
                            // replace "|" with ", "
                            MovieGenres.Add(movieDetails[2].Replace("|", ", "));
                        }
                        else
                        {
                            // quote = comma in movie title
                            // extract the movieId
                            MovieIds.Add(int.Parse(line.Substring(0, idx - 1)));
                            // remove movieId and first quote from string
                            line = line.Substring(idx + 1);
                            // find the next quote
                            idx = line.IndexOf('"');
                            // extract the movieTitle
                            MovieTitles.Add(line.Substring(0, idx));
                            // remove title and last comma from the string
                            line = line.Substring(idx + 2);
                            // replace the "|" with ", "
                            MovieGenres.Add(line.Replace("|", ", "));
                        }
                    }
                }
                // close file when done
                sr.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _logger.LogInformation("Movies in file {Count}", MovieIds.Count);
            
            try
            {
                StreamReader sr = new StreamReader(_showFileName);
                // first line contains column headers
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    // first look for quote(") in string
                    // this indicates a comma(,) in show title
                    if (line != null)
                    {
                        int idx = line.IndexOf('"');
                        if (idx == -1)
                        {
                            // no quote = no comma in show title
                            // show details are separated with comma(,)
                            string[] showDetails = line.Split(',');
                            // 1st array element contains show id
                            ShowIds.Add(int.Parse(showDetails[0]));
                            // 2nd array element contains show title
                            ShowTitles.Add(showDetails[1]);
                            // 3rd array element contains show season
                            ShowSeasons.Add(int.Parse(showDetails[2]));
                            // 4th array element contains show episode
                            ShowEpisodes.Add(int.Parse(showDetails[3]));
                            // 5th array element contains show writer(s)
                            // replace "|" with ", "
                            ShowWriters.Add(showDetails[4].Replace("|", ", "));
                        }
                        else
                        {
                            // quote = comma in show title
                            // extract the showId
                            ShowIds.Add(int.Parse(line.Substring(0, idx - 1)));
                            // remove showId and first quote from string
                            line = line.Substring(idx + 1);
                            // find the next quote
                            idx = line.IndexOf('"');
                            // extract the showTitle
                            ShowTitles.Add(line.Substring(0, idx));
                            // remove title and last comma from the string
                            line = line.Substring(idx + 2);
                            // replace the "|" with ", "
                            ShowWriters.Add(line.Replace("|", ", "));
                        }
                    }
                }
                // close file when done
                sr.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _logger.LogInformation("Shows in file {Count}", ShowIds.Count);
            
            try
            {
                StreamReader sr = new StreamReader(_videoFileName);
                // first line contains column headers
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    // first look for quote(") in string
                    // this indicates a comma(,) in video title
                    if (line != null)
                    {
                        int idx = line.IndexOf('"');
                        if (idx == -1)
                        {
                            // no quote = no comma in video title
                            // video details are separated with comma(,)
                            string[] videoDetails = line.Split(',');
                            // 1st array element contains video id
                            VideoIds.Add(int.Parse(videoDetails[0]));
                            // 2nd array element contains video title
                            VideoTitles.Add(videoDetails[1]);
                            // 3rd array element contains video format
                            VideoFormats.Add(videoDetails[2]);
                            // 4th array element contains video length
                            VideoLengths.Add(int.Parse(videoDetails[3]));
                            // 5th array element contains video region(s)
                            // replace "|" with ", "
                            VideoRegions.Add(videoDetails[4].Replace("|", ", "));
                        }
                        else
                        {
                            // quote = comma in video title
                            // extract the videoId
                            VideoIds.Add(int.Parse(line.Substring(0, idx - 1)));
                            // remove videoId and first quote from string
                            line = line.Substring(idx + 1);
                            // find the next quote
                            idx = line.IndexOf('"');
                            // extract the videoTitle
                            VideoTitles.Add(line.Substring(0, idx));
                            // remove title and last comma from the string
                            line = line.Substring(idx + 2);
                            // replace the "|" with ", "
                            VideoRegions.Add(line.Replace("|", ", "));
                        }
                    }
                }
                // close file when done
                sr.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _logger.LogInformation("Videos in file {Count}", VideoIds.Count);
        }

        public void Write(Movie movie, Show show, Video video)
        {
            Console.WriteLine("*** I am writing");

            StreamWriter swm = new StreamWriter(_movieFileName, true);
            swm.WriteLine($"{movie.Id},{movie.Title},{movie.Genres}");
            swm.Close();

            // add movie details to Lists
            MovieIds.Add(movie.Id);
            MovieTitles.Add(movie.Title);
            MovieGenres.Add(movie.Genres);
            // log transaction
            _logger.LogInformation("Movie id {Id} added", movie.Id);

            StreamWriter sws = new StreamWriter(_showFileName, true);
            sws.WriteLine($"{show.Id},{show.Title},{show.Season},{show.Episode},{show.Writers}");
            sws.Close();

            // add show details to Lists
            ShowIds.Add(show.Id);
            ShowTitles.Add(show.Title);
            ShowSeasons.Add(show.Season);
            ShowEpisodes.Add(show.Episode);
            ShowWriters.Add(show.Writers);
            // log transaction
            _logger.LogInformation("Show id {Id} added", show.Id);

            StreamWriter swv = new StreamWriter(_videoFileName, true);
            swv.WriteLine($"{video.Id},{video.Title},{video.Format},{video.Length},{video.Regions}");
            swv.Close();

            // add video details to Lists
            VideoIds.Add(video.Id);
            VideoTitles.Add(video.Title);
            VideoFormats.Add(video.Format);
            VideoLengths.Add(video.Length);
            VideoRegions.Add(video.Regions);
            // log transaction
            _logger.LogInformation("Video id {Id} added", video.Id);
        }

        public void MovieDisplay()
        {
            // Display All Movies
            // loop thru Movie Lists
            for (int i = 0; i < MovieIds.Count; i++)
            {
                // display movie details
                Console.WriteLine($"Id: {MovieIds[i]}");
                Console.WriteLine($"Title: {MovieTitles[i]}");
                Console.WriteLine($"Genre(s): {MovieGenres[i]}");
                Console.WriteLine();
            }
        }
        public void ShowDisplay()
        {
            // Display All Shows
            // loop thru Show Lists
            for (int i = 0; i < ShowIds.Count; i++)
            {
                // display show details
                Console.WriteLine($"Id: {ShowIds[i]}");
                Console.WriteLine($"Title: {ShowTitles[i]}");
                Console.WriteLine($"Season: {ShowSeasons[i]}");
                Console.WriteLine($"Episode: {ShowEpisodes[i]}");
                Console.WriteLine($"Writers: {ShowWriters[i]}");
                Console.WriteLine();
            }
        }
        public void VideoDisplay()
        {
            // Display All Videos
            // loop thru Video Lists
            for (int i = 0; i < VideoIds.Count; i++)
            {
                // display video details
                Console.WriteLine($"Id: {VideoIds[i]}");
                Console.WriteLine($"Title: {VideoTitles[i]}");
                Console.WriteLine($"Format: {VideoFormats[i]}");
                Console.WriteLine($"Length: {VideoLengths[i]}");
                Console.WriteLine($"Regions: {VideoRegions[i]}");
                Console.WriteLine();
            }
        }

        public void MovieInput() // yea this code was mostly copied from the original program with slight edits
        {
            // enter movie title and check if it is a duplicate
            Console.WriteLine("Enter movie title: ");
            string inputTitle = Console.ReadLine();
            if (inputTitle != null && MovieTitles.ConvertAll(t => t.ToLower()).Contains(inputTitle.ToLower()))
            {
                Console.WriteLine("This title is already entered.\n");
                _logger.LogInformation("Duplicate movie title {Title}", inputTitle);
            }
            else // if no duplicates detected then continue
            {
                // generate movie id from highest id in list plus 1
                int inputId = MovieIds.Max() + 1;
                // enter genre(s)
                List<string> inputGenresList = new List<string>();
                string inputGenre;
                do
                {
                    // ask user to enter genre
                    Console.WriteLine("Enter genre (\"done\" to quit): ");
                    // input genre
                    inputGenre = Console.ReadLine();
                    // if user enters "done" or does not enter a genre do not add it to list
                    if (inputGenre != null && inputGenre.ToLower() != "done" && inputGenre.Length > 0)
                    {
                        inputGenresList.Add(inputGenre);
                    }
                } while (inputGenre != null && inputGenre.ToLower() != "done");

                // specify if no genres are entered
                if (inputGenresList.Count == 0)
                {
                    inputGenresList.Add("(no genres listed)");
                }

                // use "|" as delimiter for genres
                string genresList = string.Join("|", inputGenresList);
                // if there is a comma(,) in the title, wrap it in quotes
                inputTitle = inputTitle != null && inputTitle.IndexOf(',') != -1 ? $"\"{inputTitle}\"" : inputTitle;
                
                Movie movie = new Movie
                {
                    Id = inputId,
                    Title = inputTitle,
                    Genres = genresList
                };

                MovieIds.Add(movie.Id);
                MovieTitles.Add(movie.Title);
                MovieGenres.Add(movie.Genres);
                
                StreamWriter sw = new StreamWriter(_movieFileName, true);
                movie.Display();
                sw.WriteLine($"{movie.Id},{movie.Title},{movie.Genres}");
                sw.Close();

                _logger.LogInformation("Movie id {Id} added", inputId);
            }
        }
        public void ShowInput()
        {
            // enter show title
            Console.WriteLine("Enter show title: ");
            string inputTitle = Console.ReadLine();
            // generate show id from highest id in list plus 1
            int inputId = ShowIds.Max() + 1;
            // enter show season
            Console.WriteLine("Enter show season: ");
            int inputSeason = int.Parse(Console.ReadLine() ?? string.Empty);
            // enter show episode
            Console.WriteLine("Enter show episode: ");
            int inputEpisode = int.Parse(Console.ReadLine() ?? string.Empty);
            // enter writer(s)
            List<string> inputWritersList = new List<string>();
            string inputWriter;
            do
            {
                // ask user to enter writer
                Console.WriteLine("Enter writer name (\"done\" to quit): ");
                // input writer
                inputWriter = Console.ReadLine();
                // if user enters "done" or does not enter a writer do not add it to list
                if (inputWriter != null && inputWriter.ToLower() != "done" && inputWriter.Length > 0)
                {
                    inputWritersList.Add(inputWriter);
                }
            } while (inputWriter != null && inputWriter.ToLower() != "done");

            // specify if no writers are entered
            if (inputWritersList.Count == 0)
            {
                inputWritersList.Add("(writers anonymous)");
            }

            // use "|" as delimiter for writers
            string writersList = string.Join("|", inputWritersList);
            // if there is a comma(,) in the title, wrap it in quotes
            inputTitle = inputTitle != null && inputTitle.IndexOf(',') != -1 ? $"\"{inputTitle}\"" : inputTitle;
            
            Show show = new Show
            {
                Id = inputId,
                Title = inputTitle,
                Season = inputSeason,
                Episode = inputEpisode,
                Writers = writersList
            };

            ShowIds.Add(show.Id);
            ShowTitles.Add(show.Title);
            ShowSeasons.Add(show.Season);
            ShowEpisodes.Add(show.Episode);
            ShowWriters.Add(show.Writers);
            
            StreamWriter sw = new StreamWriter(_showFileName, true);
            show.Display();
            sw.WriteLine($"{show.Id},{show.Title},{show.Season},{show.Episode},{show.Writers}");
            sw.Close();

            _logger.LogInformation("Show id {Id} added", inputId);
        }
        public void VideoInput()
        {
            // enter video title
            Console.WriteLine("Enter video title: ");
            string inputTitle = Console.ReadLine();
            // generate video id from highest id in list plus 1
            int inputId = VideoIds.Max() + 1;
            // enter video format
            Console.WriteLine("Enter video format: ");
            string inputFormat = Console.ReadLine();
            // enter video length
            Console.WriteLine("Enter video length in seconds: ");
            int inputLength = int.Parse(Console.ReadLine() ?? string.Empty);
            // enter region(s)
            List<string> inputRegionsList = new List<string>();
            string inputRegion;
            do
            {
                // ask user to enter region
                Console.WriteLine("Enter region code (\"done\" to quit): ");
                // input region
                inputRegion = Console.ReadLine();
                // if user enters "done" or does not enter a region do not add it to list
                if (inputRegion != null && inputRegion.ToLower() != "done" && inputRegion.Length > 0)
                {
                    inputRegionsList.Add(inputRegion);
                }
            } while (inputRegion != null && inputRegion.ToLower() != "done");

            // specify if no regions are entered
            if (inputRegionsList.Count == 0)
            {
                inputRegionsList.Add("(no regions specified)");
            }

            // use "|" as delimiter for regions
            string regionsList = string.Join("|", inputRegionsList);
            // if there is a comma(,) in the title, wrap it in quotes
            inputTitle = inputTitle != null && inputTitle.IndexOf(',') != -1 ? $"\"{inputTitle}\"" : inputTitle;
            
            Video video = new Video
            {
                Id = inputId,
                Title = inputTitle,
                Format = inputFormat,
                Length = inputLength,
                Regions = regionsList
            };

            VideoIds.Add(video.Id);
            VideoTitles.Add(video.Title);
            VideoFormats.Add(video.Format);
            VideoLengths.Add(video.Length);
            VideoRegions.Add(video.Regions);
            
            StreamWriter sw = new StreamWriter(_videoFileName, true);
            video.Display();
            sw.WriteLine($"{video.Id},{video.Title},{video.Format},{video.Length},{video.Regions}");
            sw.Close();

            _logger.LogInformation("Video id {Id} added", inputId);
        }
    
        public void MediaSearch()
        {
            Console.WriteLine("Enter title to search...");
            string search = Console.ReadLine();
            int matches = 0;
            
            //couldn't figure out how to get linq to work with my spaghetti class 6 code
            for (int i = 0; i < MovieIds.Count; i++)
            {
                if (search != null && MovieTitles[i].ToLower().Contains(search.ToLower()))
                {
                    Console.WriteLine($"Movie {MovieIds[i]}: {MovieTitles[i]}");
                    matches++;
                }
            }
            for (int i = 0; i < ShowIds.Count; i++)
            {
                if (search != null && ShowTitles[i].ToLower().Contains(search.ToLower()))
                {
                    Console.WriteLine($"Show {ShowIds[i]}: {ShowTitles[i]}");
                    matches++;
                }
            }
            for (int i = 0; i < VideoIds.Count; i++)
            {
                if (search != null && VideoTitles[i].ToLower().Contains(search.ToLower()))
                {
                    Console.WriteLine($"Video {VideoIds[i]}: {VideoTitles[i]}");
                    matches++;
                }
            }
            Console.WriteLine($"{matches} matches found");
        }
    }
}