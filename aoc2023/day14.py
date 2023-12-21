import aoc

input = """O....#....
O.OO#....#
.....##...
OO.#O....O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#...."""

plat = aoc.DataMatrix(input)

def TiltNorth(plat):
    moved = 0

    for y in range(1, plat.Height):
        for x in range(0, plat.Width):
            if plat[x, y] == "O" and plat[x, y - 1] == ".":
                plat.Set(x, y, ".")
                plat.Set(x, y - 1, "O")
                moved += 1

    return moved

# ..0.#.0..00
# 0...#000...

# This needs datamatrix to store rows in lists...
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
                    plat.Set(x, top, "O")
                    plat.Set(x, y, ".")
                    top += 1

FastTiltNorth(plat)

print(plat)

sum = 0
for y in range(0, plat.Height):
    for x in range(0, plat.Width):
        if plat[x, y] == "O":
            sum += plat.Height - y

print(sum)