using System;
using System.Collections.Generic;

namespace AoCUtil
{
    public class Parser
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

        public Symbol PeekSymbol()
        {
            return _lexer.Current.Symbol;
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

        public bool Accept(string str)
        {
            if (PeekSymbol() == Symbol.Ident && _lexer.Current.Raw.Equals(str))
            {
                _lexer.Advance();
                return true;
            }

            return false;
        }

        public List<string> AcceptIdents(Symbol seperator = Symbol.Error)
        {
            List<string> result = new List<string>();

            while (PeekSymbol() == Symbol.Ident)
            {
                result.Add(GetIdent());

                if (seperator != Symbol.Error)
                {
                    Accept(seperator);
                }
            }

            return result;
        }

        public List<int> AcceptNumbers(Symbol seperator = Symbol.Error)
        {
            List<int> result = new List<int>();

            while (PeekSymbol() == Symbol.Ident)
            {
                result.Add(GetNumber());

                if (seperator != Symbol.Error)
                {
                    Accept(seperator);
                }
            }

            return result;
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
