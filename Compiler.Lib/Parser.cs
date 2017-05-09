using Compiler.Lib.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lib
{
    public class Parser
    {
        public Token[] _tokens { get; private set; }

        private int readingPointer;

        public Parser(Token[] tokens)
        {
            this._tokens = tokens;

            readingPointer = 0;
        }

        public void parseTokens(){
            while (!eof())
            {
                programStart();
            }
        }

        private void programStart(){
            Token token;
            KeywordToken tokenType;

            token = getNextToken();

            if (matchToken(typeof(KeywordToken), token))
            {
                tokenType = (KeywordToken)token;

                if (tokenType.KeywordType.Equals(KeywordType.Begin))
                {
                    statementList();
                    token = getCurrentToken();
                    if (matchToken(typeof(KeywordToken), token))
                    {
                        tokenType = (KeywordToken)token;
                        if (tokenType.KeywordType.Equals(KeywordType.End))
                        {
                            return;
                        }
                        else
                        {
                            //error
                        }
                    }
                    else
                    {
                        //error
                    }
                }
                else
                {
                    //error
                }
            }
            else
            {
                //error
            }
        }

        private void statementList()
        {
            Token token;
            KeywordToken tokenType;

            statement();

            while (true)
            {
                token = getNextToken();

                if (matchToken(typeof(IdentifierToken), token))
                {
                    readingPointer--;
                    statement();
                }
                else if (matchToken(typeof(KeywordToken), token))
                {
                    tokenType = (KeywordToken)token;
                    if (tokenType.KeywordType.Equals(KeywordType.Read) || tokenType.KeywordType.Equals(KeywordType.Write))
                    {
                        readingPointer--;
                        statement();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void statement()
        {
            Token token;
            OperatorToken typeOperator;
            KeywordToken typeKeyword;
            OpenBraceToken typeOpenBrace;
            CloseBraceToken typeCloseBrace;

            token = getNextToken();

            if (matchToken(typeof(IdentifierToken), token))
            {
                token = getNextToken();
                if (matchToken(typeof(OperatorToken), token))
                {
                    typeOperator = (OperatorToken)token;
                    if (typeOperator.OperatorType.Equals(OperatorType.Assignment))
                    {
                        expression();
                        token = getCurrentToken();
                        if (matchToken(typeof(StatementSeparatorToken), token))
                        {
                            return;
                        }
                    }
                }
            }
            else if (matchToken(typeof(KeywordToken), token))
            {
                typeKeyword = (KeywordToken)token;
                if (typeKeyword.KeywordType.Equals(KeywordType.Read))
                {
                    token = getNextToken();
                    if (matchToken(typeof(OpenBraceToken), token))
                    {
                        typeOpenBrace = (OpenBraceToken)token;
                        if (typeOpenBrace.BraceType.Equals(BraceType.Round))
                        {
                            idList();

                            token = getCurrentToken();
                            if (matchToken(typeof(CloseBraceToken), token))
                            {
                                typeCloseBrace = (CloseBraceToken)token;
                                if (typeCloseBrace.BraceType.Equals(BraceType.Round))
                                {
                                    token = getNextToken();
                                    if (matchToken(typeof(StatementSeparatorToken), token))
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        //error
                                    }
                                    
                                }
                            }
                        }
                    }
                }
                else if (typeKeyword.KeywordType.Equals(KeywordType.Write))
                {
                    token = getNextToken();
                    if (matchToken(typeof(OpenBraceToken), token))
                    {
                        typeOpenBrace = (OpenBraceToken)token;
                        if (typeOpenBrace.BraceType.Equals(BraceType.Round))
                        {
                            expressionList();

                            token = getCurrentToken();
                            if (matchToken(typeof(CloseBraceToken), token))
                            {
                                typeCloseBrace = (CloseBraceToken)token;
                                if (typeCloseBrace.BraceType.Equals(BraceType.Round))
                                {
                                    token = getNextToken();
                                    if (matchToken(typeof(StatementSeparatorToken), token))
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        //error
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    //error
                }
            }
            else
            {
                //error
            }
        }

        private void expression()
        {
            Token token;
            OperatorToken typeOperator;

            primary();

            token = getNextToken();

            while (matchToken(typeof(OperatorToken), token))
            {
                typeOperator = (OperatorToken)token;
                if (typeOperator.OperatorType.Equals(OperatorType.Add) || typeOperator.OperatorType.Equals(OperatorType.Substract))
                {
                    addOperation();
                    primary();
                    token = getNextToken();
                }
            }
        }

        private void idList()
        {
            Token token;

            token = getNextToken();

            if (matchToken(typeof(IdentifierToken), token))
            {
                token = getNextToken();
                if (matchToken(typeof(VarSeparatorToken), token))
                {
                    while (matchToken(typeof(VarSeparatorToken), token))
                    {
                        token = getNextToken();
                        if (matchToken(typeof(IdentifierToken), token))
                        {
                            token = getNextToken();
                        }
                        else
                        {
                            // error
                        }
                    }
                }
                else
                {
                    //error
                }
                
            }
        }

        private void expressionList()
        {
            Token token;

            expression();

            token = getCurrentToken();

            while (matchToken(typeof(VarSeparatorToken), token))
            {
                expression();

                token = getCurrentToken();
            }

        }

        private void primary()
        {
            Token token;
            OpenBraceToken typeOpenBrace;
            CloseBraceToken typeCloseBrace;

            token = getNextToken();

            if (matchToken(typeof(OpenBraceToken), token))
            {
                typeOpenBrace = (OpenBraceToken)token;
                if (typeOpenBrace.BraceType.Equals(BraceType.Round))
                {
                    expression();

                    token = getCurrentToken();
                    if (matchToken(typeof(CloseBraceToken), token))
                    {
                        typeCloseBrace = (CloseBraceToken)token;
                        if (typeCloseBrace.BraceType.Equals(BraceType.Round))
                        {
                            token = getNextToken();
                            if (matchToken(typeof(StatementSeparatorToken), token))
                            {
                                return;
                            }
                            else
                            {
                                //error
                            }
                        }
                    }
                }
            }
            else if (matchToken(typeof(IdentifierToken), token))
            {
                return;
            }
            else if (matchToken(typeof(NumberLiteralToken), token))
            {
                return;
            }
            else
            {
                // error
            }
        }

        private void addOperation()
        {

        }

        private bool matchToken(Type type, Token token)
        {
            if (token.GetType() == type)
            {
                return true;
            }
            return false;
        }

        private Token getCurrentToken()
        {
            Token t = _tokens[readingPointer - 1];
            return t;
        }

        private Token getNextToken()
        {
            Token t = _tokens[readingPointer];
            readingPointer++;
            return t;
        }

        private bool eof()
        {
            return readingPointer >= _tokens.Length;
        }
    }


}
