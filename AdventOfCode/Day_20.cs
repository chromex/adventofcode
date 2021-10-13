using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day_20 : BetterBaseDay
    {
        private Dictionary<int, Tile> tiles = new Dictionary<int, Tile>();
        private const int TOP = 0, RIGHT = 1, BOTTOM = 2, LEFT = 3;

        class Tile
        {
            public int id;
            public Matrix<char> data = new Matrix<char>(10, 10);
            public int[] neighbor = new int[4];

            public string Edge(int edge)
            {
                switch (edge)
                {
                    case TOP: return new string(data.GetRow(0));
                    case RIGHT: return new string(data.GetCol(9));
                    case BOTTOM: return new string(data.GetRow(9));
                    default: return new string(data.GetCol(0));
                }
            }

            public bool HasEdge(string edge)
            {
                for (int idx = 0; idx < 4; ++idx)
                    if (Edge(idx) == edge || Util.RevStr(Edge(idx)) == edge)
                        return true;
                return false;
            }

            public void MarkShared(Tile other)
            {
                for (int idx = 0; idx < 4; ++idx)
                    if (other.HasEdge(Edge(idx))) 
                        neighbor[idx] = other.id;
            }

            public void OrientLeft(string leftEdge)
            {
                while (leftEdge != Edge(LEFT) && leftEdge != Util.RevStr(Edge(LEFT)))
                {
                    RR();
                }

                if (leftEdge != Edge(LEFT))
                {
                    data.FlipV();
                    Util.Swap(ref neighbor[TOP], ref neighbor[BOTTOM]);
                }
            }

            public void OrientTop(string topEdge)
            {
                while (topEdge != Edge(TOP) && topEdge != Util.RevStr(Edge(TOP)))
                {
                    RR();
                }

                if (topEdge != Edge(TOP))
                {
                    data.FlipH();
                    Util.Swap(ref neighbor[LEFT], ref neighbor[RIGHT]);
                }
            }

            private void RR()
            {
                data.RR();
                Util.Swap(ref neighbor[TOP], ref neighbor[LEFT]);
                Util.Swap(ref neighbor[LEFT], ref neighbor[BOTTOM]);
                Util.Swap(ref neighbor[BOTTOM], ref neighbor[RIGHT]);
            }
        }

        public override string Solve_1()
        {
            for (int idx = 0; idx < Input.Length; idx += 12)
            {
                Tile newTile = new Tile() { id = int.Parse(Input[idx].Substring(5, 4)) };
                for (int row = 0; row < 10; ++row)
                    newTile.data.SetRow(row, Input[idx + row + 1].ToArray());

                tiles.Values.ForEach(tile =>
                {
                    tile.MarkShared(newTile);
                    newTile.MarkShared(tile);
                });

                tiles[newTile.id] = newTile;
            }

            double result = 1;
            tiles.Values
                .Where(tile => tile.neighbor.Where(id => id == 0).Count() == 2)
                .ForEach(tile => result *= tile.id);

            return result.ToString();
        }

        public override string Solve_2()
        {
            Matrix<char> map = new Matrix<char>((int)Math.Sqrt(this.tiles.Count) * 8, (int)Math.Sqrt(this.tiles.Count) * 8);
            Tile topLeft = tiles.Values.Where(tile => tile.neighbor[TOP] == 0 && tile.neighbor[LEFT] == 0).First();
            StringBuilder sb = new StringBuilder();
            int mapRow = 0;

            for (int row = topLeft.id; row != 0; row = tiles[row].neighbor[BOTTOM])
            {
                for (int col = tiles[row].id; col != 0; col = tiles[col].neighbor[RIGHT])
                    if (tiles[col].neighbor[RIGHT] != 0)
                        tiles[tiles[col].neighbor[RIGHT]].OrientLeft(tiles[col].Edge(RIGHT));

                for (int prow = 1; prow < 9; ++prow)
                {
                    for (int col = tiles[row].id; col != 0; col = tiles[col].neighbor[RIGHT])
                        sb.Append(new string(tiles[col].data.GetRow(prow)).Substring(1, 8));

                    map.SetRow(mapRow++, sb.ToString().ToCharArray()); 
                    sb.Clear();
                }

                if (tiles[row].neighbor[BOTTOM] != 0)
                    tiles[tiles[row].neighbor[BOTTOM]].OrientTop(tiles[row].Edge(BOTTOM));
            }

            map.FlipH();
            int nFound = 0;
            while (nFound == 0)
            {
                for (int row = 1; row < map.Height - 3; ++row)
                    for (int col = 0; col < map.Width - Nessy[0].Length; ++col)
                        if (CheckNessy(map, row, col))
                            ++nFound;

                map.RR();
            }

            return (map.Rows().Select(row => row.Count(ch => ch == '#')).Sum() - (nFound * 15)).ToString();
        }

        private string[] Nessy = new string[] {
                "                  # ",
                "#    ##    ##    ###",
                " #  #  #  #  #  #   " };

        private bool CheckNessy(Matrix<char> map, int row, int col)
        {
            for (int nrow = 0; nrow < Nessy.Length; ++nrow)
            {
                for (int ncol = 0; ncol < Nessy[0].Length; ++ncol)
                {
                    if (Nessy[nrow][ncol] == '#' && map.Data[row + nrow, col + ncol] != '#')
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
