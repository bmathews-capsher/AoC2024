namespace AOC2024
{
    public class Day11B
    {
        public void Solve(List<string> data)
        {
            List<long> nums = data[0].Split(' ').Select(long.Parse).ToList();
            Dictionary<long, ((long left, long right) split, int iterationsUntilSplit)> splitMap = new();
            Dictionary<(int iteration, long num), long> splitCountMap = new();

            long count = 0;

            foreach(long num in nums)
            {
                count += GetSplitCount(0, num, splitMap, splitCountMap);
            }

            Console.WriteLine(count);
        }

        private long GetSplitCount(int iteration, long num, 
            Dictionary<long, ((long left, long right) split, int iterationsUntilSplit)> splitMap, 
            Dictionary<(int iteration, long num), long> splitCountMap)
        {
            if(splitCountMap.ContainsKey((iteration, num)))
            {
                return splitCountMap[(iteration, num)];
            }

            if (!splitMap.ContainsKey(num))
            {
                splitMap.Add(num, Iterate(num));
            }

            ((long left, long right) split, int iterationsUntilSplit) split = splitMap[num];
            int newIteration = iteration + split.iterationsUntilSplit;

            if (newIteration > 75) return 1;

            long count = 0;
            count += GetSplitCount(newIteration, split.split.left, splitMap, splitCountMap);
            count += GetSplitCount(newIteration, split.split.right, splitMap, splitCountMap);

            splitCountMap.Add((iteration, num), count);

            return count;
        }

        private ((long left, long right) split, int iterationsUntilSplit) Iterate(long num)
        {
            for(int i = 1; true; i++)
            {
                string strNum = "" + num;
                if (num == 0)
                {
                    num = 1;
                }
                else if ((strNum.Length % 2) == 0)
                {
                    (long left, long right) split = (0, 0);
                    split.left = long.Parse(strNum.Substring(0, strNum.Length / 2));
                    split.right = long.Parse(strNum.Substring(strNum.Length / 2, strNum.Length / 2));
                    return (split, i);
                }
                else
                {
                    num = num * 2024;
                }
            }
        }
    }
}