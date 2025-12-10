import aoc
import functools

input = """[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}"""

sum = 0

class Machine:
    def __init__(self, line):
        parts = line.split(" ")
        self.lights = parts[0][1:-1]

        self.buttons = []
        for i in range(1, len(parts) - 1):
            self.buttons.append(aoc.GetInts(parts[i][1:-1], ","))

    def __str__(self):
        return f"Machine {self.lights} {self.buttons}"
    
    def _Toggle(self, cur, button):
        for i in button:
            cur[i] = "#" if cur[i] == "." else "."

    def _Search(self, remDepth):
        if remDepth == 0:
            return False

        self.stack.append(self.stack[-1][:])
        cur = self.stack[-1]

        for but in self.buttons:
            self._Toggle(cur, but)
            if "".join(cur) == self.lights:
                return True
            
            if remDepth > 1:
                res = self._Search(remDepth - 1)
                if res:
                    return True
                
            self._Toggle(cur, but)
        
        self.stack.pop()

        return False

    def Search(self, maxDepth):
        self.stack = [["."] * len(self.lights)]
        return self._Search(maxDepth)


machines = []
for line in input.splitlines():
    machines.append(Machine(line))

for m in machines:
    for i in range(1, 10):
        if m.Search(i):
            print(m)
            print(i)
            sum += i
            break

print(sum)