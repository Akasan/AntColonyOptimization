#pragma once

#include <cstdio>
#include <vector>

using namespace std;

class Agent{
public:
		int length = 0;
		int id;
		int rank;
		int* route;

		Agent(int id, int city_num);
        ~Agent();

		void set_rank(int);
		void reset_info(void);
		int get_last_city(void);
		int get_length(void);
private:
};

inline void Agent::set_rank(int rank){
	this->rank = rank;
}

inline int Agent::get_length(void){
	return this->length;
}
