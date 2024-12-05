namespace AOC2024
{
        public class Day05A
        {
                public void Solve(List<string> data)
                {
                        Dictionary<int, List<int>> orders = new();

                        int row = 0;
                        for(; row < data.Count; row++)
                        {
                                string order = data[row];

                                if(order == "") break;

                                string[] parts = order.Split('|');

                                int first = int.Parse(parts[0]);
                                int second = int.Parse(parts[1]);

                                if(!orders.ContainsKey(second)) orders[second] = new();

                                orders[second].Add(first);
                        }

                        row++;

                        long sum = 0;
                        for(; row < data.Count; row++)
                        {
                                HashSet<int> bads = new();

                                string dataRow = data[row];
                                List<int> values = dataRow.Split(',').Select(int.Parse).ToList();

                                bool good = true;
                                foreach(int val in values)
                                {
                                        if(bads.Contains(val))
                                        {
                                                good = false;
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

                                if(good) sum += values[values.Count/2];
                        }

                        Console.WriteLine(sum);
                }
        }
}