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

        public List<Statement> statements = new List<Statement>();

        private int readingPointer;

        public Parser(Token[] tokens)
        {
            this._tokens = tokens;

            readingPointer = 0;
        }

        public Statement[] parseTokens()
        {
            while (!eof())
            {
                programStart();
            }

            return statements.ToArray();
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
                    if (tokenType.KeywordType.Equals(KeywordType.Read) || tokenType.KeywordType.Equals(KeywordType.Write) || tokenType.KeywordType.Equals(KeywordType.If))
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
            Token sendToken;
            OperatorToken typeOperator;
            KeywordToken typeKeyword;
            OpenBraceToken typeOpenBrace;
            CloseBraceToken typeCloseBrace;

            token = getNextToken();

            sendToken = getCurrentToken();

            if (matchToken(typeof(IdentifierToken), token))
            {
                token = getNextToken();
                if (matchToken(typeof(OperatorToken), token))
                {
                    typeOperator = (OperatorToken)token;
                    if (typeOperator.OperatorType.Equals(OperatorType.Assignment))
                    {
                        expression(sendToken);
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
                            idList(KeywordType.Read);

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
                            expressionList(sendToken);

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
                else if (typeKeyword.KeywordType.Equals(KeywordType.If))
                {
                    token = getNextToken();

                    if (matchToken(typeof(OpenBraceToken), token))
                    {
                        typeOpenBrace = (OpenBraceToken)token;
                        if (typeOpenBrace.BraceType.Equals(BraceType.Round))
                        {
                            expressionList(sendToken);

                            token = getCurrentToken();
                            if (matchToken(typeof(CloseBraceToken), token))
                            {
                                typeCloseBrace = (CloseBraceToken)token;
                                if (typeCloseBrace.BraceType.Equals(BraceType.Round))
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
                else if (typeKeyword.KeywordType.Equals(KeywordType.Int))
                {
                    idList(KeywordType.Int);
                    token = getCurrentToken();
                    if (matchToken(typeof(StatementSeparatorToken), token))
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

        private void expression(Token t)
        {
            Token token;
            OperatorToken typeOperator;

            primary(t);

            token = getNextToken();

            while (matchToken(typeof(OperatorToken), token))
            {
                typeOperator = (OperatorToken)token;
                if (typeOperator.OperatorType.Equals(OperatorType.Add) || typeOperator.OperatorType.Equals(OperatorType.Substract))
                {
                    addOperation(typeOperator);
                    primary(null);
                    token = getNextToken();
                }
                else if (typeOperator.OperatorType.Equals(OperatorType.Equals) || typeOperator.OperatorType.Equals(OperatorType.NotEquals) ||
                    typeOperator.OperatorType.Equals(OperatorType.GreaterThan) || typeOperator.OperatorType.Equals(OperatorType.LessThan) ||
                    typeOperator.OperatorType.Equals(OperatorType.GreaterEquals) || typeOperator.OperatorType.Equals(OperatorType.LessEquals))
                {
                    compareOperation(typeOperator);
                    primary(null);
                    token = getNextToken();
                }
            }
        }

        private void idList(KeywordType t)
        {
            Token token;
            Statement st = new Statement();

            if (t.Equals(KeywordType.Int))
            {
                st.type = StatementType.DeclarationStatement;
            }
            else if (t.Equals(KeywordType.Read))
            {
                st.type = StatementType.InputStatement;
            }

            token = getNextToken();

            if (matchToken(typeof(IdentifierToken), token))
            {
                st.tokens.Enqueue(token);

                token = getNextToken();
                if (matchToken(typeof(VarSeparatorToken), token))
                {
                    while (matchToken(typeof(VarSeparatorToken), token))
                    {
                        token = getNextToken();
                        if (matchToken(typeof(IdentifierToken), token))
                        {
                            st.tokens.Enqueue(token);
                            token = getNextToken();
                        }
                        else
                        {
                            // error
                        }
                    }

                    statements.Add(st);
                }
                else
                {
                    //error
                }
                
            }
        }

        private void expressionList(Token t)
        {
            Token token;

            expression(t);

            token = getCurrentToken();

            while (matchToken(typeof(VarSeparatorToken), token))
            {
                expression(t);

                token = getCurrentToken();
            }

        }

        private void primary(Token t)
        {
            Token token;
            OpenBraceToken typeOpenBrace;
            CloseBraceToken typeCloseBrace;

            Statement st;
            
            if(t == null){
                st = statements.LastOrDefault();
            }
            else
            {
                st = new Statement();
            }
            

            token = getNextToken();

            if (matchToken(typeof(OpenBraceToken), token))
            {
                typeOpenBrace = (OpenBraceToken)token;
                if (typeOpenBrace.BraceType.Equals(BraceType.Round))
                {
                    expression(null);

                    token = getCurrentToken();
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
            else if (matchToken(typeof(IdentifierToken), token))
            {
                if (t == null)
                {
                    st.tokens.Enqueue(token);
                }
                else
                {
                    if (t.Content == "write")
                    {
                        st.type = StatementType.OutputStatemnt;
                        st.tokens.Enqueue(token);
                    }
                    else if (t.Content == "if")
                    {
                        st.type = StatementType.IfStatement;
                        st.tokens.Enqueue(token);
                    }
                    else
                    {
                        st.tokens.Enqueue(t);
                        st.tokens.Enqueue(token);
                    }
                    statements.Add(st);
                }
                return;
            }
            else if (matchToken(typeof(NumberLiteralToken), token))
            {
                if (t == null)
                {
                    st.tokens.Enqueue(token);
                }
                else
                {
                    st.type = StatementType.AssignmentStatement;

                    st.tokens.Enqueue(t);
                    st.tokens.Enqueue(token);
                    statements.Add(st);
                }
                
                return;
            }
            else
            {
                // error
            }
        }

        private void addOperation(Token t)
        {
            Statement st = statements.LastOrDefault();
            st.tokens.Enqueue(t);
            if (!st.type.Equals(StatementType.OutputStatemnt))
            {
                st.type = StatementType.OperationStatement;
            }
        }

        private void compareOperation(Token t)
        {
            Statement st = statements.LastOrDefault();
            st.tokens.Enqueue(t);
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
