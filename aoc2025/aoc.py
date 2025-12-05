import functools
import itertools
import math
import re
import dataclasses
import heapq
import typing
import sys

def Pairwise(fn, lstA, lstB):
    return list(fn(x, lstB[ind]) for ind,x in enumerate(lstA))

def GetInts(str, spl=" "):
    return list(map(lambda x: int(x), filter(lambda x: x != "", str.split(spl))))

def GetFloats(str, spl=" "):
    return list(map(lambda x: float(x), filter(lambda x: x != "", str.split(spl))))

CLASS_STORE = {}
def _GetProcClass(name, desc):
    if not name in CLASS_STORE:
        fields = desc.split(" ")
        CLASS_STORE[name] = dataclasses.make_dataclass(name, fields)
    
    return CLASS_STORE[name]

def ParseInputLine(name, desc, str):
    """Parses the ints in str into a class described by name and desc.
    
    @param name: String name of the class. Nothing special.
    @param desc: String containing the class field names separated by empty spaces.
    
    Ex: 
        inputLine = "p=78,57 v=-87,54"
        ParseInputLine("Robot", "x y vx vy", inputlines)"""
    lst = re.findall(r'-?\d+', str)
    
    return _GetProcClass(name, desc)(*map(lambda x: int(x), lst))

def ParseInputLines(name, desc, lines):
    """Parses each line in lines as defined by ParseInputLine"""
    res = []
    for line in lines:
        res.append(ParseInputLine(name, desc, line))
    return res

def SplitEmptyLines(str):
    """Groups input lines as separated by empties. Useful for puzzle inputs that have multiple entries
    that are designated by contiguous placement broken by empty lines. """
    res = []
    cur = ""

    for line in str.splitlines():
        if line != "":
            cur += line + "\n"
        else:
            res.append(cur)
            cur = ""
    
    if cur != "":
        res.append(cur)

    return res

class Mat3:
    """Under tested. Use with caution."""
    def __init__(self, data=None):
        if data != None:
            self._data = []
            rows = data.split(";")
            assert(len(rows) == 3)
            for r in rows:
                self._data.append(GetFloats(r))
                assert(len(self._data[-1]) == 3)
        else:
            self._data = [[0, 0, 0], [0, 0, 0], [0, 0, 0]]
    
    def __str__(self):
        res = []
        for row in self._data:
            res.append(f"[{row}]")
        return "\n".join(res)
    
    def __getitem__(self, key):
        return self._data[key[1]][key[0]]
    
    def __setitem__(self, key, value):
        self._data[key[1]][key[0]] = value
    
    @staticmethod
    def MkIdentity():
        return Mat3("1 0 0;0 1 0;0 0 1")
    
    @staticmethod
    def MkRotLeft90():
        return Mat3("0 -1 0;1 0 0;0 0 1")
    
    @staticmethod
    def MkRotRight90():
        return Mat3("0 1 0;-1 0 0;0 0 1")
    
    @staticmethod
    def MkTransform(x, y):
        return Mat3(f"1 0 {x};0 1 {y};0 0 1")
    
    @staticmethod
    def MulMat(left, right):
        res = Mat3()
        
        res[0, 0] = left[0, 0] * right[0, 0] + left[1, 0] * right[0, 1] + left[2, 0] * right[0, 2]
        res[1, 0] = left[0, 0] * right[1, 0] + left[1, 0] * right[1, 1] + left[2, 0] * right[1, 2]
        res[2, 0] = left[0, 0] * right[2, 0] + left[1, 0] * right[2, 1] + left[2, 0] * right[2, 2]

        res[0, 1] = left[0, 1] * right[0, 0] + left[1, 1] * right[0, 1] + left[2, 1] * right[0, 2]
        res[1, 1] = left[0, 1] * right[1, 0] + left[1, 1] * right[1, 1] + left[2, 1] * right[1, 2]
        res[2, 1] = left[0, 1] * right[2, 0] + left[1, 1] * right[2, 1] + left[2, 1] * right[2, 2]

        res[0, 2] = left[0, 2] * right[0, 0] + left[1, 2] * right[0, 1] + left[2, 2] * right[0, 2]
        res[1, 2] = left[0, 2] * right[1, 0] + left[1, 2] * right[1, 1] + left[2, 2] * right[1, 2]
        res[2, 2] = left[0, 2] * right[2, 0] + left[1, 2] * right[2, 1] + left[2, 2] * right[2, 2]

        return res
    
    @staticmethod
    def MulVec(mat, vec):
        assert(len(vec) == 2)
        res = [0, 0, 0]
        res[0] = mat[0, 0] * vec[0] + mat[1, 0] * vec[1] + mat[2, 0] * 1
        res[1] = mat[0, 1] * vec[0] + mat[1, 1] * vec[1] + mat[2, 1] * 1
        res[2] = mat[0, 2] * vec[0] + mat[1, 2] * vec[1] + mat[2, 2] * 1
        return res[0:2]

