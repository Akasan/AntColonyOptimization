import sys
sys.path.append("../src")
import argparse
from max_min_ant_system import MaxMinAntSystem

def main():
    mmas = MaxMinAntSystem(city_information_filename="./dataset/kroA100.tsp",
                           agent_num=100)

    for i in range(100):
        mmas.reset_agent()
        mmas.generate_route()
        mmas.calculate_distance()
        mmas.update_pheromone()
        mmas.saturate_pheromone()


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    main()
