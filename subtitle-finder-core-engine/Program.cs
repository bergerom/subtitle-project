using System;

namespace subtitle_finder_core_engine
{
    class Program
    {
        static void Main(string[] args)
        {
            SubtitlesEngineTools.SearchVocabularyInSubtitles(new string[] {args[1]}, args[0]);
        }
    }
}
