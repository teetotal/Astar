using System;
using System.Collections.Generic;
using System.Linq;

public class AstarHelper {
    private Astar mAstar;
    public int mHeight { get; set; }
    public int mWidth { get; set; }
    public AstarHelper(int[,] map, float defaultCost) {
        mAstar = new Astar(map, defaultCost);
        mHeight = map.GetLength(0);
        mWidth = map.GetLength(1);
    }
    public Queue<int[]> Search(int startX, int startY, int endX, int endY) {
        return Search(new int[]{startX, startY}, new int[]{endX, endY});
    }
    public Queue<int[]> Search(int[] start, int[] end) {
        if(start[0] < 0 || end[0] < 0 || start[1] >= mHeight || end[1] >= mHeight) {
            return new Queue<int[]>();
        }
        Astar.Pos posStart = ConvertToInnerPos(start);
        Astar.Pos posEnd = ConvertToInnerPos(end);

        Stack<Astar.Pos> route = mAstar.Search(posStart, posEnd);
        Queue<int[]> ret = new Queue<int[]>();
        while(route.Count() > 0) {
            ret.Enqueue(ConvertFromInnerPos(route.Pop()));
        }
        return ret;
    }
    private Astar.Pos ConvertToInnerPos(int[] pos) {
        /*
        x = 6, y = 5
        [1,0] -> [5, 1]
        */ 
        return new Astar.Pos(mHeight - 1 - pos[1], pos[0]);
    }
    private int[] ConvertFromInnerPos(Astar.Pos pos) {
        int[] ret = new int[2];
        ret[0] = pos.y;
        ret[1] = mHeight - 1 - pos.x;

        return ret;
    }
}