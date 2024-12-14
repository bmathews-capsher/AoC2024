namespace AOC2024
{
    public class Day14A
    {
        const int Rows = 103;
        const int Cols = 101;
        //const int Rows = 7;
        //const int Cols = 11;
        public void Solve(List<string> data)
        {
            int[,] room = new int[Rows,Cols];
            foreach(string line in data)
            {
                string[] vectors = line.Split(' ');

                var pos = ParseVector(vectors[0]);
                var vel = ParseVector(vectors[1]);
                var distMoved = Mult(100, vel);

                (int r, int c) newPos = ((pos.r + distMoved.r) % Rows, (pos.c + distMoved.c) % Cols);
                if(newPos.r < 0) newPos.r += Rows;
                if(newPos.c < 0) newPos.c += Cols;
                room[newPos.r, newPos.c] ++;
            }

            int quad1 = 0;
            for(int r = 0; r < room.GetLength(0) / 2; r++)
            {
                for(int c = 0; c < room.GetLength(1) / 2; c++)
                {
                    quad1 += room[r, c];
                }
            }

            int quad2 = 0;
            for(int r = (room.GetLength(0) / 2) + 1; r < room.GetLength(0); r++)
            {
                for(int c = 0; c < room.GetLength(1) / 2; c++)
                {
                    quad2 += room[r, c];
                }
            }

            int quad3 = 0;
            for(int r = 0; r < room.GetLength(0) / 2; r++)
            {
                for(int c = (room.GetLength(1) / 2) + 1; c < room.GetLength(1); c++)
                {
                    quad3 += room[r, c];
                }
            }

            int quad4 = 0;
            for(int r = (room.GetLength(0) / 2) + 1; r < room.GetLength(0); r++)
            {
                for(int c = (room.GetLength(1) / 2) + 1; c < room.GetLength(1); c++)
                {
                    quad4 += room[r, c];
                }
            }

            Console.WriteLine(quad1 * quad2 * quad3 * quad4);
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
    }
}