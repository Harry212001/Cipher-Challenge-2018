import math

cipherText = input('Input ciphertext: ')
IoC = [0]*50
def ic(inputText):
    letterCounts = [0]*26
    ioc = 0
    for lett in inputText:
        letterCounts[ord(lett)-65] += 1
    for i in range(26):
        ioc += letterCounts[i]*(letterCounts[i]-1)
    return ioc/(len(inputText)*(len(inputText)-1))

for i in range(1,50):
    splitText = ['']*i
    for j in range(i):
        for k in range(math.ceil(len(cipherText)/i)):
            if j+i*k < len(cipherText):
                splitText[j] += cipherText[j+k*i]
        print(splitText[j])
    for j in range(i):
        IoC[i] += ic(splitText[j])
    IoC[i] = IoC[i]/i
print(IoC)