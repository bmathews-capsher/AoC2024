namespace AOC2024
{
    public class Day13B
    {
        public void Solve(List<string> data)
        {
            long tokens = 0;
            for (int i = 0; i < data.Count; i++)
            {
                var a = ReadButton(data[i++]);
                var b = ReadButton(data[i++]);
                var prize = ReadPrize(data[i++]);
                prize.vert += 10000000000000;
                prize.horiz += 10000000000000;

                double bPressesFloat = ((prize.vert * (a.horiz / (double)a.vert)) - prize.horiz) / ((a.horiz * b.vert / (double)a.vert) - b.horiz);
                double aPressesFloat = (prize.horiz - (bPressesFloat * b.horiz)) / a.horiz;

                long aPressesLong = (long)(aPressesFloat + 0.1);
                long bPressesLong = (long)(bPressesFloat + 0.1);

                long horizDiff = a.horiz * aPressesLong + b.horiz * bPressesLong - prize.horiz;
                long vertDiff = a.vert * aPressesLong + b.vert * bPressesLong - prize.vert;

                if (horizDiff == 0 && vertDiff == 0 && aPressesLong >= 0 && bPressesLong >= 0)
                {
                    tokens += 3 * aPressesLong + bPressesLong;
                }
            }

            Console.WriteLine(tokens);
        }

        private (long horiz, long vert) ReadButton(string line)
        {
            string valuePart = line.Substring(10);
            string[] values = valuePart.Split(", ");
            (long horiz, long vert) result;

            result.horiz = long.Parse(values[0].Split('+')[1]);
            result.vert = long.Parse(values[1].Split('+')[1]);

            return result;
        }

        private (long horiz, long vert) ReadPrize(string line)
        {
            string valuePart = line.Substring(7);
            string[] values = valuePart.Split(", ");
            (long horiz, long vert) result;

            result.horiz = long.Parse(values[0].Split('=')[1]);
            result.vert = long.Parse(values[1].Split('=')[1]);

            return result;
        }
    }
}