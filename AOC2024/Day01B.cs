namespace AOC2024
{
        public class Day01B
        {
                public void Solve(List<string> data)
                {
                        List<int> left = new();
                        Dictionary<int, int> right = new();

                        foreach(string line in data)
                        {
                                string[] values = line.Split("   ");

                                int leftVal = int.Parse(values[0]);
                                int rightVal = int.Parse(values[1]);

                                if (!right.ContainsKey(leftVal)) right.Add(leftVal, 0);
                                if (!right.ContainsKey(rightVal)) right.Add(rightVal, 0);

                                left.Add(leftVal);
                                right[rightVal]++;
                        }

                        int sum = 0;
                        foreach(int val in left)
                        {
                                sum += val * right[val];
                        }

                        Console.WriteLine(sum);
                }
        }
}