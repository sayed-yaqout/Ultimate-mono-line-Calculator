using System;
using System.Collections;
using System.Windows.Forms;

namespace calculator2
{
    public partial class Form1 : Form
    {
      
       
        public Form1()
        {
            InitializeComponent();
        }

        private ArrayList Calculate(ArrayList arraylist)
        {
          
            //we have ordered operators according to associativity
            //1.parenthesis
            if (arraylist.Contains('('))
            {
               var firstIndex = arraylist.IndexOf('(');
                var lastIndex = arraylist.LastIndexOf(')')-1;
              
                    ; //get the stream inside parenthesis
                //handle if first index -1=0
                var previousStr = arraylist[firstIndex - 1].ToString(); //get the previous char
                var previousInt = 0;
                if (int.TryParse(previousStr, out previousInt)
                ) //to make operator before parenthesis equal * if it doesn't exist
                {
                    arraylist.RemoveAt(lastIndex+1);
                    Calculate(arraylist.GetRange(firstIndex+1, lastIndex - firstIndex)); //check
                     arraylist[firstIndex] = '*';
                     
                }
                else
                {
                    arraylist.RemoveAt(firstIndex);
                    arraylist.RemoveAt(lastIndex);
                    Calculate(arraylist.GetRange(firstIndex , lastIndex - firstIndex)); //check
                    
                }

                Calculate(arraylist); //contniue to calculate other parameters
            }

            //2.Division
           Divid(ref arraylist);
           //3.Multiplication
           multiply(ref arraylist);
           //4.Subtraction
         Subtract( ref arraylist);
         //5.Addition
           Add(ref arraylist);
           return arraylist;
        }

        private void multiply(ref ArrayList arrlist)
        {
            if (arrlist.Contains('*'))
            {

                var firstIndex = arrlist.IndexOf('*');
                string num1 = Num1Detect(arrlist, firstIndex);
                string num2 = Num2Detect(arrlist, firstIndex);


                int size = num1.Length + num2.Length + 1; //size of partial arraylist
                var AddingSum = (double.Parse(num1) * double.Parse(num2)).ToString();//Adding then converting to string
                for (int i = 0; i < AddingSum.Length; i++)  //put the result of Addition in their place
                {
                    arrlist[firstIndex - num1.Length + i] = AddingSum[i];
                }
                arrlist.RemoveRange(firstIndex-num1.Length+AddingSum.Length, size - AddingSum.Length); //remove the rest
                Calculate(arrlist);//see if there is any else multply operations
            }
        }
        private void Divid(ref ArrayList arrlist)
        {
            if (arrlist.Contains('/'))
            {

                var firstIndex = arrlist.IndexOf('/');
                string num1 = Num1Detect(arrlist, firstIndex);
                string num2 = Num2Detect(arrlist, firstIndex);

                if (num2!="0")//zero division detecting system
                {
                    int size = num1.Length + num2.Length + 1; //size of partial arraylist
                    var AddingSum = (double.Parse(num1) / double.Parse(num2)).ToString();//Adding then converting to string
                    for (int i = 0; i < AddingSum.Length; i++)  //put the result of Addition in their place
                    {
                        arrlist[firstIndex - num1.Length + i] = AddingSum[i];
                    }
                    arrlist.RemoveRange(firstIndex - num1.Length + AddingSum.Length, size - AddingSum.Length); //remove the rest
                    Calculate(arrlist);//see if there is any else multply operations
                }
                else
                {
                    textBox1.Text = "Error,zero division detected";
                }
            }

        }
        private void Subtract(ref ArrayList arrlist)
        {
            if (arrlist.Contains('-'))
            {

                var firstIndex = arrlist.IndexOf('-');
                string num1 = Num1Detect(arrlist, firstIndex);
                string num2 = Num2Detect(arrlist, firstIndex);
                if (num1 != "-")//possible minus sign
                {
                    int size = num1.Length + num2.Length + 1; //size of partial arraylist
                    var AddingSum = (double.Parse(num1) - double.Parse(num2)).ToString();//Adding then converting to string
                    for (int i = 0; i < AddingSum.Length; i++)  //put the result of Addition in their place
                    {
                        arrlist[firstIndex - num1.Length + i] = AddingSum[i];
                    }
                    arrlist.RemoveRange(firstIndex - num1.Length + AddingSum.Length, size - AddingSum.Length); //remove the rest
                    Calculate(arrlist);//see if there is any else multply operations
                }

                
            }

        }
        private void Add(ref ArrayList arrlist)
        {
            if (arrlist.Contains('+'))
            {

                var firstIndex = arrlist.IndexOf('+');
                string num1 = Num1Detect(arrlist, firstIndex);
                string num2 = Num2Detect(arrlist, firstIndex);


                int size = num1.Length + num2.Length + 1; //size of partial arraylist
                var AddingSum = (double.Parse(num1) + double.Parse(num2)).ToString();//Adding then converting to string
                for (int i = 0; i < AddingSum.Length; i++)  //put the result of Addition in their place
                {
                    arrlist[firstIndex - num1.Length + i] = AddingSum[i];
                }
                arrlist.RemoveRange(firstIndex - num1.Length + AddingSum.Length, size - AddingSum.Length); //remove the rest
                Calculate(arrlist);//see if there is any else addition operations
            }
        }
        private string Num1Detect(ArrayList arrlist,int temp)
        {
            if(temp!=0)
                temp--;
            string Fnum = "";
          
            do // loop to get the first number
            {
                Fnum = Fnum.Insert(0, arrlist[temp].ToString());
                temp--;
            }
            while (
                    temp >= 0&&
                    arrlist[temp].ToString() != "/" &&
                   arrlist[temp].ToString() != "*" &&
                   arrlist[temp].ToString() != "+" &&
                   arrlist[temp].ToString() != "-" &&
                   arrlist[temp].ToString() != "(" &&
                   arrlist[temp].ToString() != ")"
            );

            return Fnum;
        }

