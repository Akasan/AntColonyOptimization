#include <cstdio>
#include <iostream>
#include "max_min_ant_system.hpp"
#include "config.hpp"

#define ALPHA 1.0
#define BETA 3.0
#define RHO 0.98
#define PHEROMONE_INIT 1.0

using namespace std;

int main(void)
{
    MaxMinAntSystem mmas;
    int i;
    for(i=0; i<ITERATION; i++){
        mmas.generate_route();
        mmas.calculate_distance();
        mmas.update_pheromone();
        mmas.modify_pheromone();
    }
    return 0;
}
