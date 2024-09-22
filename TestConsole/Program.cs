

using EleCho.Compiling;
using EleCho.Yaml.Parsing;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var yamlParser = new YamlParser(
                """
                [ wefjoi, iofej, feiowf ]
                """.AsMemory(), default);

            while(true)
            {
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);

                Console.Clear();
                var hasMore = yamlParser.Do();
                foreach (var syntax in yamlParser.Result)
                {
                    Console.WriteLine(syntax);
                }

                if (!hasMore)
                {
                    break;
                }
            }

            while (true)
            {
                Console.ReadKey(true);
            }
        }
    }
}
