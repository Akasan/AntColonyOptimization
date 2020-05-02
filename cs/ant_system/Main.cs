using System;
using System.Collections.Generic;


class ASMain{
    public const int ITERATION = 1000;
    const string FILENAME = "../kroA100.csv";
    static public int cityNum = 100;
    static public int agentNum = 100;
    static public List<Agent> agent = new List<Agent>();
    static public AntSystem asy;
    static public double alpha=1.0, beta=5.0, rho=0.98, pheromoneQ=10.0;
    static public double initPheromone = 1.0;

    static void Main(){
        asy = new AntSystem(cityNum, FILENAME, initPheromone, alpha, beta, rho, pheromoneQ);
        for (int i=0; i<agentNum; i++){
            agent.Add(new Agent(cityNum));
        }

        for (int iter=0; iter<ITERATION; iter++){
            for(int i=0; i<agentNum; i++) agent[i].reset();
            generateRoute();
			updatePheromone();
            Console.WriteLine(iter.ToString() + "   " + asy.bestFitnss.ToString());
        }
    }

    static public void generateRoute(){
        for (int i=0; i<agentNum; i++){
            asy.generateRoute(agent[i], i);
        }
    }

	static public void updatePheromone(){
        asy.reducePheromone();
        for(int i=0; i<agentNum; i++){
            asy.updatePheromone(agent[i]);
        }
	}
}

