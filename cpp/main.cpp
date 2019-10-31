#include <cstdio>
#include <iostream>
#include "ant_system.hpp"

#define ALPHA 1.0
#define BETA 3.0
#define RHO 0.98
#define PHEROMONE_INIT 1.0

using namespace std;

int main(void)
{
    AntSystem ant_system;
    ant_system.generate_route();
    return 0;
}
