using System;
using System.Text;
using System.Security.Cryptography;
/* Points to note
 * i) hashing is in the System.Security.Cryptography namespace
 * ii) a hash engine is obtained by using one of the constructors in this namespace
 * iii) the hash engine has a method ComputeHash(byte []) which operates on a *byte* array
 * iv) Often, therefore, you will need to convert text or whatever to a byte array first
 * v) Converting text to a byte array also involves picking an *encoding* (System.Text namespace)
 */
namespace HashingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] ca;
            while (true)
            {
                ca = Console.ReadLine().ToCharArray();
                byte[] dataArray = Encoding.UTF8.GetBytes(ca);
                HashAlgorithm sha = new SHA1CryptoServiceProvider();
                byte[] hash = sha.ComputeHash(dataArray);

                Console.Write("SHA1 Hash: ");
                foreach (byte b in hash)
                {
                    
                    Console.Write($"{b:x2}");
                }
                Console.WriteLine();
            }
        }
    }
}
