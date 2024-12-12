namespace AOC2024
{
    public class Day12A
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
                    (long p, long a) geo = ComputeGeometry((r, c), map[r][c], map, visited);
                    sum += geo.p * geo.a;
                }
            }

            Console.WriteLine(sum);
        }


        private (long p, long a) ComputeGeometry((int r, int c) pos, char type, List<List<char>> map, bool[,] visited)
        {
            if (!IsValid(pos, map)) return (1, 0);
            if (map[pos.r][pos.c] != type) return (1, 0);
            if (visited[pos.r, pos.c]) return (0, 0);

            visited[pos.r, pos.c] = true;

            (long p, long a) geo = (0, 1);
            (long p, long a) newGeo;

            newGeo = ComputeGeometry(Move(pos, Dir.U, 1), type, map, visited);
            geo.p += newGeo.p;
            geo.a += newGeo.a;

            newGeo = ComputeGeometry(Move(pos, Dir.R, 1), type, map, visited);
            geo.p += newGeo.p;
            geo.a += newGeo.a;

            newGeo = ComputeGeometry(Move(pos, Dir.D, 1), type, map, visited);
            geo.p += newGeo.p;
            geo.a += newGeo.a;

            newGeo = ComputeGeometry(Move(pos, Dir.L, 1), type, map, visited);
            geo.p += newGeo.p;
            geo.a += newGeo.a;
            
            return geo;
        }

        private bool IsValid((int c, int r) pos, List<List<char>> map)
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