import aoc

input = """Time:        35     93     73     66
Distance:   212   2060   1201   1044""".splitlines()

times = aoc.GetInts(input[0][5:])
dists = aoc.GetInts(input[1][9:])
races = aoc.Pairwise(lambda x, y: (x, y), times, dists)

res = 1
for race in races:
    sum = 0
    for t in range(1, race[0] + 1):
        rem = race[0] - t
        dist = rem * t
        if dist > race[1]:
            sum += 1
    res *= sum

print(res)

time = 35937366
dist = 212206012011044

def GetTravel(t):
    rem = time - t
    return int(rem * t)

def FindMin(t: int, delta: int):
    trav = GetTravel(t)

    if delta == 0:
        while trav < dist:
            t += 1
            trav = GetTravel(t)
        while GetTravel(t - 1) > dist:
            t -= 1
        return int(t)

    hd = int(delta / 2)
    if trav > dist:
        t -= hd
        return FindMin(t, hd)
    if trav < dist:
        t += hd
        return FindMin(t, hd)
    
mt = FindMin(time / 2, time / 2)
print(f"{mt} {GetTravel(mt)}")
maxt = time - mt
sum = maxt - mt + 1
print(sum)