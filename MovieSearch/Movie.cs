using System;
namespace MovieSearch
{
    public class Movie
    {
        public string ID { get; set; }
        public string TitleType { get; set; }
        public string Title { get; set; }
        //public int YearReleased { get; set; }
        public string YearReleased { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        

        internal static Movie ParseFromTSV(string line)
        {
            var columns = line.Split('\t');
            //var yearColumn = Int32.TryParse(columns[5], out int year);

            return new Movie
            {
                ID = columns[0],
                TitleType = columns[1],
                Title = columns[2],
                YearReleased = columns[5],
                //YearReleased = year,
                Runtime = columns[7],
                Genre = columns[8]
            };
        }
    }
}
