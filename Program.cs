using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        int[,] map =
        {
            {0,     -1,      0,       0,      0, 0, 0},
            {0,     -1,      0,      -1,      0, 0, 0},
            {0,     -1,      0,      -1,      0, 0, 0},
            {0,     -1,      0,      -1,      0, 0, 0},
            {0,      0,      0,      -1,      0, 0, 0}
        };

        AstarHelper astar = new AstarHelper(map, 0.1f);
        var route = astar.Search(1, 1, 6, 4);
        if(route != null) {
            while(route.Count() > 0) {
                int[] pos = route.Dequeue();
                Console.WriteLine("{0}, {1}", pos[0], pos[1]);
            }
        }
    }
}

