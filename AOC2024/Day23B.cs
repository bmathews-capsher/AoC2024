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

            HashSet<string> allNodes = new();
            allNodes.UnionWith(map.Keys);
            HashSet<string> maxConnected = FindMaxConnected(new(), allNodes, map);

            List<string> sortedGroup = new();
            sortedGroup.AddRange(maxConnected);
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

            HashSet<string> newConnected = new();
            newConnected.UnionWith(connected);

            HashSet<string> newRemaining = new();
            newRemaining.UnionWith(fullyConnected);

            HashSet<string> maxResult = new();
            maxResult.UnionWith(connected);

            foreach (string location in fullyConnected)
            {
                newConnected.Add(location);
                newRemaining.Remove(location);

                var newResult = FindMaxConnected(newConnected, newRemaining, map);
                if(newResult.Count > maxResult.Count) maxResult = newResult;

                newConnected.Remove(location);
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