namespace AOC2024
{
        public class Day03A
        {
                public void Solve(List<string> data)
                {

                        long sum = 0;
                        foreach(string row in data)
                        {
                                string[] muls = row.Split("mul(");

                                bool first = true;
                                foreach (string mul in muls)
                                {
                                        if(first)
                                        {
                                                first = false;
                                                continue;
                                        }


                                        string[] contents = mul.Split(")");

                                        if(contents.Length == 1) continue; // no close paren

                                        string[] vals = contents[0].Split(",");

                                        if(vals.Length != 2) continue; //too many commas

                                        int val1 = 0;
                                        if(!int.TryParse(vals[0], out val1)) continue; // first not a num
                                        
                                        int val2 = 0;
                                        if (!int.TryParse(vals[1], out val2)) continue; // second not a num

                                        sum += val1*val2;
                                }
                        }

                        Console.WriteLine(sum);
                }
        }
}