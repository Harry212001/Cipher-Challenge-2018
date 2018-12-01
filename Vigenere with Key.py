import math

cipherText = input('Input ciphertext: ')
iKey = input('Input key: ')
newText = ''

for i in range(len(cipherText)):
    keyIndex = i%len(iKey)
    shift = ord(iKey[keyIndex])-65
    newText += chr(65+((ord(cipherText[i])+shift-65)%26))
print(newText)