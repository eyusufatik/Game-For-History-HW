import json
import io

obj = {"questions":[]}
with io.open("sorular_raw.txt", "r", encoding='utf8') as question_file:
    temp = {}
    question = ""
    for line in question_file:
        if line.find("*") != -1:
            temp["answer"] = line[0]

        first_two = line[0:2]
        line = line.strip("* \n")
        if first_two == "A)":
            temp["question"] = question
            temp["A"] = line[3:]
        elif first_two == "B)":
            temp["B"] = line[3:]
        elif first_two == "C)":
            temp["C"] = line[3:]
        elif first_two == "D)":
            temp["D"] = line[3:]
        elif first_two == "E)":
            temp["E"] = line[3:]
            obj["questions"].append(temp)
            temp = {}
            question = ""
        elif line!="":
            question+=line+"\n"


with io.open("sorular.json", 'w', encoding='utf8') as json_file:
    json.dump(obj, json_file, ensure_ascii=False)