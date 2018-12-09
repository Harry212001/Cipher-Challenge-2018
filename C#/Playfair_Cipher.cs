using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Playfair_Cipher {
    public class Decision {
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
        char[][] key;
        public Key() {
            key = new char[5][];
            for (int i = 0; i <= 4; i++) {
            key[i] = new char[5];
            }
            List<int> numList = Enumerable.Range(65,26).ToList();
            numList.RemoveAt(9);
            Random rnd = new Random();
            int randNum = new int();
            for (int i = 0; i<=24; i++) {
                randNum = rnd.Next(0,numList.Count());
                key[i/5][i%5] = (char)numList[randNum];
                numList.RemoveAt(randNum);
            }
        }

        int[] find_index(char letter) {
            for (int j = 0; j <= 4; j++) {
                int i = Array.IndexOf(key[j], letter);
                if (i != -1) {
                    int[] coord = {j,i};
                    return coord;
                }
            }
            int[] noval = {-1,-1};
            return noval;
        }

        public string playfair_solve(string ciphText) {
            int mod(int a,int n) {
                return (a%n + n)%n;
            }
            StringBuilder newText = new StringBuilder("", ciphText.Length);
            for (int i = 0; i<=((ciphText.Length/2)-1); i++) {
                int[] coord1 = find_index(ciphText[i*2]);
                int[] coord2 = find_index(ciphText[i*2+1]);
                int[] newCoord1 = new int[2];
                int[] newCoord2 = new int[2];
                if (coord1[0] == coord2[0]) {
                    newCoord1[1] = mod((coord1[1]-1),5);
                    newCoord2[1] = mod((coord2[1]-1),5);
                    newCoord1[0] = coord1[0];
                    newCoord2[0] = coord2[0];
                }
                else {
                    if (coord1[1] == coord2[1]) {
                        newCoord1[0] = mod((coord1[0]-1),5);
                        newCoord2[0] = mod((coord2[0]-1),5);
                        newCoord1[1] = coord1[1];
                        newCoord2[1] = coord2[1];
                    }
                    else {
                        newCoord1[0] = coord1[0];
                        newCoord2[0] = coord2[0];
                        newCoord1[1] = coord2[1];
                        newCoord2[1] = coord1[1];
                    }
                }
                newText.Append(key[newCoord1[0]][newCoord1[1]]).Append(key[newCoord2[0]][newCoord2[1]]);
            }
            return newText.ToString();
        }

        public Key child_key() {
            Key childKey = this;
            Random rnd = new Random();
            int x1 = new int();
            int x2 = new int();
            int y1 = new int();
            int y2 = new int();
            int rndNum = rnd.Next(0,50);
            char[] tempArr = new char[5];
            char tempChar = new char();
            switch (rndNum) {
                case 0:
                    Array.Reverse(childKey.key);
                    break;
                case 1:
                    y1 = rnd.Next(0,5);
                    y2 = rnd.Next(0,5);
                    tempArr = childKey.key[y1];
                    childKey.key[y1] = childKey.key[y2];
                    childKey.key[y1] = tempArr;
                    break;
                case 2:
                    x1 = rnd.Next(0,5);
                    x2 = rnd.Next(0,5);
                    for (int j=0; j<=4; j++) {
                        tempChar = childKey.key[j][x1];
                        childKey.key[j][x1] = childKey.key[j][x2];
                        childKey.key[j][x2] = tempChar;
                    }
                    break;
                case 3:
                    Array.Reverse(childKey.key);
                    for (int j = 0; j<=4; j++) {
                        Array.Reverse(childKey.key[j]);
                    }
                    break;
                case 4:
                    for (int j = 0; j<=4; j++) {
                        Array.Reverse(childKey.key[j]);
                    }
                    break;
                default:
                    x1 = rnd.Next(0,5);
                    x2 = rnd.Next(0,5);
                    y1 = rnd.Next(0,5);
                    y2 = rnd.Next(0,5);
                    tempChar = childKey.key[y1][x1];
                    childKey.key[y1][x1] = childKey.key[y2][x2];
                    childKey.key[y2][x2] = tempChar;
                    break;
            }
            return childKey;
        }
        public char[][] GetKey() {
            return this.key;
        } 
    }

    class ngram_score {
        Dictionary<string, int> ngramsFile;
        Dictionary<string, double> ngrams;
        string[] lines;
        Int64 N;
        double floor;
        public ngram_score(string ngramfile) {
            ngramsFile = new Dictionary<string,int>();
            ngrams = new Dictionary<string, double>();
            lines = File.ReadAllLines(ngramfile);
            for (int i=0; i<lines.Length; i++) {
                ngramsFile.Add(lines[i].Split(" ")[0],Convert.ToInt32(lines[i].Split(" ")[1]));
            }
            N = 0;
            foreach (var item in ngramsFile) {
                N += item.Value;
            }
            foreach (var item in ngramsFile) {
                ngrams.Add(item.Key, Math.Log10((double)item.Value/(double)N));
            }
            floor = Math.Log10(0.01/(double)N);
        }
        public double score(string text) {
            double score = 0;
            for (int i = 0; i<=(text.Length-4); i++) {
                if (ngrams.ContainsKey(text.Substring(i,4))) {
                    score += ngrams[text.Substring(i,4)];
                }
                else {
                    score += floor;
                }
            }
            return score;
        }
    }

    class Program {
        static void Main(string[] args) {
            string[] file = File.ReadAllLines(@"C:\Users\harry_000\Documents\Cipher-Challenge-2018\C#\CipherText.txt");
            string ciphText = file[3];
            double TEMP = Convert.ToDouble(file[0].Split(' ')[1]);
            double STEP = Convert.ToDouble(file[1].Split(' ')[1]);
            int COUNT = Convert.ToInt32(file[2].Split(' ')[1]);
            Key parent = new Key();
            Key child = parent;
            Key best = parent;
            ngram_score fitness = new ngram_score(@"C:\Users\harry_000\Documents\Cipher-Challenge-2018\english_quadgrams.txt");
            string parText = parent.playfair_solve(ciphText);
            double parFit = fitness.score(parText);
            double bestFit = parFit;
            double dF;
            double chiFit;
            string chiText;
            int c;
            Decision dcs = new Decision();
            while (TEMP>=0) {
                Console.WriteLine("Temp: " + TEMP);
                for (c = COUNT; c>=0; c--) {
                    child = parent.child_key();
                    chiText = child.playfair_solve(ciphText);
                    chiFit = fitness.score(chiText);
                    dF = parFit-chiFit;
                    if (dF > 0) {
                        parent = child;
                        parFit = chiFit;
                    }
                    else {
                        if (dcs.Make(Math.Exp(dF/TEMP))) {
                            parent = child;
                            parFit = chiFit;
                        }
                    }
                    if (chiFit > bestFit) {
                        best = child;
                        bestFit = chiFit;
                    }
                }
                TEMP -= STEP;
            }
            Console.Clear();
            Console.WriteLine("Plaintext: " + best.playfair_solve(ciphText));
            Console.WriteLine("Key: " + best.GetKey() + "; Fitness: " + bestFit);
            Console.Beep();
        }
    }
}