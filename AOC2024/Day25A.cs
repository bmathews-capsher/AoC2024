using System.Reflection.Emit;

namespace AOC2024
{
    public class Day25A
    {

        public void Solve(List<string> data)
        {
            List<int[]> keys = new();
            List<int[]> locks = new();

            for (int i = 0; i < data.Count; i++)
            {
                bool isKey = data[i][0] == '.';
                int[] device = new int[5];
                for (int j = 0; j < 7; j++)
                {
                    string line = data[i+j];
                    for(int k = 0; k < 5; k++)
                    {
                        if(line[k] == '#') device[k]++;
                    }
                }

                if (isKey) keys.Add(device);
                else locks.Add(device);

                i += 7;
            }

            int sum = 0;
            foreach(int[] k in keys)
            {
                foreach(int[] l in locks)
                {
                    bool works = true;
                    for(int i = 0; i < 5; i++)
                    {
                        if(k[i] + l[i] > 7)
                        {
                            works = false;
                            break;
                        }
                    }

                    if(works) sum++;
                }
            }

            Console.WriteLine(sum);
        }
    }
}