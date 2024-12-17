import aoc

input = """Register A: 35200350
Register B: 0
Register C: 0

Program: 2,4,1,2,7,5,4,7,1,3,5,5,0,3,3,0""".splitlines()

# 19136
# [0, 3, 5, 4, 0]
# 51904
# [0, 3, 5, 4, 1]
# 84672
# [0, 3, 5, 4, 2]
# 117440
# [0, 3, 5, 4, 3, 0]

# OG answer 2,7,4,7,2,1,7,5,1

aop = int(input[0].split(" ")[-1])
bop = int(input[1].split(" ")[-1])
cop = int(input[2].split(" ")[-1])

inst = aoc.GetInts(input[4].split(" ")[1], ",")

def ReadCombo(val):
    if val < 4:
        return val
    if val == 4: return a
    if val == 5: return b
    if val == 6: return c
    assert(False)

def Cmp(res):
    if len(res) > len(inst):
        return False
    for i in range(len(res)):
        if res[i] != inst[i]:
            return False
    return True

newA = 35184372088832
while True:
    ip = 0

    if newA % 1000000 == 0:
        print(newA)

    a = newA
    b = bop
    c = cop

    res = []

    while ip < len(inst):
        o = inst[ip + 1]

        match inst[ip]:
            case 0: #adv
                div = 2**ReadCombo(o)
                a = int(a / div)
            case 1: #bxl
                b = b ^ o
            case 2: #bst
                b = ReadCombo(o) % 8
            case 3: #jnz
                if a != 0:
                    ip = o - 2
            case 4: #bxc
                b = b ^ c
            case 5: #out
                res.append(ReadCombo(o) % 8)
                if not Cmp(res):
                    if len(res) > 5:
                        print(f"{ip} {a} {b} {c}")
                        print(newA)
                        print(res)
                    break 
            case 6: #bdv
                b = int(a / (2**ReadCombo(o)))
            case 7: #cdv
                c = int(a / (2**ReadCombo(o)))
        ip += 2

    if res == inst:
        print(f"{ip} {a} {b} {c}")
        print(newA)
        print(res)
        break

    newA += 8
