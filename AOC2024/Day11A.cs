namespace AOC2024
{
    public class Day11A
    {

        public void Solve(List<string> data)
        {
            List<long> nums = data[0].Split(' ').Select(long.Parse).ToList();

            for(int iteration = 0; iteration < 25; iteration++)
            {
                for(int i = 0; i < nums.Count; i++)
                {
                    long num = nums[i];
                    string strNum = "" + num;

                    if(num == 0)
                    {
                        nums[i] = 1;
                    }
                    else if((strNum.Length % 2) == 0)
                    {
                        nums[i] = long.Parse(strNum.Substring(0, strNum.Length / 2));
                        nums.Insert(i+1, long.Parse(strNum.Substring(strNum.Length / 2, strNum.Length / 2)));
                        i++;
                    }
                    else
                    {
                        nums[i] = num * 2024;
                    }
                }
            }

            Console.WriteLine(nums.Count);
        }
    }
}