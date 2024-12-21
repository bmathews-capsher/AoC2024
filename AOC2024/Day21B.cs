using System.Reflection.Emit;

namespace AOC2024
{
    public class Day21B
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

            public State(int count)
            {
                CurrentButtons = new char[count];
                
                for (int i = 0; i < count; i++)
                {
                    CurrentButtons[i] = 'A';
                }
            }

            public State Clone()
            {
                State result = new(CurrentButtons.Length);
                CopyTo(result);
                return result;
            }

            public void CopyTo(State other)
            {
                Array.Copy(CurrentButtons, other.CurrentButtons, CurrentButtons.Length);
            }
        }

        public void Solve(List<string> data)
        {
            int numKeypads = 27;

            long sum = 0;
            foreach(string line in data)
            {
                State state = new(numKeypads);
                Dictionary<(char from, char to, int level), long> cache = new(); // from/to/level => presses
                long presses = 0;
                foreach(char button in line)
                {
                    presses += PressButton(button, state, 0, numKeypads-1, true, cache);
                }

                int numeric = int.Parse(line.Substring(0, 3));
                sum += numeric * presses;
            }
            Console.WriteLine(sum);
        }


        public long PressButton(char targetButton, State state, int id, int maxID, bool isNPad, Dictionary<(char from, char to, int level), long> cache)
        {

            var cacheKey = (state.CurrentButtons[id], targetButton, id);
            if(cache.ContainsKey(cacheKey))
            {
                state.CurrentButtons[id] = targetButton;
                return cache[cacheKey];
            }

            if (id == maxID)
            {
                if (!cache.ContainsKey(cacheKey))
                {
                    cache.Add(cacheKey, 1);
                }

                state.CurrentButtons[id] = targetButton;

                return 1;
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
            long vertPresses = 0;
            if (canVertFirst)
            {
                for (int i = 0; i < vertDist; i++)
                {
                    vertPresses += PressButton(vertButton, vertState, nextId, maxID, false, cache);
                }
                for (int i = 0; i < horizDist; i++)
                {
                    vertPresses += PressButton(horizButton, vertState, nextId, maxID, false, cache);
                }
                vertPresses += PressButton('A', vertState, nextId, maxID, false, cache);
            }

            State horizState = state.Clone();
            bool canHorizFirst = CanMoveHorizFirst(currentPos, targetPos, isNPad);
            long horizPresses = 0;
            if (canHorizFirst)
            {
                for (int i = 0; i < horizDist; i++)
                {
                    horizPresses += PressButton(horizButton, horizState, nextId, maxID, false, cache);
                }
                for (int i = 0; i < vertDist; i++)
                {
                    horizPresses += PressButton(vertButton, horizState, nextId, maxID, false, cache);
                }
                horizPresses += PressButton('A', horizState, nextId, maxID, false, cache);
            }

            long presses = 0;
            if (!canVertFirst) 
            {
                horizState.CopyTo(state);
                presses = horizPresses;
            }
            else if (!canHorizFirst) 
            {
                vertState.CopyTo(state);
                presses = vertPresses;
            }
            else if(vertPresses < horizPresses) 
            {
                vertState.CopyTo(state);
                presses = vertPresses;
            }
            else 
            {
                horizState.CopyTo(state);
                presses = horizPresses;
            }

            state.CurrentButtons[id] = targetButton;

            if (!cache.ContainsKey(cacheKey)) 
            {
                cache.Add(cacheKey, presses);
            }

            return presses;
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