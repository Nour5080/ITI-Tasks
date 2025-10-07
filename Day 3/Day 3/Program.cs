namespace DCSharp
{
    #region calculater class
    public class calc
    {

        #region sum
        public int sum(int x, int y) { return (x + y); }
        public double sum(double x, double y) { return (x + y); }
        #endregion

        #region subtract
        public int subtract(int x, int y) { return (x - y); }
        public double subtract(double x, double y) { return (x - y); }
        #endregion

        #region multiply
        public int multiply(int x, int y) { return (x * y); }
        public double multiply(double x, double y) { return (x * y); }
        #endregion

        #region div ide
        public int divide(int x, int y)
        {
            if (y == 0)
            {
                Console.WriteLine("Invalid numper");
                return 0;
            }
            return (x / y);
        }
        public double divide(double x, double y)
        {
            if (y == 0)
            {
                Console.WriteLine("Invalid numper");
                return 0;
            }
            return (x / y);
        }
        #endregion
    }
    #endregion

    #region Question class
    public class Question
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public int Mark { get; set; }

        public Question()
        {
            Header = string.Empty;
            Body = string.Empty;
            Mark = 0;
        }

        public Question(string header, string body, int mark)
        {
            Header = header;
            Body = body;
            Mark = mark;
        }

        public void show()
        {
            Console.WriteLine("=====================================");
            Console.WriteLine($"{Header}                         {Mark} Marks\n");
            Console.WriteLine($"{Body}");
        }
    }
    #endregion

    #region
    public class mcq : Question
    {
       public string[] Choises { get; set; }
        public int correct_ans { get; set; }

        public mcq()
        {
            Choises = new string[0];
            correct_ans = -1;
        }

        public mcq(string header, string body, int mark,
            string[] choises, int correct_ANS): base(header, body, mark)
        {
            Choises = choises;
            correct_ans= correct_ANS;
        }

        public void show()
        {
            base.show();
            if( Choises.Length > 0)
            {
                for(int i = 0; i < Choises.Length; i++)
                    Console.WriteLine($"{i+1}- {Choises[i]}" );  
            }

        }

    }
    #endregion

    internal class program
    {
        static void Main(string[] args)
        {
            #region calculater  
            Console.WriteLine("=============================");
            calc calculater = new calc();
            Console.WriteLine($"20 + 30 = {calculater.sum(20,30)}");
            Console.WriteLine($"20 - 30 = {calculater.subtract(20,30)}");
            Console.WriteLine($"20 * 30 = {calculater.multiply(20,30)}");
            Console.WriteLine($"40.5 / 20.25 = {calculater.divide(40.5, 20.25)}\n\n");
            #endregion


            Console.WriteLine("=====================================");

            #region mcq object
            string[] strings = { "75", "15", "95", "45" };
            mcq MCQ1 = new mcq("Math","3*5",10,strings,1);
            MCQ1.show();
            #endregion


            Console.WriteLine("=====================================");

            #region q= new mcq()
            mcq MCQ2 = new mcq("Names","What is name our Instructor? ", 100,
                new string[] { "Ahmed", "Maryam", "Aya", "Nour" },1);
            MCQ2.show();
            #endregion

            Console.WriteLine("=====================================");

            #region MCQ by user
            Console.WriteLine("How many questions would you want? ");
            int count_qus =int.Parse(Console.ReadLine());
            mcq[] MCQ_Array = new mcq[count_qus]; 
            for(int i = 0;i < count_qus; i++)
            {
                Console.WriteLine($"Enter the details for question numper ({i+1})  ");

                Console.WriteLine("Enter the Header: ");
                string header = Console.ReadLine();

                Console.WriteLine("Enter the Body: ");
                string body = Console.ReadLine();

                Console.WriteLine("Enter the Mark: ");
                int mark = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the Numper of Choises: ");
                int Count_choises = int.Parse(Console.ReadLine());

                string[] Choises= new string[Count_choises];

                for(int j = 0; j < Count_choises; j++)
                {
                    Console.WriteLine($"enter the choise numper {j+1}");
                    Choises[j] = Console.ReadLine();
                }
                Console.WriteLine("Enter which the true answer?");
                int true_ans = int.Parse(Console.ReadLine());
                true_ans -= 1;
                MCQ_Array[i] = new mcq(header, body, mark, Choises, true_ans);
            
            }

            for (int i = 0; i < count_qus; i++)
            {
            MCQ_Array[i].show();
            }
            #endregion


            Console.WriteLine("=====================================");

            #region Bouns: calc the ruselt
            if (MCQ_Array.Length > 0)
            {
                int Max_Degree = 0;
                int Toptal_Degree = 0;
                for (int i = 0; i <MCQ_Array.Length; i++)
                {
                    MCQ_Array[i].show();
                    Max_Degree += MCQ_Array[i].Mark;

                    Console.WriteLine("Enter the answer numper: ");
                    int user_ans= int.Parse(Console.ReadLine());
                    user_ans-= 1;  

                    if(user_ans == MCQ_Array[i].correct_ans)
                        Toptal_Degree += MCQ_Array[i].Mark;
                }
                Console.WriteLine("-------------The Quize is over------------- ");
                Console.WriteLine($"Your Degree is({Toptal_Degree}/{Max_Degree}\n\n");

            }


            Console.WriteLine("=====================================");

            #endregion
        }
    }
}