import numpy as np
from agent import Agent
from open_data import open_dataset
import random
from numba import jit


class AntSystem:
    def __init__(self,
                 city_information_filename,
                 agent_num=None,
                 pheromone_init=1.0,
                 alpha=2.0,
                 beta=4.0,
                 rho=0.8):
        """ AntSystem is a very simple ACO algorithm and this method was the first method of ACO

        Arguments:
            city_information_filename {str} -- file name of benchmark

        Keyword Arguments:
            agent_num {int} -- the number of agent
                               - if you don't set this number, it'll be set the number of cities
            pheromone_init {float} -- [description] (default: 1.0)
            alpha {float} -- [description] (default: 1.0)
            beta {float} -- [description] (default: 3.0)
            rho {float} -- [description] (default: 0.98)
        """
        self.ALPHA = alpha
        self.BETA = beta
        self.RHO = rho
        self.PHEROMONE_INIT = pheromone_init
        self.AGENT_NUM = agent_num
        self.best_score = 1e10

        self._open_city_information(city_information_filename)
        self._init_population()
        self._init_pheromone()
        print("Initialization was completed")

    def _open_city_information(self, filename):
        """ open benchmark file

        Arguments:
            filename {str} -- file name of benchmark test
        """
        self.CITY_NUM, self.city_info = open_dataset(filename)
        self.distance_arr = np.ones((self.CITY_NUM, self.CITY_NUM))

        for i in range(self.CITY_NUM):
            for j in range(i+1, self.CITY_NUM):
                dx = self.city_info[i, 0] - self.city_info[j, 0]
                dy = self.city_info[i, 1] - self.city_info[j, 1]
                dis = pow(pow(dx, 2) + pow(dy, 2), 0.5)
                self.distance_arr[i, j] = dis
                self.distance_arr[j, i] = dis

        self.distance_arr_inv = np.power(self.distance_arr, -1 * self.BETA)
        print("Distance array was generated")

    def get_distance_value(self, i, j, is_inv=True):
        """ get distance from city i to city j

        Arguments:
            i {int} -- base city
            j {int} -- compared city

        Returns:
            {float} -- distance
        """
        if is_inv:
            return self.distance_arr_inv[max(i, j), min(i, j)]
        else:
            return self.distance_arr[max(i, j), min(i, j)]

    def _init_population(self):
        """ generate population"""
        self.agent = {}

        for i in range(self.AGENT_NUM):
            threshld = random.random()
            if threshld > 0.5:
                self.agent[i] = Agent(int(np.random.randint(0, self.CITY_NUM, 1)))
            else:
                self.agent[i] = Agent(i)

    def _init_pheromone(self):
        """ initialize pheromone"""
        self.pheromone = np.ones((self.CITY_NUM, self.CITY_NUM)) * self.PHEROMONE_INIT

    def generate_route(self):
        """ generate each agents' route"""
        pheromone = np.power(self.pheromone, self.ALPHA)

        for agent_no, agent_data in self.agent.items():
            while len(agent_data.route) < self.CITY_NUM - 1:
                not_visited = []
                for i in range(self.CITY_NUM):
                    if i in agent_data.route:
                        continue
                    if i == agent_no:
                        continue
                    not_visited.append(i)

                prob_dict = {}
                prob_sum = 0.0

                for next_city in not_visited:
                    prob = pheromone[agent_data.get_last_point(), next_city] * \
                           self.distance_arr_inv[agent_data.get_last_point(), next_city]

                    prob_dict[next_city] = prob
                    prob_sum += prob

                valid_prob_dict = {}
                threshold = random.random()
                prob = random.random()
                if prob > threshold:
                    for k, v in sorted(prob_dict.items(), key=lambda x:-x[1]):
                        agent_data.append_route(k)
                        break

                    continue

                else:
                    pre_id = None
                    for i, (k, v) in enumerate(prob_dict.items()):
                        prob_dict[k] /= prob_sum
                        valid_prob_dict[k] = prob_dict[k]

                        if i > 0:
                            valid_prob_dict[k] += valid_prob_dict[pre_id]

                        if valid_prob_dict[k] > threshold and self._is_valid_city(k, agent_data):
                            agent_data.append_route(k)
                            break

                        pre_id = k

    def calculate_distance(self):
        """ calculate each agent's distance"""
        best = 1e20
        for agent_no, agent_data in self.agent.items():
            for pre, curr in agent_data.get_route_generator():
                agent_data.add_distance(self.get_distance_value(pre, curr, is_inv=False))

            if best > agent_data.distance:
                best = agent_data.distance

            if best < self.best_score:
                self.best_score = best

        print(f"Current best: {best:.1f} \t all best: {self.best_score:.1f}")

    def _is_valid_city(self, city_no, agent_data):
        """ check whether specified city is valid

        Arguments:
            city_no {int} -- city number
            agent_data {Agent} -- agent instance

        Returns:
            {bool} -- True when city_no is valid
        """
        return True if not city_no in agent_data.route else False

    def update_pheromone(self):
        """ update pheromone information"""
        self.pheromone *= self.RHO
        pre_pheromone = self.pheromone
        for agent_no, agent_data in self.agent.items():
            add_pheromone = 1.0 / agent_data.distance

            for pre, curr in agent_data.get_route_generator():
                self.pheromone[pre, curr] += add_pheromone
                self.pheromone[curr, pre] += add_pheromone

        self.pheromone[self.pheromone < 0.0001] = 0.0001

    def reset_agent(self):
        """ reset agent instance"""
        self._init_population()
