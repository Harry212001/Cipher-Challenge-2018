using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
        string keyword;
        char[][] key;
        public Key(string kw) {
            char tempChar;
            List<char> tempArr = kw.ToList();
            for (int i=0; i<tempArr.Count; i++) {
                tempChar = tempArr[i];
                tempArr[i] = '?';
                while (tempArr.Contains(tempChar)) {
                    tempArr.Remove(tempChar);
                }
                tempArr[i] = tempChar;
            }
            if (kw == "") {
                keyword = "";
            }
            else {
                keyword = string.Join("", tempArr);
            }
            generate_key();
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
            for (int i = 0; i<=ciphText.Length/2-1; i++) {
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

        void generate_key() {
            key = new char[5][];
            for (int i = 0; i <= 4; i++) {
            key[i] = new char[5];
            }
            List<int> numList = Enumerable.Range(65,26).ToList();
            numList.RemoveAt(9);
            for (int i = 0; i<=24; i++) {
                if (i<keyword.Length) {
                    key[i/5][i%5] = keyword[i];
                    numList.Remove(keyword[i]);
                }
                else {
                    key[i/5][i%5] = (char)numList[0];
                    numList.RemoveAt(0);
                }
            }
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
            string sample = File.ReadAllLines(@"C:\Users\harry_000\Documents\Cipher-Challenge-2018\Words.txt")[0];
            string[] words = sample.Split(' ');
            ngram_score fitness = new ngram_score(@"C:\Users\harry_000\Documents\Cipher-Challenge-2018\english_quadgrams.txt");
            Key bestkey = new Key("");
            string text = bestkey.playfair_solve(ciphText);
            double bestFit = fitness.score(text);
            double testFit;
            foreach (string word in words) {
                Key test = new Key(word);
                text = test.playfair_solve(ciphText);
                testFit = fitness.score(text);
                if (testFit>bestFit) {
                    bestkey = test;
                    bestFit = testFit;
                    Console.Clear();
                    Console.WriteLine(text);
                    Console.WriteLine(testFit);
                }
            }
            Console.WriteLine("Best text: " + bestkey.playfair_solve(ciphText));
            Console.WriteLine("Fit: " + bestFit);
            Console.Beep();
        }
    }
}