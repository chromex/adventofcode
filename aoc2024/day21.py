import aoc

inp = """140A
170A
169A
803A
129A""".splitlines()

NUM_ROBOTS = 25

keypad = {"7": (0, 0), "8": (1, 0), "9": (2, 0), "4": (0, 1), "5": (1, 1), "6": (2, 1), "1": (0, 2), "2": (1, 2), "3": (2, 2), "0": (1, 3), "A": (2, 3)}
dirpad = {"^": (1, 0), "A": (2, 0), "<": (0, 1), "v": (1, 1), ">": (2, 1)}

ROUTE_CACHE = {}

def IsLegalRoute(start, route, bad):
    cur = start
    for move in route:
        dir = None
        match move:
            case "^":
                dir = aoc.Direction.UP
            case ">":
                dir = aoc.Direction.RIGHT
            case "v":
                dir = aoc.Direction.DOWN
            case "<":
                dir = aoc.Direction.LEFT
        cur = aoc.Direction.MovePos(*cur, dir)
        if cur == bad:
            return False
    return True

def GenRoutes(start, end, controllingKP):
    key = (start, end, controllingKP)
    if key in ROUTE_CACHE:
        return ROUTE_CACHE[key]

    posmap = keypad if controllingKP else dirpad
    bad = (0, 3) if controllingKP else (0, 0)

    sPos = posmap[start]
    ePos = posmap[end]

    dx = ePos[0] - sPos[0]
    dy = ePos[1] - sPos[1]

    routes = []

    if dx != 0 and dy != 0:
        horz = "<" if dx < 0 else ">"
        vert = "^" if dy < 0 else "v"

        first = horz * abs(dx) + vert * abs(dy)
        second = vert * abs(dy) + horz * abs(dx)
        
        if IsLegalRoute(posmap[start], first, bad):
            routes.append(first + "A")

        if IsLegalRoute(posmap[start], second, bad):
            routes.append(second + "A")
    else:
        route = ""

        if dx < 0:
            route = "<" * abs(dx)
        elif dx > 0: 
            route = ">" * dx
        elif dy < 0:
            route = "^" * abs(dy) 
        elif dy > 0:
            route = "v" * dy 

        routes.append(route + "A")

    ROUTE_CACHE[key] = routes
    return routes

NUMPAD_CACHE = {}

def Gen_Numpad(code, depth):
    if depth > NUM_ROBOTS:
        return 1
    
    key = (code, depth)
    if key in NUMPAD_CACHE:
        return NUMPAD_CACHE[key]
    
    cur = "A"

    total = 0
    
    for c in code:
        routes = GenRoutes(cur, c, False)
        best = -1
        for r in routes:
            cand = Gen_Numpad(r, depth + 1)
            if best == -1 or cand < best:
                best = cand
        total += best
        cur = c

    NUMPAD_CACHE[key] = total
    return total

def GetShortestLen(code):
    cur = "A"

    total = 0
    
    for c in code:
        routes = GenRoutes(cur, c, True)
        best = -1
        for r in routes:
            cand = Gen_Numpad(r, 0)
            if best == -1 or cand < best:
                best = cand
        total += best
        cur = c

    return total

sum = 0
for code in inp:
    short = GetShortestLen(code)
    sum += short * int(code[:-1])

print(sum)