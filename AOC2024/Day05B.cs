namespace AOC2024
{
        public class Day05B
        {
                public void Solve(List<string> data)
                {
                        Dictionary<int, List<int>> orders = new();
                        Dictionary<int, List<int>> reverseOrders = new();

                        int row = 0;
                        for(; row < data.Count; row++)
                        {
                                string order = data[row];

                                if(order == "") break;

                                string[] parts = order.Split('|');

                                int first = int.Parse(parts[0]);
                                int second = int.Parse(parts[1]);

                                if(!orders.ContainsKey(second)) orders[second] = new();
                                if(!reverseOrders.ContainsKey(first)) reverseOrders[first] = new();

                                orders[second].Add(first);
                                reverseOrders[first].Add(second);
                        }

                        row++;

                        long sum = 0;
                        for(; row < data.Count; row++)
                        {

                                string dataRow = data[row];
                                List<int> values = dataRow.Split(',').Select(int.Parse).ToList();

                                bool swapped = true;
                                bool good = true;

                                while(swapped)
                                {
                                        HashSet<int> bads = new();
                                        Dictionary<int, int> indexes = new();
                                        swapped = false;
                                        for(int i = 0; i < values.Count; i++)
                                        {
                                                int val = values[i];
                                                indexes[val] = i;
                                                if(bads.Contains(val))
                                                {
                                                        good = false;

                                                        int swapVal = -1;
                                                        foreach(int possibleVal in reverseOrders[val])
                                                        {
                                                                if(indexes.ContainsKey(possibleVal))
                                                                {
                                                                        swapVal = possibleVal;
                                                                        break;
                                                                }
                                                        }
                                                        Swap(i, indexes[swapVal], values);
                                                        swapped = true;
                                                        break;
                                                }

                                                if(orders.ContainsKey(val))
                                                {
                                                        foreach(int bad in orders[val])
                                                        {
                                                                bads.Add(bad);
                                                        }
                                                }
                                        }
                                }

                                if(!good) sum += values[values.Count/2];
                        }

                        Console.WriteLine(sum);
                }

                private void Swap(int i, int j, List<int> values)
                {
                        int temp = values[i];
                        values[i] = values[j];
                        values[j] = temp;
                }
        }
}