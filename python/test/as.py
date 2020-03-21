import sys
sys.path.append("../src")
sys.path.append("../../prepare_dataset")
import argparse
from ant_system import AntSystem

def main():
    ant_system = AntSystem(city_information_filename="/Users/akagawaoozora/Library/Mobile Documents/com~apple~CloudDocs/Desktop/Github/AntColonyOptimization/prepare_dataset/dataset/kroA100.tsp", agent_num=100)

    for i in range(100):
        ant_system.reset_agent()
        ant_system.generate_route()
        ant_system.calculate_distance()
        ant_system.update_pheromone()

if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    main()
