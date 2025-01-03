import aoc, dataclasses

inputdata = """#############################################################################################################################################
#.#.........#.#.....#.....#...#...................#.........#.......#.....#.......#.........#...#.............#.............#.........#...#E#
#.#.#.#####.#.#.#.###.#.#.#.#.###.###.#########.#.#######.#.###.###.#.###.#.###.#.#.#.#####.#.#.#.#####.###.###.#########.#.#.#.#####.#.#.#.#
#.#.#.....#...#.#.....#.#...#.......#...#...#...#.#.......#.....#.#...#.#.#.#...#.#.#.....#...#.#.#.....................#.#.#.#.#...#.#.#...#
#.#.#####.###.#.#######.#####.###.#.###.#.#.#.###.#.#############.#####.#.#.#.###.#######.#######.#.###.#.#.#.#######.#.#.###.#.#.#.#.#.###.#
#...#.....#.......#...#.......#...#.#...#.#...#.#.#.#...#.....#.....#...............#...#.#.......#...#.#.#.#.....#...#.#.....#.#.#.#.#...#.#
#.###.#.###.#.#.###.#.#######.#.#.#.#.###.#####.#.#.#.#.###.#.#.#.#.###.#.###.#.###.#.#.#.#.#####.#####.#.###.###.#.#.#########.#.#.#.###.###
#.#.#.#.#.....#.....#...#...#.#.....#...#.....#.#.#.#.#...#.#.#...#.....#.....#...#...#.#.#...#.........#...........#.#...#.....#.#.#...#...#
#.#.#.###.###.#.#######.#.#.#.#.#.#########.#.#.#.#.#.###.#.#.###.#####.#######.#.#####.#.###.###.#####.#####.###.#####.#.#.#####.#.###.#.#.#
#...#.....#...#.....#.#...#...#.#.............#.#...#.#.....#.....#.....#...#...#...#...#...#.#...#...#.....#...#.....................#.#.#.#
###.#######.#.#.###.#.###.###.#.###.#######.###.#####.###########.###.#.#.#.#####.#.#.#####.#.#.#.#.#.#.###.#.#.###.#.#.#######.#.#.###.###.#
#...........#.......#...#.....#.....#.....................#.........#.#...#.....#.#.#.....#.#...#...#.....#...#...#.#...#.#.....#.#.....#...#
#.#.#.###############.#.#.#.#########.###.#.#####.###.#.#.#.###.#.#.#.#########.#.#######.#.#.###.#######.###.###.#.#####.#.#.###.#######.#.#
#...#.#.......#...#.#.#...#.......#...#.#.........#...#.#.#...................#.#.......#.#...#...#.......#.....#.#.#...#...#...#.#.......#.#
#.###.#.#.###.#.#.#.#.###.#######.#.###.###.#######.###.#.#.#.###.###.#####.#.#.#####.###.#.###.#.###.###.#.#.#.#.#.#.#.###.###.#.#.#.###.#.#
#...#.#.#.#.#.#...#.#.#.....#.#...#...#...#...#...#.....#.#.#...#.........#.#.#.......#...#.#.#...#...#...#.#.#...#...#...#...#...#...#...#.#
#####.###.#.#.#.#.#.#.#.#.#.#.#.#.###.#.#.###.#.#.#.#####.#.###.#########.#.#########.#.###.#.###.#.#######.#.###.#.#####.#####.#####.#.###.#
#.....#...#.#...#...#.#.#.#...#.#...#...#.....#.#.#.....#.#...#.....#.....#.........#.#.#.....#...#.#.......#.......#.....#...#.........#...#
#.#####.###.###.#####.###.###.#.#####.#######.#.#.#######.###.#####.#.#######.#.#####.#.#####.#.###.#.###.###.#######.#####.#.#######.###.###
#.#.#...#.....#.#...#...#.....#...............#.#.......#.#.#.#.....#.........#...#...#.....#.#.#...#.#.....#.#.....#.......#.#.......#.#.#.#
#.#.#.#.###.#.#.#.#.###.#.#.#.#####.#########.#.#######.#.#.#.#.###############.#.#.#######.###.###.#.#.###.#.#.###.#########.#.#####.#.#.#.#
#.#...#...#.#.#.#.#...#...#.#...#...........#.#...#...#.#.#...#.........#.......#...#.....#.........#...#...#.#...#.#.....#...#.....#.#.#.#.#
#.#######.#.###.#.###.#####.###.#.#########.#.###.#.#.#.#.#####.#######.#.#############.#.#######.#.#####.###.###.#.#.#.#.#.#.#####.#.#.#.#.#
#...#...............#...........#.#.#.......#...#.#.#.#...#...#...#...#...#.......#.....#.....#...#...#...#.#.#...#...#.#.#.#...#...#.#.....#
#.#.#.#######.#####.#############.#.#.###########.#.#.#####.#.###.#.#.#####.#.###.###.###.#####.#.###.#.###.#.#########.#.#.#####.###.#######
#.#.#.#.....#.....#.#.......#.......#.#...........#.#...........#.#.#.....#.#...#...#.#...#...#.#...#.#.#.#...#...#...#.#.#.........#.#.....#
#.#.#.#.#.#.#.#####.#.###.#.#.#######.#.#####.#####.###########.#.#####.#.###.#.###.#.#####.#.#.###.#.#.#.#.###.#.#.#.#.#.###########.#.#.#.#
#.#.#...#...#.#.....#.#...#.#.#.#.....#...#.#.#.........#.......#.......#.#.......#.#...#...#.....#.#...#...#...#...#...#...#...#...#.....#.#
#.#.#.###.#.###.#######.###.#.#.#.#######.#.#.#.#########.#.#############.#.###.###.###.#.#########.###.#.###.#####.#######.#.#.#.#.#.###.#.#
#...#...#.#.#...#...#...#...#...#...#.#...#.#...#...#.....#.#.........#...#...#.....#...#.#.........#...#.....#...#.#.....#.#.#.#.#.#...#...#
###.###.#.###.###.#.#.#########.###.#.#.###.#####.###.#.#####.#######.###.#.#.#.#####.###.#.#.#.###.#.###.#####.#.###.###.#.###.#.#.#####.###
#...#...#.........#...#.....#.....#.#.#...#.......#...#.#...#...#...#...#.#.#.....#...#...#.#.......#.#...#.....#.....#...#.....#.#.....#...#
#.#######.#######.#####.###.#.#####.#.#.#.#.#####.#.#####.#.###.#.#.###.#.#.#.###.#.###.###.#########.#####.#############.#####.#.#####.#.#.#
#.............#.#.#.....#.#.#.......#...#.#...#.#.#.......#...#...#.#...#.#.#...#.#.#.....#.#.......#.#.....#...#...#...#.#.....#...#...#.#.#
#####.#######.#.#.#.#####.#.#.#.#####.###.###.#.#.#########.#####.###.###.#.#.#.#.#.#.#####.#.#####.#.#.#.#.#.#.#.#.#.#.#.#########.#.###.#.#
#...#.....#...#...#...........#.#...#.....#...#...#...#...#.......#...#...#.#.......#.#.#...#.#.#...#.#.#...#.#...#.#.#.#.....#...#.#...#...#
#.#.#######.#.#####.###.#####.#.#.###.#####.###.#.#.#.###.#########.###.###.#########.#.#.###.#.#.###.#.#####.#####.#.#.#####.#.#.#.###.#.###
#.#...#.....#.#.....#...#...#.#.#.....#.....#...#...#...#.........#.#.....#.#.......#.#.#.......#.....#.........#...#.#.#.......#...........#
#.###.#.#####.#.###.#.###.#.###.#####.#.#####.#.#######.#.#######.#.#####.#.#.#####.#.#.###.###.#######.#.#######.###.#.#########.###.#.#.#.#
#.#.....#...#.#.#.#.....#.#...#.....#...#.....#.#.......#.#.......#...#...#.......#.#.#...#.....#.........#.....#.#...#.......#.#.#...#.#.#.#
#.#######.#.###.#.#######.###.#####.###.#.#######.#########.#####.###.###.#.###.###.#.###.#################.###.#.#.#########.#.#.#.###.###.#
#.......#.#...#.#...#.....#.#.#.....#.#.....#...............#...#.#.#...#.#.#.......#.#.....#.....#.......#.#.....#.#.......#.#.#.#...#.....#
#.#####.#.###.#.#.###.#.###.#.#.#####.#.###.#.###############.###.#.###.#.###.#.###.#.#.###.###.#.#.#####.#.#.###.#.#.#####.#.#.#.#.#.###.#.#
#.......#...#...#...#.#...#...#.#.....#.#.#...#...#.......#.......#.#...#...#.#.#...#.#.#.#...#.#...#...#.#.#...#...#...#.#...#...#.#.#...#.#
#.#.#.#####.#######.#.#.#.###.#.#.#####.#.#####.#.#.#####.#.#######.#.#####.#.#.#.###.#.#.###.#.#####.###.#.###.#.#####.#.#####.#####.#.#.#.#
#.#.#...#...........#...#...#.#.#.............#.#.#...#...#...#.....#...#.#...#...#.#...#...#.#.#.......#...#.#.#.#.....#.#...#...#...#.#.#.#
#.#.###.#.#################.#.#.#.#######.#.#.#.#####.#.#####.#.#####.#.#.###.#.###.###.#.###.#.###.###.#.###.#.###.#####.#.#.###.#.#.#.#.#.#
#.....#...#.....#...#.....#.#...#...#.......#.#.....#.#.....#...#...#.#.#.....#.......#...#...#...#.#.#.........#...#.#.....#...#.#.........#
#####.#####.###.#.#.#.###.#.###.###.#.#######.###.#.#.#####.###.#.#.###.#.###########.#.#.#.#####.#.#.###########.###.#.#####.#.#.#.###.#.#.#
#...#.#...#...#...#...#.#.#.....#.....#.......#...#.....#.........#...#.#.#.........#...#.#.......#.#...#...#.....#.......#...#.#.#.....#.#.#
#.#.#.#.#.###.#########.#.#.###.#.###########.#.###.###.#############.#.#.#.#######.#.#############.#.#.#.#.#.#.#.#.#######.###.#.#.#####.#.#
#.#...#.#.#...#...#...#...#...#.#...#...#...#.#...#...#.........#...#.#.#.........#...#...........#...#.#.#.#.#...#...#...#...............#.#
#.#####.#.#.###.#.#.#.#.#######.#####.#.#.#.#.###.#####.#.#####.###.#.#.###########.###.#########.#####.#.#.#.###.#.###.#.#.#.#.#.#.#.###.###
#.#.....#.#...#.#.#.#...#.....#.....#.#.#.#.#.....#...#...#...#.#...#...#.........#...#...#.....#.....#.#.#.#.....#.#...#.#.#...#.#.#.#.....#
#.###.###.###.#.#.#.#####.###.#####.#.#.#.#.#######.#.#####.#.#.#.###.###.#######.#######.#.#####.#####.#.#.###.#.###.###.#.#####.#.#.#.#.#.#
#.....#...#...#.#...#.....#.#.....#.#.#...#.#.......#.......#...#.....#...#...#...#.....#.#.#.....#...#.#.#.....#.....#...#.#.....#.#...#...#
###.###.###.###.#########.#.#####.#.#.#####.#.#####.###########.#.#####.#####.#.###.#.###.#.#.#####.#.#.#########.#####.###.#.###.###.###.#.#
#.....#...#...#.....#.....#.....#.#.......#.#...#...#.........#.....#...#.....#.#...#.....#.#...#...#...#.......#.....#...#.#.#.....#.#...#.#
#.#.#####.###.#####.#.#########.#.#########.###.#####.#######.#####.#.###.###.#.#.#########.###.#.#######.#####.###.#####.#.#.#.#.#.###.#.#.#
#.#...#.....#.....#...#...#.....#...........#...#...#.#...#...#...#.#.....#.#.#...#.....#.....#...#.............#.#.......#...#.#.#...#...#.#
#.#.#.#.#########.#.###.#.#.###.#######.#####.#.#.#.#.#.###.#.#.#.#########.#.#######.#.###.#######.#.#####.#####.#.#.#####.###.#.###.###.#.#
#...#...#...#...#.#...#.#.#.#...#...........#.#.#.#...#.#...#...#.#.........#...#.....#...#.........#...#.#.#...#...#...#...#...#...#...#.#.#
###.#####.#.#.#.#.#.#.#.#.#.#####.#.#######.#.#.#.#####.#.###.###.#.#.#.#######.###.#####.#.###########.#.#.#.#.###.###.#.###.#####.###.#.#.#
#...#...#.#.#.#.#.#.#...#.#...#...#.#.....#...#.#.#.....#...........#.#.......#.#...#.#...#.#...........#.#...#...#...#.#...#.....#...#...#.#
#.#.#.#.#.#.#.#.#.#.#####.#.#.#.###.#.###.#####.#.###.#.#############.###.###.#.#.###.#.#####.###########.#######.#####.###.#####.###.#####.#
#.#...#...#.#.#...#.#.#...#.#.#.#...#.#.#.#.....#.#...#...#.....#...#.....#.#.#...#.#...#.....#.....#.........#...#.....#.#.#...#...#.....#.#
#.#.#######.#.###.#.#.#.#####.#.#.#.#.#.#.#####.#.#.#######.###.#.#.###.###.#.#####.#.#.#.#####.#.###.#.###.###.###.#####.#.#.###.#####.#.###
#...#.....#.#...#.#.#.#.#...#...#.#.#.#.#...#...#.#.#.....#.#.#...#...#...........#.#.....#.......#...#.......#.#...#.#.....#.....#...#.#...#
#.#.#.###.#.###.###.#.#.#.#.#.###.#.#.#.###.#.#.#.#.#.###.#.#.#######.###.###.###.#.#######.###.#.#.#.#######.#.###.#.#.###########.#.#####.#
#.#.#...#.#...#...#...#...#.#.....#.......#...#.#...#...#...#.......#.#...#...#.....#.......#.#...#...#.....#...#...#.#...........#.#.....#.#
#.#.###.#####.###.###.#####.###.#########.#####.###.###.#####.#.###.#.#.#.#.#######.#.#.#####.###.###.#.###.###.#.###.###########.#.#####.#.#
#.#...#.........#...#.#.#...#...#.....#.#...#...#...#...#...#.#...#.#...#.#.#...#.#...#.........#.#...#...#.......#.........#...#.....#.#.#.#
#.#.#.#########.#.###.#.#.#######.###.#.#.###.###.###.###.#.#.###.#######.#.#.#.#.#####.#####.###.#.#######.#.###.#.#######.#.#.#.###.#.#.#.#
#.#.#.#.....#.....#.....#.........#...#.#.#...#...#.#.#...#.#...#.......#.#...#.#...#...#.....#...#...#...#...#...#.....#...#.#...#...#...#.#
#.#.#.#.###.#######.#######.#######.###.#.#.###.###.#.#.###.###.#######.#.#####.#.#.#.###.#####.#####.#.#.###.###.#####.#.#.#.#####.###.###.#
#...#...#.#.........#.....#.....#.#.#.....#.#...#.......#...#.#...#...#...#...#.#.#.#...#.#.....#.....#.#...#...#.......#.#.#.........#.#...#
###.#####.###########.###.#####.#.#.#######.#.###.#.#####.###.###.###.#######.#.#.#####.#.#.#####.#####.###.###.#######.#.#.#.###.###.#.#.###
#...................#.#...#.......#.......#.....#.#.....#...#...#.#...#.......#.#.......#.....#...#.......#...#...#...#...#.#...#...#...#...#
#.#.#.###.#.#.#.###.#.###.#######.#######.#.###.#######.###.#.#.#.#.#.###.#####.#.###########.#.#######.#.###.#.#.#.#.###.#.###.#.#.#.#####.#
#.#.....#.#.#...#...#...#.......#.#.....#.....#...........#.#...#...#...#.#.....#.......#...#.#.#.......#...#.#.....#...#.#.#.............#.#
#.###.#.#.#.###.#.#.###.###.#.#.###.###.#.###.###.#.#.###.#.#.#########.#.#.#.###########.#.###.#.#.#######.#.#########.#.#.#.###.#.#####.#.#
#...#...#.#...#.#.....#...#...#.....#.#.#...#.#.....#.#...#.#.........#...#.#.#...........#.....#.#.#.......#.......#...#.#.#.#...#...#.#.#.#
#.#.#.#######.#.#.###.###.###########.#.###.#.#.#####.#####.###.#####.###.#.###.#################.#.#.#.###########.#.###.###.#.#####.#.#.#.#
#.#.#.#.......#.#...#...#.#.....#.......#...#.#.....#.#...#...#.#.....#...#...#...#.............#.#.#.#.........#...#.#...#...#.#.....#.....#
###.#.#.#######.#####.#.#.#.###.###.#########.#####.#.#.#.#.#.#.#.#####.#.###.###.#.#.###.#.###.###.#.#.#####.###.###.###.#.#####.###.#####.#
#...#.#.....#...#.....#.#...#.#...#.....#.....#...#...#.#...#.#.#.#...#...#.#.#.....#...#.#...#.....#.#...#.#.....#...#...#.#.....#...#...#.#
#.#.#.#####.#.###.#####.#####.###.#####.#.#####.#.#####.#.###.#.###.#.###.#.#.#.#######.#.###.#######.###.#.#.#######.#.#.#.#.#.#######.#.###
#.#.#.#.....#.....#...#.#.....#.#.....#...#.....#...#...#.....#.....#...#.#.#.#.........#.#...#.........#.....#.....#.#.#.#.#.#.....#...#...#
#.###.#.#############.#.###.#.#.#.###.###.#.#######.#.#########.#.#.#.###.#.#.###########.#.###.#######.#####.###.#.#.#.#.#.#######.#.#####.#
#.#...#...#...........#.#...#.#.#...#.....#.......#.#.....#.....#.......#...#...........#...#.......#.....#...#...#...#.#.#...#...#...#...#.#
#.#.#.#.#.#######.###.#.#.#.#.#.###.#########.#.#.#.#####.#.#######.###.###.#####.###.#######.#####.#######.###.#######.#.###.#.#.###.#.#.#.#
#.#.#...#.#.......#...#.#...#.....#.............#.#...#.....#.......#.#.#.......#.....#.....#.#...#.#...#.......#...#...#...#.#.#...#...#.#.#
#.#.#.###.#.#######.###.###.#.#####.#.#.###.#####.###.#.###.#.#######.#.#.#######.#.###.###.#.#.#.#.#.#.#.###.###.#.#.###.###.#.###.#####.#.#
#...#.............#...#.....#.#...#.#.#.#.#...#...#.#...#.#...#.......#...#...#.......#.#.#...#.#.#...#...#.....#.#...#...#.................#
#.###.#####.#.###.###.#.#######.#.###.#.#.#.###.###.#####.#.###.#.###.#####.#.#.###.###.#.#######.#############.#.#########.#########.#####.#
#.....#.....#...#.#...#.#.......#...#...#.#...#...#.......#.#...#.#.#...#...#...#...#...#.........#.............#...........#.......#.....#.#
#####.#.###.###.#.#.###.#.#########.###.#.###.###.###.###.#.#.#.#.#.#.###.#######.###.###.#####.#.###.###.#######.###########.#####.#####.#.#
#.......#.#.....#.#...#.#...#.........#.....#.#...#.....#.#.#.....#.#.#...#...#.#.#...#.#...#...#...#.#...#...#...#.......#.......#.#...#...#
#.###.#.#.#######.###.#.###.#.#######.#.#####.#.###.###.#.#.#.###.#.#.#.###.#.#.#.#.###.#.###.#####.###.###.#.#.###.#####.#.###.###.#.#.###.#
#.#.#.#.#...#.....#...#...#.#.#.....#.#.#.....#.....#.#.#.#.#.....#.#.#.....#.#...#.#...#.#...#...#.....#...#.....#.#.....#.#.....#.#.#.#...#
#.#.#.#.###.#.###.#######.#.###.###.#.###.#.#.#######.#.#.#.#.#####.#.#######.#####.#.#.#.#.###.#####.#############.#.###.#.#####.#.###.#.###
#.#...#...#.#...#.#.....#.#.......#.#...#...#.#.......#.#...#.......#.......#.......#.#.#.#.#...#.....#.............#.#...#.................#
#.###.###.#.###.###.###.#.#.#####.#.###.###.###.#.#####.#.#.#####.#####.###.#####.#.#.#.#.#.#.#.#.#####.#############.#.###.#.#####.#.#.###.#
#.......#...#.#.......#...#...#...#...#.#...#...#.....#.#...#.....#.....#.#.#...#.#.#.#...#.#.#...#.....#.........#...#...#.#...#...#.#...#.#
#.###.#.###.#.###########.#.#.#######.#.#.###.#.#####.#.#.#####.###.###.#.#.###.#.#.#.#####.#.#.#.#.#####.#.#####.#.#.###.#.###.#.###.#####.#
#...#.#...#...#.#.........#.#.........#.#...........#.#.#.....#...#...#.#...#...#...#.#.....#.#...#...#.#.#...#...#.....#.#.#...#...#...#...#
###.#.###.###.#.#.#####.#.#.###.#.#####.#############.#.#####.#.#####.#.#.###.#######.#.#####.#######.#.#.###.#.###.#####.#.#.#####.#.#.#.###
#...#...#.....#.#.#...#.#.#...#...#...#.......................#.#.....#.#...#.#.......#.#.....#.....#.#...#...#.#...#...#...........#.#.#...#
#.###.#.#######.#.###.#.#.###.#####.#####.#######.###.#.#.#####.#.#####.###.#.#.#######.#######.###.#.#.###.###.#.#.#.#.#.###.#.#####.#.###.#
#.#...#.#.......#...#...#...#.#...........#...........#.#.....#.#.#.........#.#.....#.#.........#.....#.#...#...#.#...#.#.....#.#.....#...#.#
#.#.#.###.###.#####.#.#####.#.#############.###.#######.#####.#.#.#####.#####.#####.#.###########.#.#.###.###.###.#####.#######.#.#####.#.#.#
#...........#.....#.#...#.....#.....#.........#.#.......#...#.#.#.....#...#.........#.......#...#.#.#.....#.#.#...#.#.....#...#.#.#...#.#.#.#
#.###.#.#########.#.###.#####.#.###.#.#########.#.#######.###.#.#####.###.#.#####.#####.###.#.###.#########.#.#.###.#.#####.#.#.#.#.#.#.###.#
#.....#.#.......#.#...#.#...#.#.#.#.......#.....#...#.........#...#...#.#...............#...#.........#.....#.#.#...#...#...#.#.....#.#.#...#
#####.###.#####.#.###.#.#.#.#.#.#.#########.#######.#.#.###########.###.#.#####.###.#####.#####.###.###.###.#.#.###.###.#.#.#.###.###.#.#.###
#...#.....#...#.....#.#...#.#.#...#.#.....#.#.......#.#.............#.#...#.....#...#...#.........#...#...#.#.#.......#...#.#...#.....#...#.#
#.#.#########.###.###.#####.#.###.#.#.###.#.#.#######.#.###########.#.#.###.###.#.###.#.#########.###.#.###.#.#####.###.#.#.###.#.###.###.#.#
#.#...........#...#...#.#...#...#...#.#...#.....#.....#.....#.....#.....#...#...#...#.#.....#...#.#.#...#...#.....#.#...#.#.#...#.......#.#.#
#.#######.#####.###.###.#.#####.###.#.#.###.###.#.#########.#.###.#.#####.###.#######.#####.#.###.#.###.#.#######.###.#.#.#.#.###.#.#.#.#.#.#
#...#...#.#.......#...#.#...#...#...#.#.#.....#.#...........#.#...#...#.....#.#.......#...#.#...#...#...#...#.........#.#.#...#...#.....#.#.#
###.###.#.#.#########.#.###.#.###.###.#.#####.#.#############.#.#####.#####.#.#.#######.###.#.#.#.###.#.###.#.###########.#.###.#.#.#####.#.#
#.#...#.#.#.#.#.....#.#.#.........#...#.......#.#.........#...#.....#.......#.#...........#.#.#...#...#.#...#.#.#.........#.#...#...#.....#.#
#.###.#.#.#.#.#.###.#.#.#.#########.#########.#.#####.#####.#######.#########.###.#####.#.#.#####.#.###.#.###.#.#.###.###.#.###.#.###.#####.#
#...#.#.#.........#...#...#.......#...#.#.....#.#...#...#...#.....#.....#...#...#...#...#.#.#...#.#...#.#.......#.#...#...#.....#...#.#.....#
#.#.#.#.#####.###.#####.###.#####.#.#.#.#.#.#.#.#.#.###.#.###.###.###.#.#.#.###.###.#.#.#.#.#.#.#.###.#.#.#######.#.###.#.#######.#.#.#.###.#
#.#.#...#...#...#...#.......#...#.#...#...#...#.#.#...#.#.#...#...#...#.#.#...#...#.....#.#.........#.#.#.#.....#.#.#...#.....#.....#.#...#.#
#.#####.#.#.#.#.###.#########.###.#.#.###.#####.#.###.#.#.#.###.###.#####.###.###.#####.#.#####.###.#.#.#.#.#.#.#.#.#.#####.#.###.#.#.#####.#
#.....#.#.#...#...#.#...#.........#.#...#.#...#.....#.#...#...#...#.....#.#.#...#.#.....#.#.....#.....#.#.#.....#.#...#.....#.#...#.........#
#.#.#.#.#.#.###.###.#.#.###.###########.#.###.#####.#.#.###.#####.#####.#.#.#.###.#.#####.#.#####.#.###.###.#.###.###.#.###.#.#.#.#.#######.#
#.#.#.#...#...#...#.#.#...#.............#.......#...#.#.#...#...#...#...#...#.....#.#.............#...........................#.....#...#...#
###.#####.#.#####.#.#.###.#.###################.###.#.#.#.###.#.###.#.#.###.#######.###############.#.#.#######.###.###.###.#######.#.#.#####
#...#.....#.#...#.#...#...#.#...#.........#.....#...#.#.#...#.#.....#.#.#.#.#...............#.......#...#...#.#...#.................#.#.....#
#.#.#.#######.#.#.#####.#.###.#.#.###.###.#.#####.###.#.#####.###.#.#.#.#.#.#########.#####.#.#####.#.#.#.#.#.###.#######.#.#.###.###.#####.#
#.....#...#...#.......#.#...........#...#.#.#.....#...#...#...#...#.#.#.#.#.#.........#...#.#...#...#.#...#...#...#.......#.#...#.....#.....#
#.#####.#.#.#########.#.###.###.#######.###.###.###.#####.#.###.#.#.#.#.#.#.#.#######.#.#.#.###.#.#.#.#######.#.###.###.#.#.###.#######.#####
#.#.....#.#...#.#.....#.....#...#.....#.........#...#.....#...#.........................#.#...#.#...#.......#.#...#...#.#.....#.......#.....#
#.#.#####.###.#.#.###########.###.###.###########.###.#######.#.#.###.#.###########.#.###.#.###.###.#.#####.#####.###.#.###.#.#.###########.#
#.#...#.#.#...#.#...........#.....#...#...........#.#.#.....#.#...#.#.#.#...............#.#.....#...#...#.#.......#...#.#...#...#...........#
#.###.#.#.#.###.###########.#######.###.###########.#.#.#.#.#.###.#.#.###.#####.#####.###.#######.#####.#.#########.###.#.#.#####.#########.#
#S....#...........................#.....#.....................#.....#.........#...................#.................#.....#.......#.........#
#############################################################################################################################################"""

