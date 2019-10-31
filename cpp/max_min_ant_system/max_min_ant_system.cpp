#include "max_min_ant_system.hpp"
#include "config.hpp"
#include "utils.hpp"
#include <iostream>
#include <unistd.h>
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
    this->last_city = this->id;
}


/******************************************************************************************/
MaxMinAntSystem::MaxMinAntSystem(){
    float dis;
    float dx, dy;
    int i, j;
    float** city_info = new float*[CITY_NUM];
    int count = 0;

    this->agent = new Agent[AGENT_NUM];
    for(i=0; i<AGENT_NUM; i++)  this->agent[i].set_id(i);
    for(i=0; i<CITY_NUM; i++){
        this->distance_arr[i] = new float[CITY_NUM];
        this->pheromone_arr[i] = new float[CITY_NUM];
        city_info[i] = new float[2];
    }

    ifstream ifs(get_benchmark());
    string line;
    
    while(getline(ifs, line)){
        vector<string> strvec = split(line, ',');
        for(i=0; i<strvec.size(); i++)  city_info[count][i] = stoi(strvec.at(i));
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

MaxMinAntSystem::~MaxMinAntSystem(){
    delete[] this->agent;
    for(int i=0; i<CITY_NUM; i++){
        delete[] this->distance_arr[i];
        delete[] this->pheromone_arr[i];
    }
    delete[] this->distance_arr;
    delete[] this->pheromone_arr;
}


void MaxMinAntSystem::generate_route(){
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

void MaxMinAntSystem::calculate_distance(){
    int i, j, base, comp;
    float distance, best;

    for(i=0; i<AGENT_NUM; i++){
        distance = 0.0;
        for(j=0; j<CITY_NUM; j++){
            base = this->agent[i].route[j];
            if (j == CITY_NUM - 1){
                comp = this->agent[i].route[0];
                distance += this->distance_arr[base][comp];
            }
            else{
                comp = this->agent[i].route[j+1];
                distance += this->distance_arr[base][comp];
            }
        }
        this->agent[i].set_length(distance);
        if (i==0){
            best = distance;
            this->best_agent = 0;
        }
        else{
            if(best > distance){
                best = distance;
                this->best_agent = i;
            }
        }
    }
    if(this->is_first){
        this->best = best;
        this->is_first = false;
    }
    else{
        if (this->best > best) this->best = best;
    }
    cout << "iterartion : " << this->iteration << "\tcurrent best : " << best<< "\t all best: " << this->best << endl;
    this->iteration++;
}

void MaxMinAntSystem::update_pheromone(){
    int i, j, base, comp;
    float add_pheromone;
    
    for(i=0; i<CITY_NUM; i++){
        for(j=i+1; j<CITY_NUM; j++){
            this->pheromone_arr[i][j] = this->pheromone_arr[i][j] * RHO;
            this->pheromone_arr[j][i] = this->pheromone_arr[i][j];
        }
    }

    add_pheromone = 1.0 / this->agent[this->best_agent].get_length();
    for(j=0; j<CITY_NUM; j++){
        base = this->agent[this->best_agent].route[j];
        if (j == CITY_NUM - 1){
            comp = this->agent[this->best_agent].route[0];
        }
        else{
            comp = this->agent[this->best_agent].route[j+1];
        }
        this->pheromone_arr[base][comp] += add_pheromone;
        this->pheromone_arr[comp][base] += add_pheromone;
    }
}


void MaxMinAntSystem::modify_pheromone(){
    int i, j, base, comp;
    float lower_bound, upper_bound;

    upper_bound = 1.0 / ((1 - RHO) * this->best);
    lower_bound = upper_bound * (1 - pow(0.05, 1.0 / CITY_NUM)) / ((CITY_NUM / 2.0 - 1) * pow(0.05, 1.0/CITY_NUM));
    
    for(i=0; i<CITY_NUM; i++){
        for(j=i+1; j<CITY_NUM; j++){
            if (this->pheromone_arr[i][j] > upper_bound){
                this->pheromone_arr[i][j] = upper_bound;
                this->pheromone_arr[j][i] = upper_bound;
            }
            else if (this->pheromone_arr[i][j] < lower_bound){
                this->pheromone_arr[i][j] = lower_bound;
                this->pheromone_arr[j][i] = lower_bound;
            }
        }
    }
}
