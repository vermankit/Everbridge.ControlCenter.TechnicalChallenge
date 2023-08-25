import React from 'react';
import './Door.css'; // Import the CSS file

interface DoorProps {
  id: string
  label: string;
  isOpen: boolean;
  isLocked: boolean;
}

const Door = ({ id, label, isOpen, isLocked }: DoorProps) => {
  const toggleOpen = async () => {
    const updateOpen = !isOpen;
    console.log("call open");

    try {
      const response = await fetch(`api/Door`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ id, label, isOpen: updateOpen, isLocked }),
      });

      if (response.ok) {
        console.log("success")
      }

    } catch (error) {
      console.error('Error toggling open:', error);
    }  

  }

  const toggleLock = async () => {
    const updateLocked = !isLocked;
    
    try {
      const response = await fetch(`api/Door`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ id, label, isOpen, isLocked: updateLocked }),
      });

      if (response.ok) {
        console.log("success")
      }
      
    } catch (error) {
      console.error('Error toggling open:', error);
    }
   

  }

  return (
    <div className="door-component">
      <h3>{label}</h3>
      <div className="status-label">
        Status: {isOpen ? 'Open' : 'Closed'}, {isLocked ? 'Locked' : 'Unlocked'}
      </div>
      <button className="button" onClick={toggleOpen}>{isOpen ? 'Close' : 'Open'}</button>
      <button className="button" onClick={toggleLock}>{isLocked ? 'Unlock' : 'Lock'}</button>
    </div>
  );
}

export default Door;
