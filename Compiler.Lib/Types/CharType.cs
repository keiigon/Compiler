using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lib.Types
{
    enum CharType
    {
        Unknown = 0,
        Alpha = 1,
        Numeric = 2,
        Space = 3,
        NewLine = 4,
        Operator = 5,
        OpenBrace = 6,
        CloseBrace = 7,
        VarSeperator = 8,
        StatementSeperator = 9,
    }
}
