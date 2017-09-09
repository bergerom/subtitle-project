using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace subtitle_finder_core_engine
{
    public static class SubtitlesEngineTools
    {
        public static IEnumerable<Sample> SearchVocabularyInSubtitles(string[] vocab, string SubtilesFolder){
            var samplesWithVocab = new List<Sample>();

            String[] files = System.IO.Directory.GetFiles(SubtilesFolder);
            Console.WriteLine($"Files : {files[0]}");

            Parallel.ForEach(files, (currentFile) => {
                String fileName = System.IO.Path.GetFullPath(currentFile);
                var parser = new SubtitlesParser.Classes.Parsers.SubParser();

                using (var fileStream = File.OpenRead(fileName))
                {
                    try
                    {
                        var mostLikelyFormat = parser.GetMostLikelyFormat(fileName);
                        var items = parser.ParseStream(fileStream, Encoding.UTF8, mostLikelyFormat);
                        foreach(var sequence in items){
                            foreach(var sentence in sequence.Lines)
                            {
                                // On enlève la ponctuation
                                var sentenceStripped = Regex.Replace(sentence, @"[^\w\s]", "");
                                sentenceStripped.ToLower();

                                // Recherche du vocabulaire dans la séquence 
                                if (sentenceStripped.Split(" ").Contains(vocab[0]))
                                {
                                    samplesWithVocab.Add(new Sample
                                    {
                                        StartTime = new TimeSpan(sequence.StartTime),
                                        EndTime = new TimeSpan(sequence.EndTime),
                                        SubFileName = fileName
                                    });

                                    var strConcat = "";
                                    foreach(var str in sequence.Lines){
                                        strConcat += str + " ";
                                    }
                                    Console.WriteLine(strConcat);
                                }

                            }
                        }
                    }catch(Exception ex){
                        Console.WriteLine("Parsing of file {0}: FAILURE\n{1}", fileName, ex);
                    }
                }
            });

            return samplesWithVocab;
        }
    }
}