        private string Num2Detect(ArrayList arrlist, int temp)
        {
            temp ++;//retriger temp to next character
            string SNUm = "";
            do // loop to get the second number
            {

                SNUm += arrlist[temp].ToString();
                temp++;
            }
            while (
                temp != arrlist.Count &&
                arrlist[temp].ToString() != "/" &&
                arrlist[temp].ToString() != "*" &&
                arrlist[temp].ToString() != "+" &&
                arrlist[temp].ToString() != "-" &&
                arrlist[temp].ToString() != "(" &&
                arrlist[temp].ToString() != ")"

            );

            return SNUm;
        }
       

        private void Print(ArrayList myList)
        {
            foreach (var o in myList)
            {
                textBox1.AppendText(o.ToString());
            }
        }



        private void btnNum_Click(object sender , EventArgs e)
        {
            Button btn = sender as Button;
            textBox1.AppendText(btn.Text);
        }

        private void btnOperator_click(object sender, EventArgs e)
        {

        }

        private void getSum_Click(object sender, EventArgs e)
        {
            var myStr = textBox1.Text;
            textBox1.Clear();
            var  arrlist = new ArrayList(myStr.ToCharArray());

             ;
             Print(Calculate(arrlist));
            
            

            ////switch (Operator)
            //{
            //    case "+":
            //        Result = Num1 + Num2;
            //        break;
            //    case "-":
            //        Result = Num1 - Num2;
            //        break;
            //    case "*":
            //        Result = Num1 * Num2;
            //        break;
            //    case "/":
            //        if(Num2!=0)
            //         Result = Num1 / Num2;
            //        else
            //        {
            //            Result = 0;
            //        }
            //        break;

            //    default:
            //    {
            //        Result = 0;
            //        break;
            //    }

            //}



        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength>0)
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.TextLength - 1, 1);

            }
        }

        private void dot_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Contains("."))
            {
                textBox1.AppendText(".");

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
