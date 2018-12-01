import random
import math
from math import log10


def decision(probability):
    return random.random() < probability


def index_2d(myList, v):
    for subList in myList:
        if v in subList:
            return(myList.index(subList),subList.index(v))


def random_key():
    key = [['' for x in range(5)] for y in range(5)]
    numlist = [x for x in range(65,91)]
    numlist.remove(74)
    for j in range(5):
        for i in range(5):
            num = random.choice(numlist)
            key[j][i] = chr(num)
            numlist.remove(num)
    return(key)


def playfair_solve(text, grid):
    newtext = ''
    for i in range(len(text)//2):
        c1 = index_2d(grid, text[i*2])
        c2 = index_2d(grid, text[i*2+1])
        nc1 = [0 for x in range(2)]
        nc2 = [0 for x in range(2)]
        if c1[1] == c2[1]:
            nc1[0] = (c1[0]-1)%5
            nc1[1] = c1[1]
            nc2[0] = (c2[0]-1)%5
            nc2[1] = c2[1]
        elif c1[0] == c2[0]:
            nc1[1] = (c1[1] - 1) % 5
            nc1[0] = c1[0]
            nc2[1] = (c2[1] - 1) % 5
            nc2[0] = c2[0]
        else:
            nc1[0] = c1[0]
            nc1[1] = c2[1]
            nc2[0] = c2[0]
            nc2[1] = c1[1]
        newlett1 = grid[nc1[0]][nc1[1]]
        newlett2 = grid[nc2[0]][nc2[1]]
        newtext += newlett1 + newlett2
    return(newtext)


def child_key(key):
    rand = random.randint(0,5)
    if rand == 0: #swap two letters
        x1 = random.randint(0,4)
        x2 = random.randint(0,4)
        y1 = random.randint(0,4)
        y2 = random.randint(0,4)
        temp = key[y1][x1]
        key[y1][x1] = key[y2][x2]
        key[y2][x2] = temp
    elif rand == 1: #swap 2 rows
        row1 = random.randint(0,4)
        row2 = random.randint(0,4)
        temp = key[row1]
        key[row1] = key[row2]
        key[row2] = temp
    elif rand == 2: #swap 2 columns
        col1 = random.randint(0,4)
        col2 = random.randint(0,4)
        for j in range(5):
            temp = key[j][col1]
            key[j][col1] = key[j][col2]
            key[j][col2] = temp
    elif rand == 3: #reverse key
        key.reverse()
        for row in key:
            row.reverse()
    elif rand == 4: #flip left to right
        for row in key:
            row.reverse()
    elif rand == 5: #flip top to bottom
        key.reverse()
    return(key)


class ngram_score(object):
    def __init__(self,ngramfile,sep=' '):
        #load a file containing ngrams and counts, calculate log probabilities
        self.ngrams = {}
        file = open(ngramfile, 'r')
        for line in file:
            key,count = line.split(sep)
            self.ngrams[key] = int(count)
        self.L = len(key)
        self.N = sum(self.ngrams.values())
        #calculate log probabilities
        for key in self.ngrams.keys():
            self.ngrams[key] = log10(float(self.ngrams[key])/self.N)
        self.floor = log10(0.01/self.N)

    def score(self,text):
        #compute the score of text
        score = 0
        ngrams = self.ngrams.__getitem__
        for i in range(len(text)-self.L+1):
            if text[i:i+self.L] in self.ngrams: score += ngrams(text[i:i+self.L])
            else: score += self.floor
        return(score)


ciphText = input('Input ciphertext: ')
TEMP = float(input('Input starting temperature: '))
STEP = float(input('Input step: '))
COUNT = int(input('Input count: '))
parent = random_key()
print(parent)
fitness = ngram_score('english_quadgrams.txt')
parText = playfair_solve(ciphText, parent)
parFit = fitness.score(parText)
while TEMP >= 0:
    for C in range(COUNT,0,-1):
        child = child_key(parent)
        chiText = playfair_solve(ciphText, child)
        chiFit = fitness.score(chiText)
        dF = parFit - chiFit
        if dF > 0:
            parent = child
            parFit = chiFit
            print(parent[0])
            print(parent[1])
            print(parent[2])
            print(parent[3])
            print(parent[4])
        else:
            if decision(math.exp(dF/TEMP)):
                parent = child
                parFit = chiFit
                print(parent[0])
                print(parent[1])
                print(parent[2])
                print(parent[3])
                print(parent[4])
        print(C)
    TEMP = TEMP - STEP
    print(TEMP)
print(parent)
print(playfair_solve(ciphText, parent))