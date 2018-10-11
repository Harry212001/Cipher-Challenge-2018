def letterPlus(letter,i):
    letterNum = ord(letter)
    letterNum = (letterNum+i-65)%26 + 65
    return chr(letterNum)
cipherText = input('Input cipher text: ')
englishLetterFrequencies = {"A":8.12,"B":1.49,"C":2.71,"D":4.32,"E":12.02,"F":2.30,"G":2.03,"H":5.92,"I":7.31,"J":0.10,"K":0.69,"L":3.98,"M":2.61,"N":6.95,"O":7.68,"P":1.82,"Q":0.11,"R":6.02,"S":6.28,"T":9.10,"U":2.88,"V":1.11,"W":2.09,"X":0.17,"Y":2.11,"Z":0.07}
trialLetterFrequencies = {"A":0,"B":0,"C":0,"D":0,"E":0,"F":0,"G":0,"H":0,"I":0,"J":0,"K":0,"L":0,"M":0,"N":0,"O":0,"P":0,"Q":0,"R":0,"S":0,"T":0,"U":0,"V":0,"W":0,"X":0,"Y":0,"Z":0}
spe = [0]*26
for i in range(26):
    plainText = ''
    for alp in trialLetterFrequencies:
        trialLetterFrequencies[alp] = 0
    for let in cipherText:
        if ord(let) >=65 and ord(let) <= 90:
            plainText += letterPlus(let,i)
    for let in plainText:
        print(let)
        trialLetterFrequencies[let] += 1
    for alp in trialLetterFrequencies:
        trialLetterFrequencies[alp] = (trialLetterFrequencies[alp]/len(plainText))*100
        spe[i] += abs((trialLetterFrequencies[alp]-englishLetterFrequencies[alp])*100/englishLetterFrequencies[alp])
key = spe.index(min(spe))
newText = ''
for let in cipherText:
    if ord(let) >= 65 and ord(let) <= 90:
        newText += letterPlus(let,key)
print(key)
print(newText)