class Direction:
    UP = NORTH = 0
    RIGHT = EAST = 1
    DOWN = SOUTH =  2
    LEFT = WEST = 3

    @staticmethod
    def TurnLeft(dir):
        assert(dir >= 0 and dir < 4)
        return ((dir - 1) + 4) % 4
    
    @staticmethod
    def TurnRight(dir):
        assert(dir >= 0 and dir < 4)
        return (dir + 1) % 4
    
    @staticmethod
    def MovePos(x, y, dir):
        assert(dir >= 0 and dir < 4)
        if dir == Direction.UP: return (x, y - 1)
        elif dir == Direction.RIGHT: return (x + 1, y)
        elif dir == Direction.DOWN: return (x, y + 1)
        else: return (x - 1, y)

class StackPrint:
    depth = 0
    enabled = True

    def __init__(self, tabWidth=4):
        self.tab = " " * tabWidth

    def Push(self):
        self.depth += 1

    def Pop(self):
        self.depth = max(self.depth - 1, 0)

    def Print(self, msg):
        if self.enabled:
            print(f"{self.tab * self.depth}{msg}")

class _DataMatrixStore:
    def __init__(self, data: str, convert=None):
        self.raw = data
        
        lines = data.splitlines()
        self.__width = len(lines[0])
        self.__height = len(lines)

        self.data = []

        if convert == None:
            convert = lambda x: x

        for y in range(self.__height):
            for x in range(self.__width):
                self.data.append(convert(lines[y][x]))

    def __GetIndex(self, x, y): return (y * self.__width) + x

    def __DecomposeIndex(self, index): return (index % self.__width, int(index / self.__width))

    @property
    def Height(self): return self.__height

    @property
    def Width(self): return self.__width

    def __getitem__(self, key): 
        idx = self.__GetIndex(key[0], key[1])
        assert(idx >= 0 and idx < len(self.data))
        return self.data[idx]
    
    def __setitem__(self, key, value):
        self.data[self.__GetIndex(key[0], key[1])] = value

    def __str__(self):
        output = []

        for y in range(self.Height):
            output.append("")
            for x in range(self.Width):
                output[-1] += self.__getitem__((x, y))

        return "\n".join(output)

    def SetAll(self, value):
        for i in range(len(self.data)):
            self.data[i] = value
    
