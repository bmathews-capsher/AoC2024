namespace AOC2024
{
        public class AOC2024
        {
                public static void Main(string[] args) 
                {
                        List<long> values = new();

                        values.Add(5);
                        values.Add(2929);
                        values.Add(1);
                        values.Add(4949);
                        values.Add(492);
                        values.Add(502);
                        values.Add(229195);
                        values.Add(5249409);

                        for(int i = 0; i < 2; i++)
                        {
                                for(int j = 1; j < 500; j++)
                                {
                                        if(i == 1 && j > 2) break;
                                        values.Add(j);
                                }
                        }

                        var result = Utility.SumWithCycle(values, 1000000);

                        Console.WriteLine(result);
                }
        }
}
