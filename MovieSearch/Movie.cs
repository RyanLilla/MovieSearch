using System;
namespace MovieSearch
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string YearReleased { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        

        internal static Movie ParseFromTSV(string line)
        {
            var columns = line.Split('\t');

            return new Movie
            {
                Title = columns[0],
                YearReleased = columns[1],
                Runtime = columns[2],
                Genre = columns[3]
            };
        }
    }
}
