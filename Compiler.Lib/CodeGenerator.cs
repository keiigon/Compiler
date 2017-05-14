using Compiler.Lib.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lib
{
    public class CodeGenerator
    {
        private Stack<AssemblyLine> assembly = new Stack<AssemblyLine>();

        private Stack<AssemblyLine> locations = new Stack<AssemblyLine>();

        private Stack<AssemblyLine> instructions = new Stack<AssemblyLine>();

        Stack<Statement> sts;

        private int tempCount = 1;

        string optionLabel = "";

        public Stack<AssemblyLine> assemble(Statement[] statements)
        {
            sts = new Stack<Statement>(statements.AsEnumerable<Statement>().Reverse());

            while (sts.Count > 0)
            {
                var s = sts.Pop();

                if (s.type.Equals(StatementType.DeclarationStatement))
                {
                    declarationHandle(s);
                }
                else if (s.type.Equals(StatementType.AssignmentStatement))
                {
                    assignmentHandle(s);
                }
                else if (s.type.Equals(StatementType.OperationStatement))
                {
                    operationHandle(s);
                }
                else if (s.type.Equals(StatementType.InputStatement))
                {
                    inputHandle(s);
                }
                else if (s.type.Equals(StatementType.OutputStatemnt))
                {
                    outputHandle(s);
                }
                else if (s.type.Equals(StatementType.IfStatement))
                {
                    ifHandle(s);
                }
            }

            AssemblyLine line = new AssemblyLine()
            {
                label = "end",
                code = AssemblyInstructions.Halt,
            };

            instructions.Push(line);

            arrange();

            return assembly;
        }

        private void arrange()
        {
            while (locations.Count > 0)
            {
                assembly.Push(locations.Pop());
            }

            while (instructions.Count > 0)
            {
                assembly.Push(instructions.Pop());
            }
        }

        private void declarationHandle(Statement s)
        {
            while (s.tokens.Count > 0)
            {
                AssemblyLine line = new AssemblyLine()
                {
                    label = s.tokens.Dequeue().Content,
                    operand = "Dec 0"
                };

                locations.Push(line);
            }

        }

        private void assignmentHandle(Statement s)
        {
            AssemblyLine line1 = new AssemblyLine()
            {
                code = AssemblyInstructions.Store,
                operand = s.tokens.Dequeue().Content
            };

            Token value = s.tokens.Dequeue();

            int num = 0;

            bool isInt = int.TryParse(value.Content, out num);

            if (isInt)
            {
                string varName = "temp_" + tempCount;
                tempCount++;

                AssemblyLine line2 = new AssemblyLine()
                {
                    label = varName,
                    operand = "Dec " + value.Content
                };

                locations.Push(line2);

                AssemblyLine line3 = new AssemblyLine()
                {
                    label = optionLabel,
                    code = AssemblyInstructions.Load,
                    operand = varName
                };

                instructions.Push(line3);
                instructions.Push(line1);
            }
            else
            {
                AssemblyLine line3 = new AssemblyLine()
                {
                    code = AssemblyInstructions.Load,
                    operand = value.Content
                };

                instructions.Push(line3);
                instructions.Push(line1);
            }

            optionLabel = "";
        }

        private void operationHandle(Statement s)
        {
            AssemblyLine line1 = new AssemblyLine()
            {
                code = AssemblyInstructions.Store,
                operand = s.tokens.Dequeue().Content
            };

            while (s.tokens.Count > 0)
            {
                Token t = s.tokens.Dequeue();

                if (t.Content == "+")
                {
                    Token value = s.tokens.Dequeue();

                    string varName = "";

                    int num = 0;

                    bool isInt = int.TryParse(value.Content, out num);

                    if (isInt)
                    {
                        varName = "temp_" + tempCount;
                        tempCount++;

                        AssemblyLine line2 = new AssemblyLine()
                        {
                            label = varName,
                            operand = "Dec " + value.Content
                        };

                        locations.Push(line2);
                    }
                    else
                    {
                        varName = value.Content;
                    }

                    AssemblyLine line4 = new AssemblyLine()
                    {
                        code = AssemblyInstructions.Add,
                        operand = varName
                    };

                    instructions.Push(line4);
                }
                else if (t.Content == "-")
                {
                    Token value = s.tokens.Dequeue();

                    string varName = "";

                    int num = 0;

                    bool isInt = int.TryParse(value.Content, out num);

                    if (isInt)
                    {
                        varName = "temp_" + tempCount;
                        tempCount++;

                        AssemblyLine line2 = new AssemblyLine()
                        {
                            label = varName,
                            operand = "Dec " + value.Content
                        };

                        locations.Push(line2);
                    }
                    else
                    {
                        varName = value.Content;
                    }

                    AssemblyLine line4 = new AssemblyLine()
                    {
                        code = AssemblyInstructions.Subt,
                        operand = varName
                    };

                    instructions.Push(line4);
                }
                else
                {
                    AssemblyLine line2 = new AssemblyLine()
                    {
                        label = optionLabel,
                        code = AssemblyInstructions.Load,
                        operand = t.Content
                    };

                    instructions.Push(line2);
                }
            }

            instructions.Push(line1);

            optionLabel = "";
        }

        private void inputHandle(Statement s)
        {
            while (s.tokens.Count > 0)
            {
                AssemblyLine line1 = new AssemblyLine()
                {
                    code = AssemblyInstructions.Input,
                };

                AssemblyLine line2 = new AssemblyLine()
                {
                    code = AssemblyInstructions.Store,
                    operand = s.tokens.Dequeue().Content
                };

                instructions.Push(line1);
                instructions.Push(line2);
            }
        }

        private void outputHandle(Statement s)
        {
            if (s.tokens.Count == 1)
            {
                AssemblyLine line1 = new AssemblyLine()
                {
                    code = AssemblyInstructions.Load,
                    operand = s.tokens.Dequeue().Content
                };

                AssemblyLine line2 = new AssemblyLine()
                {
                    code = AssemblyInstructions.Output,
                };

                instructions.Push(line1);
                instructions.Push(line2);
            }
            else if (s.tokens.Count > 1)
            {
                while (s.tokens.Count > 0)
                {
                    Token t = s.tokens.Dequeue();
                    if (t.Content == "+")
                    {
                        AssemblyLine line1 = new AssemblyLine()
                        {
                            code = AssemblyInstructions.Add,
                            operand = s.tokens.Dequeue().Content
                        };
                        instructions.Push(line1);
                    }
                    else if (t.Content == "-")
                    {
                        AssemblyLine line1 = new AssemblyLine()
                        {
                            code = AssemblyInstructions.Subt,
                            operand = s.tokens.Dequeue().Content
                        };
                        instructions.Push(line1);
                    }
                    else
                    {
                        AssemblyLine line1 = new AssemblyLine()
                        {
                            code = AssemblyInstructions.Load,
                            operand = t.Content
                        };

                        instructions.Push(line1);
                    }
                }

                AssemblyLine line2 = new AssemblyLine()
                {
                    code = AssemblyInstructions.Output
                };

                instructions.Push(line2);
                

            }
        }

        private void ifHandle(Statement s)
        {
            Token t = s.tokens.Dequeue();

            string varName = "";

            int num = 0;

            bool isInt = int.TryParse(t.Content, out num);

            if (isInt)
            {
                varName = "temp_" + tempCount;
                tempCount++;

                AssemblyLine line2 = new AssemblyLine()
                {
                    label = varName,
                    operand = "Dec " + t.Content
                };

                locations.Push(line2);
            }
            else
            {
                varName = t.Content;
            }

            AssemblyLine line1 = new AssemblyLine()
            {
                code = AssemblyInstructions.Load,
                operand = varName
            };

            instructions.Push(line1);

            Token compareOperator = s.tokens.Dequeue();

            t = s.tokens.Dequeue();

            num = 0;

            isInt = int.TryParse(t.Content, out num);

            if (isInt)
            {
                varName = "temp_" + tempCount;
                tempCount++;

                AssemblyLine line2 = new AssemblyLine()
                {
                    label = varName,
                    operand = "Dec " + t.Content
                };

                locations.Push(line2);
            }
            else
            {
                varName = t.Content;
            }

            line1 = new AssemblyLine()
            {
                code = AssemblyInstructions.Subt,
                operand = varName
            };

            instructions.Push(line1);

            if (compareOperator.Content == "==" || compareOperator.Content == "!=")
            {
                AssemblyLine skip = new AssemblyLine()
                {
                    code = AssemblyInstructions.Skipcond,
                    operand = "400"
                };

                instructions.Push(skip);

                if (compareOperator.Content == "==")
                {
                    AssemblyLine jump = new AssemblyLine()
                    {
                        code = AssemblyInstructions.Jump,
                        operand = "end"
                    };

                    instructions.Push(jump);
                }
                else
                {
                    optionLabel = "if";

                    AssemblyLine jump = new AssemblyLine()
                    {
                        code = AssemblyInstructions.Jump,
                        operand = "if"
                    };

                    instructions.Push(jump);

                    jump = new AssemblyLine()
                    {
                        code = AssemblyInstructions.Jump,
                        operand = "end"
                    };

                    instructions.Push(jump);
                }
            }
            else if (compareOperator.Content == ">" || compareOperator.Content == "<=")
            {
                AssemblyLine skip = new AssemblyLine()
                {
                    code = AssemblyInstructions.Skipcond,
                    operand = "800"
                };

                instructions.Push(skip);

                if (compareOperator.Content == ">")
                {
                    AssemblyLine jump = new AssemblyLine()
                    {
                        code = AssemblyInstructions.Jump,
                        operand = "end"
                    };

                    instructions.Push(jump);
                }
                else
                {
                    optionLabel = "if";

                    AssemblyLine jump = new AssemblyLine()
                    {
                        code = AssemblyInstructions.Jump,
                        operand = "if"
                    };

                    instructions.Push(jump);

                    jump = new AssemblyLine()
                    {
                        code = AssemblyInstructions.Jump,
                        operand = "end"
                    };

                    instructions.Push(jump);
                }
            }
            else if (compareOperator.Content == "<" || compareOperator.Content == ">=")
            {
                AssemblyLine skip = new AssemblyLine()
                {
                    code = AssemblyInstructions.Skipcond,
                    operand = "000"
                };

                instructions.Push(skip);

                if (compareOperator.Content == "<")
                {
                    AssemblyLine jump = new AssemblyLine()
                    {
                        code = AssemblyInstructions.Jump,
                        operand = "end"
                    };

                    instructions.Push(jump);
                }
                else
                {
                    optionLabel = "if";

                    AssemblyLine jump = new AssemblyLine()
                    {
                        code = AssemblyInstructions.Jump,
                        operand = "if"
                    };

                    instructions.Push(jump);

                    jump = new AssemblyLine()
                    {
                        code = AssemblyInstructions.Jump,
                        operand = "end"
                    };

                    instructions.Push(jump);
                }
            }

        }
    }
}
