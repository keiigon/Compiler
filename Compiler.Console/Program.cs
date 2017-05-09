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
                            read(a, b, c);
                            end";

            Scanner sc = new Scanner(code);

            Token[] tokens = sc.CreateTokens();

            Parser ps = new Parser(tokens);

            ps.parseTokens();
        }
    }
}
