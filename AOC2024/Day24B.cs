using System.Diagnostics;

namespace AOC2024
{
    public class Day24B
    {
        public Dictionary<string, int> positions = new();

        public Day24B()
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
            public abstract string GetOp();

            protected abstract int _Operate(int value2);

            public override string ToString()
            {
                return Input1 + " " + GetOp() + " " + Input2 + " => " + Output;
            }
        }

        public class AndGate() : Gate
        {
            public override string GetOp()
            {
                return "AND";
            }


            protected override int _Operate(int value2)
            {
                return value & value2;
            }
        }

        public class OrGate() : Gate
        {
            public override string GetOp()
            {
                return "OR";
            }
            protected override int _Operate(int value2)
            {
                return value | value2;
            }
        }

        public class XorGate() : Gate
        {
            public override string GetOp()
            {
                return "XOR";
            }
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

            Dictionary<string, Gate> outputs = new();

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

                if (g.Output == "z39") g.Output = "pfw";
                else if (g.Output == "pfw") g.Output = "z39";
                if (g.Output == "z33") g.Output = "dqr";
                else if (g.Output == "dqr") g.Output = "z33";
                if (g.Output == "z21") g.Output = "shh";
                else if (g.Output == "shh") g.Output = "z21";
                if (g.Output == "vgs") g.Output = "dtk";
                else if (g.Output == "dtk") g.Output = "vgs";

                if (!gates.ContainsKey(g.Input1)) gates.Add(g.Input1, new());
                if (!gates.ContainsKey(g.Input2)) gates.Add(g.Input2, new());

                gates[g.Input1].Add(g);
                gates[g.Input2].Add(g);

                allGates.Add(g);

                outputs.Add(g.Output, g);
            }

            HashSet<string> seen = new();
            //21 z21
            //22 shh
            //26 vgs
            //27 dtk
            //33 z33
            //34 dqr
            //39 z39
            //40 pfw
            //dqr,dtk,pfw,shh,vgs,z21,z33,z39

            for (int z = 0; z < 46; z++)
            {
                string name = "z" + z.ToString("00");
                Console.WriteLine(name);
                Console.WriteLine();
                List<Gate> todo = [outputs[name]];

                while(todo.Count > 0)
                {

                    Gate curr = todo[0];
                    todo.RemoveAt(0);
                    if (seen.Contains(curr.Output)) continue;
                    seen.Add(curr.Output);

                    Console.WriteLine(curr.ToString());

                    if (outputs.ContainsKey(curr.Input1)) todo.Add(outputs[curr.Input1]);
                    if (outputs.ContainsKey(curr.Input2)) todo.Add(outputs[curr.Input2]);
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
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