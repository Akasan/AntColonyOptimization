using System;

class AntSystem{
    private double[,] distanceArr;
    private double[,] pheromoneArr;
    private double initPheromone;
    private double alpha, beta, rho;
    private int cityNum;

    public AntSystem(int cityNum, double initPheromone, double alpha, double beta, double rho)
    {
        this.cityNum = cityNum;
        this.initPheromone = initPheromone;
        this.alpha = alpha;
        this.beta = beta;
        this.rho = rho;
        resetPheromone();
    }

    private void resetPheromone(){
        pheromoneArr = new double[cityNum, cityNum];
        for (int i=0; i<cityNum; i++){
            for (int j=i+1; j<cityNum; j++){
                pheromoneArr[i, j] = initPheromone;
                pheromoneArr[j, i] = initPheromone;
            }
        }
    }

    public void generateRoute(Agent agent){
        Console.WriteLine(agent.cityNum.ToString());
    }
}
