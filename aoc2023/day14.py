import aoc

input = """.O.....##O.O#..O#....##O#.#.#..##O#..O.....#.OO..O.O.O.O...O....#O...OO....##.O..OO.O......O..O.....
....O..#O....#...#OO..#..#..O.....#O......O.O.#..OO..O....#..#.OO....##O...O..##...#.O...O.#...#.O#.
.O.O#.O...O...O....O...O#....#.#O#...O...#.#..........O#...O#O..O..#.OOO..O.O.....O#.........#O...O.
.O....O.OO.#.....#.#O......##O..O.O.##.#..O##O.O.#OO....O.O..O#.OOO..O..#..##.#...#.O.#O.#.#.....O..
..O......O..O.O.#.OO.OO...#.O....#...#O..OO.#.O......O....O.O.....O.#.OO..O.OOO.......O.O...##O...O#
.O.O.........#.O#.#OO##..#OO..OOO#....#....O.O..#.O......#.OO..#..O.O..O.O..#....#.O#..OO###.....#..
.O..#.#.OO.#...........#..O#....O..O.O#.......#.OO.#OO....O.#O.#.....#.OO.#...#..O..O##.O.O##..#.#.O
..O..#...#..OO.#O.#.O.O#O...O.O..O..O#..#..#..O..........O.O#.....O...........O.###..#.#..O#.#.O...#
#..O...#.O..O.O......#O.O##...##...OOO....#.O.O#OO.##...OO.OOO..#.O..##.....O..O.O.#O.O..#.O#O...O.#
OO....#.#...O...O.#...##O..###O.......OO...#.#...O.#..#.O......O....#.O#..#O..O...OO...#..O#..O....O
O#..#O...##O.....#.O.#.O...##......#...#OO#...O.......#.#.OO#.#..#..O#OO...O.......O.#...#.##....O..
#............O#....##.O.O..#O.#.OO.O...OOOO#.......O#O..O.......O#OO#...O.O....#.....#O.#.#..#.OO.O#
#O.O.....O....#O..OO.#O.#....#.##O.O...........O.##.#O##O...O#..O.O.O#......O...O.#....#OO#.#..O..##
.O..#O.O.#....O.........#.....OOO.......O.#O.....#OOOO#.O##O.#.O...O..#OO...O.....O..#.#......OO.O.#
O..O...O......O.O...#..#...#.OO.#.OO.O..O...#...#...O.###....#O#.O..O..#O..#.O.##.......O#..#O......
#..O...#...#OOO.#.###.#....#..O.O.#.#......OO.#.#...#....#.OO.O..#.O...#.#.....O.#O.O.#O..O....O.O..
OOO.#...##.OO.O....#.O.###.#.....O.OO#..#....OO...O#.#.O..#O.#..#.O..O....#O.OO#...O..#.....#O...O.O
.#.#...........#.............O.#..##.#O.#.O......#..O..OO.##O.O##.....#..O.O..O.OO..OO.#O.O.##O.O...
..O......O..##O.O...##......##OO..#O.#..#..OO...#...O.O..O..O.O.O##.#O.O#.#.O#....##.O.....O..#.O...
.#...#O.#.#....O..O..O#.O.....O..O.#.O....O.O...#.O#......O..O.O...#.#...O..##O...#.#...O.##..#..O#.
.O#..O.#....#.OO.#.....#O....O#O.#..#.O.....#......#O....O.........#.#O.OOOO#O.OOOO.#.....O.#.O..#OO
#..#.....O.....#.....##.O....##.O.O#OOO..O....O...#....O.O.....#.O#.O....O.O....#..O....O###.#..O#O.
...O...#..#O.O.O.......O.....O#O....#.#OO#......#..OO.....#.#...#...OO.O#O#O.#.#O....#..O....O...OO.
..#.##.#.#..............#O...#O#..##O.#.....O.#.#......O.....O#..O.O.O.#OOO####O.....O..##.O.OO.#..#
O...O#.O....#.O...........#......##.O.O#.....#.O..OO#...O#.##O..##.....#.O.O.....O#.#O.O..O...OO#...
OO.........##....##.O.O.OO.....O...........O.....O..#..#.OO........#O...OO.....#.O.........#........
#..#.##O#.O#O..#..O...#.#.O.OO...##O..#..O.O........#.#O#..#..O..#.O.....O...O..O.O....#...O#....#..
O.#.......O..O#O...OOOO#OO..O..#....#O..#...OO..#....O......#..#....O.O#.#....O.OO..O....O....OO..#.
O.OO..........#..O.#O#.#.#O...##..#..O##..##O#.OO......O..#.O.O.O.OO....#.....OOO#.O.#.O.#O##.#OO.#O
#..O....##...#.O.OO.O.O......#.OO...#......O........OO.#O..#.......OO..O..O...#.O......O.#O#O#...#OO
#...#..#OOOO#.#O#O.O#.O.#.###...O..#.....O..............O.....O.#.O....O.O....#...........O...O#.O..
.#O.O.OOO.....O...O...#.........#..O..O.#..#.O#....O#O..OO.........OO#.........#..OO.O.......#...O..
..#...O...#O...O..#.....O..O.#.......O...O.O.O.....O......O...#.....##.O.O#..O.#.O..#.O..O..OO......
.O.O..O..#O..O#OO#OO.#....O...#.O#.O.#...###........O.....O..O.OOO.O...#....O.O....O..OO.###...O...O
.#.....O..OOOO..#...#.#.O......O..O..#.##.#...O#..O.#.#.O#.....##...##O.......##..O.O.O.......#..OO#
...O...O...O..O...#..O#....O....#......O..OO#...O...#.#O.##.#..O..#.##O..#.#...O.....#.#O.O..O.O##..
.#OO#.....#O...O.#.OO......OO...O..O..O...#............#.OO##..OO..O.O......O..O...O...O...O...O....
...O....O..#.O.O###..##.O..O.O.#...#.OO.O..O.O.O.O.#.O..#.O##.......O..#......O.O..O.OOO..##O....#..
O...O.#..........O..OO.O#...#OO.OO..#O#.......O..#....O.......O.#OO.O.O.O.O##.OO..##...O#..OO#..#.#.
.O....#.#O............OOO.......#.O..#.#...#O......#....#.##..O.#.##OO..OO.#.#......O.O.....#.OO...O
..O...O#...O..O..#..#O..O#..O#..O.OO..O#.#...O####..O...O#..##......O#....O....O..O...O........O..O.
O.O.#..#.#..##..#.#....#..#..#.###.O#.O....OO...#....O#.....#.........#.###..#.#OO..#O...O...O.#....
..OO.#.O.....O.O..#.......O...O..##.#.#O..#....#OO.O....O......#.OO#....O..#O.O...#.OO#O.#....O..O.#
#...O#.O...O..O..O.O#...O.O.......OO....OO..O#.O..............O..O#.#...O#....OO..OO..O....#.#O.O..#
O.O.##..O#...#O......O...O...OOO.....#....O#...O#.#.#........O..##..##.O..##O.O..O..#............O.#
.....#....O..O.O...####...#.O.#..#.#..#....#...#.OO.#..##...OOO.#..#......O.OOO.....#O#.......OO....
...#.O#.O...#.OOO#.......OO.#..O..O##.#OO.O.#.O.O.O#.....#.O.........O...O....#.O#....#.#O..O..#..#.
...#..O......O..#.O...#.....#....O.....OOO..O..O##O#...O...O.#...O....#OOO##..........O....#O.......
...O.O.#.#...O.O#O#.#O#O....#....O..O....#.O.O#.....#....O..O..............O...##O.O....O..#....#..O
O.....O..#O....O..O##O..#..#.O.OO..#...##........#.#.#.O..###...#O.##..##..#O..O.OOO...O.O.....O....
..##.......#...O.O.O#O.#...O..OO#OOO.#..O....#...O..#....#.....OO..OO.#....O.....#.##...O.#..O...#O#
O##....O.O..##OO.#.#.O#..##............OO.O.O.#.....#..OO...#...O..##O..........#.......O..OOO...O.#
#OO.......O.O..O...#...O#.#...O..O.....O..OO.O...#OO...O....O...O.O...O....O......#.#.O.OO.....#....
...#..#OO..#..O.OO.#..#.....O..#O..#....#..#...O...O...OO..O#..O...O.........#...OO.O.#OO#.#.O..OO.#
..O#.O.O.#.#.....#...O..O#O..#..#..#.#...O..O.#.O.OO..OOO.#..OO.#.#.....OO...OO#O#.#..........O.##O.
...O#..O..O..O..#OO.###..O.........O#...OO.....O..O#....#O.O.O..O#.#...O#OO.O..#..O.O.O...O.OO.....O
.....O.#.O..O...##..#.O#O.#.......#O.OO#...O...##.OO#...OOO....#..#..#O...O.O#O.O..O.#OO...#..#.O#.O
....#...#O.O...##.#...#...##.O....#.#.O.O..##.O...#O...O#OO#..#.O.....#O#O.O.O#O...#.#.O.O..#...O..O
.O...O....O.....#.#O.#.##.##..O#..OO#O..#..O.#..O..O#....O#..#..OOOO.#.O..OO.O.##......#...#......#O
O....O.O...O#.OOO.#..O.......#..#....O#O........###..O..#.O.O.O.......#....O.O.O.##.O..#..O....O..#.
..O..OO...................OO...#........#.#...O..#.O.......#...O.......O..O#...O.#.O#....O#.O...#...
.O..#......O#O#..#......OOO........OOO........O#....O#.OO.#.......O#.OO.......##.....OO.O......O.O..
.#O..#O#O#..###.............##....#..O...#.O#O..O###.O..##..#..#OOO#.#...O.#.O.....O.#...........#..
.....O..O..OO.O.......OO.O.....#..##.##OOO#.#O..O.#....OO#OO...#...##...#.O...O....OO.O.##..O#..###.
OOO#.....#..#.O......O...O...#O..#....O.OO.#...O..........O.....O..O.....OO...#..O..O.#OO.O..OO.#...
O.OO.#...#.#...#...OOO.......O#....O....#....O.#...O.#.##.#OO.O..##......O.##OO#.......#.O...O.OOO..
.....#.........#.............O##.##..O..OOOO....#....O..#..O..#...O...OO.#O....#O.....O#O#......#..#
.#.O#O#.O#OOO.......O.O......O.#.#....#O..O..#..#.......#O.O#.....###..O.#.O......#.#.O#......#O....
..#O#.#.O.#O.OO.#O.O####.#..##.O.....#...#O#..#O#O#....O....OO#....O#.O...O.........#O##.O.O....#...
.OO..#O..O#.....OO....#O.O.....OO#...O.....OO.......#..O#...OO.#.O..O.O.#O.OO#.#O.....O...O..O.O.O..
..##OOO.O.....##..O.#.#.##..#...##.O..O....OO...##O..#.OO....#.O#.......O..O..O.#.#.#....O.O.O#O..O.
O#.#..#.##.O..O.O.....OO#....OOO.O.O.OO.....O...###.......#O...#....#.OOO.#O..OO...O..#..O.O.O.....#
OO#.....OO.O.#OO...#O...O.....O..O.#....O............#O.O.O....#.......O.O..OOO...#...#...#.........
......#..O.#.....O....OO..##..##........O.#O..........#OOOO......O......O.O##..O#O#O.O..#.#O.....#..
..O..OOO.........#.O..O..#..O..O###..O..##...O..#.O.O..#.............O.....##O.OO..#.#..O.##O.##O.O.
..#...#..O........O.O.#..##.O.#O......O.O.#OO..O..#..#O.O.....#.##..##..#......OO..#.O...#..#.O.O#..
..#...#..O......#......#..O.##.#....OO..#...#.....O.O.O..#.##...##..OO......#.OO....O..O..O.O.#.O...
......O...#O......O.O..OO.O...#O..##..#...#..#....##........##O.O......O.O...#O....OO...............
.....O..#.##....OO.O...O..O..O.O...#....O.#.O....#.#..OO......OO#...#.###..#O...O....O..##O.#..OO.O.
...OO...#...##.##.#..#...#.O..#OOO.....#.#..O...O.##O.#.#..#O.#O.#......O#......OOO.O#.....#......O.
OOO.O.O...###...#...#.#...OOO.O...O..O...#....O.OO...O..##..O........#.O.#....#......##.#O..#.#O#O..
....OO#.#.....O..O..#......OO.........O....O.....O..O#.#..##O.....#.#.#....#..O#..#OO.O...#.OO##O.OO
O.O..#..O.......O.O....O..OO.O...OO.O.......O###OOO##.O...O...#O#OO......O..O..O...#....##...O..O...
O#.O#.....O...O.O...#.##.#.......###....O.O.##......O....O..#O..#O#.O..##OO..O.###..O.OO#...#....##O
O..O..O............OO.....O.O..O...#............#O..O..O#...O..O.O##........#.#.....#....O#O.......#
............O....O#.OO...##..OO#O...#.....#OO..O#.......##O.O#...OO....#..O.....#..O...O.OO#O.O...O.
.#...#...OO...###.OO.OO....#..O...#...O.O...O....#........O.#..O#.OO..#....#O..O##.......O...#..##..
#.O.O#.O..#....#...#O#...OO..#...O.O#.OO.##...............O.#......O.....#..O..#..O..#O..O..##.##.O.
O..##.....OO.O##.O..#.O........OOOO...#..##.........#.#..O.O.OO#..O##O#....O.O#.....O.....O#..#...##
O......#.O...#...O#.#...O#........O..#....#..#.O.......O.......#.....#..O......#..O#..O#...#.#O.O...
..........OO###.#.O.O..#...##..##.#.........O#O..O.#O.......O..O#...#..O.O..#.O....#..O..OO.O......O
.OOOO..O....O.....#.....#.#.O.#O.#.O....##...O.O#O.OO.....O.O#.#...#..#.#O.O.##...O...OO..O....OO#O.
O...###O...###O...#...#O...#....#..O#..#O#....O.##..#.....#.#OO#.O...O.##O#...O.....#.......#.O.....
O...#.OO.O##.O.....#.....O..O...#.O...O.#.....#.O....O.#...#OOOO..#.#O...O...##..##..........O....O#
..OOO..O..#...OO....##....O....O...O....#..O..##.O...O.O.....#.#.O.#.#..O#O.#.....#.O.#.#.....O.O#..
#....O.OO##OO....#...#.....#....###.#..O..O.O...#.....#..O.........#O#...#............OO....#.OO..O.
.O.O#.#O#.O..O........#..O#...O.#..##O....O#O..O#O...#.O...#.....#.#......#......O...#...#..O..#....
.#..O..O..#O..OO....OO#.#...O.....O....#....O..O#...OOOO#.#...#..##.O.....O.....#..O.O..#...OO#OO..#
....#O.##.O.....#..O..OOO.#.O..#.O.....#O.O.##..#OO.......O..##...O#.#.OO..O..#...#......#...O.O....
#......#.#.....#O...O..#..#..###..O...##.OOO.O...#.O..#..##.O...#..O..O....O.......#OO.....#....OO.."""

