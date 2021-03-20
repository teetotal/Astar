using System;
using System.Collections.Generic;

public class Astar
{
    public struct Pos
    {
        public int x, y;

        public Pos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Compare(Pos p)
        {
            if(p.x == x && p.y == y)
            {
                return true;
            }

            return false;
        }
    }

    public struct State
    {
        public float F, G, H;
        public Pos id, parent;

        public State(Pos id)
        {
            this.id = id;
            F = 0;
            G = 0;
            H = 0;
            parent = new Pos(-1, -1);
        }

        public State(Pos id, Pos parent)
        {
            this.id = id;
            F = 0;
            G = 0;
            H = 0;
            this.parent = parent;
        }

        public State(float f, float g, float h, Pos id, Pos parent)
        {
            this.F = f;
            this.G = g;
            this.H = h;
            this.id = id;
            this.parent = parent;
        }

        public void SetF()
        {
            this.F = this.G + this.H;
        }
    }

    int[,] map; 
    Pos[] neighborPos = new Pos[4] {
        new Pos(0, 1),
        new Pos(0, -1),
        new Pos(1, 0),
        new Pos(-1, 0)
    };

    public Astar(int[,] map)
    {
        this.map = map;
    }

    public Stack<State> Search(Pos start, Pos end)
    {
        Stack<State> stack = new Stack<State>();
        List<State> O = new List<State>();
        List<State> C = new List<State>();

        State curr = new State(start);
        C.Add(curr);
        while(true)
        {
            List<Pos> neighbor = GetNeighbors(C, curr.id);
            for(int n = 0; n < neighbor.Count; n++)
            {
                UpsertO(ref O, GetState(neighbor[n], curr, end));
            }

            if(O.Count == 0)
                return stack;

            int minIdx = GetMinIndexFromO(O, end);
            curr = O[minIdx];
            
            C.Add(O[minIdx]);
            O.RemoveAt(minIdx);

            if(curr.id.x == end.x && curr.id.y == end.y)
            {
                break;
            }
        }
        //todo. 순서대로 해서 꼬인거 삭제해야함
        for(int n = C.Count -1; n >= 0; n--)
        {
            State s = C[n];
            stack.Push(C[n]);
            if(s.id.Compare(start))
            {
                break;
            }
        }
        
        return stack;
    }
    private List<Pos> GetNeighbors(List<State> C, Pos p)
    {
        List<Pos> list = new List<Pos>();

        for(int n=0; n < neighborPos.Length; n++)
        {
            Pos pos = new Pos(p.x + neighborPos[n].x, p.y + neighborPos[n].y);
            if(pos.x >= map.GetLength(0) || pos.x < 0 || pos.y >= map.GetLength(1) || pos.y < 0)
            {
                continue;
            }
            if(Check(C, pos))
            {
                list.Add(pos);
            }
        }

        return list;
    }
    // x + y
    private float CalDistance(Pos to, Pos from)
    {
        float sum = 0;

        Pos min = new Pos(Math.Min(to.x, from.x), Math.Min(to.y, from.y));
        Pos max = new Pos(Math.Max(to.x, from.x), Math.Max(to.y, from.y));

        for(int x = min.x; x <= max.x; x++)
        {
            for(int y = min.y; y <= max.y; y++)
            {
                sum += map[x, y];
            }
        }

        return sum;
    }

    private void UpsertO(ref List<State> O, State s)
    {
        for(int n = 0; n < O.Count; n++)
        {
            if(s.id.x == O[n].id.x && s.id.y == O[n].id.y)
            {
                O[n] = s;
                return;
            }
        }

        O.Add(s);
    }

    private int GetMinIndexFromO(List<State> O, Pos end)
    {
        int idx = 0;
        float v = O[0].F;
        for(int n = 0; n < O.Count; n++)
        {
            if(end.Compare(O[n].id))
            {
                return n;
            }

            if(v > O[n].F)
            {
                idx = n;
                v = O[n].F;
            }
        }

        return idx;
    }

    

    private State GetState(Pos p, State parent, Pos end)
    {
        State s = new State(p, parent.id);
        s.G = parent.G + map[p.x, p.y];
        s.H = CalDistance(end, p);
        s.SetF();
        return s;
    }

    
    private bool Check(List<State> C, Pos p)
    {
        for(int n = 0; n < C.Count; n++)
        {
            if(p.Compare(C[n].id) == true)
            {
                return false;
            }
        }
        return true;
    }
}