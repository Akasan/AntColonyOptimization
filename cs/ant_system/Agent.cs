using System;
using System.Collections.Generic;

class Agent{
    public double distance = 0;
    public int cityNum;
    public int cityIdx = 0;
    public List<int> route;

    public Agent(int cityNum){
        this.cityNum = cityNum;
        resetRoute();
    }

    public void setDistance(double distance){
        this.distance = distance;
    }

    // reset distance
    public void resetDistance(){
		setDistance(0.0);
    }

    public void reset(){
        setDistance(0.0);
        resetRoute();
    }

    // reset route
    public void resetRoute(){
        route = new List<int>();
        cityIdx = 0;
    }

    // append new city to route
    public void addRoute(int city){
        route.Add(city);
        cityIdx += 1;
    }

    // get last city
    public int getLastCity(){
        if (cityIdx > 0){
            return route[cityIdx - 1];
        }
        else{
            return -1;
        }
    }

    // describe route
    public void describeRoute(){
        for (int i=0; i<cityIdx; i++){
            Console.WriteLine(route[i].ToString());
        }
    }

    public bool isAlreadySet(int city){
        for (int i=0; i<cityIdx; i++){
            if (route[i] == city)return true;
        }
        return false;
    }
}
