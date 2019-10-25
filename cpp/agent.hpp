#ifndef AGENT_HPP
#define AGENT_HPP

#include <stdio.h>
#include <vector>

using namespace std;

class Agent{
public:
		int length = 0;
		int id;
		int rank;

		Agent(int id, int city_num);

		void set_rank(int);
		void reset_info(void);
		int get_last_city(void);
		int get_length(void);
private:
		//vector<int> route;

};

inline void Agent::set_rank(int rank){
	this->rank = rank;
}

inline int Agent::get_length(void){
	return this->length;
}

#endif
