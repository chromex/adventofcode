import aoc
import itertools

input = """[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}"""

class Machine:
    def __init__(self, line):
        parts = line.split(" ")
        self.target = aoc.GetInts(parts[-1])

        self.buttons = []
        for i in range(1, len(parts) - 1):
            self.buttons.append(aoc.GetInts(parts[i]))

        self.buttonVectors = []
        for butt in self.buttons:
            vec = [0] * len(self.target)
            for i in range(len(vec)):
                if i in butt:
                    vec[i] = 1
            self.buttonVectors.append(vec)

    def __str__(self):
        return f"Machine {self.target}\n{self.buttons}\n{self.buttonVectors}"
    
machines = []
for line in input.splitlines():
    machines.append(Machine(line))

def Resolve(machine, joltId, inputs):
    assert(len(inputs) == len(machine.buttons))

    ret = -machine.target[joltId]

    for idx in range(len(machine.buttonVectors)):
        ret += machine.buttonVectors[idx][joltId] * inputs[idx]

    return ret

class Solver:
    def __init__(self, data, nEquations, equationCallback):
        self.data = data
        self.nEquations = nEquations
        self.equation = equationCallback
    
    def LinearSolve(self, minInput, maxInput):
        inputRanges = []
        for idx in range(len(minInput)):
            inputRanges.append(range(minInput[idx], maxInput[idx] + 1))

        for input in itertools.product(*inputRanges):
            result = self._resolve(input)

            if result.count(0) == len(result):
                yield (result, input)

    def _resolve(self, input):
        ret = [0] * self.nEquations

        for i in range(self.nEquations):
            ret[i] = self.equation(self.data, i, input)

        return ret
    

for machine in machines:
    print(machine)

    minPresses = None

    solver = Solver(machine, len(machine.target), Resolve)
    for res in solver.LinearSolve([0] * len(machine.buttons), [14] * len(machine.buttons)):
        presses = sum(res[1])
        if minPresses == None or presses < minPresses:
            minPresses = presses
    
    print(minPresses)