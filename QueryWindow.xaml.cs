using System;
using System.Windows;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Cms;

namespace Calculator_hw
{
    /// <summary>
    /// Interaction logic for QueryWindow.xaml
    /// </summary>
    public partial class QueryWindow : Window
    {
        ObservableCollection<QueryResult> data = new ObservableCollection<QueryResult>();
        
        public QueryWindow()
        {
            InitializeComponent();

            DataDisplay();


        }

        public class QueryResult
        {
            public string Expression { get; set; }
            public string Preorder { get; set; }
            public string Postorder { get; set; }
            public string Dec_num { get; set; }
            public string Bin_num { get; set; }

        }

        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            

            var selectedItem = dataGrid.SelectedItem;
            if (selectedItem != null)
            {
                QueryResult queryResult = (QueryResult)selectedItem;
                String expression = queryResult.Expression;
                String preorder = queryResult.Preorder;
                String postorder = queryResult.Postorder;
                String dec_num = queryResult.Dec_num;
                String bin_num = queryResult.Bin_num;

                var connectionString = string.Format(
                    "server={0};uid={1};pwd={2};database={3}",
                    "localhost",
                    "root0",
                    "860324",
                    "calculator"
                    );

                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM result WHERE expression = @expression AND preorder = @preorder AND postorder = @postorder AND dec_num = @dec_num AND bin_num = @bin_num;";
                        cmd.Parameters.AddWithValue("@expression", expression);
                        cmd.Parameters.AddWithValue("@preorder", preorder);
                        cmd.Parameters.AddWithValue("@postorder", postorder);
                        cmd.Parameters.AddWithValue("@dec_num", dec_num);
                        cmd.Parameters.AddWithValue("@bin_num", bin_num);

                        int j = cmd.ExecuteNonQuery();
                        if (j > 0)
                        {
                            MessageBox.Show("Deletion Success.");
                        }
                        else
                        {
                            MessageBox.Show("Deletion Failed.");
                        }


                    }
                    conn.Close();
                }

                data.Clear();
                DataDisplay();

            }




        }

        private void Btn_previous_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            mainWindow.Show();
        }

        internal void DataDisplay() 
        {


            String expression;
            String preorder;
            String postorder;
            String dec_num;
            String bin_num;

            var connectionString = string.Format(
            "server={0};uid={1};pwd={2};database={3}",
            "localhost",
            "root0",
            "860324",
            "calculator"
            );

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM result;";

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        expression = reader.GetString("expression");
                        preorder = reader.GetString("preorder");
                        postorder = reader.GetString("postorder");
                        dec_num = reader.GetString("dec_num");
                        bin_num = reader.GetString("bin_num");

                        data.Add(new QueryResult()
                        {
                            Expression = expression,
                            Preorder = preorder,
                            Postorder = postorder,
                            Dec_num = dec_num,
                            Bin_num = bin_num
                        });
                    }


                }
                conn.Close();
            }

            dataGrid.DataContext = data;
        }
    }
}
