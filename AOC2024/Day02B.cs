namespace AOC2024
{
        public class Day02B
        {
                public void Solve(List<string> data)
                {
                        int safeCount = 0;

                        foreach(string line in data)
                        {
                                List<long> vals = line.Split(' ').Select(long.Parse).ToList();

                                for(int i = 0; i < vals.Count; i++)
                                {
                                        long removed = vals[i];
                                        vals.RemoveAt(i);
                                        if (CheckSafe(vals))
                                        {
                                                safeCount++;
                                                break;
                                        }
                                        vals.Insert(i, removed);
                                }
                        }

                        Console.WriteLine(safeCount);
                }

                public bool CheckSafe(List<long> vals)
                {
                        long diff = vals[1] - vals[0];
                        int compare = diff == 0 ? 0 : diff < 0 ? -1 : 1;

                        if (compare == 0 || Math.Abs(diff) > 3) return false;

                        for (int i = 1; i < vals.Count - 1; i++)
                        {
                                diff = vals[i + 1] - vals[i];
                                int currCompare = diff == 0 ? 0 : diff < 0 ? -1 : 1;

                                if (compare != currCompare || Math.Abs(diff) > 3)
                                {
                                        compare = 0;
                                        break;
                                }
                        }

                        return compare != 0;
                }
        }
}