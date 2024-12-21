using System.Reflection.Emit;

namespace AOC2024
{
    public class Day19A
    {
        private class TrieNode
        {
            public Dictionary<char, TrieNode> Children = new Dictionary<char, TrieNode>();
            public bool IsWord;

            public void AddWord(string word)
            {
                TrieNode current = this;

                foreach(char c in word)
                {
                    if(!current.Children.ContainsKey(c)) current.Children.Add(c, new TrieNode());
                    current = current.Children[c];
                }

                current.IsWord = true;
            }

            public List<int> FindWords(string word, int index)
            {
                List<int> result = new();

                TrieNode current = this;
                for(int i = index; i < word.Length; i++)
                {
                    char c = word[i];
                    if (!current.Children.ContainsKey(c)) break;
                    current = current.Children[c];

                    if(current.IsWord) result.Add(i+1);
                }

                return result;
            }
        }

        public void Solve(List<string> data)
        {
            TrieNode root = new();

            string[] patternsData = data[0].Split(", ");

            foreach(string pattern in patternsData)
            {
                root.AddWord(pattern);
            }

            int count = 0;
            for(int i = 2; i < data.Count; i++)
            {
                string design = data[i];
                Dictionary<int, bool> cache = new();
                if(Search(design, 0, root, cache)) count ++;
            }

            Console.WriteLine(count);
        }

        private bool Search(string design, int index, TrieNode root, Dictionary<int, bool> cache)
        {
            if(cache.ContainsKey(index)) return cache[index];
            if(index == design.Length) return true;

            List<int> parts = root.FindWords(design, index);
            parts.Sort((a, b) => b.CompareTo(a));

            foreach (int part in parts)
            {
                if(Search(design, part, root, cache)) 
                {
                    cache.Add(index, true);
                    return true;
                }
            }

            cache.Add(index, false);
            return false;
        }
    }
}