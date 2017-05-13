using Compiler.Lib;
using Compiler.Lib.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Console
{
    class Program
    {
        static void Main(string[] args)
        {
//            string code = @"begin
//int a, b, c, z;
//c = 5;
//read(a, b);
//z = a + (b - c) + (5 - 4);
//write(z, a + b);
//end";

//            string code = @"begin
//                            write(z, a);
//                            end";
            string code = @"begin
                            a = 5;
                            b = 6;
                            if(a != b)
                            b = 55;
                            end";

            Scanner sc = new Scanner(code);

            Token[] tokens = sc.CreateTokens();

            foreach(var t in tokens){
                System.Console.WriteLine(t);
            }

            Parser ps = new Parser(tokens);

            Statement[] statements = ps.parseTokens();

            System.Console.WriteLine();

            foreach (var s in statements)
            {
                System.Console.WriteLine(s);
            }

            CodeGenerator cg = new CodeGenerator();

            Stack<AssemblyLine> ass = cg.assemble(statements);

            System.Console.WriteLine();

            while (ass.Count > 0)
            {
                System.Console.WriteLine(ass.Pop());
            }

            System.Console.ReadLine();
        }
    }
}
