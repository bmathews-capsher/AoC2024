using System.Reflection.Emit;

namespace AOC2024
{
    public class Day22A
    {

        public void Solve(List<string> data)
        {
            long sum = 0;
            foreach(string line in data)
            {
                long num = long.Parse(line);
                for(int i = 0; i < 2000; i++) num = Evolve(num);
                sum += num;
            }

            Console.WriteLine(sum);
        }

        private long Evolve(long num)
        {
            num = ((num * 64) ^ num) % 16777216;
            num = ((num / 32) ^ num) % 16777216;
            num = ((num * 2048) ^ num) % 16777216;
            return num;
        }
    }
}