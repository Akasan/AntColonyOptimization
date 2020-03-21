import numpy as np

def open_dataset(filename):
    """ open benchmark file and get the number of cities and each city's coordinate

    Arguments:
        filename {str} -- file name of benchmark
    """
    filename =  "/Users/akagawaoozora/Library/Mobile Documents/com~apple~CloudDocs/Desktop/Github/AntColonyOptimization/prepare_dataset/dataset/kroa100.tsp"
    with open(filename, "r") as f:
        raw_data = f.readlines()


    city_num = int(raw_data[3].strip().split(" ")[1])

    raw_data = raw_data[6:]
    for i in range(len(raw_data)):
        item = raw_data[i].strip()
        item = item.split(" ")[1:]
        item = list(map(int, item))
        raw_data[i] = item

    del raw_data[-1]
    
    data = np.array(raw_data)
    return city_num, data
