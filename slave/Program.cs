using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace slave
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Deck d = new Deck();         
            Field field = new Field(d);

            field.Play();

            Console.ReadKey();
        }  
    }
}
