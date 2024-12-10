namespace AOC2024
{
    public class Day09A
    {
        public void Solve(List<string> data)
        {
            int[] counts = new int[data[0].Length];

            for(int i = 0; i < data[0].Length; i++)
            {
                counts[i] = data[0][i] - '0';
            }

            long sum = 0;

            bool low = true; //use lowId or highId
            int highId = (counts.Length - 1) / 2; // current high id to pull from if in highId mode
            int expandedIndex = 0; // index of the current value in the expanded form of the data
            for (int i = 0; i < counts.Length; i++)
            {
                int lowId = i/2;
                while(counts[i] > 0)
                {
                    if(low)
                    {
                        sum += expandedIndex * lowId;
                    }
                    else if(highId * 2 > i)
                    {
                        sum += expandedIndex * highId;
                        counts[highId*2] --;
                        if(counts[highId*2] == 0) highId --;
                    }
                    counts[i]--;
                    expandedIndex++;
                }

                low = !low;
            }

            Console.WriteLine(sum);
        }
    }
}