class DataMatrix:
    """The ultimate data matrix. Support for fast updates, rotations, flips, overlays, path finding... the works. It puts the team on its back."""
    
    _rot = Direction.UP
    _flipHorz = False
    _flipVert = False
    _marks = None

    def __init__(self, data: str, convert=None):
        self._store = _DataMatrixStore(data, convert)
        self._overlays = {}

    @staticmethod
    def MakeFromDims(width, height, val = "."):
        return DataMatrix(f"{val * width}\n" * height)

    def _AdjustIndex(self, x, y):
        ret = IVec2(x, y)

        fh = self._flipHorz
        fv = self._flipVert

        if self._CheckSwapDims():
            tmp = fh
            fh = fv
            fv = tmp

        if fh:
            ret.X = self.Width - ret.X - 1

        if fv:
            ret.Y = self.Height - ret.Y - 1

        if (self._rot != Direction.UP):
            ret = IVec2.Sub(ret, self._GetOrigin())

            match self._rot:
                case Direction.RIGHT:
                    ret.RotateRight()
                case Direction.DOWN:
                    ret.RotateRight()
                    ret.RotateRight()
                case Direction.LEFT:
                    ret.RotateRight()
                    ret.RotateRight()
                    ret.RotateRight()

        return (ret.X, ret.Y)

    def __getitem__(self, key):
        x, y = self._AdjustIndex(key[0], key[1])
        if (x, y) not in self._overlays:
            return self._store[x, y]
        else:
            return self._overlays[(x, y)]

    def __setitem__(self, key, value):
        x, y = self._AdjustIndex(key[0], key[1])
        self._store[x, y] = value

    def SetValue(self, x, y, value):
        """Useful in lambdas where 'map[x,y] = ?' doesn't work"""
        self[x, y] = value

    def _GetOrigin(self):
        """It should be said that this is not really the damn origin. Its where the real, unrotated
        top-left origin is after the rotations are applied. Used in correcting indexing coordinates."""
        match self._rot:
            case Direction.UP:
                return IVec2(0, 0)
            case Direction.RIGHT:
                return IVec2(self._store.Height - 1, 0)
            case Direction.DOWN:
                return IVec2(self._store.Width - 1, self._store.Height - 1)
            case Direction.LEFT:
                return IVec2(0, self._store.Width - 1)
            
        assert(False)

    def _CheckSwapDims(self):
        return (self._rot == Direction.RIGHT or self._rot == Direction.LEFT)
    
    def __str__(self):
        output = []

        output.append(f"Dims: {self.Width}x{self.Height}")
        for y in range(self.Height):
            output.append("")
            for x in range(self.Width):
                output[-1] += self[x, y]

            if self._marks != None:
                output[-1] += f" | {self._PrintMarks_Line(y, False)}"
                
        output.append("")

        return "\n".join(output)
    
    @property
    def Width(self):
        if not self._CheckSwapDims(): 
            return self._store.Width
        else:
            return self._store.Height
    
    @property
    def Height(self):
        swap = self._rot == Direction.RIGHT or self._rot == Direction.LEFT
        if not swap: 
            return self._store.Height
        else:
            return self._store.Width
    
    def IsValidIndex(self, x: int, y: int):
        return x >= 0 and y >= 0 and x < self.Width and y < self.Height
    
    def Reset(self):
        """Resets all rotation and flips"""
        self._rot = Direction.UP
        self._flipHorz = False
        self._flipVert = False

    def IterateAll(self):
        """Usage: for y, x in map.IterateAll()"""
        return itertools.product(range(self.Height), range(self.Width))
    
    def IterateNeighbors(self, x, y):
        """Usage: for y, x in map.IterateNeighbors():"""
        width = self.Width - 1
        height = self.Height - 1
        if y > 0:
            if x > 0:
                yield((y - 1, x - 1))
            yield((y - 1, x))
            if x < width:
                yield((y - 1, x + 1))
        if x > 0:
            yield((y, x - 1))
        if x < width:
            yield((y, x + 1))
        if y < height:
            if x > 0:
                yield((y + 1, x - 1))
            yield((y + 1, x))
            if x < width:
                yield((y + 1, x + 1))
    
    def VisitAll(self, callback):
        """Invokes callback(x: int, y: int) on all positions"""
        for y, x in self.IterateAll():
            callback(x, y)

    def VisitPred(self, pred, callback):
        """Invokes callback(val, x: int, y: int) on all positions where pred(val) is True"""
        for y, x in self.IterateAll():
            val = self[x, y]
            if pred(val):
                callback(val, x, y)

    def VisitValue(self, value, callback):
        """Simplified VisitPred where callback(x, y) is called on all positions where the value matches"""
        for y, x in self.IterateAll():
            if self[x, y] == value:
                callback(x, y)

    def Find(self, val):
        """Finds the first coordinates where the value equals val param"""
        for y, x in self.IterateAll():
            t = self[x, y]
            if t == val:
                return (x, y)
        return None
    
    def CountVal(self, val):
        """Returns count of specific value in the base map"""
        res = 0
        for y, x in self.IterateAll():
            if self[x, y] == val:
                res += 1
        return res

    def CountNeighborVal(self, x, y, val):
        res = 0
        for y, x in self.IterateNeighbors(x, y):
            if self[x, y] == val:
                res += 1
        return res
    
    def _GetMark(self, x:int, y:int):
        if self._marks == None:
            return 0
        
        return self._marks[*self._AdjustIndex(x, y)]

    def _IncMark(self, x:int, y:int):
        self._marks[*self._AdjustIndex(x, y)] += 1

    def _SetMark(self, x:int, y:int, val:int):
        self._marks[*self._AdjustIndex(x, y)] = val

    def Mark(self, x:int, y:int):
        if (not self.IsValidIndex(x, y)):
            return False
        
        if self._marks == None:
            self._marks = _DataMatrixStore(f"{"0" * self._store.Width}\n" * self._store.Height, lambda x: int(x))
        
        self._IncMark(x, y)
        return True
    
    def ClearMark(self, x:int, y:int):
        self._SetMark(x, y, 0)

    def ResetMarks(self):
        if self._marks != None:
            self._marks.SetAll(0)

    def IsMarked(self, x:int, y:int):
        return self._GetMark(x, y) > 0
    
    def GetMarkTotal(self, x:int, y:int):
        return self._GetMark(x, y)

    def SumMarks(self, fullCount = False):
        """Sums up all marked cells. Default is one count per cell unless fullCount is True in which case it uses the full mark count."""
        res = 0
        if self._marks != None:
            for y, x in self.IterateAll():
                if self.IsMarked(x, y):
                    res += (1 if not fullCount else self.GetMarkTotal(x, y))
        return res
    
    def VisitMarks(self, callback):
        """Usage: invokes callback(x, y) for each marked coordinate. Returns a list of results returned by the callback."""

        result = []
        if self._marks != None:
            for y, x in self.IterateAll():
                if self.IsMarked(x, y):
                    result.append(callback(x, y))
        return result

    def PrintMarks(self, showCount = False):
        if self._marks == None:
            print("no marks")
            return
        
        for y in range(self.Height):
            print(self._PrintMarks_Line(y, showCount))
        print()

    def _PrintMarks_Line(self, y: int, showCount):
        line = []
        for x in range(self.Width):
            val = self._GetMark(x, y)
            if not showCount:
                line.append("#" if val > 0 else ".")
            else:
                line.append(f"{val}" if val > 0 else ".")

        return "".join(line)

    def Overlay(self, x, y, val):
        x, y = self._AdjustIndex(x, y)
        self._overlays[(x, y)] = val

    def ResetOverlay(self):
        self._overlays.clear()

    def RotateRight(self):
        self._rot = Direction.TurnRight(self._rot)

    def RotateLeft(self):
        self._rot = Direction.TurnLeft(self._rot)

    def FlipVertical(self):
        if not self._CheckSwapDims():
            self._flipVert = not self._flipVert
        else:
            self._flipHorz = not self._flipHorz

    def FlipHorizontal(self):
        if not self._CheckSwapDims():
            self._flipHorz = not self._flipHorz
        else:
            self._flipVert = not self._flipVert

    def GetColumn(self, x: int):
        """Usage: gets the col specified by x. Allows -1 for last col."""
        res = []
        assert(x == -1 or (x >= 0 and x < self.Width))
        if x == -1:
            x = self.Width - 1
        for y in range(self.Height):
            res.append(self[x, y])
        return res

    def GetRow(self, y: int):
        """Usage: gets the row specified by y. Allows -1 for last row."""
        res = []
        assert(y == -1 or (y >= 0 and y < self.Height))
        if y == -1:
            y = self.Height - 1
        for x in range(self.Width):
            res.append(self[x, y])
        return res

    def FindPath(self, start, end, markCells = False):
        """Usage: FindPath((x, y), (nx, ny))
        Note: This assumes the walls are marked with #"""
        state = _AStarState(self, start, end)

        while not state.OutOfMoves():
            current = state.GetNextOpen()

            if current == end:
                if markCells:
                    state.MarkCells()
                return (True, state.BuildRoute())
            
            x = current[0]
            y = current[1]

            state.AddOpen(x, y, x - 1, y)
            state.AddOpen(x, y, x + 1, y)
            state.AddOpen(x, y, x, y - 1)
            state.AddOpen(x, y, x, y + 1)
        
        return None

