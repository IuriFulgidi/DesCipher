using System;
using System.Security.Cryptography;
using System.Text;

namespace DesCipher
{
    class Program
    {
        static void Main(string[] args)
        {
            //impostazione parametri di cifratura
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] key = new byte[8] { 49, 50, 51, 52, 53, 54, 55, 56 }; // "12345678"
            des.Key = key;
            des.IV = key; //

            //impostazione della password da parte dell'utente
            bool flag = false;
            while (!flag)
            {
                try
                {
                    Console.WriteLine("inserire la chiave, ad esempio '12345678'");
                    string pw = Console.ReadLine();
                    key = Encoding.ASCII.GetBytes(pw);
                    des.Key = key;
                    flag = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Chiave non valida");
                }
            }

            des.IV = key; // imposta il vettore uguale alla chiave (non obbligatorio, ma possibile)

            //processo di cifratura
            Console.WriteLine();
            Console.WriteLine("inserire il testo da cifrare");
            string plainText = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("testo inserito:");
            Console.WriteLine(plainText);

            byte[] plainData = Encoding.ASCII.GetBytes(plainText);
            ICryptoTransform enc = des.CreateEncryptor();
            byte[] encData = enc.TransformFinalBlock(plainData, 0, plainData.Length);

            Console.WriteLine();
            Console.WriteLine("testo cifrato:");
            Console.WriteLine(Encoding.ASCII.GetString(encData));

            //processo di decifratura
            ICryptoTransform dec = des.CreateDecryptor();
            byte[] decData = dec.TransformFinalBlock(encData, 0, encData.Length);
            string decText = Encoding.ASCII.GetString(decData); //-> "testo in chiaro"

            Console.WriteLine();
            Console.WriteLine("testo decifrato:");
            Console.WriteLine(Encoding.ASCII.GetString(decData));

            //Impostare la deimensione della chiave
            //string pw = "il mio gatto si chiama Cippa Lippa"; // password inserita dall'utente
            //byte[] pwData = Encoding.ASCII.GetBytes(pw);
            //// crea un digest della password
            //SHA1Managed sha = new SHA1Managed();
            //byte[] pwHash = sha.ComputeHash(pwData, 0, pwData.Length); // 20 byte
            //DESCryptoServiceProvider des1 = new DESCryptoServiceProvider();
            //// tronca il digest alla dimensione richiesta (in questo caso 8 byte)
            //Array.Resize(ref pwHash, des1.KeySize / 8); // 64 / 8 --> 8 byte
            //des1.Key = pwHash;
            //des1.IV = pwHash;
        }
    }
}
