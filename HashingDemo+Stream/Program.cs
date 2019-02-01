using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;


namespace HashingDemo_Stream
{
    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine($@"
            Computed Hash: { Hash.HashGen("Now is the winter", "of", "our discontent") }
            HASHLEN  should be {Hash.HASHLEN}
            ");
            Console.WriteLine($@"
            Computed Hash: { Hash.HashGen("Now is the winterofour discontent") }
            HASHLEN  should be {Hash.HASHLEN}
            ");
            Console.ReadLine();
            Environment.Exit(0);

            //How to deal with hashing multiple elements at once?
            //The ComputeHash method accepts a stream, so one way would be
            //to write everything to a stream and then hash that.
            //In memory streams will be faster than disc based for this 
            //purpose.

            //The algorithm is modified to accept user input over four
            //lines (line1, line2, ...) writing them to a stream as we go
            //and then hashing the final stream

            //Set up Stream etc
            UnicodeEncoding enc = new UnicodeEncoding();
            HashAlgorithm sha = new SHA1CryptoServiceProvider();
            const int HASHLEN = 20;

            // SHA1 is regarded as insecure against a well-resourced attack
            // In a non-demo application use SHA-3 (512 bit) instead
            //HashAlgorithm sha = new SHA512CryptoServiceProvider();
            //const int HASHLEN = 64;

            while (true)
            {
                char[] ca;

                //Important - 1) release the memory stream when finished with it to avoid
                //memory leaks 2) Clear the memory stream after each has is computed
                //Both can be achieved by wrapping the input-ouput cycle in a 'using'
                //statement.

                //create expandable stream (for final application a fixed size could be used for efficiency
                // and recycled) 
                using (MemoryStream memStream = new MemoryStream())
                {

                    //Get four lines of input, convert to byte array, write them into the stream
                    foreach (int i in Enumerable.Range(1, 4))
                    {
                        Console.Write($"Line {i}: ");
                        ca = Console.ReadLine().ToCharArray();
                        byte[] dataArray = enc.GetBytes(ca);


                        memStream.Write(dataArray, 0, dataArray.Length);
                    }

                    //reset the stream's pointer to its beginning!!

                    memStream.Seek(0, SeekOrigin.Begin);

                    //hash the stream contents

                    byte[] hash = sha.ComputeHash(memStream);


                    //write out the hash
                    Console.Write("SHA1 Hash: ");
                    Console.WriteLine(hashAsByteArrayToString(hash, HASHLEN));
                    //foreach (byte b in hash)
                    //{

                    //    Console.Write($"{b:x2}");
                    //}
                } //using
                Console.WriteLine();
            } //while 
        } //main

        //the presence of a utilty 'ToString' method is pr bably a strong clue that this should be
        //a class ....
        private static string hashAsByteArrayToString(byte[] ba, int expectedLength = 20)
        {
            if (ba.Length != expectedLength) throw new ArgumentOutOfRangeException();
            StringBuilder builder = new StringBuilder(expectedLength*2);
            foreach (byte b in ba) builder.AppendFormat("{0:x2}", b);
            return builder.ToString();
        }
    } // Program
} // namespace
