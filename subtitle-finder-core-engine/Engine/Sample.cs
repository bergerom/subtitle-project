using System;
using System.IO;

namespace subtitle_finder_core_engine{
    public class Sample
    {
        // Beginning of the sample (required)
        public TimeSpan StartTime {get; set;} 

        // Ending of the sample (required)
        public TimeSpan EndTime {get; set;}

        // Duration of the sample (auto computed)
        public TimeSpan Duration => EndTime - StartTime;

        // Subtitle file name
        public string SubFileName {get; set;}
    }
}