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
                Console.WriteLine("2.) Quit");
                Console.WriteLine();

                string choice = Console.ReadLine().ToLower();
                Command(choice);
            }
        }

        private static void Command(string choice)
        {
            if (choice == "movie" || choice == "1")
                QueryMovies();
            else if (choice == "quit" || choice == "2")
                quit = true;
            else
                Console.WriteLine($"{choice} is not a valid choice, please try again.");
        }

        private static void QueryMovies()
        {
            List<Movie> movies = FileParsing.ProcessBasicsFile(@"movie_data.txt");
            var db = new MovieDb();

            if (!db.Movies.Any())
            {
                foreach (var movie in movies)
                {
                    db.Movies.Add(movie);
                }
                db.SaveChanges();
            }


            var movieQuery =
                from movie in movies
                where movie.YearReleased == "2018"
                select movie;

            var resultsQuery = movieQuery
                .GroupBy(movie => movie.Title)
                .Select(g => g.OrderBy(movie => movie.Title).First());

            Console.WriteLine($"The total number of results in the search: {resultsQuery.Count()}");

            foreach (var movie in resultsQuery.Take(10))
            {
                if (movie.YearReleased == @"\N")
                    Console.WriteLine($"- {movie.Title} -- Year Released: N/A");
                else
                    Console.WriteLine($"- {movie.Title} -- Year Released: {movie.YearReleased}");
            }
            Console.WriteLine();

        }
    }
}
