namespace AOC2024
{
        public class Day04B
        {
                enum Dir
                {
                        U, UR, R, DR, D, DL, L, UL
                }

                public void Solve(List<string> data)
                {
                        int count = 0;

                        for(int i = 0; i < data.Count; i++)
                        {
                                for(int j = 0; j < data[i].Length; j++)
                                {
                                        if (CheckVal((i, j), data, 'A') && CheckUp((i, j), data))
                                        {
                                                count++;
                                        }
                                        if (CheckVal((i, j), data, 'A') && CheckRight((i, j), data))
                                        {
                                                count++;
                                        }
                                        if (CheckVal((i, j), data, 'A') && CheckDown((i, j), data))
                                        {
                                                count++;
                                        }
                                        if (CheckVal((i, j), data, 'A') && CheckLeft((i, j), data))
                                        {
                                                count++;
                                        }
                                }
                        }

                        Console.WriteLine(count);
                }

                private bool CheckUp((int i, int j) pos, List<string> data)
                {
                        return CheckVal(Move(pos, Dir.UL, 1), data, 'M') &&
                                CheckVal(Move(pos, Dir.UR, 1), data, 'M') &&
                                CheckVal(Move(pos, Dir.DR, 1), data, 'S') &&
                                CheckVal(Move(pos, Dir.DL, 1), data, 'S');
                }

                private bool CheckRight((int i, int j) pos, List<string> data)
                {
                        return CheckVal(Move(pos, Dir.UL, 1), data, 'S') &&
                                CheckVal(Move(pos, Dir.UR, 1), data, 'M') &&
                                CheckVal(Move(pos, Dir.DR, 1), data, 'M') &&
                                CheckVal(Move(pos, Dir.DL, 1), data, 'S');
                }

                private bool CheckDown((int i, int j) pos, List<string> data)
                {
                        return CheckVal(Move(pos, Dir.UL, 1), data, 'S') &&
                                CheckVal(Move(pos, Dir.UR, 1), data, 'S') &&
                                CheckVal(Move(pos, Dir.DR, 1), data, 'M') &&
                                CheckVal(Move(pos, Dir.DL, 1), data, 'M');
                }

                private bool CheckLeft((int i, int j) pos, List<string> data)
                {
                        return CheckVal(Move(pos, Dir.UL, 1), data, 'M') &&
                                CheckVal(Move(pos, Dir.UR, 1), data, 'S') &&
                                CheckVal(Move(pos, Dir.DR, 1), data, 'S') &&
                                CheckVal(Move(pos, Dir.DL, 1), data, 'M');
                }

                private bool CheckVal((int i, int j) pos, List<string> data, char val)
                {
                        if(pos.i < 0 || pos.i >= data.Count) return false;
                        if(pos.j < 0 || pos.j >= data[pos.i].Length) return false;

                        return data[pos.i][pos.j] == val;
                }

                private (int, int) Move((int i, int j) pos, Dir direction, int distance)
                {
                        switch(direction)
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

                        return (0 , 0);
                }
        }
}