ciphText = input('Input text: ')
plainText = ''
for lett in ciphText:
    if ord(lett) >= 65 and ord(lett) <= 90:
        plainText += lett
print(plainText)