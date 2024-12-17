namespace AOC2024
{
    public class Day16B
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
            List<((int r, int c), Dir dir)>[,,] cameFroms = new List<((int r, int c) pos, Dir dir)>[data.Count, data[0].Length, Enum.GetValues<Dir>().Length];

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

                    cameFroms[r, c, (int)Dir.U] = new();
                    cameFroms[r, c, (int)Dir.R] = new();
                    cameFroms[r, c, (int)Dir.D] = new();
                    cameFroms[r, c, (int)Dir.L] = new();
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
                ((int r, int c) pos, Dir dir) next = (curr.pos, nextDir);
                if (!visited[next.pos.r, next.pos.c, (int)next.dir] && !toVisit.ContainsKey(next)) toVisit.Add(next, 0);

                long newScore = scores[curr.pos.r, curr.pos.c, (int)curr.dir] + TURN_SCORE;
                long oldScore = scores[next.pos.r, next.pos.c, (int)next.dir];

                if(newScore < oldScore)
                {
                    cameFroms[next.pos.r, next.pos.c, (int)next.dir].Clear();
                    cameFroms[next.pos.r, next.pos.c, (int)next.dir].Add(curr);
                    scores[next.pos.r, next.pos.c, (int)next.dir] = newScore;
                }
                else if(newScore == oldScore)
                {
                    cameFroms[next.pos.r, next.pos.c, (int)next.dir].Add(curr);
                }

                //turn left
                nextDir = TurnLeft(curr.dir);
                next = (curr.pos, nextDir);
                if (!visited[next.pos.r, next.pos.c, (int)next.dir] && !toVisit.ContainsKey(next)) toVisit.Add(next, 0);

                newScore = scores[curr.pos.r, curr.pos.c, (int)curr.dir] + TURN_SCORE;
                oldScore = scores[next.pos.r, next.pos.c, (int)next.dir];

                if (newScore < oldScore)
                {
                    cameFroms[next.pos.r, next.pos.c, (int)next.dir].Clear();
                    cameFroms[next.pos.r, next.pos.c, (int)next.dir].Add(curr);
                    scores[next.pos.r, next.pos.c, (int)next.dir] = newScore;
                }
                else if (newScore == oldScore)
                {
                    cameFroms[next.pos.r, next.pos.c, (int)next.dir].Add(curr);
                }

                //move forward
                (int r, int c) nextPos = Move(curr.pos, curr.dir, 1);
                if (map[nextPos.r][nextPos.c] != '#')
                {
                    next = (nextPos, curr.dir);
                    if (!visited[next.pos.r, next.pos.c, (int)next.dir] && !toVisit.ContainsKey(next)) toVisit.Add(next, 0);

                    newScore = scores[curr.pos.r, curr.pos.c, (int)curr.dir] + MOVE_SCORE;
                    oldScore = scores[next.pos.r, next.pos.c, (int)next.dir];

                    if (newScore < oldScore)
                    {
                        cameFroms[next.pos.r, next.pos.c, (int)next.dir].Clear();
                        cameFroms[next.pos.r, next.pos.c, (int)next.dir].Add(curr);
                        scores[next.pos.r, next.pos.c, (int)next.dir] = newScore;
                    }
                    else if (newScore == oldScore)
                    {
                        cameFroms[next.pos.r, next.pos.c, (int)next.dir].Add(curr);
                    }
                }
            }

            HashSet<(int r, int c)> bestSpots = new();

            long finalScore = Math.Min(Math.Min(scores[end.r, end.c, 0], scores[end.r, end.c, 1]), Math.Min(scores[end.r, end.c, 2], scores[end.r, end.c, 3]));

            foreach(Dir dir in Enum.GetValues<Dir>())
            {
                if(scores[end.r, end.c, (int)dir] == finalScore)
                {
                    CollectCameFroms(bestSpots, cameFroms, (end, dir), start);
                }
            }

            foreach(var pos in bestSpots)
            {
                map[pos.r][pos.c] = 'O';
            }

            PrintMap(map);

            Console.WriteLine(bestSpots.Count);
        }

        private void CollectCameFroms(HashSet<(int r, int c)> bestSpots, List<((int r, int c) pos, Dir dir)>[,,] cameFroms, ((int r, int c) pos, Dir dir) curr, (int r, int c) start)
        {
            bestSpots.Add(curr.pos);
            if (curr.pos.r == start.r && curr.pos.c == start.c) return;

            foreach(var cameFrom in cameFroms[curr.pos.r, curr.pos.c, (int)curr.dir])
            {
                CollectCameFroms(bestSpots, cameFroms, cameFrom, start);
            }
        }
        
        private ((int r, int c) pos, Dir dir) GetNext(Dictionary<((int r, int c) pos, Dir dir), long> toVisit, long[,,] scores, (int r, int c) end)
        {
            long min = long.MaxValue;
            ((int r, int c) pos, Dir dir) minPos = ((-1, -1), Dir.U);

            foreach(var curr in toVisit.Keys)
            {
                long score = scores[curr.pos.r, curr.pos.c, (int)curr.dir];

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