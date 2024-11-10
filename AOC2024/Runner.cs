using System.Reflection;

namespace AOC2024
{
        public class Runner
        {
                public static void Run(string day, bool firstPart, string testFile)
                {
                        Assembly assembly = Assembly.GetExecutingAssembly();
                        string part = firstPart ? "A" : "B";
                        string typeName = "AOC2024.Day" + day + part;
                        object dayInstance = assembly.CreateInstance(typeName);

                        List<string> data = new(File.ReadAllLines("Input\\Day" + day + testFile + ".txt"));
                        
                        MethodInfo m = assembly.GetType(typeName).GetMethod("Solve");
                        m.Invoke(dayInstance, new Object[] { data });
                }
        }
}