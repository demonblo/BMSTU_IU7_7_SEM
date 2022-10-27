#include <iostream>
#include <cstdlib>
#include <fstream>
#define ROTER_LENGTH 256

using namespace std;
class Roter {
    int fAlph [ROTER_LENGTH];
    int sAlph [ROTER_LENGTH];
    int rotationsLeft;
public:
    Roter();
    int getFAlphEl(int i){
        return fAlph[i];
    }

    int getSAlphEl(int i){
        return sAlph[i];
    }

    void setRotor (Roter toCopy) {
        for (int i = 0; i < ROTER_LENGTH; i++) {
            int temp = toCopy.getFAlphEl(i);
            fAlph[i] = temp;
            temp = toCopy.getSAlphEl(i);
            sAlph[i] = temp;
        }

    }

    void rotateRoter() {
        int tmp = fAlph[ROTER_LENGTH - 1];
        fAlph[ROTER_LENGTH - 1] = fAlph[0];
        for (int i = 0; i < ROTER_LENGTH - 2; i++) {
            fAlph[i] = fAlph[i + 1];
        }
        fAlph[ROTER_LENGTH - 2] = tmp;

    }

    void getAlph() {
        for (int i = 0; i < ROTER_LENGTH; i++){
            std::cout << fAlph[i] << " " << sAlph[i] << endl;
        }
    }

    int getIndexByValue(int value) {
        for (int i = 0; i < ROTER_LENGTH; i++) {
            if (sAlph[i] == value) {
                return i;
            }
        }
    }

    int getValueByIndex(int index) {
        return fAlph[index];
    }

    int getIndexByValueRef(int value) {
        for (int i = 0; i < ROTER_LENGTH; i++) {
            if (fAlph[i] == value) {
                return i;
            }
        }
    }

    int decrementRotations() {
        if (rotationsLeft == 1) {
            rotationsLeft = ROTER_LENGTH;
            return 1;
        }
        else {
            rotationsLeft -= 1;
            return 0;
        }
    }

};

Roter::Roter() {
    rotationsLeft = ROTER_LENGTH;
    for (int i = 0; i < ROTER_LENGTH; i++) {
        fAlph[i] = i;
        sAlph[i] = i;
    }

    srand(time(NULL));
    for (int i = 0; i < ROTER_LENGTH; i++) {
        int j = rand() % (ROTER_LENGTH - i);

        int tmp = fAlph[j];
        fAlph[j] = fAlph[i];
        fAlph[i] = tmp;
    }
}

class Reflector {
    int fAlph [ROTER_LENGTH];
    int sAlph [ROTER_LENGTH];
public:
    Reflector();
    void getAlph() {
        for (int i = 0; i < ROTER_LENGTH; i++){
            std::cout << fAlph[i] << " " << sAlph[i] << endl;
        }
    }

    int getIndexByValue(int value) {
        for (int i = 0; i < ROTER_LENGTH; i++) {
            if (sAlph[i] == value) {
                return i;
            }
        }
    }

    int getValueByIndex(int index) {
        return fAlph[index];
    }
};

Reflector::Reflector() {
    for (int i = 0; i < ROTER_LENGTH; i++) {
        fAlph[i] = i;
        sAlph[i] = i;
    }

    for (int i = 0; i < (ROTER_LENGTH / 2); i++) {
        fAlph[i] = i + (ROTER_LENGTH / 2);
        fAlph[i + (ROTER_LENGTH / 2)] = i;
    }
}

