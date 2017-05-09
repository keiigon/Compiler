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

            token = getToken();

            if (matchToken(typeof(KeywordToken), token))
            {
                tokenType = (KeywordToken)token;

                if (tokenType.KeywordType.Equals(KeywordType.Begin))
                {
                    statementList();
                    token = getToken();
                    if (matchToken(typeof(KeywordToken), token))
                    {
                        tokenType = (KeywordToken)token;
                        if (tokenType.KeywordType.Equals(KeywordType.End))
                        { 
                        
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
                token = getToken();

                if (matchToken(typeof(IdentifierToken), token))
                {
                    statement();
                }
                else if (matchToken(typeof(KeywordToken), token))
                {
                    tokenType = (KeywordToken)token;
                    if (tokenType.KeywordType.Equals(KeywordType.Read) || tokenType.KeywordType.Equals(KeywordType.Write))
                    {
                        statement();
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

            token = getToken();

            if (matchToken(typeof(IdentifierToken), token))
            {
                token = getToken();
                if (matchToken(typeof(OperatorToken), token))
                {
                    typeOperator = (OperatorToken)token;
                    if (typeOperator.OperatorType.Equals(OperatorType.Assignment))
                    {
                        expression();
                        token = getToken();
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
                    token = getToken();
                    if (matchToken(typeof(OpenBraceToken), token))
                    {
                        typeOpenBrace = (OpenBraceToken)token;
                        if (typeOpenBrace.BraceType.Equals(BraceType.Round))
                        {
                            idList();

                            token = getToken();
                            if (matchToken(typeof(CloseBraceToken), token))
                            {
                                typeCloseBrace = (CloseBraceToken)token;
                                if (typeCloseBrace.BraceType.Equals(BraceType.Round))
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
                else if (typeKeyword.KeywordType.Equals(KeywordType.Write))
                {
                    token = getToken();
                    if (matchToken(typeof(OpenBraceToken), token))
                    {
                        typeOpenBrace = (OpenBraceToken)token;
                        if (typeOpenBrace.BraceType.Equals(BraceType.Round))
                        {
                            expressionList();

                            token = getToken();
                            if (matchToken(typeof(CloseBraceToken), token))
                            {
                                typeCloseBrace = (CloseBraceToken)token;
                                if (typeCloseBrace.BraceType.Equals(BraceType.Round))
                                {
                                    return;
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

        }

        private void idList()
        {
            Token token;

            token = getToken();

            if (matchToken(typeof(IdentifierToken), token))
            {
                token = getToken();
                if (matchToken(typeof(VarSeparatorToken), token))
                {
                    while (matchToken(typeof(VarSeparatorToken), token))
                    {
                        token = getToken();
                        if (matchToken(typeof(IdentifierToken), token))
                        {
                            token = getToken();
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

        }

        private bool matchToken(Type type, Token token)
        {
            if (token.GetType() == type)
            {
                return true;
            }
            return false;
        }

        private Token getToken()
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
