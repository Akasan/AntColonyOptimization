#ifndef ANT_SYSTEM_H
#define ANT_SYSTEM_H

#include <cstdio>
#include <iostream>
#include "config.hpp"

using namespace std;

class Agent{
public:
    float length = 0;
    int registered_city_num = 1;
    int id;
    int rank;
    int last_city;
    int* route;

    Agent();
    ~Agent();

    void set_id(int);
    void set_rank(int);
    void set_length(float);
    void reset_info(void);
    void register_city(int);
    void print_route(void);
    int get_last_city(void);
    int get_length(void);
};

inline void Agent::set_rank(int rank){
    this->rank = rank;
};

inline void Agent::set_length(float _length){
    this->length = _length;
};

inline void Agent::set_id(int _id){
    this->id = _id;
    this->last_city = _id;
};

inline int Agent::get_length(void){
    return this->length;
};

inline void Agent::print_route(void){
    for(int i=0; i<CITY_NUM; i++) cout << this->route[i] << endl;
}

inline void Agent::register_city(int city){
    this->route[this->registered_city_num] = city;
    this->registered_city_num++;
    this->last_city = city;
}

/*****************************************************************************************/

class MaxMinAntSystem{
public:
    Agent* agent;
    float best;
    int iteration=0;
    float** distance_arr = new float*[CITY_NUM]; 
    float** pheromone_arr = new float*[CITY_NUM];
    MaxMinAntSystem();
    ~MaxMinAntSystem();

    void generate_route();
    void update_pheromone();
    void modify_pheromone();
    void calculate_distance();
};

#endif
