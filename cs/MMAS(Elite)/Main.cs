using System;
using System.Collections.Generic;


class MMASEliteMain{
    public const int ITERATION = 2000;
    const string FILENAME = "../kroA100.csv";
    static public int cityNum = 100;
    static public int agentNum = 140;
    static public List<Agent> agent = new List<Agent>();
    static public MMASElite mmasElite;
    static public double alpha=1.0, beta=5.0, rho=0.50, pheromoneQ=100.0;
    static public double initPheromone = 1.0;

    static void Main(){
        mmasElite = new MMASElite(cityNum, FILENAME, initPheromone, alpha, beta, rho, pheromoneQ);
        for (int i=0; i<agentNum; i++){
            agent.Add(new Agent(cityNum));
        }

        for (int iter=0; iter<ITERATION; iter++){
            for(int i=0; i<agentNum; i++) agent[i].reset();
            mmasElite.resetEliteAntNum();
            generateRoute();
            updatePheromone();
            Console.WriteLine(iter.ToString() + "   " + mmasElite.bestFitness.ToString());
        }
    }

    static public void generateRoute(){
        for (int i=0; i<agentNum; i++){
            mmasElite.generateRoute(agent[i]);
        }
    }

    static public void updatePheromone(){
        mmasElite.updatePheromone();
    }
}

