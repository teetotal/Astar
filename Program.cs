using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Random rand = new Random();
        const int a1 = 5;
        const int a2 = 5;

        int[,] map = new int[a1, a2]
        {
            {0,     -1,      0,      0,      0},
            {0,     -1,      0,      -1,      0},
            {0,     -1,      0,      -1,      0},
            {0,     -1,      0,      -1,      0},
            {0,     0,      0,      -1,      0}
        };

        /*
        for(int y = 0; y < map.GetLength(0); y++)
        {
            for(int x = 0; x < map.GetLength(1); x++)
            {
                map[y,x] = rand.Next(5);
            }
        }
        */
        
        int[,] mapR = new int[a1, a2];
        
        Astar a = new Astar(map);

        Queue<Astar.State> q = a.Search(new Astar.Pos(0, 0), new Astar.Pos(a1-1, a2-1));
        int i = 1;

        Astar.State pre = q.Peek();
        while(q.Count > 0)
        {
            Astar.State state = q.Peek();
            Astar.Pos pos = state.id;
            
            //for debug
            if(pre.parent.x != -1 && !pre.id.Compare(state.parent))
                Console.WriteLine(String.Format("Parent: {0}, {1} ID: {2},{3} !", pre.id.x, pre.id.y, state.parent.x, state.parent.y));
            
            Console.WriteLine(String.Format("Parent: {0}, {1} ID: {2},{3}", state.parent.x, state.parent.y, state.id.x, state.id.y));
            
            pre = state;
            
            mapR[pos.x,pos.y] = i++;
            q.Dequeue();
        }
        Print(mapR);
        Print(map);
        System.Console.WriteLine("--------");
    }

    static void Print(int[,] map)
    {
        for(int y = 0; y < map.GetLength(0); y++)
        {
            for(int x = 0; x < map.GetLength(1); x++)
            {
                System.Console.Write(String.Format("{0}\t", map[y,x]));
            }
            Console.WriteLine();
        }

        Console.WriteLine();
    }
}

