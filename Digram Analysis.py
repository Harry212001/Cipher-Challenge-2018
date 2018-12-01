text = input("Input ciphertext: ")
frequencies = {}
if len(text)%2 != 0:
    text += " "

for i in range(len(text)//2):
    if text[i*2:(i+1)*2] in frequencies:
        frequencies[text[i*2:(i+1)*2]] += 1
    else:
        frequencies[text[i*2:(i+1)*2]] = 1#

print(frequencies)