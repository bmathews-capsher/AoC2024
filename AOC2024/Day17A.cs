using System.Reflection.Emit;

namespace AOC2024
{
    public class Day17A
    {
        class Computer
        {
            public DataStorage data;
            public List<int> output = new();

            public Computer(DataStorage data)
            {
                this.data = data;
            }

            public void Process()
            {
                while(data.instructionPointer < data.instructions.Count)
                {
                    Instruction instruction = data.instructions[data.instructionPointer];

                    switch(instruction.OpCode)
                    {
                        case 0:
                            adv(instruction.Operand);
                            break;
                        case 1:
                            bxl(instruction.Operand);
                            break;
                        case 2:
                            bst(instruction.Operand);
                            break;
                        case 3:
                            jnz(instruction.Operand);
                            break;
                        case 4:
                            bxc(instruction.Operand);
                            break;
                        case 5:
                            _out(instruction.Operand);
                            break;
                        case 6:
                            bdv(instruction.Operand);
                            break;
                        case 7:
                            cdv(instruction.Operand);
                            break;
                    }
                }
            }

            private int GetComboValue(int param)
            {
                switch(param)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        return param;
                    case 4:
                        return data.registerA;
                    case 5:
                        return data.registerB;
                    case 6:
                        return data.registerC;
                }

                return -1;
            }

            //divide A
            private void adv(int param)
            {
                param = GetComboValue(param);
                data.registerA = data.registerA / (1 << param);
                data.instructionPointer++;
            }

            //xor B ^ o
            private void bxl(int param)
            {
                data.registerB = data.registerB ^ param;
                data.instructionPointer++;
            }

            //mod
            private void bst(int param)
            {
                param = GetComboValue(param);
                data.registerB = param % 8;
                data.instructionPointer++;
            }

            //jump
            private void jnz(int param)
            {
                if(data.registerA == 0)
                {
                    data.instructionPointer++;
                    return;
                }
                data.instructionPointer = param / 2;
            }

            //xor B ^ C
            private void bxc(int param)
            {
                data.registerB = data.registerB ^ data.registerC;
                data.instructionPointer++;
            }

            //output
            private void _out(int param)
            {
                param = GetComboValue(param);
                output.Add(param % 8);
                data.instructionPointer++;
            }

            //divide B
            private void bdv(int param)
            {
                param = GetComboValue(param);
                data.registerB = data.registerA / (1 << param);
                data.instructionPointer++;
            }

            //divide C
            private void cdv(int param)
            {
                param = GetComboValue(param);
                data.registerC = data.registerA / (1 << param);
                data.instructionPointer++;
            }
        }

        class DataStorage
        {
            public int instructionPointer;
            public int registerA;
            public int registerB;
            public int registerC;

            public List<Instruction> instructions = new();
        }

        struct Instruction
        {
            public int OpCode;
            public int Operand;

            public Instruction(int opCode, int operand)
            {
                OpCode = opCode;
                Operand = operand;
            }

            public override string ToString()
            {
                return "OpCode: " + OpCode + ", Operand: " + Operand;
            }
        }

        public void Solve(List<string> data)
        {
            DataStorage dataStorage = new();

            dataStorage.registerA = int.Parse(data[0].Substring(12));
            dataStorage.registerB = int.Parse(data[1].Substring(12));
            dataStorage.registerC = int.Parse(data[2].Substring(12));

            int[] vals = data[4].Substring(9).Split(',').Select(int.Parse).ToArray();

            for(int i = 0; i < vals.Length; i++)
            {
                dataStorage.instructions.Add(new(vals[i], vals[i+1]));
                i++;
            }

            Computer comp = new(dataStorage);
            comp.Process();

            Console.WriteLine("Register A: " + comp.data.registerA);
            Console.WriteLine("Register B: " + comp.data.registerB);
            Console.WriteLine("Register C: " + comp.data.registerC);
            Console.WriteLine("Output: " + string.Join(',', comp.output.ToArray()));
        }

    }
}