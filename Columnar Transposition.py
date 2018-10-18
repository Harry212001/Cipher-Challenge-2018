import PyDictionary
from PyDictionary import PyDictionary
import math
import itertools

def permutations(inputList):
    return itertools.permutations(inputList)

dictionary = PyDictionary()
def means(word):
    return dictionary.meaning(word)

cipherText = input('Input ciphertext: ')

for i in range(1,len(cipherText)):
    if math.ceil(len(cipherText)/i) == math.floor(len(cipherText)/i):
        textCol = [''] * math.ceil(len(cipherText)/i)
        for j in range(math.ceil(len(cipherText)/i)):
            textCol[j] = cipherText[j*math.ceil(len(cipherText)/(len(cipherText)/i)):(j+1)*math.ceil(len(cipherText)/(len(cipherText)/i))]
        print(textCol)
        newText = ''
        nums = list(range(i))
        print(nums)
        if nums == [0]:
            perms = [[0]]
        else:
            perms = permutations(nums)
        print(perms)
        for permutation in perms:
            print(permutation)
            for j in range(math.ceil(len(cipherText)/(len(cipherText)/i))):
                print(j,math.ceil(len(cipherText)/(len(cipherText)/i)))
                for num in permutation:
                    print(num)
                    newText += textCol[num][j]
        for j in range(10):
            if means(newText[:j]) != None:
                print(newText)