###
# A*
###

class _AStarState:
    def __init__(self, map, start, end):
        self.map = map
        self.start = start
        self.end = end
        self.cameFrom = {}
        self.gScore = {start: 0}
        self.fScore = {start: self.H(*start)}
        self.openSet = set()
        self.openSet.add(start)

    def H(self, x, y):
        return self.gScore[(x, y)] + abs(self.end[0] - x) + abs(self.end[1] - y)

    def AddOpen(self, px, py, x, y):
        if not self.map.IsValidIndex(x, y) or self.map[x, y] == "#":
            return

        current = (px, py)
        neighbor = (x, y)

        tentative = self.gScore.setdefault(current, sys.maxsize) + 1
        if tentative < self.gScore.setdefault(neighbor, sys.maxsize):
            self.cameFrom[neighbor] = current
            self.gScore[neighbor] = tentative
            self.fScore[neighbor] = tentative + self.H(neighbor[0], neighbor[1])
            if neighbor not in self.openSet:
                self.openSet.add(neighbor)

    def GetNextOpen(self):
        ret = next(iter(self.openSet))
        for cand in self.openSet:
            if self.fScore[cand] < self.fScore[ret]:
                ret = cand
        self.openSet.remove(ret)
        return ret
    
    def OutOfMoves(self):
        return len(self.openSet) == 0
    
    def MarkCells(self):
        current = self.end
        self.map.Mark(current[0], current[1])
        while current != self.start:
            current = self.cameFrom[current]
            self.map.Mark(current[0], current[1])

    def BuildRoute(self):
        current = self.end
        res = [current]
        while current != self.start:
            current = self.cameFrom[current]
            res.append(current)
        return list(reversed(res))

