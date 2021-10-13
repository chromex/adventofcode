using AoCHelper;
using System.IO;
using System.Linq;

namespace AoCUtil
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

        public Parser[] InputParsers => Input.Select(line => new Parser(line)).ToArray();
    }
}
