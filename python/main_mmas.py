import argparse
from max_min_ant_sytem import MaxMinAntSystem

def main():
    mmas = MaxMinAntSystem(city_information_filename="./dataset/kroA100.tsp",
                           agent_num=100)

    # for i in range(100):
    #     ant_system.reset_agent()
    #     ant_system.generate_route()
    #     ant_system.calculate_distance()
    #     ant_system.update_pheromone()

if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    main()