@dataclasses.dataclass(order=True)
class _PrioritizedItem:
    priority: int
    item: typing.Any=dataclasses.field(compare=False)

class Heap:
    """Priority queue. Heapq is a bit odd ergonomically when data structures are fluid during implementation. Use this."""

    def __init__(self, priorityCallback):
        self._q = []
        self._priorityCallback = priorityCallback

    def Push(self, val):
        pri = self._priorityCallback(val)
        heapq.heappush(self._q, _PrioritizedItem(pri, val))
    
    def Pop(self):
        ret = None
        if self._q:
            ret = heapq.heappop(self._q).item
        return ret
    
    @property
    def empty(self):
        return len(self._q) > 0
        
class Range:
    def __init__(self, start: int, length: int):
        self.start = start
        self.end = start + length

    def __str__(self) -> str:
        return f"[{self.start}, {self.end})"
    
    @property
    def length(self) -> int:
        return self.end - self.start
    
    def ContainsValue(self, value: int) -> bool:
        return value >= self.start and value < self.end
    
    def ContainsRange(self, other) -> bool:
        return self.start <= other.start and other.end <= self.end
    
    def IntersectsRange(self, other) -> bool:
        return self.ContainsValue(other.start) or other.ContainsValue(self.start)

class IVec2:
    def __init__(self, x: int, y :int):
        self._x = x
        self._y = y

    def __str__(self):
        return f"<{self._x}, {self._y}>"

    def __getitem__(self, key):
        if key == 0:
            return self._x
        elif key == 1:
            return self._y
        
        return None

    @property
    def X(self) -> int:
        return self._x

    @X.setter
    def X(self, val):
        self._x = val

    @property
    def Y(self) -> int:
        return self._y

    @Y.setter
    def Y(self, val):
        self._y = val
    
    @staticmethod
    def Add(first, second):
        return IVec2(first.X + second.X, first.Y + second.Y)

    @staticmethod
    def Sub(first, second):
        return IVec2(first.X - second.X, first.Y - second.Y)

    @staticmethod
    def Mul(first, second):
        return IVec2(first.X * second.X, first.Y * second.Y)
    
    def RotateRight(self, pivot=None):
        if (pivot != None):
            self._x -= pivot._x
            self._y -= pivot._y

        tmp = self._x
        self._x = self._y
        self._y = -tmp

        if (pivot != None):
            self._x += pivot._x
            self._y += pivot._y

    def RotateLeft(self, pivot=None):
        if (pivot != None):
            self._x -= pivot._x
            self._y -= pivot._y

        tmp = self._x
        self._x = -self._y
        self._y = tmp

        if (pivot != None):
            self._x += pivot._x
            self._y += pivot._y

