namespace AOC2024
{
        public class Day02A
        {
                public void Solve(List<string> data)
                {
                        int safeCount = 0;

                        foreach(string line in data)
                        {
                                long[] vals = line.Split(' ').Select(long.Parse).ToArray();

                                long diff = vals[1] - vals[0];
                                int compare = diff == 0 ? 0 : diff < 0 ? -1 : 1;

                                if(compare == 0 || Math.Abs(diff) > 3) continue;

                                for(int i = 1; i < vals.Length - 1; i++)
                                {
                                        diff = vals[i+1] - vals[i];
                                        int currCompare = diff == 0 ? 0 : diff < 0 ? -1 : 1;

                                        if(compare != currCompare || Math.Abs(diff) > 3)
                                        {
                                                compare = 0;
                                                break;
                                        }
                                }

                                if(compare != 0) safeCount++;
                        }

                        Console.WriteLine(safeCount);
                }
        }
}