import numpy as np

class Agent:
    def __init__(self, start):
        self.__route = [start]
        self.__distance = 0
        self.__rank = None
        self.__start = start

    @property
    def route(self):
        return self.__route

    @property
    def distance(self):
        return self.__distance

    @property
    def rank(self):
        return self.__rank

    @rank.setter
    def rank(self, rank):
        self.__rank = rank

    def append_route(self, new_point):
        """ append next city to route

        Arguments:
            new_point {int} -- city id
        """
        self.__route.append(new_point)

    def get_last_point(self):
        """ get city where agent is now

        Returns:
            {int} -- current position
        """
        return self.__route[-1]

    def add_distance(self, distance):
        """ set distance

        Arguments:
            distance {float} -- distance
        """
        self.__distance += distance

    def get_route_generator(self):
        length = len(self.__route)
        for i, k in enumerate(self.__route):
            yield k, self.__route[(i + 1) % length]
