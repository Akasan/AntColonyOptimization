using System;
using System.Collections.Generic;


class ASMain{
    public const int ITERATION = 10;
    const string FILENAME = "../kroA100.csv";
    static public int cityNum = 100;
    static public int agentNum = 100;
    static public List<Agent> agent = new List<Agent>();
    static public AntSystem asy;
    static public double alpha=1.0, beta=3.0, rho=0.98;
    static public double initPheromone = 1.0;

    static void Main(){
        asy = new AntSystem(cityNum, FILENAME, initPheromone, alpha, beta, rho);
        for (int i=0; i<agentNum; i++){
            agent.Add(new Agent(cityNum));
        }

        for (int iter=0; iter<ITERATION; iter++){
            generateRoute();
			break;
        }
    }

    static public void generateRoute(){
        for (int i=0; i<agentNum; i++){
            asy.generateRoute(agent[i], i);
        }
    }
}

