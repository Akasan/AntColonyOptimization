using System;

class Agent{
    public int distance = 100;
    public int cityNum;
    public int cityIdx = 0;
    public int[] route;

    public Agent(int cityNum){
        this.cityNum = cityNum;
        resetRoute();
    }

    // reset distance
    public void resetDistance(){
        distance = 0;
    }

    // reset route
    public void resetRoute(){
        route = new int[cityNum];
        cityIdx = 0;
    }

    public void addRoute(int city){
        route[cityIdx] = city;
        cityIdx += 1;
    }

    public int getLastCity(){
        if (cityIdx > 0){
            return route[cityIdx - 1];
        }
        else{
            return -1;
        }
    }

    public void describeRoute(){
        for (int i=0; i<cityNum; i++){
            Console.WriteLine(route[i].ToString());
        }
    }
}
