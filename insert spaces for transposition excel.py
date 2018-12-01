text = input("Input ciphertext: ")
length = int(input("Input length: "))
char = input("Char to insert: ")
newtext = ""
for i in range(int(len(text)/length)):
    newtext += text[i*length:(i+1)*length] + char
print(newtext)