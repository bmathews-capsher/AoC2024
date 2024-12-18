using System.Reflection.Emit;

namespace AOC2024
{
    public class Day17B
    {
        class Computer
        {
            public DataStorage data;
            public List<uint> output = new();

            public Computer(DataStorage data)
            {
                this.data = data;
            }

            public void CondensedHardCodedInst()
            {
                ulong a = data.registerA;
                uint b = 0;
                uint c = 0;
                uint d = 0;
                while (a > 0)
                {
                    b = (uint)(a % 8) ^ 2;
                    c = (uint)(a >> (int)b);
                    d = b ^ 3 ^ c;
                    output.Add(d % 8);
                    a = a / 8;
                }

                data.registerA = a;
                data.registerB = b;
                data.registerC = c;
            }
        }

        class DataStorage
        {
            public ulong registerA;
            public ulong registerB;
            public ulong registerC;
        }

        public void Solve(List<string> data)
        {
            int[] vals = data[4].Substring(9).Split(',').Select(int.Parse).ToArray();
            var result = Search(vals, vals.Length - 1, 0);
            Console.WriteLine(result.A);
        }

        private (ulong A, bool success) Search(int[] vals, int index, ulong currA)
        {
            for(uint i = 0; i < 8; i++)
            {
                ulong attempt = i + currA;

                DataStorage dataStorage = new();

                dataStorage.registerA = attempt;
                dataStorage.registerB = 0;
                dataStorage.registerC = 0;
                Computer comp = new(dataStorage);
                comp.CondensedHardCodedInst();

                if(comp.output.Count > 0 && comp.output[0] == vals[index])
                {
                    if(index == 0) return (attempt, true);

                    var subResult = Search(vals, index - 1, attempt << 3);
                    if(subResult.success)
                    {
                        return subResult;
                    }
                }
            }

            return (0, false);
        }
    }
}