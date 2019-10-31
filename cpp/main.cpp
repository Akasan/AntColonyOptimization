#include <cstdio>
#include <iostream>
#include "ant_system.hpp"
#include "config.hpp"

#define ALPHA 1.0
#define BETA 3.0
#define RHO 0.98
#define PHEROMONE_INIT 1.0

using namespace std;

int main(void)
{
    AntSystem ant_system;
    int i;
    for(i=0; i<ITERATION; i++){
        ant_system.generate_route();
        ant_system.calculate_distance();
        ant_system.update_pheromone();
    }
    return 0;
}
