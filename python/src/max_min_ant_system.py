from ant_system import AntSystem


class MaxMinAntSystem(AntSystem):
    def __init__(self,
                 city_information_filename,
                 agent_num=None,
                 pheromone_init=1.0,
                 alpha=2.0,
                 beta=4.0,
                 rho=0.9):
        super(MaxMinAntSystem, self).__init__(city_information_filename,
                                              agent_num,
                                              pheromone_init,
                                              alpha,
                                              beta,
                                              rho)

        self.PHEROMONE_MIN_COEF = (1 - pow(0.05, 1.0 / self.CITY_NUM) / ((self.CITY_NUM / 2 - 1) * pow(0.05, 1.0 / self.CITY_NUM)))
        print(self.PHEROMONE_MIN_COEF)
                
    
    def saturate_pheromone(self):
        """ saturate pheromone values
        """
        pheromone_max = 1.0 / ((1 - self.RHO) * self.best_score)
        pheromone_min = pheromone_max * (1 - pow(0.05, 1.0 / self.CITY_NUM)) / ((self.CITY_NUM / 2.0 - 1) * pow(0.05, 1.0 / self.CITY_NUM))
        self.pheromone[self.pheromone < pheromone_min] = pheromone_min
        self.pheromone[self.pheromone > pheromone_max] = pheromone_max
