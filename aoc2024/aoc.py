import functools
import itertools
import math

def Pairwise(fn, lstA, lstB):
    return list(fn(x, lstB[ind]) for ind,x in enumerate(lstA))

def GetInts(str, spl=" "):
    return list(map(lambda x: int(x), filter(lambda x: x != "", str.split(spl))))

def GetNums(str, spl=" "):
    return list(map(lambda x: float(x), filter(lambda x: x != "", str.split(spl))))

def SplitEmptyLines(str):
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
    def __init__(self, data=None):
        if data != None:
            self._data = []
            rows = data.split(";")
            assert(len(rows) == 3)
            for r in rows:
                self._data.append(GetNums(r))
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
        return self.data[self.__GetIndex(key[0], key[1])]
    
    def __setitem__(self, key, value):
        self.data[self.__GetIndex(key[0], key[1])] = value

    def __str__(self):
        output = []

        for y in range(self.Height):
            output.append("")
            for x in range(self.Width):
                output[-1] += self.__getitem__((x, y))

        return "\n".join(output)
    
class DM2:
    _flipDims = False
    _indexMod = None

    def __init__(self, data: str, convert=None):
        self._store = _DataMatrixStore(data, convert)

        xOffset = self._store.Width / 2
        yOffset = self._store.Height / 2

        if self._store.Width % 2 == 0:
            xOffset -= 0.5
        if self._store.Height % 2 == 0:
            yOffset -= 0.5

        moveDown = Mat3.MkTransform(-xOffset, -yOffset)
        moveBack = Mat3.MkTransform(xOffset, yOffset)
        self._rotRight = Mat3.MulMat(Mat3.MulMat(moveBack, Mat3.MkRotRight90()), moveDown)
        self._rotLeft = Mat3.MulMat(Mat3.MulMat(moveBack, Mat3.MkRotLeft90()), moveDown)

    def _AdjustIndex(self, x, y):
        rx = x
        ry = y

        if (self._indexMod != None):
            rx, ry = Mat3.MulVec(self._indexMod, [x,  y])
            rx = int(rx + 0.1)
            ry = int(ry + 0.1)

        return (rx, ry)

    def __getitem__(self, key):
        x, y = self._AdjustIndex(key[0], key[1])
        return self._store[x, y]

    def __setitem__(self, key, value):
        x, y = self._AdjustIndex(key[0], key[1])
        self._store[x, y] = value

    def IterateAll(self):
        return itertools.product(range(self.Height), range(self.Width))
    
    @property
    def Width(self):
        if not self._flipDims: 
            return self._store.Width
        else:
            return self._store.Height
    
    @property
    def Height(self):
        if not self._flipDims: 
            return self._store.Height
        else:
            return self._store.Width
    
    def __str__(self):
        output = []

        for y in range(self.Height):
            output.append("")
            for x in range(self.Width):
                output[-1] += self[x, y].__str__()

        return "\n".join(output)
    
    def IsValidIndex(self, x: int, y: int):
        return x >= 0 and y >= 0 and x < self.Width and y < self.Height
    
    def RotateRight(self):
        self._flipDims = not self._flipDims

        if self._indexMod == None:
            self._indexMod = self._rotRight
        else:
            self._indexMod = Mat3.MulMat(self._indexMod, self._rotRight)

    def RotateLeft(self):
        self._flipDims = not self._flipDims

        if self._indexMod == None:
            self._indexMod = self._rotLeft
        else:
            self._indexMod = Mat3.MulMat(self._indexMod, self._rotLeft)

    def FlipVertical(self):
        # TODO
        pass

    def FlipHorizontal(self):
        # TODO
        pass

    def GetColumn(self, x: int):
        # TODO
        pass

    def GetRow(self, y: int):
        # TODO
        pass

