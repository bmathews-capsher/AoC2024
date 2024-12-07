namespace AOC2024
{
        public class Day06B
        {
                enum Dir
                {
                        U, UR, R, DR, D, DL, L, UL
                }

                public void Solve(List<string> data)
                {
                        char[][] map = Transform(data);
                        int[][] movement = new int[map.Length][];
                        for(int i = 0; i < map.Length; i++) movement[i] = new int[map[i].Length];

                        (int i, int j) pos = FindStart(map);
                        Dir dir = Dir.U;

                        int count = 0;

                        for(int i = 0; i < map.Length; i++)
                        {
                                for(int j = 0; j < map[i].Length; j++)
                                {
                                        if(IsBlock((i, j), map)) continue;
                                        if(i == pos.i && j == pos.j) continue;

                                        char[][] newMap = CloneMap(map);
                                        newMap[i][j] = '#';
                                        int[][] newMovement = CloneMovement(movement);
                                        if (CheckCycle(pos, dir, newMap, newMovement)) count++;
                                }
                        }

                        Console.WriteLine(count);
                }

                private bool CheckCycle((int i, int j) pos, Dir dir, char[][] map, int[][] movement)
                {
                        while(true)
                        {
                                if(HasMoved(movement[pos.i][pos.j], dir)) return true;

                                movement[pos.i][pos.j] = AddMovement(movement[pos.i][pos.j], dir);

                                (int i, int j) newPos = Move(pos, dir, 1);

                                if (!IsInBounds(newPos, map)) break;

                                if (IsBlock(newPos, map))
                                {
                                        dir = TurnRight(dir);
                                }
                                else
                                {
                                        pos = newPos;
                                }
                        }

                        return false;
                }

                private (int i, int j) FindStart(char[][] map)
                {
                        for(int i = 0; i < map.Length; i++)
                        {
                                for(int j = 0; j < map[i].Length; j++)
                                {
                                        if(map[i][j] == '^') return (i, j);
                                }
                        }

                        return (-1, -1);
                }

                private bool IsInBounds((int i, int j) pos, char[][] map)
                {
                        return pos.i >= 0 && pos.j >= 0 && pos.i < map.Length && pos.j < map[pos.i].Length;
                }

                private bool IsBlock((int i, int j) pos, char[][] map)
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

                private char[][] CloneMap(char[][] map)
                {
                        char[][] newMap = new char[map.Length][];
                        for (int i = 0; i < map.Length; i++)
                        {
                                newMap[i] = new char[map.Length];
                                for (int j = 0; j < map[i].Length; j++)
                                {
                                        newMap[i][j] = map[i][j];
                                }
                        }

                        return newMap;
                }

                private int[][] CloneMovement(int[][] movement)
                {
                        int[][] newMovement = new int[movement.Length][];
                        for (int i = 0; i < movement.Length; i++)
                        {
                                newMovement[i] = new int[movement.Length];
                                for (int j = 0; j < movement[i].Length; j++)
                                {
                                        newMovement[i][j] = movement[i][j];
                                }
                        }

                        return newMovement;
                }

                private char[][] Transform(List<string> data)
                {
                        char[][] map = new char[data.Count][];

                        for(int i = 0; i < data.Count; i++)
                        {
                                map[i] = new char[data[i].Length];
                                for(int j = 0; j < data[i].Length; j++)
                                {
                                        map[i][j] = data[i][j];
                                }
                        }

                        return map;
                }

                private bool HasMoved(int val, Dir dir)
                {
                        switch (dir)
                        {
                                case Dir.U:
                                        return (val & 1) > 0;
                                case Dir.R:
                                        return (val & 2) > 0;
                                case Dir.D:
                                        return (val & 4) > 0;
                                case Dir.L:
                                        return (val & 8) > 0;
                        }

                        return false;
                }

                private int AddMovement(int val, Dir dir)
                {
                        switch(dir)
                        {
                                case Dir.U:
                                        return val | 1;
                                case Dir.R:
                                        return val | 2;
                                case Dir.D:
                                        return val | 4;
                                case Dir.L:
                                        return val | 8;
                        }

                        return -1;
                }
        }
}