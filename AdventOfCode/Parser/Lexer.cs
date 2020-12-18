using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Lexer
    {
        private string _rawData;
        private int _beginIndex;
        private int _endIndex;
        private Dictionary<char, Symbol> _simpleSymbols = new Dictionary<char, Symbol>()
        {
            { '[', Symbol.LBracket },
            { ']', Symbol.RBracket },
            { ',', Symbol.Comma },
            { '(', Symbol.LParen },
            { ')', Symbol.RParen },
            { ':', Symbol.Colon },
            { '.', Symbol.Period },
            { '+', Symbol.Plus },
            { '-', Symbol.Minus },
            { '*', Symbol.Mult },
            { '/', Symbol.Div }
        };

        public Lexer(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                throw new ArgumentNullException();
            }

            _rawData = new string(line);

            Advance();
        }

        public Token Current { get; private set; }

        public void Advance()
        {
            if (Current?.Symbol == Symbol.EOF)
            {
                return;
            }

            _beginIndex = FindNextTokenStart();
            _endIndex = _beginIndex + 1;

            if (_beginIndex == -1)
            {
                Current = new Token(Symbol.EOF, string.Empty);
            }
            else if (_simpleSymbols.ContainsKey(_rawData[_beginIndex]))
            {
                Current = new Token(
                    _simpleSymbols[_rawData[_beginIndex]],
                    $"{_rawData[_beginIndex]}");
            }
            else
            {
                _endIndex = FindNextNonIdent();
                Current = new Token(
                    Symbol.Ident,
                    _rawData.Substring(_beginIndex, _endIndex - _beginIndex));
            }
        }

        private int FindNextTokenStart()
        {
            int index = _endIndex;

            do
            {
                if (index >= _rawData.Length)
                {
                    return -1;
                }

                if (!Char.IsWhiteSpace(_rawData[index]))
                {
                    if (_rawData[index] == '#')
                    {
                        ++index;

                        while (_rawData[index] != '\n')
                        {
                            ++index;
                        }
                    }
                    else
                    {
                        return index;
                    }
                }

                ++index;
            } while (true);
        }

        private int FindNextNonIdent()
        {
            int index = _endIndex;

            do
            {
                if (index >= _rawData.Length)
                {
                    return index;
                }

                char currentChar = _rawData[index];

                if (currentChar == '#' || Char.IsWhiteSpace(currentChar) || _simpleSymbols.ContainsKey(currentChar))
                {
                    return index;
                }

                ++index;
            } while (true);
        }
    }
}
