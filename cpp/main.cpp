#include <cstdio>
#include <iostream>
#include "agent.hpp"

#define ALPHA 1.0
#define BETA 3.0
#define RHO 0.98
#define PHEROMONE_INIT 1.0

using namespace std;

int main(void)
{
   Agent agent(10, 10);
   cout<< agent.get_length() << endl;
   
   return 0;
}
