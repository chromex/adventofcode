import aoc

def TestIVec2():
    print("[Test IVec2]")

    t = aoc.IVec2(3, 0)
    t.RotateLeft()
    t.RotateLeft()
    t.RotateLeft()
    t.RotateLeft()
    assert(t.X == 3 and t.Y == 0)

def TestDM2():
    print("[Test DM2]")

    map = aoc.DM2("abc\n123\n456\ndef")
    assert(map.GetRow(1) == ["1", "2", "3"])
    map.RotateRight()
    map.RotateRight()
    map.RotateLeft()
    map.RotateLeft()
    map.RotateRight()
    assert(map[0,0] == "d")
    assert(map[3,1] == "b")
    assert(map.GetRow(-1) == ["f", "6", "3", "c"])
    assert(map.GetColumn(2) == ["1", "2", "3"])

    map.Reset()

    map.FlipHorizontal()
    assert(map.GetRow(0) == ["c", "b", "a"])
    map.RotateRight()
    map.RotateRight()
    map.RotateRight()
    assert(map.GetColumn(2) == ["4", "5", "6"])

    map.Reset()

    map.FlipHorizontal()
    map.RotateRight()
    map.FlipVertical()
    map.RotateLeft()
    map.FlipHorizontal()
    assert(map.GetRow(1) == ["3", "2", "1"])
    assert(map.GetColumn(-1) == ["a", "1", "4", "d"])

    map = aoc.DM2.MakeFromDims(3, 6)
    assert(map.Height == 6)
    assert(map.Width == 3)

    map.Mark(0, 0)
    map.Mark(2, 3)
    map.Mark(0, 0)
    map.RotateRight()
    map.Mark(5, 0)
    assert(map.SumMarks() == 2)

TestIVec2()
TestDM2()