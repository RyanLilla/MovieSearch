using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            var movies_basics = ProcessBasicsFile(@"title.basics.tsv");
            //var movies_basics = ProcessBasicsFile(@"title.basics - Copy.tsv");
            var movies_akas = ProcessAkasFile(@"title.akas.tsv");
            
            var movieQuery =
                from movie in movies_basics
                join extras in movies_akas
                on movie.Title equals extras.Title
                where movie.TitleType != "tvEpisode" && extras.Region == "US" && movie.YearReleased == "2018"
                select new
                {
                    movie.ID,
                    movie.Title,
                    movie.YearReleased,
                    movie.Genre,
                    extras.Region,
                    extras.Language
                };

            var resultsQuery = movieQuery
                .GroupBy(movie => movie.Title)
                .Select(g => g.OrderBy(movie => movie.Title).First());

            Console.WriteLine($"The total number of results in the search: {resultsQuery.Count()}");

            foreach (var movie in resultsQuery.Take(30))
            {
                if (movie.YearReleased == @"\N")
                    Console.WriteLine($"Movie: {movie.Title} -- Year Released: N/A -- Region: {movie.Region}");
                else
                    Console.WriteLine($"Movie: {movie.Title} -- Year Released: {movie.YearReleased} -- Region: {movie.Region}");
            }
        }

        static List<Movie> ProcessBasicsFile(string path)
        {
            Console.WriteLine("Processing...");
            Console.WriteLine();

            var query = from line in File.ReadLines(path).Skip(1)
                        where line.Length > 1
                        select Movie.ParseFromTSV(line);

            return query.ToList();
        }

        static List<MovieExtras> ProcessAkasFile(string path)
        {
            var query = from line in File.ReadLines(path).Skip(1)
                        where line.Length > 1
                        select MovieExtras.ParseFromTSV(line);

            return query.ToList();

        }
    }
}
