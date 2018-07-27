using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Web.UI.Design.WebControls;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using Microsoft.Win32;

namespace TablesUsageStatistic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }
        private void Parse()
        {
            Errors.Text = "";

            var parser = new TSql120Parser(true);
            if ((tabControl.SelectedItem as TabItem).Name == "tabText")
            {
                Console.WriteLine("tabText");
                var script = parser.Parse(new StringReader(GetText()), out IList<ParseError> errors);

                if (errors.Count > 0)
                {

                    Errors.Text = "";
                    foreach (var e in errors)
                    {
                        Errors.Text += "Error: " + e.Message + " at: " + e.Offset + "\r\n";
                    }
                    return;
                }
                var statsEnumerator = new StatsVisitor();

                script.Accept(statsEnumerator);
                ResultGrid.Items.Clear();

                foreach (var i in statsEnumerator.GetDistinctNodes())
                {
                    ResultGrid.Items.Add(i);
                }
            }
            if ((tabControl.SelectedItem as TabItem).Name == "tabServer")
            {
                Console.WriteLine("tabServer");
            }
            if ((tabControl.SelectedItem as TabItem).Name == "tabFromFile")
            {
                Console.WriteLine("tabFromFile");
            }
        }
        private string GetText()
        {
            return new TextRange(InputBox.Document.ContentStart, InputBox.Document.ContentEnd).Text;
        }

        private void UIElement_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                Parse();
        }

        private void AnalyzeQueriesbutton_Click(object sender, RoutedEventArgs e)
        {
            Parse();
        }

        private void ConnConnect_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlcon = null;
            DbConnection.Dispose(sqlcon);
            sqlcon = DbConnection.GetConnection();

            

        }

        private void buttFromFileSelectPath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text|*.txt|SQL|*.sql|All|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                //FromFilePath.Text = File.ReadAllText(openFileDialog.FileName);
                FromFilePath.Text = openFileDialog.FileName;
            }
                
        }
    }
}
