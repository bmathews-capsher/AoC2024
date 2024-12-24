using System.Diagnostics;

namespace AOC2024
{
    public class Day24D
    {
        public Dictionary<string, int> positions = new();

        public Day24D()
        {
            for(int i = 0; i < 46; i++)
            {
                positions.Add("z" + i.ToString("00"), i);
            }
        }

        public abstract class Gate
        {
            public int value;
            public bool isSet;
            public string Input1;
            public string Input2;
            public string Output;

            public int Operate(int value2)
            {
                return _Operate(value2);
            }

            protected abstract int _Operate(int value2);
        }

        public class AndGate() : Gate
        {
            protected override int _Operate(int value2)
            {
                return value & value2;
            }
        }

        public class OrGate() : Gate
        {
            protected override int _Operate(int value2)
            {
                return value | value2;
            }
        }

        public class XorGate() : Gate
        {
            protected override int _Operate(int value2)
            {
                return value ^ value2;
            }
        }

        public void Solve(List<string> data)
        {

            int i = 0;
            for(; i < data.Count; i++)
            {
                string line = data[i];
                if(line.Length == 0) break;
            }


            Dictionary<string, List<Gate>> gates = new(); //wire > gate
            List<Gate> allGates = new();
            for(i++; i < data.Count; i++)
            {
                string line = data[i];

                string[] parts = line.Split(" ");

                Gate g;
                switch(parts[1])
                {
                    case "AND":
                        g = new AndGate();
                        break;
                    case "OR":
                        g = new OrGate();
                        break;
                    default:
                        g = new XorGate();
                        break;
                        
                }

                g.Input1 = parts[0];
                g.Input2 = parts[2];
                g.Output = parts[4];

                if (!gates.ContainsKey(g.Input1)) gates.Add(g.Input1, new());
                if (!gates.ContainsKey(g.Input2)) gates.Add(g.Input2, new());

                gates[g.Input1].Add(g);
                gates[g.Input2].Add(g);

                allGates.Add(g);
            }

            for(int a = 0; a < allGates.Count; a++)
            {
                for (int b = a+1; b < allGates.Count; b++)
                {
                    Console.WriteLine(b);
                    for (int c = b+1; c < allGates.Count; c++)
                    {
                        for (int d = c+1; d < allGates.Count; d++)
                        {

                            SwapGates(a, b, allGates);
                            SwapGates(c, d, allGates);

                            bool works = true;
                            for (int pow = 0; pow < 45; pow++)
                            {
                                List<(string wire, int value)> inputs = new();

                                inputs.AddRange(GenerateInput(1 << pow, 'x'));
                                inputs.AddRange(GenerateInput(3518437208883215, 'y'));

                                long output = Calculate(inputs, gates);
                                long expected = (1 << pow) + 3518437208883215;

                                if(output != expected)
                                {
                                    works = false;
                                    break;
                                }
                            }

                            if (works)
                            {
                                Console.WriteLine(a + ", " + b + ", " + c + ", " + d);
                            }
                            SwapGates(a, b, allGates);
                            SwapGates(c, d, allGates);

                        }
                    }
                }
            }
        }

        private void SwapGates(int a, int b, List<Gate> allGates)
        {
            Gate temp = allGates[a];
            allGates[a] = allGates[b];
            allGates[b] = temp;
        }

        private long Calculate(List<(string wire, int value)> inputs, Dictionary<string, List<Gate>> gates)
        {
            long output = 0;

            while (inputs.Count > 0)
            {
                var curr = inputs[0];
                inputs.RemoveAt(0);

                foreach (Gate g in gates[curr.wire])
                {
                    if (!g.isSet)
                    {
                        g.isSet = true;
                        g.value = curr.value;
                    }
                    else
                    {
                        int result = g.Operate(curr.value);

                        if (g.Output[0] == 'z')
                        {
                            int pos = positions[g.Output];

                            output |= ((long)result << pos);
                        }
                        else
                        {
                            inputs.Add((g.Output, result));
                        }
                    }
                }
            }

            return output;
        }

        private List<(string wire, int value)> GenerateInput(long num, char prefix)
        {
            List<(string wire, int value)> values = new();

            for(int i = 0; i < 45; i++)
            {
                string name = prefix + i.ToString("00");
                int value = (int) (num >> i) & 1;
                values.Add((name, value));

            }
            
            return values;
        }
    }
}