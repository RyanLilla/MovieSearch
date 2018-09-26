using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearch
{
    public static class FileParsing
    {

        public static List<Movie> ProcessBasicsFile(string path)
        {
            Console.WriteLine();
            Console.WriteLine("Processing...");

            var query = from line in File.ReadLines(path).Skip(1)
                        where line.Length > 1
                        select Movie.ParseFromTSV(line);

            return query.ToList();
        }

        public static List<MovieExtras> ProcessAkasFile(string path)
        {
            var query = from line in File.ReadLines(path).Skip(1)
                        where line.Length > 1
                        select MovieExtras.ParseFromTSV(line);

            return query.ToList();
            Console.WriteLine();

        }
    }
}
