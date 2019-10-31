#include <iostream>
#include <unistd.h>
#include <string>
#include "config.hpp"

using namespace std;

string get_benchmark(){
    char dir[255];
    int separate_pos;
    string pre_result, result;
    getcwd(dir, 255);
    pre_result = dir;
    separate_pos = pre_result.find("AntColonyOptimization");
    result = pre_result.substr(0, separate_pos) + "AntColonyOptimization/prepare_dataset/csv_dataset/" + BENCHMARK_NAME;
    return result;
}
