using System;
using System.Collections.Generic;


class ASEliteMain{
    public const int ITERATION = 2000;
    const string FILENAME = "../kroA100.csv";
    static public int cityNum = 100;
    static public int agentNum = 100;
    static public List<Agent> agent = new List<Agent>();
    static public ASElite asElite;
    static public double alpha=1.0, beta=5.0, rho=0.5, pheromoneQ=100.0;
    static public double initPheromone = 1.0;

    static void Main(){
        asElite = new ASElite(cityNum, agentNum,FILENAME, initPheromone, alpha, beta, rho, pheromoneQ);
        for (int i=0; i<agentNum; i++){
            agent.Add(new Agent(cityNum));
        }

        for (int iter=0; iter<ITERATION; iter++){
            for(int i=0; i<agentNum; i++) agent[i].reset();
            asElite.resetEliteAntNum();
            generateRoute();
            updatePheromone();
            Console.WriteLine(iter.ToString() + "   " + asElite.bestFitness.ToString());
        }
    }

    static public void generateRoute(){
        for (int i=0; i<agentNum; i++){
            asElite.generateRoute(agent[i]);
        }
    }

    static public void updatePheromone(){
        asElite.updatePheromone(agent);
    }
}