class DataMatrix:
    marks = None

    def __init__(self, data: str):
        self.raw = data
        self.lines = data.splitlines()
        self.__height = len(self.lines)
        self.__width = len(self.lines[0])

    def __getitem__(self, key):
        """
        [y] returns row y
        [x, y] returns item x in row y
        [x:z, y] returns a slice of items from x to z (exclusive) on row y
        """
        if type(key) is int:
            return self.lines[key]

        if type(key) is tuple:
            return self.lines[key[1]][key[0]]
        
    # This is dumb. I assume hard eggnog was involved.
    def Set(self, x, y, val):
        assert(len(val) == 1)
        line = self.lines[y]
        left = line[0:x]
        right = line[x+1:]
        self.lines[y] = left + val + right
        
    def GetColumn(self, x):
        res = ""
        for i in range(self.Height):
            res += self.lines[i][x]
        return res

    def __str__(self) -> str:
        output = []

        for y in range(self.__height):
            if self.marks is None:
                output.append(self.lines[y])
            else:
                output.append(f"{self.lines[y]} | {self.PrintMarks_Line(y)}")

        return "\n".join(output)
    
    @property
    def Height(self): return self.__height

    @property
    def Width(self): return self.__width

    def IndexInBounds(self, x, y):
        return x >= 0 and y >= 0 and x < self.Width and y < self.Height
    
    def Spread(self, x, y, callback):
        width = self.Width
        if y > 0:
            if x > 0:
                callback(x - 1, y - 1)
            callback(x    , y - 1)
            if x < width:
                callback(x + 1, y - 1)
        if x > 0:
            callback(x - 1, y)
        if x < width:
            callback(x + 1, y)
        if y < self.Height:
            if x > 0:
                callback(x - 1, y + 1)
            callback(x    , y + 1)
            if x < width:
                callback(x + 1, y + 1)

    def Visit(self, callback):
        for y in range(self.Height):
            for x in range(self.Width):
                callback(x, y)

    def Scan(self, pred, callback):
        for y in range(self.Height):
            for x in range(self.Width):
                val = self.lines[y][x]
                if pred(val):
                    callback(val, x, y)

    def ScanMarks(self, callback):
        result = []
        if self.marks != None:
            for y in range(self.Height):
                if self.marks[y] != None:
                    for x in range(self.Width):
                        if self.IsMarked(x, y):
                            result.append(callback(self.lines[y][x], x, y))
        return result
        
    def Mark(self, x:int, y:int):
        if (not self.IndexInBounds(x, y)):
            return False

        if self.marks == None:
            self.marks = [None] * self.__height

        if self.marks[y] == None:
            self.marks[y] = [None] * self.__width

        self.marks[y][x] = True
        return True

    def SumMarks(self):
        res = 0
        if self.marks != None:
            for y in range(self.Height):
                if self.marks[y] != None:
                    for x in range(self.Width):
                        if self.IsMarked(x, y):
                            res += 1
        return res

    def ClearMark(self, x:int, y:int):
        if self.marks != None and self.marks[y] != None:
            self.marks[y][x] = False
    
    def ResetMarks(self):
        self.marks = None

    def IsMarked(self, x:int, y:int) -> bool:
        if self.marks != None and self.marks[y] != None:
            return self.marks[y][x] is True
        return False
            
    def PrintMarks(self):
        if self.marks is None:
            print("no marks")
            return
        
        for y in range(self.__height):
            print(self.PrintMarks_Line(y))

    def PrintMarks_Line(self, y: int):
        line = self.marks[y]
        if line is None:
            return "." * self.__width
        else:
            return "".join(map(lambda item : "#" if item is True else ".", line))
        
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

    def __init__(self, other):
        self._x = other[0]
        self._y = other[1]

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

    @property
    def Y(self) -> int:
        return self._y
    
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

if __name__ == "__main__":
    vec = IVec2(1, 3)
    print(vec)
    vec.RotateRight()
    print(vec)
    vec.RotateLeft()
    print(vec)
    vec = IVec2.Add(vec, IVec2(-7, 15))
    print(vec)
    vec = IVec2(3,3)
    print(vec)
    vec.RotateRight(IVec2(1,1))
    print(vec)

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
