namespace AOC2024
{
    public class Day16A
    {
        const int TURN_SCORE = 1000;
        const int MOVE_SCORE = 1;

        enum Dir
        {
            U, R, D, L
        }

        public void Solve(List<string> data)
        {
            List<List<char>> map = new();

            long[,,] scores = new long[data.Count, data[0].Length, Enum.GetValues<Dir>().Length];
            bool[,,] visited = new bool[data.Count, data[0].Length, Enum.GetValues<Dir>().Length];

            (int r, int c) start = (0, 0);
            (int r, int c) end = (0, 0);
            for (int r = 0; r < data.Count; r++)
            {
                map.Add(new());
                for (int c = 0; c < data[r].Length; c++)
                {
                    char tile = data[r][c];
                    map[r].Add(tile);

                    if(tile == 'S') start = (r, c);
                    if(tile == 'E') end = (r, c);
                    if(tile == '#')
                    {
                        visited[r, c, (int)Dir.U] = true;
                        visited[r, c, (int)Dir.R] = true;
                        visited[r, c, (int)Dir.D] = true;
                        visited[r, c, (int)Dir.L] = true;
                    }

                    scores[r, c, (int)Dir.U] = long.MaxValue;
                    scores[r, c, (int)Dir.R] = long.MaxValue;
                    scores[r, c, (int)Dir.D] = long.MaxValue;
                    scores[r, c, (int)Dir.L] = long.MaxValue;
                }
            }

            //TODO

            Dictionary<((int r, int c) pos, Dir dir), long> toVisit = new();
            toVisit.Add((start, Dir.R), 0);
            scores[start.r, start.c, (int)Dir.R] = 0;

            while(toVisit.Count != 0)
            {
                var curr = GetNext(toVisit, scores, end);
                toVisit.Remove(curr);

                visited[curr.pos.r, curr.pos.c, (int)curr.dir] = true;

                if(curr.pos.r == end.r && curr.pos.c == end.c) break;

                //turn right
                Dir nextDir = TurnRight(curr.dir);
                if(!visited[curr.pos.r, curr.pos.c, (int)nextDir])
                {
                    ((int r, int c) pos, Dir dir) next = (curr.pos, nextDir);
                    if (!toVisit.ContainsKey(next)) toVisit.Add(next, 0);
                    long newScore = scores[curr.pos.r, curr.pos.c, (int)curr.dir] + TURN_SCORE;

                    scores[next.pos.r, next.pos.c, (int)next.dir] = Math.Min(scores[next.pos.r, next.pos.c, (int)next.dir], newScore);
                }

                //turn left
                nextDir = TurnLeft(curr.dir);
                if (!visited[curr.pos.r, curr.pos.c, (int)nextDir])
                {
                    ((int r, int c) pos, Dir dir) next = (curr.pos, nextDir);
                    if (!toVisit.ContainsKey(next)) toVisit.Add(next, 0);
                    long newScore = scores[curr.pos.r, curr.pos.c, (int)curr.dir] + TURN_SCORE;

                    scores[next.pos.r, next.pos.c, (int)next.dir] = Math.Min(scores[next.pos.r, next.pos.c, (int)next.dir], newScore);
                }

                //move forward
                (int r, int c) nextPos = Move(curr.pos, curr.dir, 1);
                if (!visited[nextPos.r, nextPos.c, (int)curr.dir])
                {
                    ((int r, int c) pos, Dir dir) next = (nextPos, curr.dir);
                    if (!toVisit.ContainsKey(next)) toVisit.Add(next, 0);
                    long newScore = scores[curr.pos.r, curr.pos.c, (int)curr.dir] + MOVE_SCORE;

                    scores[next.pos.r, next.pos.c, (int)next.dir] = Math.Min(scores[next.pos.r, next.pos.c, (int)next.dir], newScore);
                }
            }

            long finalScore = Math.Min(Math.Min(scores[end.r, end.c, 0], scores[end.r, end.c, 1]), Math.Min(scores[end.r, end.c, 2], scores[end.r, end.c, 3]));
            Console.WriteLine(finalScore);
        }


        private long GetDistance((int r, int c) pos, (int r, int c) end)
        {
            return Math.Abs(pos.r - end.r) + Math.Abs(pos.c - end.c);
        }
        
        private ((int r, int c) pos, Dir dir) GetNext(Dictionary<((int r, int c) pos, Dir dir), long> toVisit, long[,,] scores, (int r, int c) end)
        {
            long min = long.MaxValue;
            ((int r, int c) pos, Dir dir) minPos = ((-1, -1), Dir.U);

            foreach(var curr in toVisit.Keys)
            {
                long score = scores[curr.pos.r, curr.pos.c, (int)curr.dir] + GetDistance(curr.pos, end);

                if(score < min)
                {
                    min = score;
                    minPos = curr;
                }
            }

            return minPos;
        }

        private Dir TurnRight(Dir dir)
        {
            switch (dir)
            {
                case Dir.U:
                    return Dir.R;
                case Dir.R:
                    return Dir.D;
                case Dir.D:
                    return Dir.L;
                case Dir.L:
                    return Dir.U;
            }

            throw new Exception(); //can't get here
        }

        private Dir TurnLeft(Dir dir)
        {
            switch (dir)
            {
                case Dir.U:
                    return Dir.L;
                case Dir.R:
                    return Dir.U;
                case Dir.D:
                    return Dir.R;
                case Dir.L:
                    return Dir.D;
            }

            throw new Exception(); //can't get here
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
            for(int r = 0; r < map.Count; r++)
            {
                for(int c = 0; c < map.Count; c++)
                {
                    Console.Write(map[r][c]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}