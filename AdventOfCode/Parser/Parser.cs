using System;

namespace AdventOfCode
{
    class Parser
    {
        private Lexer _lexer;

        public Parser(string line)
        {
            _lexer = new Lexer(line);
        }

        public bool Done()
        {
            return _lexer.Current.Symbol == Symbol.EOF;
        }

        public string PeekIdent()
        {
            if (_lexer.Current.Symbol == Symbol.Ident)
            {
                string result = _lexer.Current.Raw;
                return result;
            }

            throw new Exception("Wrong Symbol");
        }

        public string GetIdent()
        {
            if (_lexer.Current.Symbol == Symbol.Ident)
            {
                string result = _lexer.Current.Raw;
                _lexer.Advance();
                return result;
            }

            throw new Exception("Wrong Symbol");
        }

        public int PeekNumber()
        {
            return int.Parse(PeekIdent());
        }

        public int GetNumber()
        {
            return int.Parse(GetIdent());
        }

        public bool Accept(Symbol sym)
        {
            if (_lexer.Current.Symbol == sym)
            {
                _lexer.Advance();
                return true;
            }

            return false;
        }

        public void Burn(int num = 1)
        {
            while (num-- > 0)
            {
                _lexer.Advance();
            }
        }
    }
}
