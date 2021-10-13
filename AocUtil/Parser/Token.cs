namespace AoCUtil
{
    class Token
    {
        public Token(Symbol sym, string raw)
        {
            Symbol = sym;
            Raw = raw;
        }

        public Symbol Symbol { get; private set; }

        public string Raw { get; private set; }

        public override string ToString()
        {
            return $"{Symbol} \"{Raw}\"";
        }
        
        public int AsNumber
        {
            get { return int.Parse(Raw); }
        }
    }
}
