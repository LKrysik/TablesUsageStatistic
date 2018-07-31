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
using TablesUsageStatistic.Properties;

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

            LoadProjectSettings();
           
        }
        public void LoadProjectSettings()
        {
            ConnServerNameTextBox.Text = Settings.Default["db_Server"].ToString();
            ConnDatabaseNameTextBox.Text = Settings.Default["db_Database"].ToString();

        }
        public void SaveProjectSettings()
        {
            Settings.Default["db_Server"] = ConnServerNameTextBox.Text;
            Settings.Default["db_Database"]= ConnDatabaseNameTextBox.Text;
            Settings.Default.Save();
        }
        private void Parse()
        {
            Errors.Text = "";

            var parser = new TSql120Parser(true);
            StringReader QueryStrings = new StringReader("");

            if ((tabControl.SelectedItem as TabItem).Name == "tabText")
            {
                Console.WriteLine("tabText");
                QueryStrings = new StringReader(GetText());
            }
            if ((tabControl.SelectedItem as TabItem).Name == "tabServer")
            {
                Console.WriteLine("tabServer");
            }
            if ((tabControl.SelectedItem as TabItem).Name == "tabFromFile")
            {
                Console.WriteLine("tabFromFile");
                if (FromFilePath.Text != null)
                {
                    QueryStrings = new StringReader(File.ReadAllText(FromFilePath.Text));
                }
            }
      
            var script = parser.Parse(QueryStrings, out IList<ParseError> errors);
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveProjectSettings();
        }
    }
}
