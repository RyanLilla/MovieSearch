using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearch
{
    public static class StartingUserInterface
    {

        public static bool quit = false;

        public static void CommandLoop()
        {
            while (!quit)
            {
                Console.WriteLine("What kind of title would you like to search for?");
                Console.WriteLine("1.) Movie");
                Console.WriteLine("2.) TV Show");
                Console.WriteLine("3.) Quit");
                Console.WriteLine();

                string choice = Console.ReadLine().ToLower();
                Command(choice);
            }
        }

        private static void Command(string choice)
        {
            if (choice == "movie" || choice == "1")
                QueryMovies(choice);
            else if (choice.Contains("tv") || choice == "2")
                QueryTv(choice);
            else if (choice == "quit" || choice == "3")
                quit = true;
            else
                Console.WriteLine($"{choice} is not a valid choice, please try again.");
        }

        private static void QueryMovies(string choice)
        {
            List<Movie> movies_basics = FileParsing.ProcessBasicsFile(@"title.basics.tsv");
            List<MovieExtras> movies_akas = FileParsing.ProcessAkasFile(@"title.akas.tsv");
            //List<Movie> movies_basics = FileParsing.ProcessBasicsFile(@"title.basics - Copy.tsv");

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

            foreach (var movie in resultsQuery.Take(10))
            {
                if (movie.YearReleased == @"\N")
                    Console.WriteLine($"- {movie.Title} -- Year Released: N/A -- Region: {movie.Region}");
                else
                    Console.WriteLine($"- {movie.Title} -- Year Released: {movie.YearReleased} -- Region: {movie.Region}");
            }
            Console.WriteLine();

        }

        private static void QueryTv(string choice)
        {

            List<Movie> movies_basics = FileParsing.ProcessBasicsFile(@"title.basics.tsv");
            List<MovieExtras> movies_akas = FileParsing.ProcessAkasFile(@"title.akas.tsv");
            //List<Movie> movies_basics = FileParsing.ProcessBasicsFile(@"title.basics - Copy.tsv");

            var tvQuery =
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

            var resultsQuery = tvQuery
                .GroupBy(tvShow => tvShow.Title)
                .Select(g => g.OrderBy(tvShow => tvShow.Title).First());

            Console.WriteLine($"The total number of results in the search: {resultsQuery.Count()}");

            foreach (var tvShow in resultsQuery.Take(10))
            {
                if (tvShow.YearReleased == @"\N")
                    Console.WriteLine($"- {tvShow.Title} -- Year Released: N/A -- Region: {tvShow.Region}");
                else
                    Console.WriteLine($"- {tvShow.Title} -- Year Released: {tvShow.YearReleased} -- Region: {tvShow.Region}");
            }
            Console.WriteLine();
        }
    }
}
