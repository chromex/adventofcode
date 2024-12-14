input = """1950139 0 3 837 6116 18472 228700 45"""

import aoc

# pre puzzle score: 559

# 0 - 1
# even number ditis, replace by two stones

orig = aoc.GetInts(input, " ")

rec = [ {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {} ]

def SumDigits(n):
    if n == 0:
        return 1
    
    sum = 0
    while n > 0:
        n = int(n/10)
        sum += 1
    return sum

nums = []

def Step():
    global nums
    res = []
    for n in nums:
        if n == 0:
            res.append(1)
        elif (SumDigits(n) % 2) == 0:
            st = f"{n}"
            p1 = st[:int(len(st)/2)]
            p2 = st[int(len(st)/2):]
            res.append(int(p1))
            res.append(int(p2))
        else:
            res.append(n * 2024)
    nums = res

def Explore(val, depth):
    global nums, rec
    if not val in rec[depth]:
        nums = [val]

        for i in range(int(75 / len(rec))):
            Step()

        if depth == len(rec) - 1:
            rec[depth][val] = len(nums)
        else:
            res = nums
            tsum = 0
            for v in res:
                tsum += Explore(v, depth + 1)
            rec[depth][val] = tsum
        
    return rec[depth][val]

sum = 0

for val in orig:
    sum += Explore(val, 0)

print(sum)