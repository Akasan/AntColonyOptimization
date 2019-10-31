#include "ant_system.hpp"
#include "config.hpp"
#include <iostream>
#include <ctime>
#include <cstdlib>
#include <cstdio>
#include <fstream>
#include <string>
#include <sstream>
#include <vector>
#include <cmath>

using namespace std;

vector<string> split(string& input, char delimiter)
{
    istringstream stream(input);
    string field;
    vector<string> result;
    while (getline(stream, field, delimiter)) {
        result.push_back(field);
    }
    return result;
}


/******************************************************************************************/
Agent::Agent(){
    this->route = new int[CITY_NUM];
    for(int i=0; i<CITY_NUM; i++)    this->route[i] = 0;
}
 
Agent::~Agent(){
    delete[] this->route;
}

void Agent::reset_info(void){
    this->length = 0;
    this->registered_city_num = 1;
    for(int i=0; i<CITY_NUM; i++)    this->route[i] = 0;
    this->route[0] = this->id;
}


/******************************************************************************************/
AntSystem::AntSystem(){
    float dis;
    int dx, dy, i, j;
    int** city_info = new int*[CITY_NUM];
    int count = 0;

    this->agent = new Agent[AGENT_NUM];
    for(i=0; i< AGENT_NUM; i++)  this->agent[i].set_id(i);
    for(i=0; i<CITY_NUM; i++){
        this->distance_arr[i] = new float[CITY_NUM];
        this->pheromone_arr[i] = new float[CITY_NUM];
        city_info[i] = new int[2];
    }

    ifstream ifs("../kroA100.csv");
    string line;
    
    while(getline(ifs, line)){
        vector<string> strvec = split(line, ',');
        for(i=1; i<strvec.size(); i++)  city_info[count][i-1] = stoi(strvec.at(i));
        count++;
    }

    for(i=0; i<CITY_NUM; i++){
        this->distance_arr[i][i] = 0.0;
        for(j=i+1; j<CITY_NUM; j++){
            dx = city_info[i][0] - city_info[j][0];
            dy = city_info[i][1] - city_info[j][1];
            dis = sqrt(dx*dx + dy*dy);
            this->distance_arr[i][j] = dis;
            this->distance_arr[j][i] = dis;
            this->pheromone_arr[i][j] = PHEROMONE_INIT;
            this->pheromone_arr[j][i] = PHEROMONE_INIT;
        }
    }

    for(i=0; i<CITY_NUM; i++) delete[] city_info[i];
    delete[] city_info;
}

AntSystem::~AntSystem(){
    delete[] this->agent;
    for(int i=0; i<CITY_NUM; i++){
        delete[] this->distance_arr[i];
        delete[] this->pheromone_arr[i];
    }
    delete[] this->distance_arr;
    delete[] this->pheromone_arr;
}


void AntSystem::generate_route(){
    int i, j, k;
    float prob, prob_sum, threshold;
    int last_city; 
    srand((unsigned)time(NULL));

    for(i=0; i<AGENT_NUM; i++){
        this->agent[i].reset_info();
        bool flag[CITY_NUM] = {false};
        flag[this->agent[i].id] = true;

        for(j=1; j<CITY_NUM; j++){
            prob = 0.0;
            prob_sum = 0.0;
            last_city = this->agent[i].last_city;

            for(k=0; k<CITY_NUM; k++){
                if(flag[k] == false){
                    prob_sum += pow(this->pheromone_arr[last_city][k], ALPHA) * pow(1.0 / this->distance_arr[last_city][k], BETA);
                }
            }

            threshold = float(rand()) / RAND_MAX;

            for(k=0; k<CITY_NUM; k++){
                if(flag[k] == false){
                    prob += pow(this->pheromone_arr[last_city][k], ALPHA) * pow(1.0 / this->distance_arr[last_city][k], BETA) / prob_sum;
                    if(prob > threshold){
                        this->agent[i].register_city(k);
                        flag[k] = true;
                        break;                
                    }
                }
            }
        }
    }
}
