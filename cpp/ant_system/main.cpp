#include <cstdio>
#include <iostream>
#include "ant_system.hpp"
#include "config.hpp"

using namespace std;

int main(void)
{
    AntSystem ant_system;
    int i;
    for(i=0; i<ITERATION; i++){
		cout << "Iteration : [" << i+1 << " / " << ITERATION << "]\t"; 
        ant_system.generate_route();
        ant_system.calculate_distance();
        ant_system.update_pheromone();
    }
    return 0;
}
