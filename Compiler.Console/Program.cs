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
            string code = @"int a = 5;
                            int b = 4;
                            int c = a + b";

            Scanner sc = new Scanner(code);

            Token[] tokens = sc.CreateTokens();
        }
    }
}
