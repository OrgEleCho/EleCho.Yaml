using EleCho.Yaml.Parsing;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tokens = YamlLexer.Lex(
                """
                abc: &abc 123
                qwe:
                  a: 123
                  b: 456
                """).ToArray();

            Console.WriteLine(string.Join(",\r\n", tokens));
        }
    }
}
