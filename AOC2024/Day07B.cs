using System.Numerics;

namespace AOC2024
{
        public class Day07B
        {

                public void Solve(List<string> data)
                {
                        BigInteger sum = 0;
                        foreach(string line in data)
                        {
                                string[] parts = line.Split(new char[]{':', ' '});

                                long result = long.Parse(parts[0]);
                                List<BigInteger> values = new();
                                for(long i = 2; i < parts.Length; i++)
                                {
                                        values.Add(long.Parse(parts[i]));
                                }

                                if(Possible(result, values, 0, 0)) sum += result;
                        }

                        Console.WriteLine(sum);
                }

                private bool Possible(BigInteger result, List<BigInteger> values, int index, BigInteger currResult)
                {
                        if(index == values.Count) return currResult == result;

                        //add
                        if (Possible(result, values, index + 1, currResult + values[index])) return true;
                        //mult
                        if (Possible(result, values, index + 1, currResult * values[index])) return true;

                        // merge
                        BigInteger temp = values[index];
                        int place = 1;
                        while(temp > 0)
                        {
                                place *= 10;
                                temp /= 10;
                        }

                        BigInteger newResult = (currResult * place) + values[index];

                        if(Possible(result, values, index + 1, newResult)) return true;

                        //none worked
                        return false;
                }
        }
}