using System;
using System.Collections.Generic;
using System.IO;

class AntSystem{
    private double[,] distanceArr, distanceArrInv, pheromoneArr;
    private double initPheromone;
    private double alpha, beta, rho, pheromoneQ;

    public double bestFitnss = -1;
    public Agent bestAgent;
    private int cityNum;

    public AntSystem(int cityNum, string filename, double initPheromone, double alpha, double beta, double rho, double pheromoneQ)
    {
        this.cityNum = cityNum;
        this.initPheromone = initPheromone;
        this.alpha = alpha;
        this.beta = beta;
        this.rho = rho;
        this.pheromoneQ = pheromoneQ;
        distanceArr = new double[cityNum, cityNum];
        distanceArrInv = new double[cityNum, cityNum];
        makePheromoneArr();
        makeDistanceArr(cityNum, filename);
    }

    private void makePheromoneArr(){
        pheromoneArr = new double[cityNum, cityNum];
        for (int i=0; i<cityNum; i++){
            for (int j=i+1; j<cityNum; j++){
                pheromoneArr[i, j] = initPheromone;
                pheromoneArr[j, i] = initPheromone;
            }
        }
    }

    private void makeDistanceArr(int cityNum, string filename){
        double[,] cityInfo = new double[cityNum, 2];
        double dis;
        int count = 0;
        StreamReader sr = new StreamReader(filename);
        {
            while(!sr.EndOfStream){
                string line = sr.ReadLine();
                string[] city = line.Split(",");
                cityInfo[count, 0] = double.Parse(city[0]);
                cityInfo[count, 1] = double.Parse(city[1]);
                count += 1;
            }
        }

        for(int i=0; i<cityNum; i++)
        {
            for(int j=i+1; j<cityNum; j++){
                dis = Math.Pow(Math.Pow(cityInfo[i, 0] - cityInfo[j, 0], 2.0) + Math.Pow(cityInfo[i, 1] - cityInfo[j, 1], 2.0), 0.5);
                distanceArr[i, j] = dis;
                distanceArr[j, i] = dis;
                distanceArrInv[i, j] = 1.0 / dis;
                distanceArrInv[j, i] = 1.0 / dis;
            }
        }
    }

    public void generateRoute(Agent agent, int city){
        double probDenominator, dRandom, probSum, distance=0.0;
        Random cRandom = new System.Random();
        int i, j, preCity = city;

        agent.addRoute(city);
        for(i=1; i<cityNum; i++){
            double[] v = new double[cityNum];
            probDenominator = 0.0;

            for(j=0; j<cityNum; j++){
                if(!agent.isAlreadySet(j)){
                    double val = Math.Pow(pheromoneArr[preCity, j], alpha) * Math.Pow(distanceArrInv[preCity, j], beta);
                    v[j] = val;
                    probDenominator += val;
                }
            }
            dRandom = cRandom.NextDouble();
            probSum = 0;

            for(j=0; j<cityNum; j++){
                if(!agent.isAlreadySet(j)){
                    probSum += v[j] / probDenominator;
                    if(probSum>dRandom){
                        agent.addRoute(j);
                        distance += distanceArr[preCity, j];
                        preCity = j;
                        break;
                    }
                }
            }
        }
        distance += distanceArr[preCity, city];
        agent.setDistance(distance);

        if(bestFitnss==-1 || distance < bestFitnss){
            bestFitnss = distance;
            bestAgent = agent;
        }
    }

    public void reducePheromone(){
        for(int i=0; i<cityNum; i++){
            for(int j=i+1; j<cityNum; j++){
                pheromoneArr[i, j] = pheromoneArr[i, j] * rho;
                pheromoneArr[j, i] = pheromoneArr[i, j];
            }
        }
    }

    public void updatePheromone(Agent agent){
        double add = pheromoneQ / agent.distance;
        for(int i=0; i<cityNum-1; i++){
            pheromoneArr[agent.route[i], agent.route[i+1]] += add;
            pheromoneArr[agent.route[i+1], agent.route[i]] += add;
        }
        pheromoneArr[agent.route[0], agent.route[cityNum-1]] += add;
        pheromoneArr[agent.route[cityNum-1], agent.route[0]] += add;
    }
}
