namespace AOC2024
{
        public class Utility
        {
                public static (bool found, long sum, int initialItemCount, int cycleLength, int numCycles, int remainingItemCount) SumWithCycle(List<long> values, int itemCount)
                {
                        (bool found, long sum, int initialItemCount, int cycleLength, int numCycles, int remainingItemCount) result = (false, 0, 0, 0, 0, 0);
                        for (int startIndex = 0; !result.found && startIndex < 1000 && startIndex < values.Count; startIndex++)
                        {
                                for (int cycleLength = 1; !result.found && cycleLength < 1000 && startIndex + cycleLength < values.Count; cycleLength++)
                                {
                                        if(IsCycle(values, startIndex, startIndex + cycleLength))
                                        {
                                                result.found = true;
                                                result.initialItemCount = startIndex+1;
                                                result.cycleLength = cycleLength;
                                                result.numCycles = (itemCount - result.initialItemCount) / result.cycleLength;
                                                result.remainingItemCount = (itemCount - result.initialItemCount) % result.cycleLength;
                                        }
                                }
                        }

                        for(int i = 0; i < result.initialItemCount; i++)
                        {
                                result.sum += values[i];
                        }

                        long cycleSum = 0;
                        for(int i = 0; i < result.cycleLength; i++)
                        {
                                cycleSum += values[result.initialItemCount + i];
                        }

                        result.sum += cycleSum * result.numCycles;

                        for(int i = 0; i < result.remainingItemCount; i++)
                        {
                                result.sum += values[result.initialItemCount + i];
                        }

                        return result;
                }

                private static bool IsCycle(List<long> values, int startIndex, int cycleIndex)
                {
                        for(int i = 0; i < 100 && i + cycleIndex < values.Count; i ++)
                        {
                                if(values[startIndex+i] != values[cycleIndex+i]) return false;
                        }

                        return true;
                }
        }
}