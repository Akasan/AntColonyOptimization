cmake_minimum_required(VERSION 2.8)
project(AntColonyOptimization CXX)

add_library(ant_system STATIC ant_system.cpp)
add_executable(main.o main.cpp)
target_link_libraries(main.o ant_system)
