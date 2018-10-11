englishLetterFrequencies = {"A":8.12,"B":1.49,"C":2.71,"D":4.32,"E":12.02,"F":2.30,"G":2.03,"H":5.92,"I":7.31,"J":0.10,"K":0.69,"L":3.98,"M":2.61,"N":6.95,"O":7.68,"P":1.82,"Q":0.11,"R":6.02,"S":6.28,"T":9.10,"U":2.88,"V":1.11,"W":2.09,"X":0.17,"Y":2.11,"Z":0.07}
letterFrequencies = {"A":0,"B":0,"C":0,"D":0,"E":0,"F":0,"G":0,"H":0,"I":0,"J":0,"K":0,"L":0,"M":0,"N":0,"O":0,"P":0,"Q":0,"R":0,"S":0,"T":0,"U":0,"V":0,"W":0,"X":0,"Y":0,"Z":0}
errorList = {"A":0,"B":0,"C":0,"D":0,"E":0,"F":0,"G":0,"H":0,"I":0,"J":0,"K":0,"L":0,"M":0,"N":0,"O":0,"P":0,"Q":0,"R":0,"S":0,"T":0,"U":0,"V":0,"W":0,"X":0,"Y":0,"Z":0}
substitutionAlphabet = {"A":'',"B":'',"C":'',"D":'',"E":'',"F":'',"G":'',"H":'',"I":'',"J":'',"K":'',"L":'',"M":'',"N":'',"O":'',"P":'',"Q":'',"R":'',"S":'',"T":'',"U":'',"V":'',"W":'',"X":'',"Y":'',"Z":''}
CipherText = input('Input ciphertext: ')
for lett in CipherText:
    letterFrequencies[lett]+=1
for lett in englishLetterFrequencies:
    errorList = {"A": 0, "B": 0, "C": 0, "D": 0, "E": 0, "F": 0, "G": 0, "H": 0, "I": 0, "J": 0, "K": 0, "L": 0, "M": 0,
                 "N": 0, "O": 0, "P": 0, "Q": 0, "R": 0, "S": 0, "T": 0, "U": 0, "V": 0, "W": 0, "X": 0, "Y": 0, "Z": 0}
    for trial in letterFrequencies:
        errorList[trial]=abs(englishLetterFrequencies[lett]-letterFrequencies[trial])
    substitutionAlphabet[lett]=min(errorList, key=lambda k: errorList[k])
print(substitutionAlphabet)
plainText = ''
for lett in CipherText:
    if ord(let) >= 65 and ord(let) <= 90:
        plainText += substitutionAlphabet[lett]
print(plaintText)