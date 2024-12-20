import aoc, sys

inp = """26,67
47,59
34,67
7,7
10,29
6,29
13,23
58,59
11,38
13,9
21,5
70,47
15,24
23,24
51,43
10,9
61,45
18,13
31,57
27,27
14,29
40,45
31,51
59,30
17,64
20,25
3,16
25,6
3,10
35,65
67,49
5,1
17,9
65,27
34,47
19,26
55,43
17,4
12,27
61,43
15,7
29,4
19,2
50,49
59,47
54,51
27,1
37,53
63,47
19,11
41,42
11,43
28,67
17,3
53,43
39,47
51,49
65,64
31,65
69,44
31,55
25,7
29,51
23,23
47,43
63,37
9,35
55,52
2,19
55,58
54,27
35,55
67,41
42,53
67,62
37,11
15,28
3,25
5,20
25,15
4,17
23,3
57,52
40,41
13,20
54,61
13,19
31,68
4,1
64,53
33,7
25,51
24,5
31,14
65,33
19,21
29,5
60,53
68,57
15,1
13,10
55,56
50,43
55,35
17,22
31,49
30,49
59,70
53,29
9,1
59,63
59,56
3,1
55,47
30,47
15,21
13,38
51,41
25,16
27,7
33,65
69,34
9,13
69,43
57,57
61,47
22,13
64,59
57,53
11,7
61,40
62,69
27,5
69,67
49,57
54,37
55,45
27,58
61,30
12,13
65,57
63,27
5,9
36,57
25,14
60,27
69,33
31,63
17,20
57,26
59,29
69,55
33,63
13,11
15,6
9,9
43,56
65,63
60,47
25,5
63,42
35,53
6,11
15,19
45,43
27,21
7,9
7,29
48,41
67,58
66,57
65,65
9,17
22,23
69,60
67,39
67,45
35,42
25,60
20,13
33,15
20,11
27,63
33,47
69,49
33,11
59,62
35,52
55,53
43,55
44,41
65,41
10,3
39,48
12,7
16,9
61,48
63,26
62,45
57,33
15,25
5,19
31,7
6,15
13,17
3,17
53,54
60,51
21,21
15,2
35,22
41,56
0,9
8,37
10,25
67,51
63,67
9,4
59,68
48,53
33,62
66,33
39,42
23,19
25,9
63,41
52,57
2,5
53,45
11,27
1,16
57,38
50,57
49,34
19,15
47,51
36,47
64,61
3,14
5,28
29,19
69,41
28,57
35,60
63,33
7,18
32,23
5,46
17,1
55,51
23,5
28,21
22,5
13,32
61,53
31,3
9,15
7,15
57,54
48,43
33,53
30,55
69,56
48,51
24,17
53,59
57,36
5,24
69,39
32,57
39,7
29,15
29,59
45,57
27,12
3,21
20,7
33,52
56,29
4,13
41,53
49,63
9,22
68,49
29,11
49,43
33,19
22,9
21,13
67,35
69,54
30,29
45,50
7,20
23,11
19,29
3,9
65,31
8,27
64,49
11,0
11,25
12,15
59,39
59,26
45,55
38,55
59,53
24,23
62,67
6,17
11,36
31,54
51,59
21,64
61,50
57,28
4,19
66,29
5,31
57,59
11,30
19,14
9,7
36,49
19,4
63,34
27,4
23,21
68,29
64,57
29,1
7,3
66,35
50,53
55,39
62,29
63,35
67,44
4,7
11,1
1,11
47,41
25,2
58,27
67,42
64,65
57,45
19,22
17,37
30,1
55,31
31,47
57,29
61,69
69,42
21,9
19,3
37,54
27,8
2,1
63,51
67,66
3,15
44,49
11,33
64,29
61,31
63,43
20,1
70,67
23,1
14,27
10,39
45,49
35,15
65,55
45,46
35,50
17,17
5,13
57,55
61,67
39,55
21,3
54,55
21,63
15,12
63,44
61,33
7,26
65,51
27,3
17,12
65,46
59,45
19,1
69,46
13,8
5,8
52,65
31,0
47,37
3,5
67,47
17,15
13,3
52,41
17,13
43,45
11,14
35,45
67,31
9,40
21,18
43,47
33,46
9,19
30,57
30,21
59,59
29,50
23,8
51,55
55,63
48,57
7,22
33,12
55,55
5,26
29,52
41,41
14,17
21,23
28,55
53,51
52,53
43,41
15,9
51,45
11,11
57,48
53,68
13,7
28,61
18,9
60,33
41,44
18,7
40,47
30,63
53,49
63,55
9,41
13,1
11,41
26,21
25,11
21,66
39,51
39,45
11,35
49,66
21,6
13,25
7,23
49,45
57,27
29,61
57,63
17,30
69,69
43,43
27,22
66,61
63,65
9,6
47,57
17,16
64,69
57,35
9,28
31,61
17,67
64,51
63,53
57,68
24,55
16,25
47,49
59,35
9,43
7,25
44,63
27,15
61,55
11,17
46,55
60,63
33,68
24,21
28,5
7,4
14,19
62,33
35,51
61,57
65,47
11,39
9,23
38,5
9,27
32,65
57,61
59,49
45,51
25,13
2,9
7,19
65,61
47,46
49,51
67,67
46,69
16,3
66,47
21,17
21,48
35,63
17,11
62,55
69,47
41,55
65,66
23,2
7,30
55,57
45,47
3,23
66,51
37,49
11,19
30,25
63,45
67,37
53,58
44,43
69,30
45,53
25,8
9,21
5,6
11,15
16,19
27,2
40,53
21,19
30,53
58,41
33,61
63,59
30,17
9,37
9,2
15,13
59,33
64,27
1,5
15,27
66,69
48,39
67,65
30,23
14,5
31,18
25,3
51,47
31,62
49,49
67,59
42,41
27,17
31,46
33,21
21,10
70,37
9,25
24,67
47,63
67,48
11,18
67,43
45,52
43,46
59,27
65,44
11,23
29,60
9,29
11,21
5,17
34,63
39,57
19,19
9,5
60,35
29,57
29,63
42,47
34,59
1,1
55,42
23,13
61,58
59,66
13,41
61,64
55,38
8,1
68,53
3,4
67,63
29,67
33,20
10,15
29,13
21,20
16,35
61,32
13,4
56,61
53,39
7,27
23,18
27,23
11,31
35,47
32,51
57,50
29,21
13,12
59,44
69,51
29,18
47,44
61,62
68,33
51,37
59,69
56,69
62,53
64,35
55,68
13,24
13,21
9,39
27,65
49,41
19,18
29,53
45,37
30,67
63,62
37,57
21,16
49,53
6,5
57,64
29,25
43,44
67,40
29,64
13,5
49,52
54,45
11,29
59,57
25,23
36,19
15,14
1,3
18,1
20,17
17,21
15,3
63,29
24,3
27,47
5,11
3,24
6,13
37,51
55,33
50,37
39,19
3,3
9,36
47,47
29,66
47,48
49,31
57,43
5,15
13,13
47,45
36,1
41,45
61,49
11,6
21,25
1,7
13,33
69,57
14,9
23,25
61,61
25,20
26,17
65,29
27,62
16,31
65,45
33,51
19,9
8,7
3,19
9,16
33,59
65,69
26,65
2,13
26,11
46,43
55,62
57,39
33,67
33,56
48,55
52,43
31,21
58,37
43,48
5,2
11,22
13,31
17,19
51,50
61,27
57,41
53,63
13,36
64,31
25,19
28,15
68,69
53,60
31,15
63,69
55,41
65,56
65,59
68,67
33,55
14,1
9,3
5,25
58,43
18,5
27,19
11,24
23,12
59,58
33,54
33,10
7,24
37,55
8,33
59,46
47,55
23,31
3,22
59,60
19,5
42,51
65,38
23,15
9,30
53,37
31,59
69,37
19,7
57,51
52,55
67,57
67,55
3,7
34,1
46,47
61,37
21,11
9,34
8,13
27,61
60,67
69,53
11,13
29,16
67,36
41,58
60,29
61,56
11,5
59,65
44,57
67,69
7,21
15,5
55,44
63,46
7,5
0,5
54,41
57,47
8,9
65,35
51,51
46,57
51,57
26,5
12,33
67,64
65,49
23,7
34,13
35,61
65,32
68,39
10,43
53,53
12,1
69,45
7,10
35,49
44,53
15,15
69,52
5,23
17,26
69,59
57,40
67,53
16,11
61,35
41,43
31,11
65,68
9,18
9,32
2,7
59,51
29,2
65,67
35,62
35,59
63,60
58,65
9,38
63,49
30,59
55,40
17,7
67,38
37,47
5,3
61,59
6,3
69,36
56,35
11,9
49,48
35,54
9,33
24,13
37,58
25,66
17,28
27,14
29,55
13,15
41,57
63,48
27,18
49,47
33,48
12,3
13,22
28,69
25,55
62,51
11,20
39,53
29,10
67,33
27,13
22,21
61,51
36,65
4,11
15,16
23,9
12,9
43,57
63,63
15,11
17,5
33,50
9,20
59,55
9,44
67,29
56,63
40,51
19,10
69,50
1,2
52,51
49,55
33,57
32,11
21,15
31,67
3,29
5,27
18,15
31,53
13,30
46,41
59,61
17,23
29,3
35,36
21,7
47,50
38,45
38,49
50,45
57,56
57,58
3,20
61,29
49,30
33,64
11,34
69,31
43,54
23,17
7,17
29,9
27,9
49,39
11,3
49,46
63,57
12,17
37,46
45,45
58,49
9,31
31,19
8,25
13,40
62,65
35,57
13,29
41,47
31,60
56,43
29,65
9,42
31,20
69,35
53,57
21,4
21,1
19,17
55,59
33,49
63,31
27,25
15,33
59,54
27,59
53,41
49,56
20,21
17,25
26,29
25,10
55,37
62,59
33,58
6,9
11,37
7,1
19,13
56,53
22,1
29,47
18,23
27,45
62,37
52,45
61,65
65,54
1,9
23,67
67,46
13,26
45,41
47,53
27,11
50,41
5,22
16,7
43,61
3,43
41,39
26,69
9,62
62,25
45,27
10,53
49,11
58,17
27,33
2,25
61,2
49,5
3,50
53,47
32,39
9,54
45,21
11,69
69,4
37,3
3,59
41,34
66,31
56,15
1,18
50,21
59,19
55,21
69,8
47,19
22,45
23,55
15,45
39,36
23,37
65,37
3,48
53,69
38,29
55,27
48,33
53,24
49,6
31,45
23,41
33,44
25,50
40,59
3,44
0,59
38,57
23,34
40,21
46,59
35,26
59,31
5,54
70,21
27,69
38,15
51,9
61,21
37,37
39,62
39,9
37,65
37,17
63,61
65,24
14,59
41,7
23,16
36,67
58,45
47,5
39,35
27,35
55,5
2,65
47,9
41,32
61,0
11,53
21,28
4,41
53,2
29,39
61,39
6,45
29,12
45,16
12,55
42,59
35,21
37,52
38,65
67,12
10,59
64,3
51,35
15,35
49,61
51,21
53,8
49,20
9,69
7,46
43,1
37,39
36,7
49,24
19,25
39,29
4,43
15,42
14,69
19,41
53,55
69,23
35,9
9,65
65,9
47,38
5,36
25,29
5,48
12,47
35,5
61,3
27,55
39,5
1,59
22,65
58,21
69,63
27,31
4,59
35,16
5,21
55,18
33,9
11,57
28,9
3,47
29,43
3,39
6,49
0,47
43,25
66,17
24,39
41,23
27,41
39,25
39,67
69,24
9,46
47,69
59,17
20,33
25,53
47,20
45,34
27,28
69,64
40,9
61,4
28,45
53,7
27,34
28,7
41,65
1,15
39,21
65,1
24,29
29,33
18,37
59,14
68,11
15,65
12,41
13,45
28,41
4,57
69,28
22,33
10,49
45,1
17,69
24,41
47,29
5,70
2,61
17,49
9,51
37,56
7,52
19,54
28,49
19,23
33,25
31,43
44,1
39,18
35,39
26,25
67,8
47,33
13,39
31,8
44,15
38,33
35,7
57,65
45,11
41,17
17,52
21,54
19,66
20,57
13,53
13,69
55,22
33,36
37,25
35,67
15,55
37,69
18,31
1,40
68,17
26,39
41,13
31,9
5,32
3,67
33,16
29,45
56,17
39,1
13,43
60,41
19,69
27,36
15,51
25,47
24,31
45,32
43,13
23,57
20,29
61,41
69,7
39,43
59,15
3,11
16,47
47,7
40,27
63,25
33,27
58,7
19,65
3,34
10,67
34,23
29,49
7,13
63,15
51,67
1,45
43,15
50,59
65,21
63,13
23,39
44,65
46,63
13,59
35,44
5,50
52,3
25,35
1,23
45,39
37,61
31,4
30,37
5,39
43,27
43,3
3,40
24,53
49,59
22,51
25,45
31,13
51,65
50,5
35,40
21,58
51,62
65,3
63,11
50,17
49,9
31,44
53,6
21,29
41,24
33,41
5,45
15,61
53,11
51,2
17,63
46,11
65,7
3,42
1,67
33,31
29,31
15,62
65,5
19,27
13,67
67,23
26,53
61,1
19,50
59,21
63,5
35,30
17,57
30,39
65,13
33,24
70,63
24,49
21,49
37,40
43,32
23,42
17,43
23,26
3,63
37,23
19,53
56,9
39,26
35,27
3,46
53,1
58,31
0,43
38,7
1,64
64,15
17,27
21,67
25,43
51,20
45,63
30,13
7,39
7,40
43,59
7,55
41,22
17,51
23,61
41,4
67,26
17,46
37,62
13,49
11,44
35,28
3,13
7,65
1,13
65,42
57,15
56,21
57,23
9,12
51,39
1,68
51,17
50,33
49,68
42,3
5,61
15,69
63,7
41,67
51,26
17,41
61,11
37,10
23,45
36,5
13,63
43,39
57,11
45,17
41,49
25,63
17,59
41,3
52,47
10,61
15,50
66,15
42,63
41,35
49,19
43,37
43,51
58,15
15,23
51,27
23,36
33,45
45,13
43,29
47,15
31,27
27,42
7,53
25,57
65,40
41,27
25,1
40,5
52,29
17,62
29,32
27,43
39,65
68,3
21,50
27,44
49,37
13,47
38,27
22,61
3,69
68,13
21,70
33,37
62,21
21,47
59,7
31,5
59,25
57,3
17,29
11,61
69,1
63,14
37,19
5,51
69,9
25,34
7,35
1,31
6,43
26,55
43,66
11,51
61,36
55,48
49,7
45,7
35,43
11,45
35,2
57,25
65,6
49,69
33,35
35,11
65,11
58,33
18,43
63,6
61,9
19,59
37,35
45,10
37,20
55,67
51,30
53,67
33,39
43,65
41,5
69,29
42,39
49,21
2,31
51,64
15,53
3,31
51,24
61,5
31,36
23,65
33,18
6,57
42,69
57,0
5,35
1,28
16,37
53,15
20,41
48,11
64,25
25,26
15,41
13,61
25,17
15,66
63,3
46,67
66,1
1,27
43,24
11,49
19,35
17,58
1,25
39,38
5,64
41,26
50,63
53,9
67,3
15,37
3,37
25,67
6,31
56,23
54,35
34,39
35,1
51,7
18,53
25,49
69,25
40,55
47,65
37,7
17,56
49,26
23,51
11,55
25,21
17,55
35,69
49,25
51,11
19,39
14,49
58,5
19,40
49,14
25,25
27,48
23,33
2,67
35,4
15,56
61,42
35,3
57,9
47,3
37,15
43,7
5,41
53,64
31,23
49,29
51,36
25,27
31,6
1,37
35,70
16,43
46,7
3,64
39,49
21,57
55,19
47,39
42,37
37,22
57,7
5,7
35,19
10,11
23,47
9,59
37,8
37,21
41,61
37,33
9,70
49,33
53,32
26,41
1,17
5,62
39,20
5,65
6,39
35,35
5,69
25,31
51,1
55,7
53,21
12,53
52,39
3,53
35,33
20,37
36,35
45,9
51,5
27,57
60,17
11,47
29,17
31,26
45,69
46,1
47,61
43,34
39,32
2,51
18,59
47,16
61,7
21,33
3,45
55,25
33,5
3,62
14,47
58,1
20,47
55,24
55,23
3,41
51,53
25,61
16,39
63,39
29,30
1,60
48,59
51,12
42,17
39,59
19,34
37,27
31,29
69,27
43,8
57,21
1,19
19,44
12,43
20,31
45,24
19,49
37,24
23,69
37,45
40,69
70,1
47,66
44,17
53,5
57,69
54,19
31,39
10,51
53,31
40,29
43,31
35,18
68,15
55,15
23,53
23,46
19,33
59,24
33,13
51,3
1,43
16,51
66,3
5,34
41,10
53,34
49,17
14,45
7,49
3,57
33,14
61,25
4,67
68,23
61,17
6,67
33,2
51,63
33,33
7,33
8,15
1,39
15,58
59,6
3,33
7,61
39,34
67,6
27,51
21,39
41,29
27,38
45,5
65,15
23,59
22,43
40,1
15,68
43,30
55,11
25,70
52,69
39,39
23,49
39,63
9,47
25,33
49,27
9,45
38,37
19,51
7,67
33,34
65,25
15,22
36,25
2,57
2,49
13,64
41,37
21,37
45,26
30,43
41,18
43,63
39,13
17,53
12,67
2,69
45,18
19,68
27,29
69,3
69,15
43,67
57,31
48,63
1,35
52,23
33,43
39,33
1,30
7,37
1,12
45,19
19,67
47,8
56,47
69,13
7,60
23,29
39,17
36,37
48,25
59,23
47,35
43,28
37,41
5,33
58,13
67,54
51,0
35,29
15,44
21,62
37,59
49,28
33,17
2,37
60,9
22,27
19,70
3,35
52,9
21,51
1,57
70,13
43,33
60,21
32,27
53,61
45,59
37,5
55,6
45,15
59,43
69,5
29,27
69,61
47,25
21,27
60,23
14,61
67,1
43,11
7,47
13,51
1,55
57,49
11,46
53,33
55,61
38,23
51,6
67,25
50,39
25,32
44,21
41,63
63,12
69,17
11,59
17,31
43,4
61,15
9,64
51,69
61,44
9,53
1,51
30,33
53,4
62,23
26,31
7,51
37,60
5,5
19,37
57,67
17,61
24,57
19,60
63,10
69,19
11,63
48,5
33,23
35,41
5,52
46,35
19,24
66,11
23,35
22,57
55,2
3,27
49,22
62,7
36,15
39,15
10,69
21,69
37,31
49,15
21,68
63,9
9,49
49,3
55,4
17,39
20,51
41,33
32,41
53,27
47,11
38,3
59,11
53,17
21,35
1,36
60,5
39,41
5,67
51,23
4,33
34,33
43,6
57,37
22,39
56,3
41,66
1,69
16,53
0,21
17,45
9,56
45,25
45,23
15,67
1,29
11,48
59,1
4,55
3,61
1,22
67,61
18,61
23,63
56,11
5,57
21,65
64,1
59,13
55,34
52,17
65,23
21,55
62,17
50,67
24,59
48,1
7,11
1,54
5,43
29,35
11,67
27,53
63,17
49,65
49,13
52,35
68,27
7,57
67,11
39,61
51,61
32,15
15,43
34,5
2,27
3,55
67,13
67,19
23,40
11,65
67,27
31,31
45,2
30,27
63,1
5,66
49,23
38,11
31,33
53,35
12,63
25,59
54,11
57,13
67,5
65,8
39,27
17,47
29,7
1,61
1,65
43,19
69,11
5,37
45,30
15,31
67,7
34,9
66,21
27,49
24,45
53,23
29,40
25,64
55,69
33,22
33,42
39,37
41,51
63,18
44,37
61,10
44,39
48,69
47,31
33,3
21,45
13,27
45,29
55,49
48,61
12,61
44,59
14,55
64,17
45,33
2,35
35,13
43,36
58,19
22,53
14,33
47,14
53,20
59,9
15,39
41,69
51,25
55,65
34,31
47,24
61,38
14,41
17,33
4,29
37,67
40,39
19,43
7,54
37,63
41,8
54,15
7,68
53,16
15,59
55,1
69,18
43,12
5,59
1,63
43,35
46,27
45,61
47,36
25,39
47,21
62,13
52,13
59,67
6,35
45,65
23,48
62,19
28,29
49,35
20,45
40,67
57,19
61,13
60,13
52,49
18,47
63,8
17,65
56,31
55,3
19,61
43,10
47,17
67,17
65,19
68,19
48,37
19,42
59,5
26,47
37,44
49,67
11,56
66,5
27,24
23,52
29,69
37,64
54,9
69,6
17,66
19,36
9,11
64,39
10,65
49,8
41,25
43,69
39,12
47,23
42,15
13,57
67,22
19,31
1,33
31,25
18,29
39,64
3,51
51,29
65,20
31,35
48,13
39,2
53,19
31,37
26,51
27,67
13,65
55,17
2,45
32,33
43,5
47,1
54,65
57,17
44,61
15,29
25,44
34,27
47,32
29,23
40,31
3,49
49,1
60,19
19,47
21,43
66,25
47,67
27,46
2,53
15,17
23,68
36,13
59,2
7,69
44,27
69,21
60,11
13,58
41,19
13,50
63,21
51,31
56,13
33,8
41,15
25,69
21,38
29,41
21,59
53,13
35,31
8,51
53,22
50,11
51,13
65,17
14,65
31,69
1,38
27,39
31,17
35,17
53,26
43,20
47,18
5,47
43,49
7,31
43,17
5,53
57,5
15,63
19,45
61,23
7,63
44,13
38,17
21,61
5,60
68,61
23,27
54,29
9,63
29,29
1,53
25,41
7,45
17,40
41,14
19,64
39,3
32,3
58,11
52,67
67,15
33,69
65,14
7,41
19,56
3,58
22,31
25,36
35,23
41,50
55,13
8,49
43,53
40,61
35,25
38,67
41,6
41,64
31,41
53,25
49,10
33,1
15,54
53,12
39,14
3,65
46,13
33,30
55,9
54,47
5,55
7,43
41,9
37,30
9,67
61,16
7,62
41,21
21,41
22,35
21,53
14,35
31,34
12,69
24,63
61,63
63,19
23,28
41,20
19,57
42,29
28,35
15,47
9,61
1,41
13,37
42,1
18,39
5,29
39,23
27,37
37,38
55,29
46,21
23,43
38,51
12,51
65,53
17,68
46,5
69,65
45,35
1,32
45,67
31,42
35,37
19,63
51,33
48,29
45,3
59,3
29,37
37,43
51,28
36,11
1,21
5,38
67,9
32,31
57,1
44,69
37,13
43,21
9,57
18,49
33,38
59,37
12,59
21,31
25,37
55,66
13,35
46,29
1,49
17,34
40,13
25,62
67,21
13,55
63,22
16,63
51,14
37,29
59,38
43,9
39,69
56,7
51,32
65,39
67,2
44,7
37,9
15,57
36,33
51,60
65,43
28,25
11,66
5,49
19,55
70,25
61,19
43,26
1,47
30,7
49,16
51,19
25,56
25,65
63,4
41,1
39,11
7,56
43,23
53,3
47,62
26,59
51,18
37,68
41,11
53,65
32,69
9,55
51,15
45,4
5,63
59,8
45,31
63,23
21,60
7,64
54,31
17,35
15,49
47,4
49,2
39,31
7,59
2,55
64,21
41,59
0,51
37,42
33,29
59,41
47,64
7,42
37,1
65,10
47,27
31,1
0,27
9,58
17,32
8,67
39,70
43,68
47,13
43,22
42,61
41,31
8,61
58,2
40,54
47,58
38,48
16,14
62,22
49,4
62,48
2,3
6,46
42,68
34,40
16,62
52,66
30,19
10,47
25,4
11,52
56,25
38,36
31,38
56,60
41,62
57,24
62,49
42,65
54,52
26,43
28,62
12,24
68,70
36,62
30,42
70,62
18,68
30,10
32,8
4,66
16,38
54,68
28,3
27,0
27,56
10,63
0,19
54,42
40,20
24,48
14,51
28,37
26,2
42,49
44,24
32,67
54,39
29,42
52,60
38,47
59,40
2,41
70,64
16,6
18,70
56,1
0,30
14,70
23,60
41,70
31,50
51,42
1,48
66,20
22,16
3,70
28,8
10,52
42,45
64,45
1,46
14,40
31,48
15,38
9,60
63,66
44,47
24,16
70,44
29,58
0,25
30,50
52,26
28,47
55,28
32,63
40,24
60,20
30,38
18,57
67,70
59,64
58,57
66,42
36,66
4,60
1,4
2,23
0,69
6,66
31,66
44,28
52,61
52,38
29,70
66,38
20,43
56,33
2,66
36,58
3,68
40,60
37,50
43,38
18,30
38,18
60,31
51,38
12,22
18,2
59,20
63,2
62,43
4,68
35,66
61,46
14,52
56,70
64,26
56,38
13,54
63,70
40,58
18,17
44,50
60,40
63,52
0,8
34,46
48,38
60,64
44,66
56,14
62,66
43,40
57,34
37,32
8,57
32,66
3,36
70,0
0,68
70,35
54,7
18,63
28,34
32,9
8,69
66,10
16,60
36,16
20,20
22,3
32,37
18,41
28,31
39,0
20,65
18,18
29,20
0,49
36,48
46,37
56,26
45,44
4,30
28,58
38,68
12,64
66,36
38,60
54,10
0,46
7,70
45,36
66,50
46,61
42,46
40,43
54,34
48,28
2,34
4,20
0,67
68,64
44,16
48,42
54,33
70,60
44,51
24,66
20,30
38,22
36,45
17,2
13,60
12,28
50,61
30,69
34,28
5,42
1,34
58,3
36,36
18,6
12,52
46,49
58,35
32,68
22,63
62,41
4,36
34,64
38,50
53,46
58,68
26,20
32,61
4,32
30,58
64,2
10,70
64,47
12,2
8,62
32,34
70,45
50,6
67,50
13,52
66,7
16,61
10,55
11,64
48,18
42,34
8,58
14,18
44,52
34,34
58,25
0,62
56,46
13,56
58,69
30,52
52,36
23,70
3,38
18,0
22,14
15,4
1,20
40,30
70,18
46,0
2,24
69,66
48,64
2,63
36,18
34,2
54,32
51,40
29,26
40,18
8,65
30,34
13,0
38,25
47,52
23,32
60,34
68,12
66,70
2,58
23,30
48,44
38,31
1,26
33,4
55,20
18,14
46,2
4,70
26,68
48,65
46,42
8,70
3,66
2,36
67,56
18,40
65,36
20,70
26,45
31,10
24,70
29,38
57,4
67,28
48,45
33,60
53,48
68,21
24,36
52,42
11,60
20,16
34,21
21,40
31,70
18,21
14,3
52,0
46,25
54,53
18,32
22,36
41,36
32,62
1,66
36,61
60,48
56,48
48,26
32,7
25,40
66,43
48,60
2,46
40,37
21,2
10,26
8,26
46,56
32,60
58,46
8,60
54,28
50,18
18,35
46,23
60,1
30,60
54,1
29,8
38,34
67,32
58,4
52,1
46,53
6,10
59,32
54,64
58,52
46,46
24,20
20,59
2,48
68,55
67,18
23,58
34,37
46,38
40,17
45,66
11,62
70,19
30,36
32,14
29,44
10,0
12,68
4,6
63,38
52,25
32,59
56,24
47,2
46,44
51,44
36,60
41,54
60,70
49,64
62,36
48,16
48,49
56,39
47,54
34,3
30,44
65,2
69,70
4,2
37,48
52,68
4,23
54,22
1,24
24,19
32,49
0,24
21,14
45,62
31,40
50,0
40,44
50,62
23,62
32,12
51,66
55,14
24,58
50,48
35,64
50,42
16,65
29,0
32,64
17,14
68,62
26,19
44,55
14,50
54,21
32,6
8,53
4,69
46,24
28,66
40,56
2,43
8,55
56,66
44,70
48,46
64,41
54,6
28,56
6,30
24,46
24,4
21,52
12,19
26,13
51,4
60,30
41,16
58,53
53,28
44,67
40,63
24,30
42,33
44,46
70,20
22,29
16,16
40,22
0,17
43,70
0,2
64,13
40,68
6,60
60,2
6,58
61,20
36,70
58,18
18,38
22,38
45,22
42,50
34,66
64,50
50,64
39,16
20,50
41,46
60,49
34,57
58,38
69,62
46,26
50,70
7,58
10,27
40,38
3,54
28,70
56,6
43,52
60,32
30,46
56,44
48,68
24,44
32,46
26,1
11,58
45,68
16,1
56,27
26,12
17,6
2,47
52,10
6,64
20,61
54,40
6,68
16,42
18,16
54,56
36,63
2,59
24,62
22,20
57,2
56,49
16,41
10,36
36,51
37,70
18,54
64,42
34,36
10,31
14,56
14,2
57,22
21,34
18,42
26,33
10,37
28,33
70,41
18,58
43,62
26,4
60,3
40,19
30,0
4,25
30,45
22,30
58,54
35,48
54,38
40,46
60,22
4,42
40,35
10,38
53,36
58,10
45,48
12,29
7,0
58,32
68,66
40,57
16,0
34,52
48,3
59,52
22,68
10,45
53,66
4,8
43,58
2,50
70,11
36,52
16,46
70,30
44,31
4,27
50,19
53,0
2,40
31,32
20,63
34,56
54,59
70,68
14,62
45,14
50,68
11,16
20,2
52,62
4,61
63,64
54,44
58,28
4,31
48,47
27,70
22,54
6,6
56,22
2,68
58,29
36,8
8,4
28,18
40,28
23,56
27,16
70,10
38,52
66,62
60,62
57,20
30,15
57,62
3,26
49,12
66,14
60,44
70,16
44,20
42,66
35,46
18,20
68,38
12,4
47,70
34,68
55,70
26,37
31,12
16,66
43,60
10,54
20,60
19,20
34,43
28,6
55,10
42,64
6,7
36,56
24,15
50,10
66,8
48,10
18,8
50,32
33,28
57,46
24,64
22,4
70,34
28,50
22,37
18,60
68,9
2,62
28,60
39,8
38,44
18,28
16,30
18,50
51,52
0,33
38,8
18,52
44,0
30,28
7,16
5,4
9,68
9,14
60,26
34,4
28,13
47,10
42,7
50,29
31,24
60,8
26,44
18,48
59,50
60,69
6,2
24,47
42,32
22,40
68,47
45,70
6,61
62,5
30,2
44,34
6,40
14,8
46,31
8,29
28,38
46,19
60,66
70,66
69,2
55,36
69,38
68,56
10,18
42,5
1,42
34,70
26,52
64,10
30,4
58,56
42,6
66,22
67,20
50,2
14,38
2,60
11,8
23,20
37,16
4,28
67,0
46,8
16,64
22,67
48,66
22,56
3,52
54,69
20,48
45,60
68,7
20,36
56,62
6,25
44,40
62,50
62,0
48,9
34,19
34,6
70,17
34,42
61,8
7,28
50,4
56,50
44,38
34,29
12,38
62,38
4,44
10,57
44,64
12,32
18,69
43,50
16,50
13,18
57,30
30,70
8,50
22,47
42,11
11,50
37,36
60,18
22,46
42,42
12,10
32,44
13,70
24,18
35,68
32,35
42,30
69,12
47,0
36,26
64,43
20,35
15,48
60,38
10,32
16,27
22,49
38,63
8,46
5,10
8,11
70,49
2,44
24,8
6,59
15,60
21,8
52,30
8,8
52,40
5,30
7,34
32,25
23,22
35,24
44,23
46,34
69,40
64,52
26,36
48,62
58,55
42,31
34,8
28,19
51,10
61,18
38,38
6,36
40,4
37,4
24,69
68,52
10,12
54,18
36,59
43,18
16,56
43,64
15,64
61,12
0,54
12,35
67,10
8,0
0,15
30,62
6,26
68,28
16,26
41,38
39,44
25,48
0,1
68,42
16,55
16,69
42,52
30,30
48,34
60,46
66,23
19,16
4,52
68,63
0,63
41,48
16,4
17,48
4,51
20,58
62,24
64,60
54,46
23,4
69,48
16,2
46,48
65,70
58,0
51,16
36,2
6,4
8,6
34,16
34,24
4,48
64,44
36,39
21,0
16,15
18,19
1,6
54,70
5,18
4,21
8,40
56,56
40,36
28,28
70,69
14,57
63,68
31,52
66,0
32,36
32,47
41,68
10,30
50,22
14,53
56,8
52,14
45,58
48,52
48,32
61,54
35,6
14,13
54,23
70,33
22,60
36,27
1,44
70,65
17,50
16,18
6,52
20,44
2,33
58,8
53,14
34,26
4,16
14,66
29,14
55,0
44,45
60,24
20,34
52,4
23,6
25,46
39,56
2,30
4,65
14,60
54,5
40,3
28,17
62,54
54,8
26,40
68,30
57,42
62,40
61,26
66,45
21,46
58,30
8,59
2,8
20,46
24,60
48,67
54,16
28,11
36,12
70,3
28,59
58,66
3,30
50,54
36,43
16,40
40,7
39,4
13,42
11,42
56,10
57,14
54,30
50,66""".splitlines()

map = aoc.DM2.MakeFromDims(71, 71)

for i in range(1024):
    map[aoc.GetInts(inp[i], ",")] = "#"

goal = (map.Width - 1, map.Height - 1)

map.FindPath((0, 0), goal, True)

for i in range(1024, len(inp)):
    ns = aoc.GetInts(inp[i], ",")

    map[ns] = "#"
    
    if map.IsMarked(*ns):
        map.ResetMarks()
        if not map.FindPath((0, 0), goal, True):
            print(ns)
            break
