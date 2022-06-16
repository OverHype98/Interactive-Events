import { observer } from 'mobx-react-lite';
import { useEffect, useState } from 'react'
import { Link, useHistory, useParams } from 'react-router-dom';
import { Button, FormField, Header, Input, Segment } from 'semantic-ui-react';
import LoadingComponent from '../../../app/layout/LoadingComponent';
import { useStore } from '../../../app/stores/store';
import { v4 as uuid } from "uuid";
import { Formik, Form, useFormikContext, useFormik } from 'formik';
import * as Yup from 'yup';
import MyTextInput from '../../../app/common/form/MyTextInput';
import MyTextArea from '../../../app/common/form/MyTextArea';
import MySelectInput from '../../../app/common/form/MySelectInput';
import { categoryOptions } from '../../../app/common/options/categoryOptions';
import MyDateInput from '../../../app/common/form/MyDateInput';
import { ActivityFormValues } from '../../../app/models/activity';

export default observer(function ActivityForm() {
     const history = useHistory();

     const { activityStore, randomActivitiesStore } = useStore();
     const { createActivity, updateActivity, loadActivity, loadingInitial } = activityStore;
     const { id } = useParams<{ id: string }>();

     const [activity, setActivity] = useState<ActivityFormValues>(new ActivityFormValues());
     const [title, setTitle] = useState<string>("Title");
     const [description, setDescription] = useState<string>("Description");


     const validationSchema = Yup.object({
          title: Yup.string().required('The activity title is required'),
          description: Yup.string().required('The activity description is required'),
          category: Yup.string().required('The activity category is required'),
          date: Yup.string().required('The activity date is required').nullable(),
          city: Yup.string().required('The activity city is required'),
          venue: Yup.string().required('The activity venue is required')
     })

     useEffect(() => {
          if (id) loadActivity(id).then(activity => setActivity(new ActivityFormValues(activity)))
     }, [id, loadActivity])

     function handleFormSubmit(activity: ActivityFormValues) {
          if (!activity.id) {
               let newActivity = {
                    ...activity,
                    id: uuid()
               }
               createActivity(newActivity).then(() =>
                    history.push(`/activities/${newActivity.id}`))
          } else {
               updateActivity(activity).then(() =>
                    history.push(`/activities/${activity.id}`)
               )
          }
     }

     async function randomize() {
          var activity = await randomActivitiesStore.getRandomActivity()
          setTitle(activity!.type)
          setDescription(activity!.activity)
     }


     if (loadingInitial) return <LoadingComponent content='Loading activity...' />




     return (
          <Segment inverted clearing>

               <Header content='Activity Details' sub color='teal' />
               <Formik
                    validationSchema={validationSchema}
                    enableReinitialize={true}
                    initialValues={activity}
                    onSubmit={values => handleFormSubmit(values)}>
                    {({ handleSubmit, isValid, isSubmitting, dirty }) => (
                         <Form className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                           
                              <MyTextInput
                                   name='title'
                                   placeholder={title}
                                   
                              />
                              <MyTextArea
                                   
                                   rows={3}
                                   placeholder={description}
                                   name='description'
                              />
                              <MySelectInput
                                   option={categoryOptions}
                                   placeholder='Category'
                                   name='category'
                              />
                              <MyDateInput
                                   showTimeSelect
                                   timeCaption='Time'
                                   dateFormat='MMMM d, yyyy h:mm aa'
                                   placeholderText='Date'
                                   name='date'
                              />
                              <Header content='Location Details' sub color='teal' />

                              <MyTextInput
                                   placeholder='City'
                                   name='city'
                              />
                              <MyTextInput
                                   placeholder='Venue'
                                   name='venue'
                              />

                              <Button
                                   disabled={isSubmitting || !dirty || !isValid}
                                   loading={isSubmitting}
                                   floated='right'
                                   positive type='submit' content='Submit' />
                              <Button as={Link} to='/activities' floated='right' type='button' content='Cancel' />

                              <Button
                              inverted
                                   basic
                                   type='button'
                                   icon='random'
                                   content='Generate random activity title'
                                   onClick={() => randomize()}
                                   floated='right'
                              />



                         </Form>
                    )}
               </Formik>

          </Segment>
     )
})
