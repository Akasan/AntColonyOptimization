int main()
{
    ifstream ifs("kroA100.csv");
    int count = 0;
    int** array = new int*[100];

    for(int i=0; i<100; i++){
        array[i] = new int[100];
    }

    string line;
    while (getline(ifs, line)) {
        
        vector<string> strvec = split(line, ',');
        
        for (int i=1; i<strvec.size();i++){
            array[count][i-1] = stoi(strvec.at(i));
        }
        count += 1;
    }

    for(int i=0; i<100; i++){
        printf("%d %d\n", array[i][0], array[i][1]);
    }

    for(int i=0; i<100; i++){
        delete[] array[i];
    }
    delete[] array;

} 
