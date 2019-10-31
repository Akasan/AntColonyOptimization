cmake_minimum_required(VERSION 2.8)
project(AntColonyOptimization CXX)

add_library(max_min_ant_system STATIC max_min_ant_system.cpp)
add_executable(main.o main.cpp)
target_link_libraries(main.o max_min_ant_system)
