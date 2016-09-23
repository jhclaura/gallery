﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class RoomData {
    public bool loaded;
    public string name;
    public Room room;

    public RoomData(string roomName) {
        name = roomName;
        loaded = false;
        room = null;
    }
}


public class RoomManager : MonoBehaviour {

    private RoomData[] roomData = new RoomData[] {
       new RoomData("IntroRoom"),
       new RoomData("DarkRoom"),
       new RoomData("MiniRoom"),
       new RoomData("OceanRoom"),
       new RoomData("OldRoom"),
       new RoomData("MirrorRoom"),
       new RoomData("BigHall")
    };

    public IEnumerator LoadAll() {
        foreach (RoomData room in roomData)
        {
            yield return SceneManager.LoadSceneAsync(room.name, LoadSceneMode.Additive);
        }

        // Pair up rooms with room objects
        // Not a great way of doing this...
        Room[] rooms = GameObject.FindObjectsOfType<Room>();

        for (int i = 0; i < roomData.Length; ++i) {
            RoomData roomDataItem = roomData[i];

            foreach (Room room in rooms)
            {
                //Debug.Log(room.roomName + " " + roomDataItem.name);
                if (roomDataItem.name == room.roomName) {
                    roomDataItem.room = room;
                }
            }
        }

       //Debug.Log(roomData);
    }
    
    public void ActivateRoom(int roomIndex)
    {
        roomData[roomIndex].room.Activate();
    }

    public void DeactivateRoom(int roomIndex)
    {
        roomData[roomIndex].room.Deactivate();
    }
}
