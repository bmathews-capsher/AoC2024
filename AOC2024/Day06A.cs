namespace AOC2024
{
        public class Day06A
        {
                enum Dir
                {
                        U, UR, R, DR, D, DL, L, UL
                }

                public void Solve(List<string> data)
                {
                        List<List<char>> map = Transform(data);

                        bool done = false;

                        (int i, int j) pos = FindStart(map);
                        Dir dir = Dir.U;

                        while(!done)
                        {
                                map[pos.i][pos.j] = 'X';
                                (int i, int j) newPos = Move(pos, dir, 1);

                                if(!IsInBounds(newPos, map))
                                {
                                        done = true;
                                        break;
                                }

                                if(IsBlock(newPos, map))
                                {
                                        dir = TurnRight(dir);
                                }
                                else
                                {
                                        pos = newPos;
                                }
                        }

                        int count = 0;
                        for(int i = 0; i < map.Count; i++)
                        {
                                for(int j = 0; j < map[i].Count; j++)
                                {
                                        if(map[i][j] == 'X') count++;
                                }
                        }

                        Console.WriteLine(count);
                }

                private (int i, int j) FindStart(List<List<char>> map)
                {
                        for(int i = 0; i < map.Count; i++)
                        {
                                for(int j = 0; j < map[i].Count; j++)
                                {
                                        if(map[i][j] == '^') return (i, j);
                                }
                        }

                        return (-1, -1);
                }

                private bool IsInBounds((int i, int j) pos, List<List<char>> map)
                {
                        return pos.i >= 0 && pos.j >= 0 && pos.i < map.Count && pos.j < map[pos.i].Count;
                }

                private bool IsBlock((int i, int j) pos, List<List<char>> map)
                {
                        return map[pos.i][pos.j] == '#';
                }

                private Dir TurnRight(Dir dir)
                {
                        switch(dir)
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

                        return Dir.U;
                }

                private (int, int) Move((int i, int j) pos, Dir direction, int distance)
                {
                        switch (direction)
                        {
                                case Dir.U:
                                        return (pos.i - distance, pos.j);
                                case Dir.UR:
                                        return (pos.i - distance, pos.j + distance);
                                case Dir.R:
                                        return (pos.i, pos.j + distance);
                                case Dir.DR:
                                        return (pos.i + distance, pos.j + distance);
                                case Dir.D:
                                        return (pos.i + distance, pos.j);
                                case Dir.DL:
                                        return (pos.i + distance, pos.j - distance);
                                case Dir.L:
                                        return (pos.i, pos.j - distance);
                                case Dir.UL:
                                        return (pos.i - distance, pos.j - distance);
                        }

                        return (0, 0);
                }

                private List<List<char>> Transform(List<string> data)
                {
                        List<List<char>> map = new();

                        for(int i = 0; i < data.Count; i++)
                        {
                                map.Add(new());
                                for(int j = 0; j < data[i].Length; j++)
                                {
                                        map[i].Add(data[i][j]);
                                }
                        }

                        return map;
                }
        }
}