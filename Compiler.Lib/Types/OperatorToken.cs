using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lib.Types
{
    public class OperatorToken : Token
    {
        static readonly Dictionary<string, OperatorType> validOperators = new Dictionary<string, OperatorType>()
        {
            { "+", OperatorType.Add },
            { "-", OperatorType.Substract },
            { "=", OperatorType.Assignment },
            { "==", OperatorType.Equals },
            { ">=", OperatorType.GreaterEquals },
            { ">", OperatorType.GreaterThan },
            { "<=", OperatorType.LessEquals },
            { "<", OperatorType.LessThan },
            { "!=", OperatorType.NotEquals },
            
        };

        public OperatorType OperatorType { get; private set; }

        public OperatorToken(string content)
            : base(content)
        {
            OperatorType = validOperators[content];
        }
    }
}
