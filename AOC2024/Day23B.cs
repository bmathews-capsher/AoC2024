using System.Reflection.Emit;

namespace AOC2024
{
    public class Day23B
    {

        public void Solve(List<string> data)
        {
            Dictionary<string, HashSet<string>> map = new();

            foreach (string line in data)
            {
                string[] parts = line.Split('-');
                if (!map.ContainsKey(parts[0])) map.Add(parts[0], new());
                if (!map.ContainsKey(parts[1])) map.Add(parts[1], new());

                map[parts[0]].Add(parts[1]);
                map[parts[1]].Add(parts[0]);
            }

            HashSet<string> maxConnected = FindMaxConnected(new(), [.. map.Keys], map);

            List<string> sortedGroup = [.. maxConnected];
            sortedGroup.Sort();

            Console.WriteLine(string.Join(',', sortedGroup));
        }

        public HashSet<string> FindMaxConnected(HashSet<string> connected, HashSet<string> remaining, Dictionary<string, HashSet<string>> map)
        {
            HashSet<string> fullyConnected = new();

            foreach(string location in remaining)
            {
                if(IsFullyConnected(connected, location, map))
                {
                    fullyConnected.Add(location);
                }
            }

            HashSet<string> maxResult = [.. connected];

            while(fullyConnected.Count > 0)
            {
                string location = fullyConnected.First();

                fullyConnected.Remove(location);
                connected.Add(location);

                var newResult = FindMaxConnected(connected, fullyConnected, map);
                if(newResult.Count > maxResult.Count) maxResult = newResult;

                connected.Remove(location);
            }

            return maxResult;
        }

        public bool IsFullyConnected(HashSet<string> locations, string newLocation, Dictionary<string, HashSet<string>> map)
        {
            var connections = map[newLocation];
            foreach (string location in locations)
            {
                if (!connections.Contains(location)) return false;
            }

            return true;
        }
    }
}