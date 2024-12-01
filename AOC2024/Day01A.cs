namespace AOC2024
{
        public class Day01A
        {
                public void Solve(List<string> data)
                {
                        List<int> left = new();
                        List<int> right = new();

                        foreach(string line in data)
                        {
                                string[] values = line.Split("   ");

                                left.Add(int.Parse(values[0]));
                                right.Add(int.Parse(values[1]));
                        }

                        left.Sort();
                        right.Sort();

                        int sum = 0;
                        for(int i = 0; i < left.Count; i++)
                        {
                                sum += Math.Abs(left[i] - right[i]);
                        }

                        Console.WriteLine(sum);
                }
        }
}