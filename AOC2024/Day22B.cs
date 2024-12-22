using System.Reflection.Emit;

namespace AOC2024
{
    public class Day22B
    {

        public void Solve(List<string> data)
        {
            Dictionary<int, long> totals = new();

            foreach(string line in data)
            {
                long num = long.Parse(line);

                HashSet<int> firsts = new();

                int sequence = 0;
                int prevPrice = (int)(num % 10);

                for(int i = 0; i < 2000; i++) 
                {
                    num = Evolve(num);
                    int price = (int)(num % 10);
                    int change = price - prevPrice;

                    sequence = (sequence * 100) % 100000000;
                    sequence += change + 10;

                    //start recording after 4 changes
                    if(i >= 3)
                    {
                        if(!firsts.Contains(sequence)) 
                        {
                            firsts.Add(sequence);

                            if(!totals.ContainsKey(sequence)) totals.Add(sequence, 0);
                            totals[sequence] += price;
                        }
                    }

                    prevPrice = price;
                }
            }

            long maxPrice = -int.MaxValue;

            foreach(var entry in totals.Values)
            {
                if(entry > maxPrice)
                {
                    maxPrice = entry;
                }
            }

            Console.WriteLine(maxPrice);
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