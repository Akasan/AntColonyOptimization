#!/bin/bash

gcc -c agent.cpp
gcc -c main.cpp
gcc -o main main.o agent.o
