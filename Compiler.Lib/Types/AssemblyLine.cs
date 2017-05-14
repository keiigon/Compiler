using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lib.Types
{
    public class AssemblyLine
    {
        public string label { set; get; }
        public AssemblyInstructions code { set; get; }
        public string operand { set; get; }

        public override string ToString()
        {
            StringBuilder st = new StringBuilder();

            st.Append(string.IsNullOrEmpty(this.label) ? "" : this.label + ", ");
            st.Append(this.code.Equals(AssemblyInstructions.None) ? "" : this.code + " ");
            st.Append(string.IsNullOrEmpty(this.operand) ? "" : this.operand);

            return st.ToString();
        }


        //comment
    }
}
