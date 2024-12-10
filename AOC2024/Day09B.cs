namespace AOC2024
{
    public class Day09B
    {
        public void Solve(List<string> data)
        {
            List<int> ids = new();
            List<int> counts = new();

            int maxId = -1;
            for(int i = 0; i < data[0].Length; i++)
            {
                if((i%2) == 0)
                {
                    maxId = i / 2;
                    ids.Add(maxId);
                }
                else
                {
                    ids.Add(-1);
                }

                counts.Add(data[0][i] - '0');
            }

            //move each id if possible, starting highest to lowest
            for(int id = maxId; id >= 0; id--)
            {
                //get the current index of the id being worked on
                int fromIndex = 0;
                for(int i = ids.Count-1; i >= 0; i--)
                {
                    if(ids[i] == id)
                    {
                        fromIndex = i;
                        break;
                    }
                }

                //find the earliest slot we can put it in
                for(int i = 0; i < fromIndex; i++)
                {
                    if(ids[i] != -1) continue; //not empty
                    if(counts[i] < counts[fromIndex]) continue; // not big enough

                    Move(fromIndex, i, ids, counts);
                    break;
                }
            }

            long sum = 0;
            int expandedIndex = 0;
            for (int i = 0; i < counts.Count; i++)
            {
                while(counts[i] > 0)
                {
                    if(ids[i] > -1) 
                    {
                        sum += expandedIndex * ids[i];
                    }
                    counts[i] --;
                    expandedIndex++;
                }
            }

            Console.WriteLine(sum);
        }

        private void Move(int fromIndex, int toIndex, List<int> ids, List<int> counts)
        {
            int moveId = ids[fromIndex];
            ids[fromIndex] = -1;

            counts.Insert(toIndex, counts[fromIndex]);
            ids.Insert(toIndex, moveId);

            int oldToIndex = toIndex + 1;
            counts[oldToIndex] -= counts[toIndex];

            if(counts[oldToIndex] == 0)
            {
                counts.RemoveAt(oldToIndex);
                ids.RemoveAt(oldToIndex);
            }    
        }
    }
}