using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lib.Types
{
    public class KeywordToken : Token
    {
        private static readonly Dictionary<string, KeywordType> validKeywords = new Dictionary<string, KeywordType>()
        {
            { "begin", KeywordType.Begin },
            { "end", KeywordType.End },
            { "read", KeywordType.Read },
            { "write", KeywordType.Write },
            { "while", KeywordType.While },
            { "if", KeywordType.If },
            { "int", KeywordType.Int },
        };

        public KeywordType KeywordType { get; private set; }

        public KeywordToken(string content)
            : base(content)
        {
            KeywordType = validKeywords[content];
        }

        public static bool IsKeyword(string s)
        {
            return validKeywords.ContainsKey(s);
        }

    }
}
