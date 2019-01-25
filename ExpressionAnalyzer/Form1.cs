using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpressionAnalyzer
{
    public enum OP
    {
        AND,
        OR
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Çalculate(object sender, EventArgs e)
        {
            string input = inputTxt.Text.ToUpper();
            Stack<string> stack = new Stack<string>();

            string[] splitted = input.Split(' ');

            for (int i = 0; i < splitted.Length; ++i)
            {
                string value = splitted[i];

                if (value.Equals(")"))
                {
                    List<string> data = new List<string>();

                    while (true)
                    {
                        var value2 = stack.Pop();
                        
                        if (value2.Equals("("))
                        {
                            data.Reverse(); //because the data pushed in is in wrong order
                            var r = Calc(data.ToArray());
                            //push the result back into the stack
                            stack.Push(r ? "T" : "F");
                            break;
                        }
                        else
                        {
                            data.Add(value2);
                        }
                    }
                }
                else
                    stack.Push(splitted[i]);
            }
            
            //calculate the final statement. At this point there is no more brackets inside
            bool result = Calc(stack.ToArray());

            MessageBox.Show(result ? "T" : "F");
        }
        
        /// <summary>
        /// expressions passed inside will be evaluated from left to right, 
        /// regardless of precedence
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        bool Calc(params string[] exp)
        {
            bool result = exp[0].Equals("T") ? true : false;
            for (int i = 1; i < exp.Length; i+=2)
            {
                result = Calc(result, exp[i + 1].Equals("T") ? true : false, exp[i]);
            }
            return result;
        }

        bool Calc(bool a, bool b, string opStr)
        {
            OP op = opStr.Equals("&&") ? OP.AND : OP.OR;
            
            bool result = false;

            switch (op)
            {
                case OP.AND:
                    result = a && b;
                    break;

                case OP.OR:
                    result = a || b;
                    break;
            }
            return result;
        }
    }
}
