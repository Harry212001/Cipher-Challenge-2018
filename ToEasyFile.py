fileop = open("./Original.txt", "r")
newtext = ""
lines = fileop.readlines()
for line in lines:
    for lett in line:
        if ((lett >= 'A' and lett <= 'Z') or (lett >= 'a' and lett <= 'z') or lett == ' ') and not (lett == 'J' or lett == 'j'):
            newtext += lett
    newtext += " "
newtext = newtext.upper()
fileop.close()
newfile = open("./Words.txt", "w")
newfile.writelines(newtext)