namespace AOC2024
{
        public class Day08A
        {

                public void Solve(List<string> data)
                {
                        Dictionary<char, List<(int i, int j)>> antennas = new();

                        for(int i = 0; i < data.Count; i++)
                        {
                                for(int j = 0; j < data[i].Length; j++)
                                {
                                        if(data[i][j] == '.') continue;

                                        if(!antennas.ContainsKey(data[i][j]))
                                        {
                                                antennas.Add(data[i][j], new());
                                        }

                                        antennas[data[i][j]].Add((i, j));
                                }
                        }

                        HashSet<(int i, int j)> antinodes = new();

                        foreach(List<(int i, int j)> positions in antennas.Values)
                        {
                                for(int a = 0; a < positions.Count; a++)
                                {
                                        for(int b = a+1; b < positions.Count; b++)
                                        {
                                                (int i, int j) posA = positions[a];
                                                (int i, int j) posB = positions[b];

                                                int yDiff = posA.i - posB.i;
                                                int xDiff = posA.j - posB.j;

                                                antinodes.Add((posA.i + yDiff, posA.j + xDiff));
                                                antinodes.Add((posB.i - yDiff, posB.j - xDiff));
                                        }
                                }
                        }

                        int count = 0;

                        foreach((int i, int j) pos in antinodes)
                        {
                                if(InBounds(data, pos)) count++;
                        }

                        Console.WriteLine(count);
                }

                private bool InBounds(List<string> data, (int i, int j) position)
                {
                        return position.i >= 0 && position.j >= 0 && position.i < data.Count && position.j < data[position.i].Length;
                }
        }
}