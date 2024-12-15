namespace AOC2024
{
    public class Day15A
    {
        enum Dir
        {
            U, UR, R, DR, D, DL, L, UL
        }

        public void Solve(List<string> data)
        {
            List<List<char>> map = new();

            (int r, int c) pos = (0, 0);
            for (int r = 0; r < data.Count; r++)
            {
                if(data[r].Length == 0) break;
                map.Add(new());
                for (int c = 0; c < data[r].Length; c++)
                {
                    char p = data[r][c];
                    if(p == '@') pos = (r, c);
                    map[r].Add(data[r][c]);
                }
            }

            string moves = "";
            for(int r = map.Count; r < data.Count; r++)
            {
                moves += data[r];
            }

            //PrintMap(map);

            foreach (char move in moves)
            {
                switch(move)
                {
                    case '^':
                        pos = TryMove(pos, Dir.U, map).pos;
                        break;
                    case '>':
                        pos = TryMove(pos, Dir.R, map).pos;
                        break;
                    case 'v':
                        pos = TryMove(pos, Dir.D, map).pos;
                        break;
                    case '<':
                        pos = TryMove(pos, Dir.L, map).pos;
                        break;
                }
                //Console.WriteLine("Move: " + move);
                //PrintMap(map);
            }


            long sum = 0;
            for (int r = 0; r < map.Count; r++)
            {
                for (int c = 0; c < map[r].Count; c++)
                {
                    if(map[r][c] == 'O')
                    {
                        sum += 100 * r + c;
                    }
                }
            }

            Console.WriteLine(sum);
        }

        private (bool success, (int r, int c) pos) TryMove((int r, int c) pos, Dir dir, List<List<char>> map)
        {
            char currType = map[pos.r][pos.c];

            var newPos = Move(pos, dir, 1);
            char newType = map[newPos.r][newPos.c];

            if (newType == '#') return (false, pos);
            if (newType == '.')
            {
                map[pos.r][pos.c] = '.';
                map[newPos.r][newPos.c] = currType;
                return (true, newPos);
            }

            //box
            var result = TryMove(newPos, dir, map);
            if(!result.success) return (false, pos);

            map[pos.r][pos.c] = '.';
            map[newPos.r][newPos.c] = currType;
            return (true, newPos);
        }

        private (int r, int c) Move((int r, int c) pos, Dir direction, int distance)
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