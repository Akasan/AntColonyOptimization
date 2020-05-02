using System;
using System.Collections.Generic;
using System.IO;

class MMAS{
    private double[,] distanceArr, distanceArrInv, pheromoneArr;
    private double alpha, beta, rho, pheromoneQ;

    public double bestFitness = -1;
    public Agent bestAgent;
    private int cityNum;

    public MMAS(int cityNum, string filename, double initPheromone, double alpha, double beta, double rho, double pheromoneQ)
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

        if(bestFitness == -1 || distance < bestFitness){
            bestFitness = distance;
            bestAgent = agent;
        }
    }

    private void reducePheromone(double pheromoneMin){
        double newPheromone;
        int i, j;
        for(i=0; i<cityNum; i++){
            for(j=i+1; j<cityNum; j++){
                newPheromone = pheromoneArr[i, j] * rho;
                newPheromone = newPheromone<pheromoneMin?pheromoneMin: newPheromone;
                pheromoneArr[i, j] = newPheromone;
                pheromoneArr[j, i] = newPheromone;
            }
        }
    }

    public void updatePheromone(){
        double add = 1.0 / bestFitness;
        double newPheromone;
        double pheromoneMax = add / (1-rho);
        double pheromoneMin = (1 - Math.Pow(0.05, 1.0/cityNum)) * pheromoneMax / ((cityNum/2-1)*Math.Pow(0.05, 1.0/cityNum));
        reducePheromone(pheromoneMin);
        int i, j;
        int city1, city2;

        for(i=0; i<cityNum; i++){
            city1 = bestAgent.route[i];
            city2 = bestAgent.route[(i+1)%cityNum];
            newPheromone = pheromoneArr[city1, city2] + add;
            pheromoneArr[city1, city2] = newPheromone;
            pheromoneArr[city2, city1] = newPheromone;
        }

        for(i=0; i<cityNum; i++){
            for(j=i+1; j<cityNum; j++){
                if(pheromoneArr[i, j]>pheromoneMax){
                    pheromoneArr[i, j] = pheromoneMax;
                    pheromoneArr[j, i] = pheromoneMax;
                }
            }
        }
    }
}