class SortedLinkedList:
    _root = None
    _len = 0

    class _Node:
        def __init__(self, val, prev=None, next=None):
            self.value = val
            self.prev = prev
            self.next = next

    def __init__(self, Compare):
        self._compare = Compare

    def __str__(self):
        ret = []
        
        cur = self._root
        while cur != None:
            ret.append(f"{cur.value}")
            cur = cur.next

        return "<" + ", ".join(ret) + ">"
    
    @property
    def Length(self): return self._len

    def Add(self, item):
        if self._root == None:
            self._root = SortedLinkedList._Node(item)
            self._len += 1
            return

        cur = self._root
        while cur != None:
            cmp = self._compare(item, cur.value)
            if cmp < 0:
                newNode = SortedLinkedList._Node(item, cur.prev, cur)
                cur.prev = newNode

                if newNode.prev == None:
                    self._root = newNode
                else:
                    newNode.prev.next = newNode

                break

            if cur.next != None:
                cur = cur.next
            else:
                cur.next = SortedLinkedList._Node(item, cur, None)
                break
        
        self._len += 1

    def Remove(self, item):
        cur = self._root

        while cur != None:
            if cur.value != item:
                cur = cur.next
                continue
            
            if cur.prev != None:
                cur.prev.next = cur.next
            else:
                self._root = cur.next

            if cur.next != None:
                cur.next.prev = cur.prev

            self._len -= 1

            return
        
        raise Exception("No matching value found")
    
    def PopFront(self):
        if self._root == None:
            return None
        
        res = self._root.value
        self.Remove(res)
        return res
    
    def IsEmpty(self):
        return self._root == None

    class _SLLIter:
        def __init__(self, node):
            self._cur = node

        def __next__(self):
            if self._cur != None:
                ret = self._cur.value
                self._cur = self._cur.next
                return ret
            else:
                raise StopIteration

    def __iter__(self):
        return SortedLinkedList._SLLIter(self._root)

@dataclasses.dataclass
class Line:
    a: float
    b: float
    c: float

    @staticmethod
    def MakeFromPoints(ax, ay, bx, by):
        a = ay - by
        b = bx - ax
        c = ax * by - bx * ay
        return Line(a, b, -c)

    def Intersect(self, other):
        """Computes the intersection of two lines."""
        D = self.a * other.b - self.b * other.a
        if D != 0:
            Dx = self.c * other.b - self.b * other.c
            Dy = self.a * other.c - self.c * other.a
            x = Dx / D
            y = Dy / D
            return (x, y)
        return False

if __name__ == "__main__":
    L1 = Line.MakeFromPoints(0,1, 2,3)
    L2 = Line.MakeFromPoints(2,3, 0,4)

    R = L1.Intersect(L2)
    if R:
        print("Intersection detected:", R)
    else:
        print("No single intersection point detected")

def IsPrime(x):
    for i in range(2, x):
        if (x % i) == 0:
            return False
    return True

__primes = [2]
__primesCheckMax = 2
def GetPrimes(max: int, limitResults=True):
    global __primes, __primesCheckMax

    if __primesCheckMax < max:
        for i in range(__primesCheckMax + 1, max + 1):
            if IsPrime(i):
                __primes.append(i)
        __primesCheckMax = max 
    
    if limitResults:
        for i in range(0, len(__primes)):
            if __primes[i] > max:
                return __primes[0:i]

    return __primes

def GetPrimeFactors(x: int):
    result = []
    for prime in GetPrimes(int(x / 2), False):
        if x == prime:
            result.append(prime)
            break
        elif x % prime == 0:
            result.append(prime)
            x /= prime
    return result

def LCM(nums):
    for i in nums:
        assert(type(i) == int)

    factors = set()
    for n in nums:
        factors.update(GetPrimeFactors(n))
    return functools.reduce(lambda x,y: x*y, factors)
