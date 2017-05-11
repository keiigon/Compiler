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
            string code = @"begin
                            int a, b, c, z;
                            c = 5;
                            read(a, b);
                            z = a + b + c;
                            write(z, a);
                            end";

            Scanner sc = new Scanner(code);

            Token[] tokens = sc.CreateTokens();

            foreach(var t in tokens){
                System.Console.WriteLine(t);
            }

            Parser ps = new Parser(tokens);

            ps.parseTokens();

            System.Console.ReadLine();
        }
    }
}
