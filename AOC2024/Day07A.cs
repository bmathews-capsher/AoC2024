namespace AOC2024
{
        public class Day07A
        {

                public void Solve(List<string> data)
                {
                        long sum = 0;
                        foreach(string line in data)
                        {
                                string[] parts = line.Split(new char[]{':', ' '});

                                long result = long.Parse(parts[0]);
                                List<long> values = new();
                                for(long i = 2; i < parts.Length; i++)
                                {
                                        values.Add(long.Parse(parts[i]));
                                }

                                if(Possible(result, values, 0, 0)) sum += result;
                        }

                        Console.WriteLine(sum);
                }

                private bool Possible(long result, List<long> values, int index, long currResult)
                {
                        if(index == values.Count) return currResult == result;

                        if (Possible(result, values, index + 1, currResult + values[index])) return true;
                        if (Possible(result, values, index + 1, currResult * values[index])) return true;

                        return false;
                }
        }
}