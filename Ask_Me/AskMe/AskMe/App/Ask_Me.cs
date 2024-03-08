using AskMe.Context;
using AskMe.Context.Entities;
using AskMe.Enums;
using AskMe.UI;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace AskMe.App
{
    public class Ask_Me
    {
        private UserAccount SelectedAccount;
        public void Login_Sign()
        {
            int Option = AppScreen.LoginPage();

            if (Option == 1)
            {
                Sign_Up.Sign();
                Console.Clear();
            }

            bool OK = false;
            do
            {
                string User = Validator.convert<string>("Plz Enter Your User Name");
                using (Data_Context ctx = new Data_Context())
                {
                    var user = (from U in ctx.Users
                                where U.Username == User
                                select U).FirstOrDefault();
                    if (user == null) Utinity.PrintMsg("This User Not Found Plz Enter Valid User", false);
                    else
                    {
                        string Pass = Validator.convert<string>("Plz Enter Your Password");
                        if (user.Password != Pass) Utinity.PrintMsg("This Password Is Wrong Plz Enter Correct Password", false);
                        else
                        {
                            OK = true;
                            SelectedAccount = new UserAccount()
                            {
                                Id = user.Id,
                                Name = user.Name,
                                Password = Pass,
                                Age = user.Age,
                                Email = user.Email,
                                Username = user.Name,
                            };
                        }
                    }
                }
            } while (OK == false);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nSucessfull Login\n");
            Console.ResetColor();
            Console.WriteLine($"Welcome Back : {SelectedAccount.Name}");
            Utinity.Print_Dot(10);

        }
        public void Process_Menue()
        {
            switch (Validator.convert<int>("Plz Enter Option : "))
            {
                case (int)Opertions.Print_Question_To_Me:
                    Console.WriteLine("All Question To Me");
                    Print_all_Question_To_Me();
                    break;

                case (int)(int)Opertions.Print_Question_From_Me:
                    Console.WriteLine("All Question From Me");
                    Print_all_Question_From_Me();
                    break;

                case (int)Opertions.Answer_Question:
                    Answer_Question();
                    break;
                case (int)Opertions.Delete_Question:
                    Delete_Question();
                    break;
                case (int)Opertions.Ask_Question:
                    Console.Clear();
                    Ask_Question();
                    break;
                case (int)Opertions.Feeds:
                    Console.Clear();
                    Feeds();
                    break;
                case (int)Opertions.List_System_Users:
                    User_List();
                    break;
                case (int)Opertions.LogOut:
                    Console.WriteLine("Loging Out...");
                    LogOut();
                    Run();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Option.");
                    Console.ResetColor();
                    break;

            }

        }
        internal void Run()
        {
            Console.Clear();
            Login_Sign();
            while (true)
            {  
                Console.Clear();
                AppScreen.Display_Menue();
                Process_Menue();
                Utinity.PressEnter();
                Console.Clear();
            }

        }
        public void Print_all_Question_To_Me()
        {
            Console.Clear();
            Console.WriteLine("---------- All Question To Me ----------");
            using (Data_Context ctx = new Data_Context())
            {
                var All_Question = from Q in ctx.Question
                                   join U in ctx.Users
                                   on Q.To_ID equals U.Id
                                   where Q.To_ID == SelectedAccount.Id
                                   select new
                                   {
                                       idQ = Q.Id_Question,
                                       idF = Q.From_ID,
                                       Ques = Q.Body,
                                       Ans = Q.Answer,
                                       Anony = U.Is_Unkown,
                                       Th = Q.Thread,
                                   };
                if (All_Question.Count() == 0) { Utinity.PrintMsg("No Question To You"); };
                foreach (var Q in All_Question)
                {
                    if (Q.Th == null)
                        Console.WriteLine($"Question ID : {Q.idQ}       From User : {(Q.Anony == false ? Q.idF : "Anonymous")}" +
                            $"\nQuestion : {Q.Ques}" +
                            $"\nAnswer : {Q.Ans}");
                    else Console.WriteLine($"Thread Question ID : {Q.idQ}     For Base Question : {Q.Th.Id_Question} " +
                        $"    From User : {(Q.Anony == false ? Q.idF : "Anonymous")}" +
                        $"\nQuestion : {Q.Ques}" +
                        $"\nAnswer : {Q.Ans}");
                }
            }

        }


        public void Print_all_Question_From_Me()
        {
            Console.Clear();
            Console.WriteLine("---------- All Question From Me ----------");
            using (Data_Context ctx = new Data_Context())
            {
                var All_Question = from Q in ctx.Question
                                   join U in ctx.Users
                                   on Q.From_ID equals U.Id
                                   where Q.From_ID == SelectedAccount.Id
                                   select new
                                   {
                                       idQ = Q.Id_Question,
                                       idT = Q.To_ID,
                                       Ques = Q.Body,
                                       Ans = Q.Answer,
                                       Anony = U.Is_Unkown,
                                       Th = Q.Thread,
                                   };
                if (All_Question.Count() == 0) { Utinity.PrintMsg("No Question From You"); };
                foreach (var Q in All_Question)
                {
                    if (Q.Th == null)
                        Console.WriteLine($"Question ID : {Q.idQ}       To User : {Q.idT}" +
                            $"\nQuestion : {Q.Ques}" +
                            $"\nAnswer : {Q.Ans}");
                    else Console.WriteLine($"Thread Question ID : {Q.idQ}     For Base Question : {Q.Th.Id_Question} " +
                        $"    To User : {Q.idT}" +
                        $"\nQuestion : {Q.Ques}" +
                        $"\nAnswer : {Q.Ans}");
                }
            }
        }

        public void Answer_Question()
        {
            Console.Clear();
            int Ques_ID = Validator.convert<int>("Plz Enter Question Id Want To Insert Answer");
            using (Data_Context ctx = new Data_Context()) {
                var Ques = (from Q in ctx.Question
                           where Q.Id_Question == Ques_ID
                           select Q).FirstOrDefault();
                if (Ques == null) { Utinity.PrintMsg($"No Question With Id {Ques_ID}"); }
                else
                {
                    Console.WriteLine($"Plz Enter Answer For Question Id {Ques_ID}" +
                        $"\nQuestion : {Ques.Body}");
                    string Ans = Validator.convert<string>("Enter Answer : ");
                    if (Ques.Answer == null)
                    {
                        Ques.Answer = Ans;
                        ctx.SaveChanges();
                    }
                    else
                    {
                        Utinity.PrintMsg("Warning : This Question Has Answer Enter 1 To Update Else Enter 2",false);
                        int input;
                        do
                        {
                            input = Validator.convert<int>("Plz Enter 1 Or 2 Only");

                        } while (input > 2 || input < 1);

                        if(input == 1)
                        {
                            Ques.Answer = Ans;
                            ctx.SaveChanges();
                        }
                    }
                }
            }
        }

        public void Delete_Question()
        {
            int Ques_Id = Validator.convert<int>("Plz Enter ID Question Want To Delete ");
            using (Data_Context ctx = new Data_Context()) {
                var Ques = (from Q in ctx.Question
                            where Q.Id_Question == Ques_Id && Q.From_ID == SelectedAccount.Id
                            select Q).FirstOrDefault();
                if (Ques == null) { Console.WriteLine("Can Not Delete This Quesiton"); }
                else
                {
                    ctx.Database.ExecuteSqlInterpolated($"Delete from Question\r\nwhere ThreadId_Question={Ques_Id}");
                    ctx.Database.ExecuteSqlInterpolated($"Delete from Question\r\nwhere Id_Question={Ques_Id} and From_ID={SelectedAccount.Id}");
                }

            }
        }

        public void Ask_Question()
        {
            using (Data_Context ctx = new Data_Context())
            {

                int To = Validator.convert<int>("Plz Enter ID User Want To Sent Question To You : ");

                while (To == SelectedAccount.Id)
                {
                    Utinity.PrintMsg("Can Not Sent Question To You.");
                    To = Validator.convert<int>("Plz Enter ID User Want To Sent Question To You : ");
                }

                var User_To = ctx.Users.FirstOrDefault(U => U.Id == To);
                if (User_To == null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("This User Not Found");
                    Console.ResetColor();
                    return;
                }

                int Anony = Validator.convert<int>("Plz Enter 1 If You Want Anonymous Question Else Enter Any Number");

                if (Anony == 1)
                {
                    if (User_To?.Is_Unkown == false)
                    {
                        Console.WriteLine("Can Not Sent Anonymous Question To This User");
                        int Op = Validator.convert<int>("If You Want To Contain Press 1 else Press Any Number");
                        if (Op != 1) return;
                    }
                }

                // If Thread Question
                int ID_Ques;
                var Ques = false;
                do
                {
                    ID_Ques = Validator.convert<int>("If You Want Thread Question Plz Enter Id Of Question Else press -1");
                    if (ID_Ques == -1) break;
                    Ques = ctx.Question.Any(p => p.Id_Question == ID_Ques);
                    Utinity.PrintMsg("This Question Not Found Plz Enter Invalid Question");

                } while (Ques == false);


                string Question = Validator.convert<string>("Plz Enter Question :");

                // Check if Question Exact

                var res = ctx.Question.Any(Q => Q.Body == Question);
                if (res == true) { Utinity.PrintMsg("This Question Has Found "); }
                else
                {
                    Question ques = new Question
                    {
                        Body = Question,
                        From_ID = SelectedAccount.Id,
                        To_ID = To,
                        AnonymouseQuestion = (Anony == 1 ? 1 : null),
                    };
                    ques.Thread = (ID_Ques == -1 ? null : ctx.Question.Find(ID_Ques));

                    ctx.Question.Add(ques);
                    ctx.SaveChanges();
                }
            }
            
            
        }

        public void User_List()
        {
            Console.Clear();
            using (Data_Context ctx = new Data_Context()) {
                Console.WriteLine("--------- List Of User System ----------");
                var Users = from U in ctx.Users
                            select new
                            {
                                Name = U.Name,
                                Id = U.Id,
                            };
                foreach (var user in Users)
                {
                    Console.WriteLine($"Name = {user.Name}     ID = {user.Id}");
                    Console.WriteLine("--------------------------------------");
                }
               
            }
        }

        public void Feeds()
        {
            Console.WriteLine("---------- Feeds ----------\n");
            using(Data_Context ctx = new Data_Context())
            {
                var feed = from F in ctx.Question
                           where F.Answer != null
                           select F;
                foreach(var f in feed)
                {
                    if (f.Thread != null)
                    {
                        Console.WriteLine($"Thread Question ID : {f.Id_Question}    Main Question ID : {f.Thread.Id_Question}   " +
                            $"From User ID =  {(f.AnonymouseQuestion==null?f.From_ID:"Anonymous")}\n" +
                            $"To User ID = {f.To_ID}\n" +
                            $"Question : {f.Body}\n" +
                            $"Answer : {(f.Answer==null?"Not Answer Yet":f.Answer)}" +
                            $"\n--------------------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine($"Question ID : {f.Id_Question}   From User ID =  {(f.AnonymouseQuestion == null ? f.From_ID : "Anonymous")}\n" +
                           $"To User ID = {f.To_ID}\n" +
                           $"Question : {f.Body}\n" +
                           $"Answer : {(f.Answer == null ? "Not Answer Yet" : f.Answer)}" +
                           $"\n--------------------------------------------------");
                    }
                }
            }
        }

        internal static void LogOut()
        {
            Console.Clear();
            Utinity.PrintMsg("Thank You To Use My App");
            Utinity.Print_Dot();
            Console.Clear();
        }
    }
}
                
