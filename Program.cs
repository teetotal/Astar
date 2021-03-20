using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Random rand = new Random();
        int a1 = 8;
        int a2 = 8;

        int[,] map = new int[a1, a2];

        for(int y = 0; y < map.GetLength(0); y++)
        {
            for(int x = 0; x < map.GetLength(1); x++)
            {
                map[y,x] = rand.Next(5);
            }
        }
        
        int[,] mapR = new int[a1, a2];
        
        Astar a = new Astar(map);

        Stack<Astar.State> stack = a.Search(new Astar.Pos(a1-1, a2-1), new Astar.Pos(0, 0));
        int i = 1;

        Astar.State pre = stack.Peek();
        while(stack.Count > 0)
        {
            Astar.State state = stack.Peek();
            Astar.Pos pos = state.id;

            if(pre.parent.x != -1 && !pre.id.Compare(state.parent))
            {
                Console.WriteLine(String.Format("From: {0}, {1} To: {2},{3} !", pre.id.x, pre.id.y, state.parent.x, state.parent.y));
            }
            Console.WriteLine(String.Format("From: {0}, {1} To: {2},{3}", state.parent.x, state.parent.y, state.id.x, state.id.y));
            
            pre = state;
            
            mapR[pos.x,pos.y] = i++;
            stack.Pop();
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

