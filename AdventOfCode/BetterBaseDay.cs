using AoCHelper;
using System.IO;

namespace AdventOfCode
{
    public abstract class BetterBaseDay : BaseDay
    {
        private string[] _lines;

        public string[] Input
        {
            get 
            {
                if (_lines == null)
                {
                    _lines = File.ReadAllLines(InputFilePath);
                }

                return _lines;
            }
        }
    }
}
