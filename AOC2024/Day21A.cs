using System.Reflection.Emit;

namespace AOC2024
{
    public class Day21A
    {
        //nPad < dPad1 < dPad2 < dPad3

        public class NumPad
        {
            public static Dictionary<char, (int r, int c)> positions;

            static NumPad()
            {
                positions = new();
                positions.Add('7', (0, 0));
                positions.Add('8', (0, 1));
                positions.Add('9', (0, 2));
                positions.Add('4', (1, 0));
                positions.Add('5', (1, 1));
                positions.Add('6', (1, 2));
                positions.Add('1', (2, 0));
                positions.Add('2', (2, 1));
                positions.Add('3', (2, 2));
                positions.Add('0', (3, 1));
                positions.Add('A', (3, 2));
            }
        }

        public class DPad
        {
            public static Dictionary<char, (int r, int c)> positions;

            static DPad()
            {
                positions = new();
                positions.Add('^', (0, 1));
                positions.Add('A', (0, 2));
                positions.Add('<', (1, 0));
                positions.Add('v', (1, 1));
                positions.Add('>', (1, 2));
            }
        }

        public class State
        {
            public char[] CurrentButtons;
            public string[] ButtonPresses;

            public State()
            {
                CurrentButtons = new char[4];
                CurrentButtons[0] = 'A';
                CurrentButtons[1] = 'A';
                CurrentButtons[2] = 'A';
                CurrentButtons[3] = 'A';

                ButtonPresses = new string[4];
                ButtonPresses[0] = "";
                ButtonPresses[1] = "";
                ButtonPresses[2] = "";
                ButtonPresses[3] = "";
            }

            public State Clone()
            {
                State result = new();
                CopyTo(result);
                return result;
            }

            public void CopyTo(State other)
            {
                Array.Copy(CurrentButtons, other.CurrentButtons, CurrentButtons.Length);
                Array.Copy(ButtonPresses, other.ButtonPresses, ButtonPresses.Length);
            }
        }

        public void Solve(List<string> data)
        {
            long sum = 0;
            foreach(string line in data)
            {
                State state = new();
                foreach(char button in line)
                {
                    PressButton(button, state, 0, 3, true);
                }

                int numeric = int.Parse(line.Substring(0, 3));
                int length = state.ButtonPresses[3].Length;
                sum += numeric * length;
            }
            Console.WriteLine(sum);
        }


        public void PressButton(char targetButton, State state, int id, int maxID, bool isNPad)
        {
            state.ButtonPresses[id] += targetButton;
            if(id == maxID) 
            {
                state.CurrentButtons[id] = targetButton;
                return;
            }


            int nextId = id + 1;

            (int r, int c) currentPos;
            (int r, int c) targetPos;
            if(isNPad)
            {
                currentPos = NumPad.positions[state.CurrentButtons[id]];
                targetPos = NumPad.positions[targetButton];
            }
            else
            {
                currentPos = DPad.positions[state.CurrentButtons[id]];
                targetPos = DPad.positions[targetButton];
            }


            int vertDist = currentPos.r - targetPos.r;
            char vertButton = vertDist > 0 ? '^' : 'v';
            vertDist = Math.Abs(vertDist);

            int horizDist = currentPos.c - targetPos.c;
            char horizButton = horizDist > 0 ? '<' : '>';
            horizDist = Math.Abs(horizDist);

            State vertState = state.Clone();
            bool canVertFirst = CanMoveVertFirst(currentPos, targetPos, isNPad);
            if (canVertFirst)
            {
                for (int i = 0; i < vertDist; i++)
                {
                    PressButton(vertButton, vertState, nextId, maxID, false);
                }
                for (int i = 0; i < horizDist; i++)
                {
                    PressButton(horizButton, vertState, nextId, maxID, false);
                }
                PressButton('A', vertState, nextId, maxID, false);
            }

            State horizState = state.Clone();
            bool canHorizFirst = CanMoveHorizFirst(currentPos, targetPos, isNPad);
            if (canHorizFirst)
            {
                for (int i = 0; i < horizDist; i++)
                {
                    PressButton(horizButton, horizState, nextId, maxID, false);
                }
                for (int i = 0; i < vertDist; i++)
                {
                    PressButton(vertButton, horizState, nextId, maxID, false);
                }
                PressButton('A', horizState, nextId, maxID, false);
            }

            if (!canVertFirst) horizState.CopyTo(state);
            else if (!canHorizFirst) vertState.CopyTo(state);
            else if(vertState.ButtonPresses[maxID].Length < horizState.ButtonPresses[maxID].Length) vertState.CopyTo(state);
            else horizState.CopyTo(state);

            state.CurrentButtons[id] = targetButton;
        }

        public static bool CanMoveVertFirst((int r, int c) current, (int r, int c) target, bool nPad)
        {
            if(nPad)
            {
                if (current.c > 0) return true;
                if (target.r != 3) return true;
            }
            else
            {
                if (current.c > 0) return true;
                if (target.r != 0) return true;
            }
            return false;
        }

        public static bool CanMoveHorizFirst((int r, int c) current, (int r, int c) target, bool nPad)
        {
            if (nPad)
            {
                if (current.r < 3) return true;
                if (target.c != 0) return true;
            }
            else
            {
                if (current.r > 0) return true;
                if (target.c != 0) return true;
            }
            return false;
        }
    }
}