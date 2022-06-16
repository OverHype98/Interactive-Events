import { Profile } from "./profile";

export interface Activity {
     id: string;
     title: string;
     category: string;
     description: string;
     date: Date | null;
     city: string;
     venue: string;
     hostUsername: string;
     isCanceled: boolean;
     isGoing: boolean;
     isHost: boolean;
     host?: Profile;
     attendees: Profile[];
}

export class Activity implements Activity {
     constructor(init?: ActivityFormValues){
          Object.assign(this, init);
     }
}

export interface RandomActivity {
     activity: string;
     key: string;
     type: string;
     participants: number;
     price: number;
     link: string;
     accessibility: number;
}

export class RandomActivity implements RandomActivity{
     constructor(){
          this.accessibility = 0;
          this.activity = "";
          this.key = "";
          this.link = "";
          this.participants = 0;
          this.price = 0;
          this.type = "";

     }
}

export class ActivityFormValues {
     id?: string = undefined;
     title: string = '';
     category: string = '';
     description: string = '';
     date: Date | null = null;
     city: string = '';
     venue: string = '';

     constructor(activity?: Activity){
          if(activity) {
               this.id = activity.id;
               this.title = activity.title;
               this.category = activity.category;
               this.description = activity.description;
               this.date = activity.date;
               this.venue = activity.venue;
               this.city = activity.city;
          }
     }
}