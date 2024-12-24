using System.Reflection.Emit;

namespace AOC2024
{
    public class Day24A
    {
        public abstract class Gate
        {
            public Dictionary<string, bool> SetWires = new(); //wire name -> is wire set
            public Dictionary<string, int> Wires = new(); //wire name -> wire value
            public string Input1;
            public string Input2;
            public string Output;

            public bool CanOperate()
            {
                return SetWires[Input1] && SetWires[Input2];
            }

            public int Operate()
            {
                SetWires[Input1] = false;
                SetWires[Input2] = false;
                return _Operate();
            }

            protected abstract int _Operate();
        }

        public class AndGate() : Gate
        {
            protected override int _Operate()
            {
                return Wires[Input1] & Wires[Input2];
            }
        }

        public class OrGate() : Gate
        {
            protected override int _Operate()
            {
                return Wires[Input1] | Wires[Input2];
            }
        }

        public class XorGate() : Gate
        {
            protected override int _Operate()
            {
                return Wires[Input1] ^ Wires[Input2];
            }
        }

        public void Solve(List<string> data)
        {
            List<(string wire, int value)> todo = new();

            int i = 0;
            for(; i < data.Count; i++)
            {
                string line = data[i];
                if(line.Length == 0) break;
                
                string[] parts = line.Split(": ");
                todo.Add((parts[0], int.Parse(parts[1])));
            }


            Dictionary<string, List<Gate>> gates = new(); //wire > gate
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
                g.SetWires.Add(g.Input1, false);
                g.SetWires.Add(g.Input2, false);
                g.Wires.Add(g.Input1, 0);
                g.Wires.Add(g.Input2, 0);

                if (!gates.ContainsKey(g.Input1)) gates.Add(g.Input1, new());
                if (!gates.ContainsKey(g.Input2)) gates.Add(g.Input2, new());

                gates[g.Input1].Add(g);
                gates[g.Input2].Add(g);
            }

            long output = 0;
            

            while(todo.Count > 0)
            {
                var curr = todo[0];
                todo.RemoveAt(0);

                foreach(Gate g in gates[curr.wire])
                {
                    g.SetWires[curr.wire] = true;
                    g.Wires[curr.wire] = curr.value;

                    if(g.CanOperate())
                    {
                        int result = g.Operate();

                        if(g.Output[0] == 'z')
                        {
                            int pos = int.Parse(g.Output.Substring(1));

                            output |= ((long)result << pos);
                        }
                        else
                        {
                            todo.Add((g.Output, result));
                        }
                    }
                }
            }

            Console.WriteLine(output);
        }
    }
}