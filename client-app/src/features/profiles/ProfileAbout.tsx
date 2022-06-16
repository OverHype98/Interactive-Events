import React, { useState } from 'react';
import { useStore } from "../../app/stores/store";
import { Button, Grid, Header, Tab } from "semantic-ui-react";
import { observer } from 'mobx-react-lite';
import ProfileEdit from './ProfileEdit';
export default observer(function ProfileAbout() {
    const { profileStore } = useStore();
    const { isCurrentUser, profile } = profileStore;
    const [editMode, setEditMode] = useState(false);
    return (
        <Tab.Pane inverted>
            <Grid>
                <Grid.Column width='16'>
                    <Header inverted floated='left' icon='user' content={`About ${profile?.displayName}`} />
                    {isCurrentUser && (
                        <Button 
                            inverted
                            floated='right'
                            basic
                            content={editMode ? 'Cancel' : 'Edit Profile'}
                            onClick={() => setEditMode(!editMode)}
                        />
                    )}
                </Grid.Column>
                <Grid.Column inverted width='16'>
                    {editMode ? <ProfileEdit setEditMode={setEditMode} /> :
                        <span style={{ whiteSpace: 'pre-wrap' }}>{profile?.bio}</span>}
                </Grid.Column>
            </Grid>
        </Tab.Pane>
    )
})