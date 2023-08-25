import React, { useEffect, useState } from 'react'
import Door from '../../common/door/Door'
import { DoorType } from '../../../types'
import * as signalR from "@microsoft/signalr";
const Dashboard = () => {
  const [doors, setDoors] = useState<DoorType[]>([]);  

  useEffect(() => {     
    fetch(`api/Door`)
      .then(response => response.json())
      .then(json => setDoors(json))
  }, [])


  useEffect(() => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl("notificationHub")
      .build();

    connection.start().then(() => {
      console.log("Connected to SignalR hub");
    });
  

    connection.on("NotifyDoorAdded", (addedDoor: DoorType) => {  
      let newDoor = { ...addedDoor };       
      setDoors(prevDoors => [...prevDoors, newDoor]);
    });

    connection.on("NotifyDoorUpdated", (updatedDoor: DoorType) => {      
      setDoors(prevDoors => {
        return prevDoors.map(door => {
            if (door.id === updatedDoor.id) {              
                return updatedDoor;
            }
            return door;
        });
    });
    });   
    return () => {
      connection.stop();
    };
  }, []);

  useEffect(() => {
    console.log("Updated doors:", doors);
  }, [doors]);
  // const toggleOpen = (id: string) => {
  //   console.log(id);
  // };

  // const toggleLock = (id: string) => {
  //   console.log(id);
  // };
  return (
    <div>
      <h1>Door Moniter</h1>
      {
        doors.map((door: DoorType) => {
          return <Door key={door.id} id = {door.id} label={door.label} isOpen={door.isOpen} isLocked={door.isLocked}         
          ></Door>
        })
      }
    </div>
  )
}


export default Dashboard