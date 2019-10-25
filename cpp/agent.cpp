#include "agent.hpp"
#include <stdio.h>

Agent::Agent(int id, int city_num){
	this->id = id;
	//this->route = vector<int>(city_num);
}

void Agent::reset_info(void){
	this->length = 0;
}
