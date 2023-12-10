import functools

def Pairwise(fn, lstA, lstB):
    return list(fn(x, lstB[ind]) for ind,x in enumerate(lstA))

def GetInts(str):
    return list(map(lambda x: int(x), filter(lambda x: x != "", str.split(" "))))

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
