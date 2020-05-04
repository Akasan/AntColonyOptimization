using System;
using System.Collections.Generic;
using System.IO;

class AntSystem{
    private double[,] distanceArr, distanceArrInv, pheromoneArr;
    private double alpha, beta, rho, pheromoneQ;

    public double bestFitnss = -1;
    public Agent bestAgent;
    private int cityNum;

    public AntSystem(int cityNum, string filename, double initPheromone, double alpha, double beta, double rho, double pheromoneQ)
    {
        this.cityNum = cityNum;
        this.alpha = alpha;
        this.beta = beta;
        this.rho = rho;
        this.pheromoneQ = pheromoneQ;
        distanceArr = new double[cityNum, cityNum];
        distanceArrInv = new double[cityNum, cityNum];
        makePheromoneArr(initPheromone);
        makeDistanceArr(cityNum, filename);
    }

    private void makePheromoneArr(double initPheromone){
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
        int count = 0, i, j;

        StreamReader sr = new StreamReader(filename);
        while(!sr.EndOfStream){
            string line = sr.ReadLine();
            string[] city = line.Split(",");
            cityInfo[count, 0] = double.Parse(city[0]);
            cityInfo[count, 1] = double.Parse(city[1]);
            count += 1;
        }
        sr.Close();

        for(i=0; i<cityNum; i++)
        {
            for(j=i+1; j<cityNum; j++){
                dis = Math.Pow(Math.Pow(cityInfo[i, 0] - cityInfo[j, 0], 2.0) + Math.Pow(cityInfo[i, 1] - cityInfo[j, 1], 2.0), 0.5);
                 distanceArr[i, j] = distanceArr[j, i] = dis;
                 distanceArrInv[i, j] = distanceArrInv[j, i] = dis;
            }
        }
    }

    public void generateRoute(Agent agent){
        double probDenominator, dRandom, probSum, distance=0.0;
        Random cRandom = new System.Random();
        Random firstCity = new System.Random();
        int fCity = firstCity.Next(0, cityNum);
        int i, j, preCity = fCity;

        agent.addRoute(preCity);
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
                if(v[j] != 0){
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
        distance += distanceArr[preCity, fCity];
        agent.setDistance(distance);

        if(bestFitnss==-1 || distance < bestFitnss){
            bestFitnss = distance;
            bestAgent = agent;
        }
    }

    private void reducePheromone(){
        for(int i=0; i<cityNum; i++){
            for(int j=i+1; j<cityNum; j++){
                pheromoneArr[i, j] = pheromoneArr[i, j] * rho;
                pheromoneArr[j, i] = pheromoneArr[i, j];
            }
        }
    }

    public void updatePheromone(Agent agent){
        reducePheromone();
        double add = pheromoneQ / agent.distance;
        int city1, city2;
        for(int i=0; i<cityNum; i++){
            city1 = agent.route[i];
            city2 = agent.route[(i+1)%cityNum];
            pheromoneArr[city1, city2] += add;
            pheromoneArr[city2, city1] += add;
        }
    }
}
