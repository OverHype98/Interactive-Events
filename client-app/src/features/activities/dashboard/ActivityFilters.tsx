import { observer } from 'mobx-react-lite';
import React, { useState } from 'react';
import Calendar from 'react-calendar';
import { Dropdown, Header, Input, Menu, Search } from 'semantic-ui-react';
import { useStore } from '../../../app/stores/store';

export default observer(function ActivityFilters() {
  const { activityStore } = useStore();
  const [search, setSearch] = useState<string>("");

  function handleSearchChange (value : string){
    setSearch(value)
    if(value.length > 3)
    {
      activityStore.setPredicate('search', value)
    }
  }

  return (
    <>
      <Menu vertical size='large' className='inverted' style={{ width: '100%', marginTop: '25px' }}>
        <Header className='inverted' icon='filter' attached color='blue' content='Filters' />
        <Menu.Item
          content='All Activities'
          active={activityStore.predicate.has('all')}
          onClick={() => activityStore.setPredicate('all', 'true')}
        />
        <Menu.Item
          content="I'm going"
          active={activityStore.predicate.has('isGoing')}
          onClick={() => activityStore.setPredicate('isGoing', 'true')}
        />
        <Menu.Item
          content="I'm hosting"
          active={activityStore.predicate.has('isHost')}
          onClick={() => activityStore.setPredicate('isHost', 'true')}
        />
        <Dropdown item text='Categories'>
          <Dropdown.Menu>
            <Dropdown.Item
              content="Drinks"
              onClick={() => activityStore.setPredicate('drinks', 'true')}
            />
            <Dropdown.Item
              content="Culture"
              onClick={() => activityStore.setPredicate('culture', 'true')}
            />
            <Dropdown.Item
              content="Film"
              onClick={() => activityStore.setPredicate('film', 'true')}
            />
            <Dropdown.Item
              content="Food"
              onClick={() => activityStore.setPredicate('food', 'true')}
            />
            <Dropdown.Item
              content="Music"
              onClick={() => activityStore.setPredicate('music', 'true')}
            />
            <Dropdown.Item
              content="Travel"
              onClick={() => activityStore.setPredicate('travel', 'true')}
            />
            <Dropdown.Item
              content="Recreational"
              onClick={() => activityStore.setPredicate('recreational', 'true')}
            />
            <Dropdown.Item
              content="Relaxation"
              onClick={() => activityStore.setPredicate('relaxation', 'true')}
            />
            <Dropdown.Item
              content="Social"
              onClick={() => activityStore.setPredicate('social', 'true')}
            />
            <Dropdown.Item
              content="Cooking"
              onClick={() => activityStore.setPredicate('cooking', 'true')}
            />
            <Dropdown.Item
              content="Education"
              onClick={() => activityStore.setPredicate('education', 'true')}
            />

          </Dropdown.Menu>
        </Dropdown>
        <Menu.Item>
          <Input 
          icon='search' 
          value={search} 
          placeholder='Search events...' 
          onChange={(e: { target: { value: any; }; }) => handleSearchChange(e.target.value)}
        />
        </Menu.Item>
      </Menu>


      <Header />
      <Calendar
        className="react-calendar"
        onChange={(date: Date) => activityStore.setPredicate('startDate', date as Date)}
        value={activityStore.predicate.get('startDate') || new Date()}
      />
    </>
  );
});
