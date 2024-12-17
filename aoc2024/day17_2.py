import aoc

input = """Register A: 35200350
Register B: 0
Register C: 0

Program: 2,4,1,2,7,5,4,7,1,3,5,5,0,3,3,0""".splitlines()

inst = aoc.GetInts(input[4].split(" ")[1], ",")
li = len(inst)

def Cmp(res):
    if len(res) > len(inst):
        return False
    for i in range(len(res)):
        if res[i] != inst[i]:
            return False
    return True

newA = 0

while True:
    ip = 0
    a = newA
    b = 0
    c = 0
    i = 0

    if newA % 1000000 == 0:
        print(newA)

    fault = False
    while a > 0:
        b = a % 8
        b = b ^ 2 
        c = int(a / (2**b)) 
        b = b ^ c 
        b = b ^ 3 
        if i >= li:
            fault = True
            break
        if (b % 8) != inst[i]:
            fault = True
            break
        i += 1
        a = int(a / 8)

    if i == li and not fault:
        print(newA)
        break

    newA += 8