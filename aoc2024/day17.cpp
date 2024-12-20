#include <iostream>
#include <cmath>

struct State
{
    uint64_t a = 35200350;
    uint64_t b = 0;
    uint64_t c = 0;
};

int prog[] = { 2,4,1,2,7,5,4,7,1,3,5,5,0,3,3,0 };
int progLen = sizeof(prog) / sizeof(*prog);

int bestIdx = 0;
int increment = 1;

uint64_t DoFast(uint64_t initialA)
{
    State state;
    state.a = initialA;

    uint64_t fpow[] = {1, 2, 4, 8, 16, 32, 64, 128};

    int idx = 0;
    uint64_t ret = 0;

    while (state.a > 0)
    {
        // 2 4
        state.b = state.a % 8;

        // 1 2
        state.b = state.b ^ 2;

        // 7 5
        state.c = uint64_t(state.a / fpow[state.b]);

        // 4 7
        state.b = state.b ^ state.c;

        // 1 3
        state.b = state.b ^ 3;

        // 5 5
        ret = (ret * 10) + (state.b % 8);

        if (idx >= progLen || ((state.b % 8) != prog[idx]))
            return ret;

        ++idx;

        if (idx >= bestIdx)
        {
            bestIdx = idx;

            if ((bestIdx - 4) > 0)
            {
                increment = 1;
                int dec = bestIdx - 4;
                while (dec-- > 0)
                    increment *= 8;
            }
        }

        // 0 3
        state.a = uint64_t(state.a / 8);
    }

    return ret;
}

int main()
{
    uint64_t goal = 2412754713550330;

    uint64_t a = 35184372088832;
    while (true)
    {
        uint64_t fast = DoFast(a);

        if (fast == goal)
        {
            break;
        }

        a += increment;
    }

    std::cout << a << std::endl;
}
