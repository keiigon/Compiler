using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lib.Types
{
    public enum AssemblyInstructions
    {
        None,
        Load,
        Store,
        Add,
        Subt,
        Input,
        Output,
        Halt,
        Skipcond,
        Jump,
        Clear,
        AddI,
        JumpI,
        JNS

    }
}
