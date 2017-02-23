using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deadline
{
    public class IOClient : IClient
    {
        public void LearnState(GameState game)
        {
            SaveResultIfBetter(new Result(null));
            throw new NotImplementedException();
        }

        public bool TakeAction(Result r)
        {
            // r.Print()?
            throw new NotImplementedException();
        }

        public void SaveResultIfBetter(Result r)
        {
            r.Quality = 9;
            // take name from executing file
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(location);
            var fileName = Path.GetFileNameWithoutExtension(location);

            // create folder if not exist
            var resultDirectory = Path.Combine(directory, "Output", fileName);
            Directory.CreateDirectory(resultDirectory);

            // find txt file 
            var files = Directory.EnumerateFiles(resultDirectory, "*.txt");
            var file = files.Any() ? Path.GetFileNameWithoutExtension(files.First()) : null;
            var oldFilePath = file == null ? null : Path.Combine(resultDirectory, file + ".txt");
            
            // get its name as quality
            var currentBest = file == null ? Result.WorstQuality : file.ParseAllTokens<long>();

            // if lower than current then delete file and replace with current result
            if(r.Quality > currentBest)
            {
                if (file != null)
                    File.Delete(oldFilePath);


                var newFilePath = Path.Combine(resultDirectory, r.Quality.ToString() + ".txt");
                var stdout = Console.Out;
                using(var writer = new StreamWriter(newFilePath))
                {
                    Console.SetOut(writer);
                    r.Print();
                }
                Console.SetOut(stdout);
            }
        }
    }
}
