using System;

namespace CipherEncryption
{
    internal class Caesar
    {

        static string Encrypt(string Message)
        {
            string Result = "";

            foreach (var i in Message)
            {
                char c = i;

                if (char.IsLetter(i)) 
                {
                    char BaseC = char.IsLower(i) ? 'a' : 'A';
                    c = (char)(((c - BaseC + 6) % 26) + BaseC);
                }
                Result += c;
            }
            return Result;
        }


        static string Decrypt(string Message)
        {
            string DResult = "";

            foreach (var i in Message)
            {
                char c = i;

                if (char.IsLetter(i)) 
                {
                    char BaseC = char.IsLower(i) ? 'a' : 'A';
                    c = (char)(((c - BaseC + 20) % 26) + BaseC);
                }
                DResult += c;
            }
            return DResult;
        }


        static void Main()
        {
            string Message;
            string Result;
            string EorD;
            string YorN;

            Console.WriteLine("Welcome to the Text Encryption Machine!");

            do
            {
                Console.WriteLine("\nWould you like to Encrypt[E] or Decrypt[D] your message?");
                EorD = Console.ReadLine();

                // if encryption is chosen
                if (EorD == "E" || EorD == "e")
                {
                    Console.WriteLine("\nPlease type the message you would like to encrypt here:");
                    Message = Console.ReadLine();

                    Result = Encrypt(Message);
                    Console.WriteLine("\nThe resulting message is below \n" + Result);
                }
                // if decryption is chosen
                if (EorD == "D" || EorD == "d")
                {
                    Console.WriteLine("\nPlease type the message you would like to decrypt here " +
                        "or copy and paste your message from the above result:");
                    Message = Console.ReadLine();

                    Result = Decrypt(Message);
                    Console.WriteLine("\nThe resulting message is below \n" + Result);
                }

                Console.WriteLine("\nWould you like to continue using the Text Encryption Machine? [Y/N]");
                YorN = Console.ReadLine();
            } while (YorN == "Y" || YorN == "y");

            Console.WriteLine("\nThank you for using the Text Encyption Machine! [Enter]");
            Console.ReadKey();
        }
    }
}
