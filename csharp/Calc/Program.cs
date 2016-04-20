using System.IO;
using Antlr4.Runtime;

namespace Calc
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = null;
            if (args.Length > 0) inputFile = args[0];
            var istream = System.Console.In;
            if (inputFile != null) istream = File.OpenText(inputFile);

            var input = new AntlrInputStream(istream);
            var lexer = new LabeledExprLexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new LabeledExprParser(tokens);
            var tree = parser.prog();

            var eval = new EvalVisitor();
            eval.Visit(tree);
        }
    }
}
