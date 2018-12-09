ciphText = input('Input text: ')
plainText = ''
for lett in ciphText:
    if (lett >= 'A' and lett <= 'Z') or lett == ' ':
        plainText += lett
print(plainText)