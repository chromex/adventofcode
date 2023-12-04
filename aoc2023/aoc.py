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