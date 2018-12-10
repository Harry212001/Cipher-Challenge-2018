using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Playfair2 {
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
        char[][] key;
        string keyword;

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

        
    }
}