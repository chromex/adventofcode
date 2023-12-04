import aoc

input = """467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598.."""

matrix = aoc.DataMatrix(input)

def Mark(x, y):
    if str.isdigit(matrix[x, y]):
        matrix.Mark(x, y)
        lx = x - 1
        while matrix.IndexInBounds(lx, y) and str.isdigit(matrix[lx, y]):
            matrix.Mark(lx, y)
            lx = lx - 1
        lx = x + 1
        while matrix.IndexInBounds(lx, y) and str.isdigit(matrix[lx, y]):
            matrix.Mark(lx, y)
            lx = lx + 1

def GetNum(val, x, y):
    num = 0
    while matrix.IndexInBounds(x, y) and str.isdigit(matrix[x, y]):
        num = (num * 10) + int(matrix[x, y])
        matrix.ClearMark(x, y)
        x = x + 1
    return num

sum = 0

def MarkNumbers(val, x, y):
    global sum

    matrix.Spread(x, y, Mark)

    nums = matrix.ScanMarks(GetNum)
    matrix.ResetMarks()

    if len(nums) == 2:
        sum = sum + (nums[0] * nums[1])

matrix.Scan(lambda x: x == "*", MarkNumbers)

print(sum)
#print(matrix)