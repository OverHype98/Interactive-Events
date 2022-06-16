import { createContext, useContext } from "react";
import ActivityStore from "./activityStore";
import CommonStore from "./commonStore";
import ModalStore from "./modalStore";
import ProfileStore from "./profileStore";
import UserStore from "./userStore";
import CommentStore from "./commentStore";
import RandomActivitiesStore from "./randomActivitiesStore";

interface Store{
     activityStore: ActivityStore;
     commonStore: CommonStore;
     userStore: UserStore;
     modalStore: ModalStore;
     profileStore: ProfileStore;
     commentStore: CommentStore;
     randomActivitiesStore: RandomActivitiesStore;
}

export const store: Store = {
     activityStore: new ActivityStore(),
     commonStore: new CommonStore(),
     userStore: new UserStore(),
     modalStore: new ModalStore(),
     profileStore: new ProfileStore(),
     commentStore: new CommentStore(),
     randomActivitiesStore: new RandomActivitiesStore()
}

export const StoreContext = createContext(store);

export function useStore(){
     return useContext(StoreContext);
}