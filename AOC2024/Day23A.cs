using System.Reflection.Emit;

namespace AOC2024
{
    public class Day23A
    {

        public void Solve(List<string> data)
        {
            Dictionary<string, List<string>> map = new();

            foreach(string line in data)
            {
                string[] parts = line.Split('-');
                if (!map.ContainsKey(parts[0])) map.Add(parts[0], new());
                if (!map.ContainsKey(parts[1])) map.Add(parts[1], new());

                map[parts[0]].Add(parts[1]);
                map[parts[1]].Add(parts[0]);
            }   

            HashSet<string> visitedA = new();

            int count = 0;

            foreach(var locationA in map.Keys)
            {
                if (visitedA.Contains(locationA)) continue;

                visitedA.Add(locationA);

                HashSet<string> visitedB = new();
                foreach (string locationB in map[locationA])
                {
                    if(visitedA.Contains(locationB)) continue;
                    if(visitedB.Contains(locationB)) continue;

                    visitedB.Add(locationB);

                    foreach(var locationC in map[locationB])
                    {
                        if (visitedA.Contains(locationC)) continue;
                        if (visitedB.Contains(locationC)) continue;

                        foreach (var locationD in map[locationC])
                        {
                            if(locationD != locationA) continue;
                            if(locationA[0] == 't' || locationB[0] == 't' || locationC[0] == 't')
                            {
                                count++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine(count);
        }
    }
}