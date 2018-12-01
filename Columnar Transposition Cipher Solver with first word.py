import math
text = input("Input ciphertext: ")
word = input("Input first word (if known): ")
last = input("Input last word (if known): ")
for i in range(1,26):
    while math.floor(len(text)/i) != math.ceil(len(text)/i):
        text += " "
    textcols = [""]*i
    for j in range(i):
        for k in range(math.ceil(len(text)/i)):
            textcols[j] += text[k*i+j]

    sequence =
    for k in range(len(word)):
        for j in range(i):
            if textcols[j][k] == word[k]:
                count = 0

