using System.Data;

namespace AOC2024
{
        public class Day03B
        {
                public void Solve(List<string> data)
                {
                        string singleLine = String.Join(' ', data);

                        List<string> ons = new List<string>();

                        string[] greens = singleLine.Split("do()");

                        foreach(string green in greens)
                        {
                                string[] reds = green.Split("don't()");
                                ons.Add(reds[0]);
                        }

                        long sum = 0;
                        foreach(string on in ons)
                        {
                                string[] muls = on.Split("mul(");

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