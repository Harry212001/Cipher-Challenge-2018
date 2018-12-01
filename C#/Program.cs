using System;
using System.Collections.Generic;
using System.Linq;

namespace Playfair_Cipher {
    class Decision {
        public bool Make(double probability) {
            Random rnd = new Random();
            if (rnd.NextDouble()<probability) {
                return true;
            }
            else {
                return false;
            }
        }
    }


    class Key {
        string ciphertext { get; set; }
        char[,] key { get; set; }
        void random_key() {
            List<int> numList = Enumerable.Range(65,91).ToList();
            numList.RemoveAt(10);
            
        }
    }


    class Program {
        static void Main(string[] args) {
            Decision dcs = new Decision();
            Console.WriteLine(dcs.Make(0.5));
        }
    }
}
