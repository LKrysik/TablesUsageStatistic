using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web.UI.Design.WebControls;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.SqlServer.TransactSql.ScriptDom;

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
            var enumerator = new EnumeratorVisitor();
            var statsEnumerator = new StatsVisitor();

            script.Accept(statsEnumerator);
            Stats.Items.Clear();

            foreach (var i in statsEnumerator.GetDistinctNodes())
            {
                Stats.Items.Add(i);

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

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void AnalyzeQueriesbutton_Click(object sender, RoutedEventArgs e)
        {
            Parse();
        }

   
    }
}
