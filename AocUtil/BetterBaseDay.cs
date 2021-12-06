using AoCHelper;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

        public sealed override ValueTask<string> Solve_1()
        {
            return ValueTask.FromResult(P1());
        }

        public sealed override ValueTask<string> Solve_2()
        {
            return ValueTask.FromResult(P2());
        }

        public abstract string P1();
        public abstract string P2();
    }
}
