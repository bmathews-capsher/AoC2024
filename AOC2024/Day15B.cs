namespace AOC2024
{
    public class Day15B
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
                    switch(data[r][c])
                    {
                        case '#':
                            map[r].Add('#');
                            map[r].Add('#');
                            break;
                        case 'O':
                            map[r].Add('[');
                            map[r].Add(']');
                            break;
                        case '.':
                            map[r].Add('.');
                            map[r].Add('.');
                            break;
                        case '@':
                            map[r].Add('@');
                            map[r].Add('.');
                            pos = (r, c * 2);
                            break;
                    }
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
                        if(CanMove(pos, Dir.U, map)) pos = Move(pos, Dir.U, map);
                        break;
                    case '>':
                        if (CanMove(pos, Dir.R, map)) pos = Move(pos, Dir.R, map);
                        break;
                    case 'v':
                        if (CanMove(pos, Dir.D, map)) pos = Move(pos, Dir.D, map);
                        break;
                    case '<':
                        if (CanMove(pos, Dir.L, map)) pos = Move(pos, Dir.L, map);
                        break;
                }
                //Console.WriteLine("Move: " + move);
                //PrintMap(map);
            }



            PrintMap(map);
            long sum = 0;
            for (int r = 0; r < map.Count; r++)
            {
                for (int c = 0; c < map[r].Count; c++)
                {
                    if(map[r][c] == '[')
                    {
                        sum += 100 * r + c;
                    }
                }
            }

            Console.WriteLine(sum);
        }

        private bool CanMove((int r, int c) pos, Dir dir, List<List<char>> map)
        {
            var newPos = Move(pos, dir, 1);
            char newType = map[newPos.r][newPos.c];

            if (newType == '#') return false;
            if (newType == '.') return  true;

            //box
            if (newType == '[') 
            {
                bool result = CanMove(newPos, dir, map);
                if (dir == Dir.U || dir == Dir.D) result = result && CanMove((newPos.r, newPos.c + 1), dir, map);
                return result;
            }
            if (newType == ']')
            {
                bool result = CanMove(newPos, dir, map);
                if (dir == Dir.U || dir == Dir.D) result = result && CanMove((newPos.r, newPos.c - 1), dir, map);
                return result;
            }

            return false; // how'd we get here...
        }

        private (int r, int c) Move((int r, int c) pos, Dir dir, List<List<char>> map)
        {
            char currType = map[pos.r][pos.c];

            var newPos = Move(pos, dir, 1);
            char newType = map[newPos.r][newPos.c];

            if (newType == '#') return (-1, -1); // should have chcecked 'CanMove' first
            if (newType == '.')
            {
                map[pos.r][pos.c] = '.';
                map[newPos.r][newPos.c] = currType;
                return newPos;
            }

            //box
            if (newType == '[')
            {
                Move(newPos, dir, map);
                if(dir == Dir.U || dir == Dir.D) Move((newPos.r, newPos.c + 1), dir, map);
                map[pos.r][pos.c] = '.';
                map[newPos.r][newPos.c] = currType;
                return newPos;
            }
            if (newType == ']')
            {
                Move(newPos, dir, map);
                if (dir == Dir.U || dir == Dir.D) Move((newPos.r, newPos.c - 1), dir, map);
                map[pos.r][pos.c] = '.';
                map[newPos.r][newPos.c] = currType;
                return newPos;
            }

            return (-1, -1); // how'd we get here...
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
                for(int c = 0; c < map[r].Count; c++)
                {
                    Console.Write(map[r][c]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}