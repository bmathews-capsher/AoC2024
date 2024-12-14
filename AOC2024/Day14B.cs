namespace AOC2024
{
    public class Day14B
    {
        const int Rows = 103;
        const int Cols = 101;
        public void Solve(List<string> data)
        {
            List<((int r, int c) pos, (int r, int c) vel)> bots = new();
            foreach(string line in data)
            {
                string[] vectors = line.Split(' ');

                var pos = ParseVector(vectors[0]);
                var vel = ParseVector(vectors[1]);

                bots.Add((pos, vel));
            }

            for(int i = 0; i < 10000; i++)
            {
                int[,] room = new int[Rows,Cols];
                foreach(var bot in bots)
                {
                    var distMoved = Mult(i, bot.vel);
                    (int r, int c) newPos = ((bot.pos.r + distMoved.r) % Rows, (bot.pos.c + distMoved.c) % Cols);
                    if(newPos.r < 0) newPos.r += Rows;
                    if(newPos.c < 0) newPos.c += Cols;
                    room[newPos.r, newPos.c] ++;
                }

                PrintRoom(room, i+1);
            }
        }

        private (int r, int c) ParseVector(string vector)
        {
            string[] vals = vector.Substring(2).Split(',');

            return (int.Parse(vals[1]), int.Parse(vals[0]));
        }

        private (int r, int c) Mult(int val, (int r, int c) vec)
        {
            return (vec.r * val, vec.c * val);
        }

        private void PrintRoom(int[,] room, int time)
        {
            List<string> output = new();

            int maxRowCount = 0;
            for(int r = 0; r < room.GetLength(0); r++)
            {
                int currCount = 0;
                bool found = false;
                string line = "";
                for(int c = 0; c < room.GetLength(1); c++)
                {
                    if(room[r,c] > 0) 
                    {
                        line += 'X';
                        if(found)
                        {
                            currCount ++;
                        }
                        found = true;
                    }
                    else 
                    {
                        line += '.';
                        found = false;
                    }

                    if(!found) maxRowCount = Math.Max(maxRowCount, currCount);
                }

                output.Add(line);
            }
            
            if(maxRowCount > 15)
            {
                Console.WriteLine(time);
                for(int r = 0; r < room.GetLength(0); r++)
                {
                    Console.WriteLine(output[r]);
                }
                Console.WriteLine();
            }
        }
    }
}