int main() {
    Roter fRoter;
    Roter sRoter;
    Roter tRoter;
    Roter fRoterCheck;
    Roter sRoterCheck;
    Roter tRoterCheck;
    Reflector fReflector;
    fRoterCheck.setRotor(fRoter);
    sRoterCheck.setRotor(sRoter);
    tRoterCheck.setRotor(tRoter);
    //ifstream fin("VALORANT.exe");
    //ofstream fout("VALORANT_encr.exe");
    //ifstream fin("kek.mp4");
    //ofstream fout("kek_encr.mp4");
    //ifstream fin("test.txt");
    ifstream fin("test.txt");
    ofstream fout("test_encr.txt");

    char sim;
    int encrSim;
    int buff[409600];
    int i = 0;
    if (fin.is_open())
    {
        while (fin.get(sim))
        {
            encrSim = (int)sim;
            //cout << "|" << encrSim << " " << sim << "|";

            //Процесс шифрования символа
            int temp = fRoter.getValueByIndex(fRoter.getIndexByValue(encrSim)); //Зашифрованный символ на выходе из первого Ротера
            temp = sRoter.getValueByIndex(sRoter.getIndexByValue(temp)); //Зашифрованный символ на выходе из второго Ротера
            temp = tRoter.getValueByIndex(tRoter.getIndexByValue(temp)); //Зашифрованный символ на выходе из третьего Ротера
            temp = fReflector.getValueByIndex(fReflector.getIndexByValue(temp)); //Зашифрованный символ на вы ходе из Рефлектора
            temp = tRoter.getIndexByValueRef(temp); //Зашифрованный символ при обратном ходе на третьем ротере
            temp = sRoter.getIndexByValueRef(temp); //Зашифрованный символ при обратном ходе на втором ротере
            temp = fRoter.getIndexByValueRef(temp); //Зашифрованный символ при обратном ходе на первом ротере

            //Прокрутка ротера/ротеров после шифрования одного символа
            fRoter.rotateRoter();
            if (fRoter.decrementRotations()) {
                sRoter.rotateRoter();
                if (sRoter.decrementRotations()) {
                    tRoter.rotateRoter();
                }
            }

            char encrSimCh = static_cast<char>(temp);
            buff[i] = temp;
            i += 1;
            //cout << "/" << temp << " " << encrSimCh << "/";
            fout << encrSimCh;
        }
        if (i == 0){
            cout << "Файл пуст..." << endl;
            fin.close();
            fout.close();
            return 4;
        }
        fin.close();
        fout.close();
        //cout << endl;
    }
    else {
        cout << "Невозможно открыть файл!" << endl;

        return 3;
    }

    //Проверка на то что, если зашифрованный набор символов "прогнать" через исходную Энигму, то получится исходный набор символов
    //fout.open("VALORANT_encr_check.exe");
    //fout.open("kek_encr_check.mp4");
    fout.open("test_encr_check.txt");
    int j = 0;
    while (j < i) {
        encrSim = buff[j];
        //cout << "|" << buff[j] << "|";

        //Процесс шифрования символа
        int temp = fRoterCheck.getValueByIndex(
                fRoterCheck.getIndexByValue(encrSim)); //Зашифрованный символ на выходе из первого Ротера
        temp = sRoterCheck.getValueByIndex(
                sRoterCheck.getIndexByValue(temp)); //Зашифрованный символ на выходе из второго Ротера
        temp = tRoterCheck.getValueByIndex(
                tRoterCheck.getIndexByValue(temp)); //Зашифрованный символ на выходе из третьего Ротера
        temp = fReflector.getValueByIndex(
                fReflector.getIndexByValue(temp)); //Зашифрованный символ на вы ходе из Рефлектора
        temp = tRoterCheck.getIndexByValueRef(temp); //Зашифрованный символ при обратном ходе на третьем ротере
        temp = sRoterCheck.getIndexByValueRef(temp); //Зашифрованный символ при обратном ходе на втором ротере
        temp = fRoterCheck.getIndexByValueRef(temp); //Зашифрованный символ при обратном ходе на первом ротере

        //Прокрутка ротера/ротеров после шифрования одного символа
        fRoterCheck.rotateRoter();
        if (fRoterCheck.decrementRotations()) {
            sRoterCheck.rotateRoter();
            if (sRoterCheck.decrementRotations()) {
                tRoterCheck.rotateRoter();
            }
        }

        char encrSimCh = static_cast<char>(temp);
        //cout << encrSimCh;
        fout << encrSimCh;
        j += 1;

    }

    return 0;
}