plat = aoc.DM2(input)

def FastTiltNorth(plat):
    for x in range(plat.Width):
        top = 0
        while plat[x, top] == "O":
            top += 1

        for y in range(0, plat.Height):
            v = plat[x, y]
            if v == "#":
                top = y + 1
                while top < plat.Height and plat[x, top] == "O":
                    top += 1
            elif v == "O":
                if top < y:
                    plat[x, top] = "O"
                    plat[x, y] = "."
                    top += 1

def Hash(plat):
    sum = 0
    for y in range(0, plat.Height):
        for x in range(0, plat.Width):
            if plat[x, y] == "O":
                sum += plat.Height - y
    return sum

def Cycle(plat):
    FastTiltNorth(plat)
    h = Hash(plat)
    plat.RotateRight()
    FastTiltNorth(plat)
    h = (h * 100000000) + Hash(plat) 
    plat.RotateRight()
    FastTiltNorth(plat)
    h = (h * 100000000) + Hash(plat) 
    plat.RotateRight()
    FastTiltNorth(plat)
    h = (h * 100000000) + Hash(plat) 
    plat.RotateRight()
    return h

# 114001160012900121
res = {}
minI = 0
maxI = 0
for i in range(1, 1000000000):
    v = Cycle(plat)
    if res.get(v) != None:
        minI = res[v]
        maxI = i
        break
    res[v] = i

dif = maxI - minI

minI += int((1000000000 - maxI) / dif) * dif

while minI < 1000000000:
    Cycle(plat)
    print(Hash(plat))
    minI += 1

print(Hash(plat))