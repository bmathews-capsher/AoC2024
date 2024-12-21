using System.Reflection.Emit;

namespace AOC2024
{
    public class Day20B
    {
        enum Dir
        {
            U, R, D, L
        }
        public void Solve(List<string> data)
        {
            List<List<char>> map = new();
            (int r, int c) start = (0, 0);
            (int r, int c) end = (0, 0);

            for (int r = 0; r < data.Count; r++)
            {
                map.Add(new());
                for (int c = 0; c < data[r].Length; c++)
                {
                    char cell = data[r][c];
                    map[r].Add(cell);

                    if (cell == 'S') start = (r, c);
                    if (cell == 'E') end = (r, c);
                }
            }

            int[,] costs = GetCosts(map, start, end);

            int count = 0;
            int threshold = 100;
            int cheatDist = 20;
            for(int r = 1; r < map.Count - 1; r++)
            {
                for(int c = 1; c < map[r].Count - 1; c++)
                {
                    if(map[r][c] == '#') continue;
                    (int r, int c) pos = (r, c);

                    for(int cR = r - cheatDist; cR <= r + cheatDist; cR++)
                    {
                        int hDist = cheatDist - Math.Abs(cR - r);
                        for(int cC = c - hDist; cC <= c + hDist; cC++)
                        {
                            int saved = CalculateSaved(pos, (cR, cC), costs, map);

                            if(saved >= threshold)
                            {
                                count++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine(count);
        }

        private int CalculateSaved((int r, int c) pos, (int r, int c) cheatPos, int[,] costs, List<List<char>> map)
        {
            if (!CanMoveTo(cheatPos, map)) return -1;

            int totalDist = Math.Abs(pos.r - cheatPos.r) + Math.Abs(pos.c - cheatPos.c);
            int saved = (costs[cheatPos.r, cheatPos.c] - costs[pos.r, pos.c]) - totalDist;

            return saved;
        }

        private int[,] GetCosts(List<List<char>> map, (int r, int c) start, (int r, int c) end)
        {

            int[,] costs = new int[map.Count, map[0].Count];
            for (int r = 0; r < map.Count; r++)
            {
                for (int c = 0; c < map[r].Count; c++)
                {
                    costs[r, c] = int.MaxValue;
                }
            }

            bool[,] visited = new bool[map.Count, map[0].Count];


            HashSet<(int r, int c)> toVisit = new();
            toVisit.Add(start);
            costs[start.r, start.c] = 0;

            while (toVisit.Count != 0)
            {
                var curr = FindNext(toVisit, costs, end);
                toVisit.Remove(curr);

                if (curr.r == end.r && curr.c == end.c) break;

                visited[curr.r, curr.c] = true;

                int nextCost = costs[curr.r, curr.c] + 1;

                foreach (Dir dir in Enum.GetValues(typeof(Dir)))
                {
                    var next = Move(curr, dir, 1);
                    if (CanMoveTo(next, map) && !visited[next.r, next.c])
                    {
                        toVisit.Add(next);

                        if(nextCost < costs[next.r, next.c])
                        {
                            costs[next.r, next.c] = nextCost;
                        }

                        costs[next.r, next.c] = Math.Min(nextCost, costs[next.r, next.c]);
                    }
                }
            }

            return costs;
        }

        private int GetCost(List<List<char>> map, (int r, int c) start, (int r, int c) end, int maxCost)
        {

            int[,] costs = new int[map.Count, map[0].Count];
            for (int r = 0; r < map.Count; r++)
            {
                for (int c = 0; c < map[r].Count; c++)
                {
                    costs[r, c] = int.MaxValue;
                }
            }

            bool[,] visited = new bool[map.Count, map[0].Count];


            HashSet<(int r, int c)> toVisit = new();
            toVisit.Add(start);
            costs[start.r, start.c] = 0;

            while (toVisit.Count != 0)
            {
                var curr = FindNext(toVisit, costs, end);
                toVisit.Remove(curr);

                if (curr.r == end.r && curr.c == end.c) break;

                visited[curr.r, curr.c] = true;

                int currCost = costs[curr.r, curr.c];
                if(currCost > maxCost) continue;

                foreach (Dir dir in Enum.GetValues(typeof(Dir)))
                {
                    var next = Move(curr, dir, 1);
                    if (CanMoveTo(next, map) && !visited[next.r, next.c])
                    {
                        toVisit.Add(next);
                        costs[next.r, next.c] = Math.Min(currCost + 1, costs[next.r, next.c]);
                    }
                }
            }

            return costs[end.r, end.c];
        }

        private (int r, int c) FindNext(HashSet<(int r, int c)> toVisit, int[,] costs, (int r, int c) end)
        {
            int minCost = int.MaxValue;
            (int r, int c) minPos = (-1, -1);
            foreach (var pos in toVisit)
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
            return IsValid(pos, map) && map[pos.r][pos.c] != '#';
        }

        private bool IsValid((int r, int c) pos, List<List<char>> map)
        {
            return pos.r >= 0 && pos.c >= 0 &&
                pos.r < map.Count && pos.c < map[0].Count;
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