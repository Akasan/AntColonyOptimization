#!/bin/bash

rm *.o
g++ -c agent.cpp
g++ -c ant_system.cpp
g++ -c main.cpp
g++ -o main main.o agent.o ant_system.o -std=c++17