map = aoc.DM2(inputdata)
sx, sy = map.Find("S")
ex, ey = map.Find("E")
dir = aoc.Direction.EAST

bestMap = {}

tried = {}
possible = aoc.Heap(lambda x: x[3])
score = 0
x = sx
y = sy

def TryAddPossible(move):
    global tried, possible

    key = move[:3]

    if key not in bestMap or move[3] < bestMap[key][0]:
        bestMap[key] = [move[3], [move[4]]]
    elif move[3] == bestMap[key][0]:
        bestMap[key][1].append(move[4])

    if key not in tried or tried[key] > move[3]:
        possible.Push(move)
        tried[key] = move[3]

def GetBestNeigh(move):
    key = move[:3]
    if key in bestMap:
        return bestMap[key][1]
    return []

move = None

while True:
    nx, ny = aoc.Direction.MovePos(x, y, dir)
    if map[nx, ny] != "#":
        TryAddPossible((*aoc.Direction.MovePos(x, y, dir), dir, score + 1, move))
    TryAddPossible((x, y, aoc.Direction.TurnLeft(dir), score + 1000, move))
    TryAddPossible((x, y, aoc.Direction.TurnRight(dir), score + 1000, move))

    move = possible.Pop()

    if move[0] == ex and move[1] == ey:
        map.Mark(move[0], move[1])
        walk = list(GetBestNeigh(move))
        while len(walk) > 0:
            cur = walk.pop()
            if cur == None:
                continue
            map.Mark(cur[0], cur[1])
            walk.extend(GetBestNeigh(cur))

        break

    x = move[0]
    y = move[1]
    dir = move[2]
    score = move[3]

print(map.SumMarks())