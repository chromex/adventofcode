using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_24 : BetterBaseDay
    {
        private Vec2 MoveEast(Vec2 v) => new Vec2(v.X + 1, v.Y);
        private Vec2 MoveWest(Vec2 v) => new Vec2(v.X - 1, v.Y);
        private Vec2 MoveSouthEast(Vec2 v) => v + new Vec2(Math.Abs(v.Y % 2), -1);
        private Vec2 MoveSouthWest(Vec2 v) => v + new Vec2(-Math.Abs((v.Y + 1) % 2), -1);
        private Vec2 MoveNorthEast(Vec2 v) => v + new Vec2(Math.Abs(v.Y % 2), 1);
        private Vec2 MoveNorthWest(Vec2 v) => v + new Vec2(-Math.Abs((v.Y + 1) % 2), 1);

        private Dictionary<string, bool> map;

        public override string Solve_1()
        {
            map = new();

            foreach (string line in Input)
            {
                Vec2 pos = new Vec2(0, 0);

                for (int idx = 0; idx < line.Length; ++idx)
                {
                    switch (line[idx])
                    {
                        case 'e': pos = MoveEast(pos); break;
                        case 'w': pos = MoveWest(pos); break;
                        case 's':
                            if (line[++idx] == 'e')
                                pos = MoveSouthEast(pos);
                            else
                                pos = MoveSouthWest(pos);
                            break;
                        case 'n':
                            if (line[++idx] == 'e')
                                pos = MoveNorthEast(pos);
                            else
                                pos = MoveNorthWest(pos);
                            break;
                    }
                }

                if (map.ContainsKey(pos.ToString()))
                    map[pos.ToString()] = !map[pos.ToString()];
                else
                    map[pos.ToString()] = true;
            }

            return map.Values.Count(val => val).ToString();
        }

        private void Ensure(Dictionary<string, int> neighbors, Vec2 pos)
        {
            string key = pos.ToString();
            if (!neighbors.ContainsKey(key))
                neighbors[key] = 0;
        }

        private void Increment(Dictionary<string, int> neighbors, Vec2 pos)
        {
            Ensure(neighbors, pos);
            neighbors[pos.ToString()] += 1;
        }

        public override string Solve_2()
        {
            for (int count = 0; count < 100; ++count)
            {
                Dictionary<string, int> neighbors = new();
                
                foreach (var kvp in map)
                {
                    if (kvp.Value)
                    {
                        Vec2 pos = new Vec2(kvp.Key);
                        Ensure(neighbors, pos);
                        Increment(neighbors, MoveEast(pos));
                        Increment(neighbors, MoveWest(pos));
                        Increment(neighbors, MoveSouthEast(pos));
                        Increment(neighbors, MoveSouthWest(pos));
                        Increment(neighbors, MoveNorthEast(pos));
                        Increment(neighbors, MoveNorthWest(pos));
                    }
                }

                foreach (var kvp in neighbors)
                {
                    bool blackTile = false;
                    if (map.ContainsKey(kvp.Key))
                        blackTile = map[kvp.Key];

                    if (blackTile)
                    {
                        map[kvp.Key] = kvp.Value == 1 || kvp.Value == 2;
                    }
                    else
                    {
                        map[kvp.Key] = kvp.Value == 2;
                    }
                }

                Console.WriteLine($"Day {count + 1}: {map.Values.Count(val => val)}");
            }

            return map.Values.Count(val => val).ToString();
        }
    }
}
