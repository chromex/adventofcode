import functools
import itertools

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

class Vec2:
    def __init__(self, x, y):
        self.x = x
        self.y = y

class Mat3:
    def __init__(self, data=None):
        self._data = [[0, 0, 0], [0, 0, 0], [0, 0, 0]]

        if data != None:
            rows = data.split(";")
            assert(len(rows) == 3)
            for i,r in enumerate(rows):
                self._data[i] = GetNums(r)
                assert(len(self._data[i]) == 3)
    
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

class DM2:
    class _DataMatrixStore:
        def __init__(self, data: str):
            self.raw = data
            
            lines = data.splitlines()
            self.__width = len(lines[0])
            self.__height = len(lines)

            self.data = [""] * self.__width * self.__height

            for y in range(self.__height):
                for x in range(self.__width):
                    self.data[self.__GetIndex(x, y)] = lines[y][x]

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

        def CoordIter(self):
            """Use is 'for y,x in mat.CoordIter():"""
            return itertools.product(range(self.Height, self.Width))

        def __str__(self):
            output = []

            for y in range(self.Height):
                output.append("")
                for x in range(self.Width):
                    output[-1] += self.__getitem__((x, y))

            return "\n".join(output)
        
    __flipDims = False

    def __init__(self, data: str):
        self.__store = DM2._DataMatrixStore(data)

    def __getitem__(self, key):
        x, y = key[0], key[1]
        # TODO: transform index
        return self.__store[x, y]

    def __setitem__(self, key, value):
        x, y = key[0], key[1]
        # TODO: transform index
        self.__store[x, y] = value
    
    @property
    def Width(self):
        if not self.__flipDims: 
            return self.__store.Width
        else:
            return self.__store.Height
    
    @property
    def Height(self):
        if not self.__flipDims: 
            return self.__store.Height
        else:
            return self.__store.Width
    
    def __str__(self):
        output = []

        for y in range(self.Height):
            output.append("")
            for x in range(self.Width):
                output[-1] += self[x, y]

        return "\n".join(output)
    
    def IsValidIndex(self, x: int, y: int):
        return x >= 0 and y >= 0 and x < self.Width and y < self.Height
    
    def RotateRight(self):
        # TODO
        self.__flipDims = not self.__flipDims
        pass

    def RotateLeft(self):
        # TODO
        self.__flipDims = not self.__flipDims
        pass

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

if __name__ == "__main__":
    input = """O....#....
O.OO#....#
.....##...
OO.#O.F..O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#...."""
    dm = DM2(input)
    print(dm)
    print(dm[6, 3])
    dm[6,3] = "C"
    print(dm)    

    mat = Mat3.MkTransform(-1.5, -1.5)
    rot = Mat3.MkRotLeft90()
    mat = Mat3.MulMat(rot, mat)
    mat = Mat3.MulMat(Mat3.MkTransform(1.5, 1.5), mat)
    print(rot)
    print(Mat3.MulVec(mat, [3, 1]))

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
        if self.marks == None:
            self.marks = [None] * self.__height

        if self.marks[y] == None:
            self.marks[y] = [None] * self.__width

        self.marks[y][x] = True

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
        return f"({self.start}, {self.end})"
    
    @property
    def length(self) -> int:
        return self.end - self.start
    
    def ContainsValue(self, value: int) -> bool:
        return value >= self.start and value < self.end
    
    def ContainsRange(self, other) -> bool:
        return self.start <= other.start and other.end <= self.end
    
    def IntersectsRange(self, other) -> bool:
        return self.ContainsValue(other.start) or other.ContainsValue(self.start)

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
