using AoCUtil;
using System;

namespace aoc2015
{
    class Day_22 : BetterBaseDay
    {
        private record Player
        {
            public int Health { get; set; }
            public int Mana { get; set; }
            public int Shield { get; set; }
            public int Recharge { get; set; }
            public int Poison { get; set; }
        }

        private record Boss
        {
            public int Health { get; set; } 
            public int Damage { get; init; }
        }

        private int _min = int.MaxValue;

        private static void TickEffects(Player p, Boss b)
        {
            if (p.Recharge > 0)
            {
                p.Mana += 101;
                --p.Recharge;
            }

            if (p.Poison > 0)
            {
                b.Health -= 3;
                --p.Poison;
            }

            if (p.Shield > 0)
            {
                --p.Shield;
            }
        }

        private void BossTurn(Player p, Boss b, int spent)
        {
            if (b.Health <= 0)
            {
                _min = Math.Min(_min, spent);
                return;
            }

            p = p with { };
            b = b with { };

            TickEffects(p, b);

            if (b.Health <= 0)
            {
                _min = Math.Min(_min, spent);
                return;
            }

            if (p.Shield > 0)
            {
                p.Health -= 1;
            }
            else
            {
                p.Health -= b.Damage;
            }

            if (p.Health <= 0)
            {
                return;
            }

            PlayerTurn(p, b, spent);
        }

        private void PlayerTurn(Player p, Boss b, int spent)
        {
            if (p.Mana < 53 || spent > _min)
            {
                return;
            }

            p = p with { };
            b = b with { };

            if (_hard)
            {
                p.Health -= 1;
                if (p.Health <= 0)
                {
                    return;
                }
            }

            TickEffects(p, b);

            if (b.Health <= 0)
            {
                _min = Math.Min(_min, spent);
                return;
            }

            // Player attacks
            for (int index = 0; index < 5; ++index)
            {
                Player pa = p with { };
                Boss ba = b with { };

                switch (index)
                {
                    case 0:
                        {
                            pa.Mana -= 53;
                            ba.Health -= 4;
                            BossTurn(pa, ba, spent + 53);
                        }
                        break;

                    case 1:
                        if (pa.Mana >= 73)
                        {
                            pa.Mana -= 73;
                            pa.Health += 2;
                            ba.Health -= 2;
                            BossTurn(pa, ba, spent + 73);
                        }
                        break;

                    case 2:
                        if (pa.Mana >= 113 && pa.Shield == 0)
                        {
                            pa.Mana -= 113;
                            pa.Shield = 6;
                            BossTurn(pa, ba, spent + 113);
                        }
                        break;

                    case 3:
                        if (pa.Mana >= 173 && pa.Poison == 0)
                        {
                            pa.Mana -= 173;
                            pa.Poison = 6;
                            BossTurn(pa, ba, spent + 173);
                        }
                        break;

                    case 4:
                        if (pa.Mana >= 229 && pa.Recharge == 0)
                        {
                            pa.Mana -= 229;
                            pa.Recharge = 5;
                            BossTurn(pa, ba, spent + 229);
                        }
                        break;
                }
            }
        }

        public override string P1()
        {
            Player p = new() { Health = 50, Mana = 500 };
            Boss b = new() { Health = 55, Damage = 8 };

            PlayerTurn(p, b, 0);

            return _min.ToString();
        }

        private bool _hard = false;

        public override string P2()
        {
            _min = int.MaxValue;
            _hard = true;

            Player p = new() { Health = 50, Mana = 500 };
            Boss b = new() { Health = 55, Damage = 8 };

            PlayerTurn(p, b, 0);

            return _min.ToString();
        }
    }
}
