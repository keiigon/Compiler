using Compiler.Lib.Types;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lib
{
    public class Scanner
    {
        public string Code { get; private set; }

        private int readingPointer;

        public Scanner(string code)
        {
            this.Code = code;

            readingPointer = 0;
        }

        public Token[] CreateTokens()
        {
            var tokens = new List<Token>();

            var builder = new StringBuilder();

            return tokens.ToArray();
        }

        private void readToken(StringBuilder builder, CharType typeToRead)
        {
            while (!eof() && lookNextCharType().Equals(typeToRead))
                builder.Append(getNextChar());
        }

        private void skip(CharType typeToSkip)
        {
            while (lookNextCharType().Equals(typeToSkip))
                getNextChar();
        }

        private CharType lookNextCharType()
        {
            return charTypeOf(lookNextChar());
        }

        private CharType getNextCharType()
        {
            return charTypeOf(getNextChar());
        }

        private CharType charTypeOf(char c)
        {
            switch (c)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                case '%':
                case '&':
                case '|':
                case '=':
                    return CharType.Operator;
                case '(':
                case '[':
                case '{':
                    return CharType.OpenBrace;
                case ')':
                case ']':
                case '}':
                    return CharType.CloseBrace;
                case ',':
                    return CharType.VarSeperator;
                case ';':
                    return CharType.StatementSeperator;
                case '\r':
                case '\n':
                    return CharType.NewLine;
            }

            switch (char.GetUnicodeCategory(c))
            {
                case UnicodeCategory.DecimalDigitNumber:
                    return CharType.Numeric;
                case UnicodeCategory.LineSeparator:
                    return CharType.NewLine;
                case UnicodeCategory.ParagraphSeparator:
                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.OtherLetter:
                case UnicodeCategory.UppercaseLetter:
                    return CharType.Alpha;
                case UnicodeCategory.SpaceSeparator:
                    return CharType.Space;
            }

            return CharType.Unknown;
        }

        private char lookNextChar()
        {
            //TODO: Check for eof()
            return Code[readingPointer];
        }

        private char getNextChar()
        {
            var ret = lookNextChar();
            readingPointer++;
            return ret;
        }

        private bool eof()
        {
            return readingPointer >= Code.Length;
        }


    }
}
