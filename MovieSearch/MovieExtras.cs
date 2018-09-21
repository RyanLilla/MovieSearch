using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearch
{
    public class MovieExtras
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Region { get; set; }
        public string Language { get; set; }

        internal static MovieExtras ParseFromTSV(string line)
        {
            var columns = line.Split('\t');

            return new MovieExtras
            {
                ID = columns[0],
                Title = columns[2],
                Region = columns[3],
                Language = columns[4]
            };
        }
    }
}
