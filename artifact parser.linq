<Query Kind="Program">
  <NuGetReference>morelinq</NuGetReference>
  <NuGetReference>protobuf-net</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>ProtoBuf</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
    var raw = @"00 01 0A A9 51 0A 24 63 66 37 66 38 38 62 31 2D
39 33 32 34 2D 34 62 31 66 2D 61 64 66 31 2D 39
66 62 30 32 63 66 35 63 61 66 31 12 06 34 35 30
31 30 34 1A 1E 0A 07 63 72 65 61 74 6F 72 12 13
12 11 66 62 31 30 30 30 30 31 35 32 39 33 32 37
31 35 35 1A 14 0A 07 63 72 65 61 74 65 64 12 09
08 08 50 C3 A7 CB FF C1 29 1A CF 3F 0A 07 6D 65
6D 62 65 72 73 12 C3 3F 08 09 5A A4 01 12 A1 01
08 0A 62 18 0A 06 70 6C 61 79 65 72 12 0E 12 0C
6B 6F 6E 67 31 31 39 35 37 37 39 31 62 0C 0A 04
72 61 6E 6B 12 04 08 01 18 02 62 0C 0A 06 74 72
6F 6F 6E 73 12 02 08 01 62 15 0A 04 6E 61 6D 65
12 0D 12 0B 43 72 61 7A 79 73 63 6F 72 63 68 62
26 0A 0A 61 63 74 69 76 65 53 6B 69 6E 12 18 12
16 4C 5F 41 54 41 36 65 50 46 6B 61 53 65 33 70
53 68 56 4D 54 65 51 62 0E 0A 05 6C 65 76 65 6C
12 05 08 01 18 86 01 62 18 0A 0B 6C 61 73 74 53
65 73 73 69 6F 6E 12 09 08 08 50 89 9A 94 BE 8D
2C 5A A6 01 08 01 12 A1 01 08 0A 62 18 0A 06 70
6C 61 79 65 72 12 0E 12 0C 6B 6F 6E 67 31 39 37
34 32 38 37 39 62 0C 0A 04 72 61 6E 6B 12 04 08
01 18 02 62 0C 0A 06 74 72 6F 6F 6E 73 12 02 08
01 62 16 0A 04 6E 61 6D 65 12 0E 12 0C 50 65 74
65 72 57 61 6C 74 65 72 73 62 26 0A 0A 61 63 74
69 76 65 53 6B 69 6E 12 18 12 16 62 58 55 7A 56
71 46 32 50 30 32 34 69 37 5F 45 61 64 41 32 55
67 62 0D 0A 05 6C 65 76 65 6C 12 04 08 01 18 78
62 18 0A 0B 6C 61 73 74 53 65 73 73 69 6F 6E 12
09 08 08 50 EB AB A7 EA E5 2A 5A A8 01 08 02 12
A3 01 08 0A 62 1F 0A 06 70 6C 61 79 65 72 12 15
12 13 73 69 6D 70 6C 65 31 34 34 30 38 36 37 39
36 30 31 30 32 62 0C 0A 04 72 61 6E 6B 12 04 08
01 18 03 62 0C 0A 06 74 72 6F 6F 6E 73 12 02 08
01 62 10 0A 04 6E 61 6D 65 12 08 12 06 44 65 6E
76 65 72 62 26 0A 0A 61 63 74 69 76 65 53 6B 69
6E 12 18 12 16 36 57 4A 38 35 67 48 79 43 30 4B
67 6D 4A 63 53 53 5A 58 66 43 41 62 0E 0A 05 6C
65 76 65 6C 12 05 08 01 18 8D 01 62 18 0A 0B 6C
61 73 74 53 65 73 73 69 6F 6E 12 09 08 08 50 AD
F9 B1 F2 D4 2B 5A AC 01 08 03 12 A7 01 08 0A 62
1D 0A 06 70 6C 61 79 65 72 12 13 12 11 66 62 31
30 30 30 30 32 32 39 31 31 34 37 39 37 34 62 0C
0A 04 72 61 6E 6B 12 04 08 01 18 02 62 0C 0A 06
74 72 6F 6F 6E 73 12 02 08 01 62 17 0A 04 6E 61
6D 65 12 0F 12 0D 62 69 6F 6E 69 63 70 69 6B 61
63 68 75 62 26 0A 0A 61 63 74 69 76 65 53 6B 69
6E 12 18 12 16 36 57 4A 38 35 67 48 79 43 30 4B
67 6D 4A 63 53 53 5A 58 66 43 41 62 0D 0A 05 6C
65 76 65 6C 12 04 08 01 18 73 62 18 0A 0B 6C 61
73 74 53 65 73 73 69 6F 6E 12 09 08 08 50 CD AE
8D DD B5 2B 5A A5 01 08 04 12 A0 01 08 0A 62 1F
0A 06 70 6C 61 79 65 72 12 15 12 13 73 69 6D 70
6C 65 31 33 39 35 37 39 36 38 31 35 32 31 38 62
0C 0A 04 72 61 6E 6B 12 04 08 01 18 02 62 0C 0A
06 74 72 6F 6F 6E 73 12 02 08 01 62 0D 0A 04 6E
61 6D 65 12 05 12 03 63 78 63 62 26 0A 0A 61 63
74 69 76 65 53 6B 69 6E 12 18 12 16 53 6C 59 67
55 67 68 45 6F 55 47 31 66 71 35 36 64 65 30 62
6A 77 62 0E 0A 05 6C 65 76 65 6C 12 05 08 01 18
83 01 62 18 0A 0B 6C 61 73 74 53 65 73 73 69 6F
6E 12 09 08 08 50 CE C7 A4 A6 AE 2B 5A AF 01 08
05 12 AA 01 08 0A 62 1F 0A 06 70 6C 61 79 65 72
12 15 12 13 73 69 6D 70 6C 65 31 33 34 32 35 33
36 38 30 33 39 33 37 62 0C 0A 04 72 61 6E 6B 12
04 08 01 18 02 62 0C 0A 06 74 72 6F 6F 6E 73 12
02 08 01 62 17 0A 04 6E 61 6D 65 12 0F 12 0D 4E
69 63 6B 57 61 74 74 65 72 73 6F 6E 62 26 0A 0A
61 63 74 69 76 65 53 6B 69 6E 12 18 12 16 4C 5F
41 54 41 36 65 50 46 6B 61 53 65 33 70 53 68 56
4D 54 65 51 62 0E 0A 05 6C 65 76 65 6C 12 05 08
01 18 8E 01 62 18 0A 0B 6C 61 73 74 53 65 73 73
69 6F 6E 12 09 08 08 50 A1 A0 B4 9A 8B 2C 5A AB
01 08 06 12 A6 01 08 0A 62 17 0A 06 70 6C 61 79
65 72 12 0D 12 0B 6B 6F 6E 67 35 35 39 31 36 30
37 62 0C 0A 04 72 61 6E 6B 12 04 08 01 18 02 62
15 0A 06 74 72 6F 6F 6E 73 12 0B 08 06 41 03 9D
36 90 7E 4A 12 41 62 12 0A 04 6E 61 6D 65 12 0A
12 08 43 61 72 75 73 69 75 73 62 26 0A 0A 61 63
74 69 76 65 53 6B 69 6E 12 18 12 16 4C 5F 41 54
41 36 65 50 46 6B 61 53 65 33 70 53 68 56 4D 54
65 51 62 0E 0A 05 6C 65 76 65 6C 12 05 08 01 18
92 01 62 18 0A 0B 6C 61 73 74 53 65 73 73 69 6F
6E 12 09 08 08 50 80 F9 C6 BD 99 2C 5A A4 01 08
07 12 9F 01 08 0A 62 19 0A 06 70 6C 61 79 65 72
12 0F 12 0D 61 72 6D 6F 72 5A 68 65 6E 72 79 66
74 62 0C 0A 04 72 61 6E 6B 12 04 08 01 18 02 62
0C 0A 06 74 72 6F 6F 6E 73 12 02 08 01 62 12 0A
04 6E 61 6D 65 12 0A 12 08 5A 68 65 6E 72 79 66
74 62 26 0A 0A 61 63 74 69 76 65 53 6B 69 6E 12
18 12 16 62 58 55 7A 56 71 46 32 50 30 32 34 69
37 5F 45 61 64 41 32 55 67 62 0E 0A 05 6C 65 76
65 6C 12 05 08 01 18 8E 01 62 18 0A 0B 6C 61 73
74 53 65 73 73 69 6F 6E 12 09 08 08 50 C8 AB AF
CC BF 2B 5A A8 01 08 08 12 A3 01 08 0A 62 1B 0A
06 70 6C 61 79 65 72 12 11 12 0F 61 72 6D 6F 72
66 72 65 64 6F 70 61 73 74 61 62 0C 0A 04 72 61
6E 6B 12 04 08 01 18 03 62 0C 0A 06 74 72 6F 6F
6E 73 12 02 08 01 62 14 0A 04 6E 61 6D 65 12 0C
12 0A 66 72 65 64 6F 70 61 73 74 61 62 26 0A 0A
61 63 74 69 76 65 53 6B 69 6E 12 18 12 16 4C 5F
41 54 41 36 65 50 46 6B 61 53 65 33 70 53 68 56
4D 54 65 51 62 0E 0A 05 6C 65 76 65 6C 12 05 08
01 18 81 01 62 18 0A 0B 6C 61 73 74 53 65 73 73
69 6F 6E 12 09 08 08 50 91 91 81 F8 E5 2B 5A B2
01 08 09 12 AD 01 08 0A 62 1F 0A 06 70 6C 61 79
65 72 12 15 12 13 73 69 6D 70 6C 65 31 34 32 30
35 32 36 30 33 37 33 37 30 62 0C 0A 04 72 61 6E
6B 12 04 08 01 18 02 62 15 0A 06 74 72 6F 6F 6E
73 12 0B 08 06 41 00 00 00 00 00 30 8F 40 62 11
0A 04 6E 61 6D 65 12 09 12 07 4B 65 6E 4D 65 74
7A 62 26 0A 0A 61 63 74 69 76 65 53 6B 69 6E 12
18 12 16 4C 5F 41 54 41 36 65 50 46 6B 61 53 65
33 70 53 68 56 4D 54 65 51 62 0E 0A 05 6C 65 76
65 6C 12 05 08 01 18 81 01 62 18 0A 0B 6C 61 73
74 53 65 73 73 69 6F 6E 12 09 08 08 50 DA 84 A5
E2 99 2C 5A AB 01 08 0A 12 A6 01 08 0A 62 1F 0A
06 70 6C 61 79 65 72 12 15 12 13 73 69 6D 70 6C
65 31 34 32 33 37 30 38 38 31 38 32 37 38 62 0C
0A 04 72 61 6E 6B 12 04 08 01 18 04 62 0C 0A 06
74 72 6F 6F 6E 73 12 02 08 01 62 14 0A 04 6E 61
6D 65 12 0C 12 0A 54 68 65 4F 72 62 69 74 65 64
62 26 0A 0A 61 63 74 69 76 65 53 6B 69 6E 12 18
12 16 68 2D 62 62 30 46 78 42 42 55 6D 76 71 41
56 32 6A 30 5A 58 75 67 62 0D 0A 05 6C 65 76 65
6C 12 04 08 01 18 69 62 18 0A 0B 6C 61 73 74 53
65 73 73 69 6F 6E 12 09 08 08 50 E2 EE DC F5 8F
2C 5A AE 01 08 0B 12 A9 01 08 0A 62 1F 0A 06 70
6C 61 79 65 72 12 15 12 13 73 69 6D 70 6C 65 31
34 31 34 37 30 33 31 36 30 32 35 35 62 0C 0A 04
72 61 6E 6B 12 04 08 01 18 01 62 15 0A 06 74 72
6F 6F 6E 73 12 0B 08 06 41 7F 8C B9 4B 45 74 1B
41 62 0D 0A 04 6E 61 6D 65 12 05 12 03 4F 6E 69
62 26 0A 0A 61 63 74 69 76 65 53 6B 69 6E 12 18
12 16 36 57 4A 38 35 67 48 79 43 30 4B 67 6D 4A
63 53 53 5A 58 66 43 41 62 0E 0A 05 6C 65 76 65
6C 12 05 08 01 18 96 01 62 18 0A 0B 6C 61 73 74
53 65 73 73 69 6F 6E 12 09 08 08 50 AD B4 9D DD
99 2C 5A B0 01 08 0C 12 AB 01 08 0A 62 1F 0A 06
70 6C 61 79 65 72 12 15 12 13 73 69 6D 70 6C 65
31 34 34 31 31 35 30 31 31 34 39 35 30 62 0C 0A
04 72 61 6E 6B 12 04 08 01 18 03 62 0C 0A 06 74
72 6F 6F 6E 73 12 02 08 01 62 19 0A 04 6E 61 6D
65 12 11 12 0F 54 68 65 4E 69 6E 6A 61 50 65 6E
67 75 69 6E 62 26 0A 0A 61 63 74 69 76 65 53 6B
69 6E 12 18 12 16 4C 5F 41 54 41 36 65 50 46 6B
61 53 65 33 70 53 68 56 4D 54 65 51 62 0D 0A 05
6C 65 76 65 6C 12 04 08 01 18 65 62 18 0A 0B 6C
61 73 74 53 65 73 73 69 6F 6E 12 09 08 08 50 80
E3 FC E9 F0 2B 5A A7 01 08 0D 12 A2 01 08 0A 62
1F 0A 06 70 6C 61 79 65 72 12 15 12 13 73 69 6D
70 6C 65 31 34 35 31 37 35 39 30 31 33 34 38 34
62 0C 0A 04 72 61 6E 6B 12 04 08 01 18 03 62 0C
0A 06 74 72 6F 6F 6E 73 12 02 08 01 62 10 0A 04
6E 61 6D 65 12 08 12 06 66 66 66 61 61 63 62 26
0A 0A 61 63 74 69 76 65 53 6B 69 6E 12 18 12 16
36 57 4A 38 35 67 48 79 43 30 4B 67 6D 4A 63 53
53 5A 58 66 43 41 62 0D 0A 05 6C 65 76 65 6C 12
04 08 01 18 7F 62 18 0A 0B 6C 61 73 74 53 65 73
73 69 6F 6E 12 09 08 08 50 E0 E8 DF A4 D3 2B 5A
B3 01 08 0E 12 AE 01 08 0A 62 1D 0A 06 70 6C 61
79 65 72 12 13 12 11 66 62 31 30 30 30 30 30 37
37 36 39 31 39 30 30 38 62 0C 0A 04 72 61 6E 6B
12 04 08 01 18 02 62 15 0A 06 74 72 6F 6F 6E 73
12 0B 08 06 41 61 54 52 27 08 B0 E8 40 62 14 0A
04 6E 61 6D 65 12 0C 12 0A 44 72 6C 2D 4F 6D 6E
69 61 72 62 26 0A 0A 61 63 74 69 76 65 53 6B 69
6E 12 18 12 16 63 78 4C 33 32 6B 5F 6C 31 45 32
77 67 6E 6E 68 4A 38 52 68 4D 51 62 0E 0A 05 6C
65 76 65 6C 12 05 08 01 18 89 01 62 18 0A 0B 6C
61 73 74 53 65 73 73 69 6F 6E 12 09 08 08 50 D1
97 E8 A2 99 2C 5A AA 01 08 0F 12 A5 01 08 0A 62
1F 0A 06 70 6C 61 79 65 72 12 15 12 13 73 69 6D
70 6C 65 31 34 36 30 39 38 39 32 39 30 34 37 37
62 0C 0A 04 72 61 6E 6B 12 04 08 01 18 02 62 0C
0A 06 74 72 6F 6F 6E 73 12 02 08 01 62 13 0A 04
6E 61 6D 65 12 0B 12 09 54 68 65 42 69 6F 6E 69
63 62 26 0A 0A 61 63 74 69 76 65 53 6B 69 6E 12
18 12 16 36 57 4A 38 35 67 48 79 43 30 4B 67 6D
4A 63 53 53 5A 58 66 43 41 62 0D 0A 05 6C 65 76
65 6C 12 04 08 01 18 7B 62 18 0A 0B 6C 61 73 74
53 65 73 73 69 6F 6E 12 09 08 08 50 AF ED E6 D9
91 2C 5A A8 01 08 10 12 A3 01 08 0A 62 1D 0A 06
70 6C 61 79 65 72 12 13 12 11 66 62 31 30 30 30
30 33 32 34 33 30 32 36 35 35 39 62 0C 0A 04 72
61 6E 6B 12 04 08 01 18 03 62 0C 0A 06 74 72 6F
6F 6E 73 12 02 08 01 62 13 0A 04 6E 61 6D 65 12
0B 12 09 54 72 69 6C 6F 62 69 74 65 62 26 0A 0A
61 63 74 69 76 65 53 6B 69 6E 12 18 12 16 4C 5F
41 54 41 36 65 50 46 6B 61 53 65 33 70 53 68 56
4D 54 65 51 62 0D 0A 05 6C 65 76 65 6C 12 04 08
01 18 73 62 18 0A 0B 6C 61 73 74 53 65 73 73 69
6F 6E 12 09 08 08 50 BB DA 9D D6 8A 2C 5A A8 01
08 11 12 A3 01 08 0A 62 1F 0A 06 70 6C 61 79 65
72 12 15 12 13 66 62 31 30 32 30 36 38 39 34 33
39 30 33 30 33 36 38 39 62 0C 0A 04 72 61 6E 6B
12 04 08 01 18 02 62 0C 0A 06 74 72 6F 6F 6E 73
12 02 08 01 62 11 0A 04 6E 61 6D 65 12 09 12 07
4A 65 20 53 75 69 73 62 26 0A 0A 61 63 74 69 76
65 53 6B 69 6E 12 18 12 16 36 57 4A 38 35 67 48
79 43 30 4B 67 6D 4A 63 53 53 5A 58 66 43 41 62
0D 0A 05 6C 65 76 65 6C 12 04 08 01 18 79 62 18
0A 0B 6C 61 73 74 53 65 73 73 69 6F 6E 12 09 08
08 50 E5 D9 9D A9 98 2C 5A A6 01 08 12 12 A1 01
08 0A 62 1F 0A 06 70 6C 61 79 65 72 12 15 12 13
73 69 6D 70 6C 65 31 34 31 32 38 35 37 38 32 30
30 34 31 62 0C 0A 04 72 61 6E 6B 12 04 08 01 18
03 62 0C 0A 06 74 72 6F 6F 6E 73 12 02 08 01 62
0E 0A 04 6E 61 6D 65 12 06 12 04 73 61 64 65 62
26 0A 0A 61 63 74 69 76 65 53 6B 69 6E 12 18 12
16 4C 5F 41 54 41 36 65 50 46 6B 61 53 65 33 70
53 68 56 4D 54 65 51 62 0E 0A 05 6C 65 76 65 6C
12 05 08 01 18 84 01 62 18 0A 0B 6C 61 73 74 53
65 73 73 69 6F 6E 12 09 08 08 50 F2 95 AA B1 E2
2B 5A A1 01 08 13 12 9C 01 08 0A 62 19 0A 06 70
6C 61 79 65 72 12 0F 12 0D 61 72 6D 6F 72 52 6F
64 6F 6B 61 6C 61 62 0C 0A 04 72 61 6E 6B 12 04
08 01 18 02 62 0C 0A 06 74 72 6F 6F 6E 73 12 02
08 01 62 0F 0A 04 6E 61 6D 65 12 07 12 05 48 61
76 69 6B 62 26 0A 0A 61 63 74 69 76 65 53 6B 69
6E 12 18 12 16 4C 5F 41 54 41 36 65 50 46 6B 61
53 65 33 70 53 68 56 4D 54 65 51 62 0E 0A 05 6C
65 76 65 6C 12 05 08 01 18 94 01 62 18 0A 0B 6C
61 73 74 53 65 73 73 69 6F 6E 12 09 08 08 50 EE
C5 98 F3 D2 2B 5A B1 01 08 14 12 AC 01 08 0A 62
1F 0A 06 70 6C 61 79 65 72 12 15 12 13 73 69 6D
70 6C 65 31 34 36 30 36 39 30 37 31 39 37 33 34
62 0C 0A 04 72 61 6E 6B 12 04 08 01 18 02 62 15
0A 06 74 72 6F 6F 6E 73 12 0B 08 06 41 00 00 00
00 C0 AA D2 40 62 10 0A 04 6E 61 6D 65 12 08 12
06 4D 69 67 68 74 79 62 26 0A 0A 61 63 74 69 76
65 53 6B 69 6E 12 18 12 16 36 57 4A 38 35 67 48
79 43 30 4B 67 6D 4A 63 53 53 5A 58 66 43 41 62
0E 0A 05 6C 65 76 65 6C 12 05 08 01 18 81 01 62
18 0A 0B 6C 61 73 74 53 65 73 73 69 6F 6E 12 09
08 08 50 B3 D1 FF C8 99 2C 5A A6 01 08 15 12 A1
01 08 0A 62 1D 0A 06 70 6C 61 79 65 72 12 13 12
11 66 62 31 30 30 30 30 30 36 34 37 31 30 37 33
31 39 62 0C 0A 04 72 61 6E 6B 12 04 08 01 18 03
62 0C 0A 06 74 72 6F 6F 6E 73 12 02 08 01 62 10
0A 04 6E 61 6D 65 12 08 12 06 44 69 6E 6F 72 61
62 26 0A 0A 61 63 74 69 76 65 53 6B 69 6E 12 18
12 16 4C 5F 41 54 41 36 65 50 46 6B 61 53 65 33
70 53 68 56 4D 54 65 51 62 0E 0A 05 6C 65 76 65
6C 12 05 08 01 18 80 01 62 18 0A 0B 6C 61 73 74
53 65 73 73 69 6F 6E 12 09 08 08 50 DD 88 C4 AB
F7 2B 5A A4 01 08 16 12 9F 01 08 0A 62 1D 0A 06
70 6C 61 79 65 72 12 13 12 11 66 62 31 30 30 30
30 31 37 37 39 33 35 35 37 35 31 62 0C 0A 04 72
61 6E 6B 12 04 08 01 18 03 62 0C 0A 06 74 72 6F
6F 6E 73 12 02 08 01 62 0F 0A 04 6E 61 6D 65 12
07 12 05 52 6F 6D 61 6E 62 26 0A 0A 61 63 74 69
76 65 53 6B 69 6E 12 18 12 16 36 57 4A 38 35 67
48 79 43 30 4B 67 6D 4A 63 53 53 5A 58 66 43 41
62 0D 0A 05 6C 65 76 65 6C 12 04 08 01 18 67 62
18 0A 0B 6C 61 73 74 53 65 73 73 69 6F 6E 12 09
08 08 50 E1 A2 9D F4 91 2C 5A AA 01 08 17 12 A5
01 08 0A 62 1F 0A 06 70 6C 61 79 65 72 12 15 12
13 73 69 6D 70 6C 65 31 34 37 32 31 36 38 37 31
39 32 37 33 62 0C 0A 04 72 61 6E 6B 12 04 08 01
18 04 62 0C 0A 06 74 72 6F 6F 6E 73 12 02 08 01
62 13 0A 04 6E 61 6D 65 12 0B 12 09 54 72 75 6E
6B 73 37 37 32 62 26 0A 0A 61 63 74 69 76 65 53
6B 69 6E 12 18 12 16 7A 31 58 37 78 36 46 36 45
30 69 53 56 56 66 5F 55 35 69 75 35 51 62 0D 0A
05 6C 65 76 65 6C 12 04 08 01 18 67 62 18 0A 0B
6C 61 73 74 53 65 73 73 69 6F 6E 12 09 08 08 50
CE BB DF E0 96 2C 5A A0 01 08 18 12 9B 01 08 0A
62 16 0A 06 70 6C 61 79 65 72 12 0C 12 0A 6B 6F
6E 67 36 34 30 30 33 34 62 0C 0A 04 72 61 6E 6B
12 04 08 01 18 03 62 0C 0A 06 74 72 6F 6F 6E 73
12 02 08 01 62 12 0A 04 6E 61 6D 65 12 0A 12 08
53 68 61 64 79 4C 6E 65 62 26 0A 0A 61 63 74 69
76 65 53 6B 69 6E 12 18 12 16 4C 5F 41 54 41 36
65 50 46 6B 61 53 65 33 70 53 68 56 4D 54 65 51
62 0D 0A 05 6C 65 76 65 6C 12 04 08 01 18 6F 62
18 0A 0B 6C 61 73 74 53 65 73 73 69 6F 6E 12 09
08 08 50 AD 94 8D C4 F7 2B 5A A9 01 08 19 12 A4
01 08 0A 62 17 0A 06 70 6C 61 79 65 72 12 0D 12
0B 66 62 35 39 34 30 34 37 31 38 36 62 0C 0A 04
72 61 6E 6B 12 04 08 01 18 02 62 15 0A 06 74 72
6F 6F 6E 73 12 0B 08 06 41 85 E8 23 83 C3 D3 0E
41 62 10 0A 04 6E 61 6D 65 12 08 12 06 53 54 41
54 49 43 62 26 0A 0A 61 63 74 69 76 65 53 6B 69
6E 12 18 12 16 4C 5F 41 54 41 36 65 50 46 6B 61
53 65 33 70 53 68 56 4D 54 65 51 62 0E 0A 05 6C
65 76 65 6C 12 05 08 01 18 92 01 62 18 0A 0B 6C
61 73 74 53 65 73 73 69 6F 6E 12 09 08 08 50 84
C1 ED EA 99 2C 5A AC 01 08 1A 12 A7 01 08 0A 62
22 0A 06 70 6C 61 79 65 72 12 18 12 16 73 74 65
61 6D 37 36 35 36 31 31 39 38 32 37 37 32 30 36
35 35 34 62 0C 0A 04 72 61 6E 6B 12 04 08 01 18
04 62 0C 0A 06 74 72 6F 6F 6E 73 12 02 08 01 62
12 0A 04 6E 61 6D 65 12 0A 12 08 41 6E 61 72 6B
69 73 74 62 26 0A 0A 61 63 74 69 76 65 53 6B 69
6E 12 18 12 16 4C 5F 41 54 41 36 65 50 46 6B 61
53 65 33 70 53 68 56 4D 54 65 51 62 0D 0A 05 6C
65 76 65 6C 12 04 08 01 18 75 62 18 0A 0B 6C 61
73 74 53 65 73 73 69 6F 6E 12 09 08 08 50 98 8D
F3 81 89 2C 5A A0 01 08 1B 12 9B 01 08 0A 62 17
0A 06 70 6C 61 79 65 72 12 0D 12 0B 61 72 6D 6F
72 6F 75 62 69 74 6F 62 0C 0A 04 72 61 6E 6B 12
04 08 01 18 02 62 0C 0A 06 74 72 6F 6F 6E 73 12
02 08 01 62 10 0A 04 6E 61 6D 65 12 08 12 06 6F
75 62 69 74 6F 62 26 0A 0A 61 63 74 69 76 65 53
6B 69 6E 12 18 12 16 53 6C 59 67 55 67 68 45 6F
55 47 31 66 71 35 36 64 65 30 62 6A 77 62 0E 0A
05 6C 65 76 65 6C 12 05 08 01 18 81 01 62 18 0A
0B 6C 61 73 74 53 65 73 73 69 6F 6E 12 09 08 08
50 BB C0 C2 D5 F4 2B 5A 9E 01 08 1C 12 99 01 08
0A 62 16 0A 06 70 6C 61 79 65 72 12 0C 12 0A 61
72 6D 6F 72 6B 61 6E 61 72 62 0C 0A 04 72 61 6E
6B 12 04 08 01 18 02 62 0C 0A 06 74 72 6F 6F 6E
73 12 02 08 01 62 0F 0A 04 6E 61 6D 65 12 07 12
05 6B 61 6E 61 72 62 26 0A 0A 61 63 74 69 76 65
53 6B 69 6E 12 18 12 16 4C 5F 41 54 41 36 65 50
46 6B 61 53 65 33 70 53 68 56 4D 54 65 51 62 0E
0A 05 6C 65 76 65 6C 12 05 08 01 18 8B 01 62 18
0A 0B 6C 61 73 74 53 65 73 73 69 6F 6E 12 09 08
08 50 D7 C7 B5 DA F0 2B 5A 9E 01 08 1D 12 99 01
08 0A 62 16 0A 06 70 6C 61 79 65 72 12 0C 12 0A
6B 6F 6E 67 31 39 32 36 32 32 62 0C 0A 04 72 61
6E 6B 12 04 08 01 18 02 62 0C 0A 06 74 72 6F 6F
6E 73 12 02 08 01 62 10 0A 04 6E 61 6D 65 12 08
12 06 78 61 74 68 69 6C 62 26 0A 0A 61 63 74 69
76 65 53 6B 69 6E 12 18 12 16 4C 5F 41 54 41 36
65 50 46 6B 61 53 65 33 70 53 68 56 4D 54 65 51
62 0D 0A 05 6C 65 76 65 6C 12 04 08 01 18 7A 62
18 0A 0B 6C 61 73 74 53 65 73 73 69 6F 6E 12 09
08 08 50 80 92 C1 F9 DD 2B 5A A8 01 08 1E 12 A3
01 08 0A 62 1F 0A 06 70 6C 61 79 65 72 12 15 12
13 73 69 6D 70 6C 65 31 34 35 38 35 36 39 37 30
38 38 37 37 62 0C 0A 04 72 61 6E 6B 12 04 08 01
18 02 62 0C 0A 06 74 72 6F 6F 6E 73 12 02 08 01
62 10 0A 04 6E 61 6D 65 12 08 12 06 48 61 6E 73
65 6C 62 26 0A 0A 61 63 74 69 76 65 53 6B 69 6E
12 18 12 16 4C 5F 41 54 41 36 65 50 46 6B 61 53
65 33 70 53 68 56 4D 54 65 51 62 0E 0A 05 6C 65
76 65 6C 12 05 08 01 18 90 01 62 18 0A 0B 6C 61
73 74 53 65 73 73 69 6F 6E 12 09 08 08 50 CC E8
F4 8A 92 2C 5A A3 01 08 1F 12 9E 01 08 0A 62 19
0A 06 70 6C 61 79 65 72 12 0F 12 0D 61 72 6D 6F
72 61 73 61 71 31 32 33 34 62 0C 0A 04 72 61 6E
6B 12 04 08 01 18 03 62 0C 0A 06 74 72 6F 6F 6E
73 12 02 08 01 62 12 0A 04 6E 61 6D 65 12 0A 12
08 61 73 61 71 31 32 33 34 62 26 0A 0A 61 63 74
69 76 65 53 6B 69 6E 12 18 12 16 37 77 37 36 32
64 48 75 6D 6B 53 79 44 67 4E 4B 72 47 4D 6A 7A
67 62 0D 0A 05 6C 65 76 65 6C 12 04 08 01 18 70
62 18 0A 0B 6C 61 73 74 53 65 73 73 69 6F 6E 12
09 08 08 50 D4 83 F9 C7 99 2C 5A 9C 01 08 20 12
97 01 08 0A 62 17 0A 06 70 6C 61 79 65 72 12 0D
12 0B 61 72 6D 6F 72 61 66 67 31 32 33 62 0C 0A
04 72 61 6E 6B 12 04 08 01 18 03 62 0C 0A 06 74
72 6F 6F 6E 73 12 02 08 01 62 0D 0A 04 6E 61 6D
65 12 05 12 03 41 66 67 62 26 0A 0A 61 63 74 69
76 65 53 6B 69 6E 12 18 12 16 4C 5F 41 54 41 36
65 50 46 6B 61 53 65 33 70 53 68 56 4D 54 65 51
62 0D 0A 05 6C 65 76 65 6C 12 04 08 01 18 76 62
18 0A 0B 6C 61 73 74 53 65 73 73 69 6F 6E 12 09
08 08 50 CE E2 97 A2 F6 2B 5A A9 01 08 21 12 A4
01 08 0A 62 1D 0A 06 70 6C 61 79 65 72 12 13 12
11 66 62 31 30 30 30 30 33 38 32 36 35 34 37 35
36 37 62 0C 0A 04 72 61 6E 6B 12 04 08 01 18 03
62 0C 0A 06 74 72 6F 6F 6E 73 12 02 08 01 62 14
0A 04 6E 61 6D 65 12 0C 12 0A 58 78 4B 69 4C 4C
65 72 50 54 62 26 0A 0A 61 63 74 69 76 65 53 6B
69 6E 12 18 12 16 4C 5F 41 54 41 36 65 50 46 6B
61 53 65 33 70 53 68 56 4D 54 65 51 62 0D 0A 05
6C 65 76 65 6C 12 04 08 01 18 6B 62 18 0A 0B 6C
61 73 74 53 65 73 73 69 6F 6E 12 09 08 08 50 F1
9A F3 A6 91 2C 5A B8 01 08 22 12 B3 01 08 0A 62
1F 0A 06 70 6C 61 79 65 72 12 15 12 13 73 69 6D
70 6C 65 31 34 35 38 35 38 39 38 34 31 35 33 35
62 0C 0A 04 72 61 6E 6B 12 04 08 01 18 02 62 17
0A 04 6E 61 6D 65 12 0F 12 0D 43 68 61 6E 75 72
73 5F 50 72 69 64 65 62 26 0A 0A 61 63 74 69 76
65 53 6B 69 6E 12 18 12 16 36 57 4A 38 35 67 48
79 43 30 4B 67 6D 4A 63 53 53 5A 58 66 43 41 62
0E 0A 05 6C 65 76 65 6C 12 05 08 01 18 96 01 62
15 0A 06 74 72 6F 6F 6E 73 12 0B 08 06 41 D7 59
1A 25 97 B0 10 41 62 18 0A 0B 6C 61 73 74 53 65
73 73 69 6F 6E 12 09 08 08 50 9F DB B6 D5 98 2C
5A A5 01 08 23 12 A0 01 08 0A 62 1F 0A 06 70 6C
61 79 65 72 12 15 12 13 66 62 31 30 32 30 37 35
30 32 33 38 30 35 35 36 30 33 36 62 0C 0A 04 72
61 6E 6B 12 04 08 01 18 03 62 0E 0A 04 6E 61 6D
65 12 06 12 04 52 69 63 6B 62 26 0A 0A 61 63 74
69 76 65 53 6B 69 6E 12 18 12 16 4C 5F 41 54 41
36 65 50 46 6B 61 53 65 33 70 53 68 56 4D 54 65
51 62 0D 0A 05 6C 65 76 65 6C 12 04 08 01 18 75
62 0C 0A 06 74 72 6F 6F 6E 73 12 02 08 01 62 18
0A 0B 6C 61 73 74 53 65 73 73 69 6F 6E 12 09 08
08 50 C8 E1 FD 91 99 2C 5A B1 01 08 24 12 AC 01
08 0A 62 1F 0A 06 70 6C 61 79 65 72 12 15 12 13
73 69 6D 70 6C 65 31 34 35 37 36 36 36 31 30 32
32 31 33 62 0C 0A 04 72 61 6E 6B 12 04 08 01 18
02 62 10 0A 04 6E 61 6D 65 12 08 12 06 48 69 74
6D 61 6E 62 26 0A 0A 61 63 74 69 76 65 53 6B 69
6E 12 18 12 16 4C 5F 41 54 41 36 65 50 46 6B 61
53 65 33 70 53 68 56 4D 54 65 51 62 0E 0A 05 6C
65 76 65 6C 12 05 08 01 18 96 01 62 15 0A 06 74
72 6F 6F 6E 73 12 0B 08 06 41 04 56 0E 2D 02 2E
C8 40 62 18 0A 0B 6C 61 73 74 53 65 73 73 69 6F
6E 12 09 08 08 50 8F BA C0 C0 97 2C 5A B6 01 08
25 12 B1 01 08 0A 62 22 0A 06 70 6C 61 79 65 72
12 18 12 16 73 74 65 61 6D 37 36 35 36 31 31 39
38 31 32 33 34 36 31 37 36 34 62 0C 0A 04 72 61
6E 6B 12 04 08 01 18 04 62 13 0A 04 6E 61 6D 65
12 0B 12 09 4B 72 61 7A 7A 79 4B 31 30 62 26 0A
0A 61 63 74 69 76 65 53 6B 69 6E 12 18 12 16 63
78 4C 33 32 6B 5F 6C 31 45 32 77 67 6E 6E 68 4A
38 52 68 4D 51 62 0D 0A 05 6C 65 76 65 6C 12 04
08 01 18 6C 62 15 0A 06 74 72 6F 6F 6E 73 12 0B
08 06 41 22 91 A3 26 B7 51 BC 40 62 18 0A 0B 6C
61 73 74 53 65 73 73 69 6F 6E 12 09 08 08 50 E7
FC B6 D3 99 2C 5A B3 01 08 26 12 AE 01 08 0A 62
22 0A 06 70 6C 61 79 65 72 12 18 12 16 73 74 65
61 6D 37 36 35 36 31 31 39 38 31 38 36 36 36 36
33 39 39 62 0C 0A 04 72 61 6E 6B 12 04 08 01 18
04 62 19 0A 04 6E 61 6D 65 12 11 12 0F 44 65 70
72 65 73 73 65 64 43 6F 6E 6F 69 64 62 26 0A 0A
61 63 74 69 76 65 53 6B 69 6E 12 18 12 16 51 4D
69 73 46 35 38 49 66 55 2D 34 70 4E 38 42 38 70
33 69 64 67 62 0D 0A 05 6C 65 76 65 6C 12 04 08
01 18 72 62 0C 0A 06 74 72 6F 6F 6E 73 12 02 08
01 62 18 0A 0B 6C 61 73 74 53 65 73 73 69 6F 6E
12 09 08 08 50 A0 AA 81 E4 96 2C 5A AF 01 08 27
12 AA 01 08 0A 62 1D 0A 06 70 6C 61 79 65 72 12
13 12 11 66 62 31 30 30 30 30 30 30 33 34 30 37
33 37 38 30 62 0C 0A 04 72 61 6E 6B 12 04 08 01
18 03 62 10 0A 04 6E 61 6D 65 12 08 12 06 47 61
6C 61 67 61 62 26 0A 0A 61 63 74 69 76 65 53 6B
69 6E 12 18 12 16 62 4D 6C 76 54 69 37 50 57 30
36 6D 70 30 6B 50 2D 4C 46 6F 75 67 62 0E 0A 05
6C 65 76 65 6C 12 05 08 01 18 81 01 62 15 0A 06
74 72 6F 6F 6E 73 12 0B 08 06 41 F8 56 11 5B 6A
97 EA 40 62 18 0A 0B 6C 61 73 74 53 65 73 73 69
6F 6E 12 09 08 08 50 84 A5 9A BF 99 2C 5A A7 01
08 28 12 A2 01 08 0A 62 17 0A 06 70 6C 61 79 65
72 12 0D 12 0B 6B 6F 6E 67 34 38 35 32 31 34 32
62 0C 0A 04 72 61 6E 6B 12 04 08 01 18 04 62 0F
0A 04 6E 61 6D 65 12 07 12 05 44 61 73 4E 44 62
26 0A 0A 61 63 74 69 76 65 53 6B 69 6E 12 18 12
16 6B 42 47 6F 35 78 4A 6B 5A 55 69 76 48 31 68
34 4D 43 78 49 6B 41 62 0D 0A 05 6C 65 76 65 6C
12 04 08 01 18 42 62 15 0A 06 74 72 6F 6F 6E 73
12 0B 08 06 41 00 00 00 00 00 7E C0 40 62 18 0A
0B 6C 61 73 74 53 65 73 73 69 6F 6E 12 09 08 08
50 A2 99 DF D0 99 2C 5A AF 01 08 29 12 AA 01 08
0A 62 1D 0A 06 70 6C 61 79 65 72 12 13 12 11 66
62 38 39 30 36 31 33 38 33 34 33 32 39 32 30 38
62 0C 0A 04 72 61 6E 6B 12 04 08 01 18 04 62 11
0A 04 6E 61 6D 65 12 09 12 07 47 52 49 46 46 49
4E 62 26 0A 0A 61 63 74 69 76 65 53 6B 69 6E 12
18 12 16 63 78 4C 33 32 6B 5F 6C 31 45 32 77 67
6E 6E 68 4A 38 52 68 4D 51 62 0D 0A 05 6C 65 76
65 6C 12 04 08 01 18 37 62 15 0A 06 74 72 6F 6F
6E 73 12 0B 08 06 41 BA 90 34 A5 46 68 C6 40 62
18 0A 0B 6C 61 73 74 53 65 73 73 69 6F 6E 12 09
08 08 50 86 F6 9E E5 99 2C 5A AE 01 08 2A 12 A9
01 08 0A 62 1F 0A 06 70 6C 61 79 65 72 12 15 12
13 73 69 6D 70 6C 65 31 33 35 30 30 31 33 38 34
31 30 37 37 62 0C 0A 04 72 61 6E 6B 12 04 08 01
18 04 62 0E 0A 04 6E 61 6D 65 12 06 12 04 64 6A
63 64 62 26 0A 0A 61 63 74 69 76 65 53 6B 69 6E
12 18 12 16 4C 57 6C 75 50 69 49 6F 54 55 71 36
74 32 43 48 36 69 6A 53 4C 67 62 0D 0A 05 6C 65
76 65 6C 12 04 08 01 18 47 62 18 0A 0B 6C 61 73
74 53 65 73 73 69 6F 6E 12 09 08 08 50 BD 94 85
B5 99 2C 62 15 0A 06 74 72 6F 6F 6E 73 12 0B 08
06 41 0E E0 2D 90 E0 7A B6 40 5A B0 01 08 2B 12
AB 01 08 0A 62 1B 0A 06 70 6C 61 79 65 72 12 11
12 0F 61 72 6D 6F 72 61 6C 65 78 5F 72 65 79 65
73 62 0C 0A 04 72 61 6E 6B 12 04 08 01 18 04 62
14 0A 04 6E 61 6D 65 12 0C 12 0A 61 6C 65 78 5F
72 65 79 65 73 62 26 0A 0A 61 63 74 69 76 65 53
6B 69 6E 12 18 12 16 68 69 54 44 6E 49 39 45 78
30 69 4C 65 46 41 6B 74 42 6E 58 30 77 62 0D 0A
05 6C 65 76 65 6C 12 04 08 01 18 57 62 18 0A 0B
6C 61 73 74 53 65 73 73 69 6F 6E 12 09 08 08 50
FC D5 F8 D1 99 2C 62 15 0A 06 74 72 6F 6F 6E 73
12 0B 08 06 41 00 00 00 00 00 1E BF 40 5A C2 01
08 2C 12 BD 01 08 0A 62 2A 0A 06 70 6C 61 79 65
72 12 20 12 1E 73 69 6D 70 6C 65 35 37 32 64 64
64 34 38 65 36 39 34 61 61 31 36 35 65 30 30 35
33 35 63 62 0C 0A 04 72 61 6E 6B 12 04 08 01 18
04 62 17 0A 04 6E 61 6D 65 12 0F 12 0D 6D 65 73
74 65 72 68 75 6E 31 32 33 34 62 26 0A 0A 61 63
74 69 76 65 53 6B 69 6E 12 18 12 16 74 78 32 56
47 68 33 6C 2D 55 32 4B 72 72 62 34 6A 6D 61 73
6A 77 62 0D 0A 05 6C 65 76 65 6C 12 04 08 01 18
23 62 18 0A 0B 6C 61 73 74 53 65 73 73 69 6F 6E
12 09 08 08 50 CD E6 80 CC 99 2C 62 15 0A 06 74
72 6F 6F 6E 73 12 0B 08 06 41 F6 4D 87 3B B4 1B
AA 40 5A AB 01 08 2D 12 A6 01 08 0A 62 17 0A 06
70 6C 61 79 65 72 12 0D 12 0B 6B 6F 6E 67 32 39
31 39 32 38 39 62 0C 0A 04 72 61 6E 6B 12 04 08
01 18 04 62 13 0A 04 6E 61 6D 65 12 0B 12 09 73
63 72 61 74 63 68 36 39 62 26 0A 0A 61 63 74 69
76 65 53 6B 69 6E 12 18 12 16 4C 57 6C 75 50 69
49 6F 54 55 71 36 74 32 43 48 36 69 6A 53 4C 67
62 0D 0A 05 6C 65 76 65 6C 12 04 08 01 18 44 62
18 0A 0B 6C 61 73 74 53 65 73 73 69 6F 6E 12 09
08 08 50 E5 AC 81 DF 99 2C 62 15 0A 06 74 72 6F
6F 6E 73 12 0B 08 06 41 56 96 21 8E D1 35 F7 40
5A A8 01 08 2E 12 A3 01 08 0A 62 17 0A 06 70 6C
61 79 65 72 12 0D 12 0B 61 72 6D 6F 72 4C 75 69
7A 65 6D 62 0C 0A 04 72 61 6E 6B 12 04 08 01 18
04 62 10 0A 04 6E 61 6D 65 12 08 12 06 4C 75 69
7A 65 6D 62 26 0A 0A 61 63 74 69 76 65 53 6B 69
6E 12 18 12 16 4B 37 4A 56 64 6D 54 2D 49 30 71
78 68 65 32 56 4B 57 73 55 67 41 62 0D 0A 05 6C
65 76 65 6C 12 04 08 01 18 5E 62 15 0A 06 74 72
6F 6F 6E 73 12 0B 08 06 41 00 00 00 00 00 00 3A
40 62 18 0A 0B 6C 61 73 74 53 65 73 73 69 6F 6E
12 09 08 08 50 C4 89 8C D9 99 2C 1A 10 0A 05 63
6F 6C 6F 72 12 07 08 02 20 FA 81 E0 02 1A 14 0A
04 6C 6F 67 6F 12 0C 12 0A 63 6C 61 6E 5F 6C 6F
67 6F 32 1A C4 03 0A 0B 64 65 73 63 72 69 70 74
69 6F 6E 12 B4 03 12 B1 03 20 41 46 20 41 72 6D
61 64 61 20 43 6C 61 6E 20 46 6F 72 20 54 68 6F
75 67 68 73 20 45 78 70 65 72 69 65 6E 63 65 20
50 6C 61 79 65 72 73 20 41 6E 64 20 41 63 74 69
76 65 20 50 6C 61 79 65 72 73 2E 52 65 73 70 65
63 74 20 4F 74 68 65 72 73 20 61 6E 64 20 74 68
65 79 20 77 69 6C 6C 20 72 65 73 70 65 63 74 20
79 6F 75 20 28 53 54 45 41 4C 49 4E 47 20 49 53
20 4E 4F 54 20 50 45 52 4D 49 54 54 45 44 20 49
4E 20 54 48 49 53 20 43 4C 41 4E 29 20 20 20 20
20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20
20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20
20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20
20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20
20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20
20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20
20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20
20 20 20 20 20 20 20 20 20 20 20 57 65 20 53 74
69 63 6B 20 54 6F 67 65 74 68 65 72 20 41 73 20
41 20 43 6C 61 6E 20 41 6E 64 20 42 65 20 41 73
20 42 65 73 74 20 57 65 20 43 61 6E 2E 20 20 68
74 74 70 3A 2F 2F 61 66 61 72 6D 61 64 61 2E 65
6E 6A 69 6E 2E 63 6F 6D 2F 20 20 20 20 20 20 20
20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20
20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20
20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20
20 20 2D 2D 2D 20 48 61 70 70 79 20 46 6C 79 69
6E 67 20 3A 33 20 20 20 20 20 1A 13 0A 04 6E 61
6D 65 12 0B 12 09 41 46 20 41 52 4D 41 44 41 1A
D8 06 0A 0C 74 72 6F 6F 6E 48 69 73 74 6F 72 79
12 C7 06 08 0A 62 15 0A 06 32 30 31 35 2D 33 12
0B 08 06 41 FD 3A 70 A2 0E 5D 47 41 62 15 0A 06
32 30 31 35 2D 34 12 0B 08 06 41 07 9F 61 E3 3C
EB 57 41 62 15 0A 06 32 30 31 35 2D 35 12 0B 08
06 41 50 7B A8 D4 D8 73 5D 41 62 15 0A 06 32 30
31 35 2D 36 12 0B 08 06 41 6E CD 85 93 27 41 61
41 62 15 0A 06 32 30 31 35 2D 37 12 0B 08 06 41
39 6D A0 2A F5 3C 64 41 62 15 0A 06 32 30 31 35
2D 38 12 0B 08 06 41 6D BF A2 1A 9F 56 57 41 62
15 0A 06 32 30 31 35 2D 39 12 0B 08 06 41 CB B6
87 0E 30 0A 55 41 62 16 0A 07 32 30 31 35 2D 31
30 12 0B 08 06 41 B2 2E 6E 1B A3 0D 45 41 62 16
0A 07 32 30 31 35 2D 31 31 12 0B 08 06 41 FD FC
D1 2A 69 2D 4F 41 62 16 0A 07 32 30 31 35 2D 31
32 12 0B 08 06 41 AF 7B F2 CC 80 BF 51 41 62 15
0A 06 32 30 31 36 2D 31 12 0B 08 06 41 06 E6 89
38 97 AF 59 41 62 15 0A 06 32 30 31 36 2D 32 12
0B 08 06 41 FF 90 7E 5B 27 E1 54 41 62 15 0A 06
32 30 31 36 2D 33 12 0B 08 06 41 5A C0 A5 3E 4F
C6 54 41 62 15 0A 06 32 30 31 36 2D 34 12 0B 08
06 41 36 4E D1 8F 51 2F 5A 41 62 15 0A 06 32 30
31 36 2D 35 12 0B 08 06 41 7D DD 6E B4 EC FC 55
41 62 15 0A 06 32 30 31 36 2D 36 12 0B 08 06 41
15 9F 61 11 1E FF 53 41 62 15 0A 06 32 30 31 36
2D 37 12 0B 08 06 41 53 93 F3 C0 F2 11 57 41 62
15 0A 06 32 30 31 36 2D 38 12 0B 08 06 41 11 84
C3 18 03 05 4A 41 62 15 0A 06 32 30 31 36 2D 39
12 0B 08 06 41 23 7B 5E 44 8E 9E 43 41 62 16 0A
07 32 30 31 36 2D 31 30 12 0B 08 06 41 87 70 18
17 35 D6 54 41 62 16 0A 07 32 30 31 36 2D 31 31
12 0B 08 06 41 35 70 CE F1 C6 98 61 41 62 16 0A
07 32 30 31 36 2D 31 32 12 0B 08 06 41 97 F6 06
54 DF 93 63 41 62 15 0A 06 32 30 31 37 2D 31 12
0B 08 06 41 4E 71 F6 69 69 26 59 41 62 15 0A 06
32 30 31 37 2D 32 12 0B 08 06 41 BD 74 93 18 99
B5 52 41 62 15 0A 06 32 30 31 37 2D 33 12 0B 08
06 41 78 AC 8B 37 1E 0A 48 41 62 15 0A 06 32 30
31 37 2D 34 12 0B 08 06 41 B8 CE D2 58 77 3E 4A
41 62 15 0A 06 32 30 31 37 2D 35 12 0B 08 06 41
F9 7B 3C 55 44 8D 50 41 62 15 0A 06 32 30 31 37
2D 36 12 0B 08 06 41 F1 95 B2 6C 8C 8C 4B 41 62
15 0A 06 32 30 31 37 2D 37 12 0B 08 06 41 6C C5
FE D2 3F 73 3D 41 62 15 0A 06 32 30 31 37 2D 38
12 0B 08 06 41 1F FA C8 CC 73 3B 45 41 62 15 0A
06 32 30 31 37 2D 39 12 0B 08 06 41 D7 BC E3 A8
1B B2 4A 41 62 16 0A 07 32 30 31 37 2D 31 30 12
0B 08 06 41 8E 50 46 A3 EF 65 50 41 62 16 0A 07
32 30 31 37 2D 31 31 12 0B 08 06 41 C5 BA B8 DD
78 6D 4C 41 62 16 0A 07 32 30 31 37 2D 31 32 12
0B 08 06 41 96 FF 90 E0 BF 03 56 41 62 15 0A 06
32 30 31 38 2D 31 12 0B 08 06 41 C7 E9 70 DD 51
7E 53 41 62 15 0A 06 32 30 31 38 2D 32 12 0B 08
06 41 21 9A 2D F6 08 F3 37 41 1A 15 0A 06 74 72
6F 6F 6E 73 12 0B 08 06 41 21 9A 2D F6 08 F3 37
41 1A 12 0A 05 72 61 6E 6B 31 12 09 12 07 4D 6F
6E 61 72 63 68 1A 1B 0A 05 72 61 6E 6B 32 12 12
12 10 45 6C 69 74 65 20 46 6F 72 65 72 75 6E 6E
65 72 1A 19 0A 05 72 61 6E 6B 33 12 10 12 0E 57
65 61 70 6F 6E 20 4D 65 69 73 74 65 72 1A 1A 0A
05 72 61 6E 6B 34 12 11 12 0F 48 69 72 65 64 2D
4D 65 72 63 65 6E 61 72 79 1A 12 0A 0C 61 70 70
6C 69 63 61 74 69 6F 6E 73 12 02 08 09 1A 0C 0A
04 72 61 6E 6B 12 04 08 01 18 09 1A 21 0A 1B 6C
69 76 65 73 69 63 33 77 2D 42 78 64 4D 55 36 71
57 68 58 39 74 33 5F 45 61 41 12 02 08 0A 1A 29
0A 1A 64 61 74 65 69 63 33 77 2D 42 78 64 4D 55
36 71 57 68 58 39 74 33 5F 45 61 41 12 0B 08 06
41 00 86 CB 63 4F F2 CC 42 1A 22 0A 1A 72 61 6E
6B 69 63 33 77 2D 42 78 64 4D 55 36 71 57 68 58
39 74 33 5F 45 61 41 12 04 08 01 18 01 1A 21 0A
1B 73 63 6F 72 65 69 63 33 77 2D 42 78 64 4D 55
36 71 57 68 58 39 74 33 5F 45 61 41 12 02 08 06
1A 2E 0A 1F 68 69 67 68 73 63 6F 72 65 69 63 33
77 2D 42 78 64 4D 55 36 71 57 68 58 39 74 33 5F
45 61 41 12 0B 08 06 41 0A 00 00 00 E0 22 01 41
1A 22 0A 1A 72 61 6E 6B 54 46 57 47 33 6A 54 53
61 45 53 64 49 7A 66 59 76 61 34 71 43 51 12 04
08 01 18 01 1A 21 0A 1B 6C 69 76 65 73 54 46 57
47 33 6A 54 53 61 45 53 64 49 7A 66 59 76 61 34
71 43 51 12 02 08 0A 1A 21 0A 1B 73 63 6F 72 65
54 46 57 47 33 6A 54 53 61 45 53 64 49 7A 66 59
76 61 34 71 43 51 12 02 08 06 1A 22 0A 1A 72 61
6E 6B 36 54 5A 38 34 6D 32 46 6F 55 79 4A 71 6A
5A 4B 38 67 72 4B 61 77 12 04 08 01 18 01 1A 21
0A 1B 6C 69 76 65 73 36 54 5A 38 34 6D 32 46 6F
55 79 4A 71 6A 5A 4B 38 67 72 4B 61 77 12 02 08
0A 1A 21 0A 1B 73 63 6F 72 65 36 54 5A 38 34 6D
32 46 6F 55 79 4A 71 6A 5A 4B 38 67 72 4B 61 77
12 02 08 06 1A 22 0A 1A 72 61 6E 6B 6C 66 39 39
74 34 32 62 68 45 47 73 42 6D 59 30 65 74 6C 74
56 77 12 04 08 01 18 01 1A 21 0A 1B 6C 69 76 65
73 6C 66 39 39 74 34 32 62 68 45 47 73 42 6D 59
30 65 74 6C 74 56 77 12 02 08 0A 1A 21 0A 1B 73
63 6F 72 65 6C 66 39 39 74 34 32 62 68 45 47 73
42 6D 59 30 65 74 6C 74 56 77 12 02 08 06 1A 22
0A 1A 72 61 6E 6B 68 49 31 48 6C 75 31 4F 49 55
57 48 49 78 50 6A 6F 72 31 5F 5A 77 12 04 08 01
18 01 1A 21 0A 1B 6C 69 76 65 73 68 49 31 48 6C
75 31 4F 49 55 57 48 49 78 50 6A 6F 72 31 5F 5A
77 12 02 08 0A 1A 21 0A 1B 73 63 6F 72 65 68 49
31 48 6C 75 31 4F 49 55 57 48 49 78 50 6A 6F 72
31 5F 5A 77 12 02 08 06 20 A7 9D C9 8B 01
";
    // chimical neutralizer
    var data = raw.Split(new[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(x => byte.Parse(x, NumberStyles.HexNumber))
        .ToArray();
    using (var stream = new MemoryStream(data))
    using (var reader = new BinaryReader(stream))
    {
        var token = default(string);
        if (reader.ReadByte() != 0)
        {
            var length = reader.ReadUInt16();
            var buffer = reader.ReadBytes(length);
            token = Encoding.UTF8.GetString(buffer);
        }

        var error = reader.Read() == 0;
        if (error)
        {
            Serializer.Deserialize<LoadError>(stream).Dump();
            throw new InvalidDataException();
        }
        
        var results = Serializer.Deserialize<LoadArrayMessage<BigDbObject>>(stream).Dump();
        results.Objects
            .Select(x => Artifact.FromBigDbObject(x))
            .Dump();
    }
}

// Define other methods and classes here
public class Artifact
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string Bitmap { get; set; }
    public int Level { get; set; }
    public int LevelPotential { get; set; }
    public bool Revealed { get; set; }
    public bool Upgraded { get; set; }
    public Dictionary<string, double> Stats { get; set; }

    public static Artifact FromBigDbObject(BigDbObject data)
    {
        return new Artifact
        {
            ID = data.Key,
            Name = data.Properties["name"].Text,
            Bitmap = data.Properties["bitmap"].Text,
            Level = data.Properties["level"].Integer,
            LevelPotential = data.Properties["levelPotential"].Integer,
            Stats = data.Properties["stats"].Array.Values
                .Select(x => x.Object.Value)
                .ToDictionary(x => x.Text, x => x.Double)
        };
    }
}

