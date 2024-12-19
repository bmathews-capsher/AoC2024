using System.Reflection.Emit;

namespace AOC2024
{
    public class Day18A
    {
        const int SIZE = 71;
        const int TIME = 1024;

        enum Dir
        {
            U, R, D, L
        }
        public void Solve(List<string> data)
        {
            List<List<char>> map = new();
            int[,] costs = new int[SIZE, SIZE];

            for (int r = 0; r < SIZE; r++)
            {
                map.Add(new());
                for (int c = 0; c < SIZE; c++)
                {
                    map[r].Add('.');
                    costs[r, c] = int.MaxValue;
                }
            }

            bool[,] visited = new bool[SIZE, SIZE];

            for (int i = 0; i < TIME; i++)
            {
                string[] vals = data[i].Split(',');
                (int r, int c) pos = (int.Parse(vals[1]), int.Parse(vals[0]));
                map[pos.r][pos.c] = '#';
            }

            HashSet<(int r, int c)> toVisit = new();
            toVisit.Add((0, 0));
            costs[0, 0] = 0;
            (int r, int c) end = (SIZE - 1, SIZE - 1);

            while(toVisit.Count != 0)
            {
                var curr = FindNext(toVisit, costs, end);
                toVisit.Remove(curr);

                if(curr.r == SIZE - 1 && curr.c == SIZE - 1) break;

                visited[curr.r, curr.c] = true;

                int currCost = costs[curr.r, curr.c];

                foreach(Dir dir in Enum.GetValues(typeof(Dir)))
                {
                    var next = Move(curr, dir, 1);
                    if (CanMoveTo(next, map) && !visited[next.r, next.c])
                    {
                        toVisit.Add(next);
                        costs[next.r, next.c] = Math.Min(currCost + 1, costs[next.r, next.c]);
                    }
                }
            }

            Console.WriteLine(costs[SIZE-1, SIZE-1]);
        }

        private (int r, int c) FindNext(HashSet<(int r, int c)> toVisit, int[,] costs, (int r, int c) end)
        {
            int minCost = int.MaxValue;
            (int r, int c) minPos = (-1, -1);
            foreach(var pos in toVisit)
            {
                int currCost = costs[pos.r, pos.c] + GetDistance(pos, end);
                if (currCost < minCost)
                {
                    minCost = currCost;
                    minPos = pos;
                }
            }

            return minPos;
        }

        private int GetDistance((int r, int c) pos, (int r, int c) end)
        {
            return Math.Abs(pos.r - end.r) + Math.Abs(pos.c - end.c);
        }

        private bool CanMoveTo((int r, int c) pos, List<List<char>> map)
        {
            return pos.r >= 0 && pos.c >= 0 &&
                pos.r < map.Count && pos.c < map[0].Count &&
                map[pos.r][pos.c] == '.';
        }

        private (int r, int c) Move((int r, int c) pos, Dir direction, int distance)
        {
            switch (direction)
            {
                case Dir.U:
                    return (pos.r - distance, pos.c);
                case Dir.R:
                    return (pos.r, pos.c + distance);
                case Dir.D:
                    return (pos.r + distance, pos.c);
                case Dir.L:
                    return (pos.r, pos.c - distance);
            }

            return (0, 0);
        }

        private void PrintMap(List<List<char>> map)
        {
            for (int r = 0; r < map.Count; r++)
            {
                for (int c = 0; c < map.Count; c++)
                {
                    Console.Write(map[r][c]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}