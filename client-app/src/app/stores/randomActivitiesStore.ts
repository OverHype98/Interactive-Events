import { makeAutoObservable } from "mobx";
import agent from "../api/agent";
import { RandomActivity } from "../models/activity";

export default class RandomActivitiesStore {
    activity: RandomActivity | undefined;

    constructor() {
         makeAutoObservable(this)
    }
    
    getRandomActivity = async () => {
         try {
              const activity = await agent.RandomActivities.get();
              return activity;
         } catch (error) {
              console.log(error);
         }
    }

   
}