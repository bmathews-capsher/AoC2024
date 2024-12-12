namespace AOC2024
{
    public class Day12B
    {
        enum Dir
        {
            U, UR, R, DR, D, DL, L, UL
        }

        public void Solve(List<string> data)
        {
            List<List<char>> map = new();

            for (int r = 0; r < data.Count; r++)
            {
                map.Add(new());
                for (int c = 0; c < data[r].Length; c++)
                {
                    map[r].Add(data[r][c]);
                }
            }

            bool[,] visited = new bool[map.Count, map[0].Count];


            long sum = 0;
            for (int r = 0; r < data.Count; r++)
            {
                for (int c = 0; c < data[r].Length; c++)
                {
                    if(visited[r, c]) continue;
                    long a = ComputeArea((r, c), map[r][c], map, visited);

                    bool[,] uVisited = new bool[map.Count, map[0].Count];
                    bool[,] rVisited = new bool[map.Count, map[0].Count];
                    bool[,] dVisited = new bool[map.Count, map[0].Count];
                    bool[,] lVisited = new bool[map.Count, map[0].Count];

                    long s = ComputeSides((r, c), map[r][c], map, uVisited, rVisited, dVisited, lVisited);

                    sum += a * s;
                }
            }

            Console.WriteLine(sum);
        }

        private long ComputeArea((int r, int c) pos, char type, List<List<char>> map, bool[,] visited)
        {
            if (!IsValid(pos, map)) return 0;
            if (map[pos.r][pos.c] != type) return 0;
            if (visited[pos.r, pos.c]) return 0;

            visited[pos.r, pos.c] = true;

            long a = 1;

            a += ComputeArea(Move(pos, Dir.U, 1), type, map, visited);
            a += ComputeArea(Move(pos, Dir.R, 1), type, map, visited);
            a += ComputeArea(Move(pos, Dir.D, 1), type, map, visited);
            a += ComputeArea(Move(pos, Dir.L, 1), type, map, visited);

            return a;
        }


        private long ComputeSides((int r, int c) pos, char type, List<List<char>> map, bool[,] uVisited, bool[,] rVisited, bool[,] dVisited, bool[,] lVisited)
        {
            if (!IsValid(pos, map)) return 0;
            if (map[pos.r][pos.c] != type) return 0;
            if(uVisited[pos.r, pos.c] && rVisited[pos.r, pos.c] && dVisited[pos.r, pos.c] && lVisited[pos.r, pos.c]) return 0;

            long sides = 0;

            //up
            if (!uVisited[pos.r, pos.c])
            {
                (int r, int c) currPos = pos;

                if (IsEdge(Move(currPos, Dir.U, 1), type, map)) sides++;

                while (!IsEdge(currPos, type, map) && IsEdge(Move(currPos, Dir.U, 1), type, map))
                {
                    uVisited[currPos.r, currPos.c] = true;
                    currPos = Move(currPos, Dir.R, 1);
                }

                currPos = pos;
                while (!IsEdge(currPos, type, map) && IsEdge(Move(currPos, Dir.U, 1), type, map))
                {
                    uVisited[currPos.r, currPos.c] = true;
                    currPos = Move(currPos, Dir.L, 1);
                }
            }
            uVisited[pos.r, pos.c] = true;

            //right
            if (!rVisited[pos.r, pos.c])
            {
                (int r, int c) currPos = pos;

                if (IsEdge(Move(currPos, Dir.R, 1), type, map)) sides++;

                while (!IsEdge(currPos, type, map) && IsEdge(Move(currPos, Dir.R, 1), type, map))
                {
                    rVisited[currPos.r, currPos.c] = true;
                    currPos = Move(currPos, Dir.U, 1);
                }

                currPos = pos;
                while (!IsEdge(currPos, type, map) && IsEdge(Move(currPos, Dir.R, 1), type, map))
                {
                    rVisited[currPos.r, currPos.c] = true;
                    currPos = Move(currPos, Dir.D, 1);
                }
            }
            rVisited[pos.r, pos.c] = true;

            //down
            if (!dVisited[pos.r, pos.c])
            {
                (int r, int c) currPos = pos;

                if (IsEdge(Move(currPos, Dir.D, 1), type, map)) sides++;

                while (!IsEdge(currPos, type, map) && IsEdge(Move(currPos, Dir.D, 1), type, map))
                {
                    dVisited[currPos.r, currPos.c] = true;
                    currPos = Move(currPos, Dir.R, 1);
                }

                currPos = pos;
                while (!IsEdge(currPos, type, map) && IsEdge(Move(currPos, Dir.D, 1), type, map))
                {
                    dVisited[currPos.r, currPos.c] = true;
                    currPos = Move(currPos, Dir.L, 1);
                }
            }
            dVisited[pos.r, pos.c] = true;

            //left
            if (!lVisited[pos.r, pos.c])
            {
                (int r, int c) currPos = pos;

                if (IsEdge(Move(currPos, Dir.L, 1), type, map)) sides++;

                while (!IsEdge(currPos, type, map) && IsEdge(Move(currPos, Dir.L, 1), type, map))
                {
                    lVisited[currPos.r, currPos.c] = true;
                    currPos = Move(currPos, Dir.U, 1);
                }

                currPos = pos;
                while (!IsEdge(currPos, type, map) && IsEdge(Move(currPos, Dir.L, 1), type, map))
                {
                    lVisited[currPos.r, currPos.c] = true;
                    currPos = Move(currPos, Dir.D, 1);
                }
            }
            lVisited[pos.r, pos.c] = true;

            sides += ComputeSides(Move(pos, Dir.U, 1), type, map, uVisited, rVisited, dVisited, lVisited);
            sides += ComputeSides(Move(pos, Dir.R, 1), type, map, uVisited, rVisited, dVisited, lVisited);
            sides += ComputeSides(Move(pos, Dir.D, 1), type, map, uVisited, rVisited, dVisited, lVisited);
            sides += ComputeSides(Move(pos, Dir.L, 1), type, map, uVisited, rVisited, dVisited, lVisited);

            return sides;
        }

        private bool IsEdge((int r, int c) pos, char type, List<List<char>> map)
        {
            if(!IsValid(pos, map)) return true;
            if(map[pos.r][pos.c] != type) return true;

            return false;
        }

        private bool IsValid((int r, int c) pos, List<List<char>> map)
        {
            return pos.c >= 0 && pos.r >= 0 && pos.r < map.Count && pos.c < map[0].Count;
        }

        private (int, int) Move((int r, int c) pos, Dir direction, int distance)
        {
            switch (direction)
            {
                case Dir.U:
                    return (pos.r - distance, pos.c);
                case Dir.UR:
                    return (pos.r - distance, pos.c + distance);
                case Dir.R:
                    return (pos.r, pos.c + distance);
                case Dir.DR:
                    return (pos.r + distance, pos.c + distance);
                case Dir.D:
                    return (pos.r + distance, pos.c);
                case Dir.DL:
                    return (pos.r + distance, pos.c - distance);
                case Dir.L:
                    return (pos.r, pos.c - distance);
                case Dir.UL:
                    return (pos.r - distance, pos.c - distance);
            }

            return (0, 0);
        }
    }
}