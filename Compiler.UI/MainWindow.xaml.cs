using Compiler.Lib;
using Compiler.Lib.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Compiler.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string savePath = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_chooseFile_Click(object sender, RoutedEventArgs e)
        {
            txt_code.Text = "";

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "TXT Files (*.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                savePath = dlg.FileName;
                string line = "";

                using (var reader = new StreamReader(filename))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        txt_code.Text += line + Environment.NewLine;
                    }

                    txt_code.Text = txt_code.Text.Substring(0, txt_code.Text.Length - 2);
                }

                txt_code.Visibility = Visibility.Visible;
                btn_compile.Visibility = Visibility.Visible;
            }
        }

        private void btn_compile_Click(object sender, RoutedEventArgs e)
        {
            savePath = savePath.Substring(0, savePath.Length - 4);

            savePath = savePath + "_assembly.txt";

            txt_code_11.Text = "";

            Scanner sc = new Scanner(txt_code.Text);

            Token[] tokens = sc.CreateTokens();

            Parser ps = new Parser(tokens);

            Statement[] statements = ps.parseTokens();

            CodeGenerator cg = new CodeGenerator();

            Stack<AssemblyLine> ass = cg.assemble(statements);

            while (ass.Count > 0)
            {
                string txt = ass.Pop() + Environment.NewLine;

                txt_code_11.Text += txt;
            }

            File.WriteAllText(savePath, txt_code_11.Text);

            border_1.Visibility = Visibility.Visible;

            

        }
    }
}
