sub = input('Input the substitution alphabet: ')
substitutionAlphabet = {"A":'',"B":'',"C":'',"D":'',"E":'',"F":'',"G":'',"H":'',"I":'',"J":'',"K":'',"L":'',"M":'',"N":'',"O":'',"P":'',"Q":'',"R":'',"S":'',"T":'',"U":'',"V":'',"W":'',"X":'',"Y":'',"Z":''}
for lett in substitutionAlphabet:
    substitutionAlphabet[lett]=sub[ord(lett)-65]
cipherText = input('Input ciphertext: ')
plainText = ''
for lett in cipherText:
    if ord(lett) >= 65 and ord(lett) <= 90:
        plainText += substitutionAlphabet[lett]
print(plainText)