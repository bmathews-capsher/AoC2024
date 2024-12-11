namespace AOC2024
{
    public class Day10B
    {
        enum Dir
        {
            U, UR, R, DR, D, DL, L, UL
        }

        public void Solve(List<string> data)
        {
            List<List<int>> map = new();

            for(int r = 0; r < data.Count; r++)
            {
                map.Add(new());
                for (int c = 0; c < data[0].Length; c++)
                {
                    map[r].Add(data[r][c] - '0');
                }
            }

            long sum = 0;
            for (int r = 0; r < map.Count; r++)
            {
                for (int c = 0; c < map[0].Count; c++)
                {
                    if(map[r][c] != 0) continue;
                    sum += FindNines((r, c), 0, map);
                }
            }

            Console.WriteLine(sum);
        }

        private int FindNines((int r, int c) pos, int target, List<List<int>> map)
        {
            if(!IsValid(pos, map)) return 0;
            if (map[pos.r][pos.c] != target) return 0;
            if (target == 9) 
            {
                return 1;
            }

            int sum = 0;
            sum += FindNines(Move(pos, Dir.U, 1), target + 1, map);
            sum += FindNines(Move(pos, Dir.R, 1), target + 1, map);
            sum += FindNines(Move(pos, Dir.D, 1), target + 1, map);
            sum += FindNines(Move(pos, Dir.L, 1), target + 1, map);
            return sum;
        }

        private bool IsValid((int c, int r) pos, List<List<int>> map)
        {
            return pos.c >= 0 && pos.r >= 0 && pos.c < map.Count && pos.r < map[0].Count;
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