#region ProtoBuf
[ProtoContract]
public class LoadArrayMessage<TMessage>
{
    [ProtoMember(1)]
    public TMessage[] Objects { get; set; }
}

[ProtoContract]
public class LoadError
{
    [ProtoMember(1)] public int ErrorCode { get; set; }
    [ProtoMember(2)] public string Message { get; set; }

}

[ProtoContract]
public class BigDbObject
{
    [ProtoMember(1)]
    public string Key { get; set; }
    [ProtoMember(2)]
    public string Version { get; set; }
    [ProtoMember(3)]
    public Dictionary<string, ValueObject> Properties { get; set; }
    [ProtoMember(4)]
    public int CreatorID { get; set; }
}

[ProtoContract]
public class ObjectProperty
{
    [ProtoMember(1)] public string Name { get; set; }
    [ProtoMember(2)] public ValueObject Value { get; set; }
}

[ProtoContract]
public class ValueObject
{
    [ProtoMember(1)] public int Type { get; set; }
    [ProtoMember(2)] public string Text { get; set; }
    [ProtoMember(3)] public int Integer { get; set; }
    [ProtoMember(4)] public uint UnsignedInteger { get; set; }
    [ProtoMember(5)] public long Long { get; set; }
    [ProtoMember(6)] public bool Bool { get; set; }
    [ProtoMember(7)] public float Float { get; set; }
    [ProtoMember(8)] public double Double { get; set; }
    [ProtoMember(9)] public byte[] Bytes { get; set; }
    [ProtoMember(10)] public DateTime DateTime { get; set; }
    [ProtoMember(11)] public Dictionary<int, ValueObject> Array { get; set; }
    [ProtoMember(12)] public ObjectProperty Object { get; set; }

    private static Dictionary<int, Func<ValueObject, object>> ValueGetterMapping = typeof(ValueObject)
        .GetProperties()
        .Where(p => p.SetMethod != null)
        .ToDictionary<PropertyInfo, int, Func<ValueObject, object>>(
            p => p.GetCustomAttribute<ProtoMemberAttribute>().Tag,
            p => v => p.GetValue(v));
    public dynamic Value => ValueGetterMapping[Type + 2](this);
}

[ProtoContract]
public class ArrayProperty
{
    [ProtoMember(1)] public int Index { get; set; }
    [ProtoMember(2)] public ValueObject Value { get; set; }
}
#endregion