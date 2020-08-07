using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using MySql.Data.MySqlClient;



namespace Calculator_hw
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        Stack myStack = new Stack();
        Stack myStack2 = new Stack();

        Dictionary<char, int> myDictionary = new Dictionary<char, int>()
        {
            { '+', 0 },
            { '-', 0 },
            { '*', 1 },
            { '/', 1 }
        };



        public MainWindow()
        {
            InitializeComponent();
            txt_num.Text = "0";
            
        }

        private void Btn_1_Click(object sender, RoutedEventArgs e)
        {
            if (txt_num.Text == "0" || txt_num.Text == "")
            {
                txt_num.Text = "1";
            }
            else
            {
                txt_num.Text = txt_num.Text + "1";
            }
        }

        private void Btn_2_Click(object sender, RoutedEventArgs e)
        {
            if (txt_num.Text == "0" || txt_num.Text == "")
            {
                txt_num.Text = "2";
            }
            else
            {
                txt_num.Text = txt_num.Text + "2";
            }
        }

        private void Btn_3_Click(object sender, RoutedEventArgs e)
        {
            if (txt_num.Text == "0" || txt_num.Text == "")
            {
                txt_num.Text = "3";
            }
            else
            {
                txt_num.Text = txt_num.Text + "3";
            }
        }

        private void Btn_4_Click(object sender, RoutedEventArgs e)
        {
            if (txt_num.Text == "0" || txt_num.Text == "")
            {
                txt_num.Text = "4";
            }
            else
            {
                txt_num.Text = txt_num.Text + "4";
            }
        }

        private void Btn_5_Click(object sender, RoutedEventArgs e)
        {
            if (txt_num.Text == "0" || txt_num.Text == "")
            {
                txt_num.Text = "5";
            }
            else
            {
                txt_num.Text = txt_num.Text + "5";
            }
        }

        private void Btn_6_Click(object sender, RoutedEventArgs e)
        {
            if (txt_num.Text == "0" || txt_num.Text == "")
            {
                txt_num.Text = "6";
            }
            else
            {
                txt_num.Text = txt_num.Text + "6";
            }
        }

        private void Btn_7_Click(object sender, RoutedEventArgs e)
        {
            if (txt_num.Text == "0" || txt_num.Text == "")
            {
                txt_num.Text = "7";
            }
            else
            {
                txt_num.Text = txt_num.Text + "7";
            }
        }

        private void Btn_8_Click(object sender, RoutedEventArgs e)
        {
            if (txt_num.Text == "0" || txt_num.Text == "")
            {
                txt_num.Text = "8";
            }
            else
            {
                txt_num.Text = txt_num.Text + "8";
            }
        }

        private void Btn_9_Click(object sender, RoutedEventArgs e)
        {
            if (txt_num.Text == "0" || txt_num.Text == "")
            {
                txt_num.Text = "9";
            }
            else
            {
                txt_num.Text = txt_num.Text + "9";
            }
        }

        private void Btn_0_Click(object sender, RoutedEventArgs e)
        {
            String str = txt_num.Text;
            char c = str[str.Length - 1];

            if (txt_num.Text == "0" || txt_num.Text == "")
            {
                txt_num.Text = "0";
            }
            else if(c=='/')//除以 0之情況
            {
                //do nothing.
            }
            else
            {
                txt_num.Text = txt_num.Text + "0";
            }
        }

        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (txt_num.Text != "")
            {
                String str = txt_num.Text;
                char c = str[str.Length - 1];
                if ((c != '+') && (c != '-') && (c != '*') && (c != '/'))
                {
                    txt_num.Text += "+";
                }
            }
        }

        private void Btn_sub_Click(object sender, RoutedEventArgs e)
        {
            if (txt_num.Text != "")
            {
                String str = txt_num.Text;
                char c = str[str.Length - 1];
                if ((c != '+') && (c != '-') && (c != '*') && (c != '/'))
                {
                    txt_num.Text += "-";
                }
            }
        }

        private void Btn_mul_Click(object sender, RoutedEventArgs e)
        {
            if (txt_num.Text != "")
            {
                String str = txt_num.Text;
                char c = str[str.Length - 1];
                if ((c != '+') && (c != '-') && (c != '*') && (c != '/'))
                {
                    txt_num.Text += "*";
                }
            }
        }

        private void Btn_div_Click(object sender, RoutedEventArgs e)
        {
            if (txt_num.Text != "")
            {
                String str = txt_num.Text;
                char c = str[str.Length - 1];
                if ((c != '+') && (c != '-') && (c != '*') && (c != '/'))
                {
                    txt_num.Text += "/";
                }
            }
        }

        private void Btn_del_Click(object sender, RoutedEventArgs e)
        {
            if (txt_num.Text != "")
            {              
                txt_num.Text = txt_num.Text.Remove(txt_num.Text.Length-1, 1); //start at (txt_num.Text.Length-1)th position, delete 1 characters
            }
        }

        private void Btn_enter_Click(object sender, RoutedEventArgs e) //equal '='
        {
            String expression = txt_num.Text;
            txt_res_postorder.Text = Infix_to_postfix(expression);
            txt_res_preorder.Text = Infix_to_prefix(expression);
            String dec_num = Postfix_eval(expression);
            txt_res_decimal.Text = dec_num;
            txt_res_binary.Text= ToBinary(dec_num);
            
        }

        private void Btn_ac_Click(object sender, RoutedEventArgs e)
        {
            txt_num.Text = "0"; 
        }

        internal String Infix_to_postfix(String expression) 
        {
            int index = 0; //紀錄目前處理到expression的第幾個字元，第一個字元index為0 
            String postorder="";
            char x,y; //key
            int a, b;  //value
            char c;

            while (index!=expression.Length)
            { 
                x = expression[index];
                if ((x != '+') && (x != '-') && (x != '*') && (x != '/')) //x是operand
                {
                    postorder += x;
                }
                else //x是operator
                {
                    if (myStack.Count != 0)
                    {
                        y = Convert.ToChar(myStack.Peek());
                        a = myDictionary[x];
                        b = myDictionary[y];
                        if (a > b)
                        {
                            myStack.Push(x);
                        }
                        else
                        {
                            do
                            {
                                c = Convert.ToChar(myStack.Pop());
                                postorder += c;
                                if (myStack.Count != 0)
                                {
                                    y = Convert.ToChar(myStack.Peek());
                                    b = myDictionary[y];
                                }
                                else
                                {
                                    break;
                                }
                            } while (a <= b);
                            myStack.Push(x);
                        }
                    }
                    else 
                    {
                        myStack.Push(x);
                    }
                }
                index++;
            }
            while(myStack.Count!=0) 
            {
                c = Convert.ToChar(myStack.Pop());
                postorder += c;
            };
            
            myStack.Clear();

            return postorder;
            
        }

        private String Infix_to_prefix(String expression)
        {
            String temp="";
            String preorder = "";
            char c;

            for (int i = 0; i < expression.Length; i++)
            {
                myStack.Push(expression[i]);
            }
            for (int i = 0; i < expression.Length; i++)
            {
                c = Convert.ToChar(myStack.Pop());
                temp += c;
            }
            myStack.Clear();
            temp = Infix_to_postfix(temp);
            for (int i = 0; i < temp.Length; i++)
            {
                myStack.Push(temp[i]);
            }
            for (int i = 0; i < temp.Length; i++)
            {
                c = Convert.ToChar(myStack.Pop());
                preorder += c;
            }
            myStack.Clear();

            return preorder;

        }

        private String Postfix_eval(String exp) //multi-digit postfix evaluation
        {
            int index =  0;
            char x;
            myStack.Clear();
            myStack2.Clear();

            //Step 1 : 在infix中插空格 (space separator)
            while (index != exp.Length) 
            {
                do {
                    index++; //假設一開始一定是operand，也就是第一個數字不為負數
                    if (index < exp.Length)
                    {
                        x = exp[index];
                    }
                    else
                    {
                        break;
                    }
                    
                } while((x != '+') && (x != '-') && (x != '*') && (x != '/'));//x is operand
                if (index < exp.Length)
                {
                    exp = exp.Insert(index, " ");
                    index += 2;
                    exp = exp.Insert(index, " ");
                }
                else 
                {
                    //exp = exp.Insert(index, "@"); //@表示式子已經結束了
                    //String.Insert does not modify the string it is called on, but instead returns a new string with the inserted text in.
                    break;               
                }
            }

            //Step 2 : infix轉postfix
            index = 0;
            String postorder = "";
            String str1;
            int a, b;
            char op,op1; //operator

            while (index < exp.Length)
            {
                x = exp[index];
                
                if ((x != '+') && (x != '-') && (x != '*') && (x != '/')) //x是operand
                {                    
                    do
                    {
                        
                        myStack2.Push(x);
                        index++;
                        if (index == exp.Length)
                        {
                            break;
                        }
                        x = exp[index];

                    } while (x != ' ');
                    if (myStack2.Count == 1)
                    {
                        str1 = Convert.ToString(myStack2.Pop());
                        postorder = postorder + str1 + " ";
                    }
                    else
                    {
                        str1 = "";
                        do
                        {                            
                            str1 += Convert.ToString(myStack2.Pop());


                        } while (myStack2.Count != 0);
                        char[] charArr = str1.ToCharArray();
                        Array.Reverse(charArr);
                        str1 = new string(charArr); //透過初始化將已經反轉的陣列值 再轉換為 字串
                        postorder = postorder + str1 + " ";
                    }
                    index++;
                }
                else //x是operator
                {
                    if (myStack.Count != 0)
                    {
                        op = Convert.ToChar(myStack.Peek());
                        a = myDictionary[x];
                        b = myDictionary[op];
                        if (a > b)
                        {
                            myStack.Push(x);
                        }
                        else
                        {
                            do
                            {
                                op1 = Convert.ToChar(myStack.Pop());
                                postorder = postorder + op1 + " ";
                                if (myStack.Count != 0)
                                {
                                    op1 = Convert.ToChar(myStack.Peek());
                                    b = myDictionary[op1];
                                }
                                else
                                {
                                    break;
                                }
                            } while (a <= b);
                            myStack.Push(x);
                        }
                    }
                    else
                    {
                        myStack.Push(x);
                    }
                    index+=2;
                }
                
            }
            while (myStack.Count != 0)
            {
                x = Convert.ToChar(myStack.Pop());
                postorder = postorder + x + " ";
            };

            postorder = postorder.Remove(postorder.Length-1, 1);
            postorder += '@';

            myStack.Clear();
            myStack2.Clear();



            //Step 3 : postfix evaluation 
            index = 0;
            int flag = 0;
            Double num, num2;
            String result;
            
            while (postorder[index]!='@') 
            {
                x = postorder[index];
                index++;
                if ((x != '+') && (x != '-') && (x != '*') && (x != '/') && (x!=' ')) //x is operand
                {
                    if (flag == 1)
                    {
                        num = Convert.ToDouble(Convert.ToString(myStack.Pop()));
                        num = Convert.ToDouble(Convert.ToString(x)) + 10 * num;
                        myStack.Push(Convert.ToString(num));
                    }
                    else
                    {
                        myStack.Push(x);
                        flag = 1;
                    }
                }
                else if (x == ' ')
                {
                    flag = 0;
                }
                else //x is operator
                {
                    flag = 0;
                    num2 = Convert.ToDouble(Convert.ToString(myStack.Pop()));
                    num = Convert.ToDouble(Convert.ToString(myStack.Pop()));
                    switch (x)
                    {
                        case '+':
                            myStack.Push(Convert.ToString((num + num2)));
                            break;
                        case '-':
                            myStack.Push(Convert.ToString((num - num2)));
                            break;
                        case '*':
                            myStack.Push(Convert.ToString((num * num2)));
                            break;
                        case '/':
                            myStack.Push(Convert.ToString((num / num2)));
                            break;
                    }
                }           
            }
            result = Convert.ToString(myStack.Pop());


            myStack.Clear();


            return result;
        }

        private String ToBinary(String dec_num)
        {
            String result;
           

            if (dec_num[0] != '-') //正數
            {
                if (dec_num.Contains('.')) //含小數
                {
                    string[] number = dec_num.Split('.');
                    result = Convert.ToString(Convert.ToInt32(number[0]), 2);
                    result = result + '.' + FractionToBinary(number[1]);
                    
                }
                else //不含小數
                {
                    result = Convert.ToString(Convert.ToInt32(dec_num), 2);
                }
            }
            else //負數
            {
                dec_num = dec_num.Remove(0, 1);

                if (dec_num.Contains('.')) //含小數
                {
                    string[] number = dec_num.Split('.');
                    result = Convert.ToString(Convert.ToInt32(number[0]), 2);
                    result = '-' + result + '.' + FractionToBinary(number[1]);
                }
                else //不含小數
                {
                    result = '-' + Convert.ToString(Convert.ToInt32(dec_num), 2);
                }
            }

            return result;
            
        }

        private String FractionToBinary(String fraction) 
        {
            fraction = "0." + fraction;
            double cal = Convert.ToDouble(fraction);
            String result_binary = "";

            do 
            {
                cal *=2;
                if (cal >= 1)
                {
                    cal -= 1;
                    result_binary += 1;
                }
                else 
                {
                    result_binary += 0;
                }
            } while (cal!=0);

            return result_binary;

        }

        private void Btn_insert_Click(object sender, RoutedEventArgs e)
        {
            String expression = txt_num.Text;
            String preorder = txt_res_preorder.Text;
            String postorder = txt_res_postorder.Text;
            String dec_num = txt_res_decimal.Text;
            String bin_num = txt_res_binary.Text;

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

                    cmd.CommandText = "SELECT COUNT(*) FROM result WHERE expression = @expression;";
                    cmd.Parameters.AddWithValue("@expression", expression);
                    cmd.ExecuteNonQuery();
                    Int32 i = Convert.ToInt32(cmd.ExecuteScalar()); //ExecuteScalar() : Executes the query, and returns the first column of the first row in the result set returned by the query.
                    if (i == 0)
                    {
                        using (var cmd2 = conn.CreateCommand())
                        {

                            cmd2.CommandText = "INSERT INTO result VALUES ('DEFAULT',  @expression, @preorder, @postorder, @dec_num, @bin_num)";
                            cmd2.Parameters.AddWithValue("@expression", expression);
                            cmd2.Parameters.AddWithValue("@preorder", preorder);
                            cmd2.Parameters.AddWithValue("@postorder", postorder);
                            cmd2.Parameters.AddWithValue("@dec_num", dec_num);
                            cmd2.Parameters.AddWithValue("@bin_num", bin_num);

                            int j = cmd2.ExecuteNonQuery();
                            if (j != 0)
                            {
                                MessageBox.Show("Insertion Success.");
                            }
                            else
                            {
                                MessageBox.Show("Insertion Failed.");
                            }
                        }
                    }
                    else 
                    {
                        MessageBox.Show("The expression already exists!");
                    }
                }
                conn.Close();
            }
          
        }

        private void Btn_query_Click(object sender, RoutedEventArgs e)
        {
            QueryWindow queryWindow = new QueryWindow();
            this.Visibility = Visibility.Hidden;
            queryWindow.Show();
        }
    }
}
