#include "agent.hpp"
#include <iostream>
#include <cstdio>

using namespace std;

Agent::Agent(int id, int city_num){
	this->id = id;
    this->route = new int[city_num];
}

Agent::~Agent(){
	delete[] this->route;
}

void Agent::reset_info(void){
	this->length = 0;
}


