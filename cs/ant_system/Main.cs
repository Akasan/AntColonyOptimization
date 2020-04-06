using System;
using System.Collections.Generic;


class ASMain{
    static public int cityNum = 10;
    static List<Agent> agent = new List<Agent>();
    static AntSystem asy;
    static public double alpha=1.0, beta=3.0, rho=0.98;
    static public double initPheromone = 1.0;

    static void Main(){
        asy = new AntSystem(cityNum, initPheromone, alpha, beta, rho);
        for (int i=0; i<cityNum; i++){
            agent.Add(new Agent(cityNum));
        }
    }
}
