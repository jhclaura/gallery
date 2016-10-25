using UnityEngine;
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

	public void PairRoom() {
		// Pair up rooms with room objects
		// Not a great way of doing this...
		Room[] rooms = GameObject.FindObjectsOfType<Room>();

		for (int i = 0; i < roomData.Length; ++i) {
			RoomData roomDataItem = roomData[i];

			foreach (Room room in rooms)
			{

				if (roomDataItem.name == room.roomName) {
					roomDataItem.room = room;
					//                    Debug.Log(room.roomName + " " + roomDataItem.name);
					Debug.Log(room.roomName + " loaded!");
				}
			}
		}
	}

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

                if (roomDataItem.name == room.roomName) {
                    roomDataItem.room = room;
//                    Debug.Log(room.roomName + " " + roomDataItem.name);
					Debug.Log(room.roomName + " loaded!");
                }
            }
        }
    }
    
    public void ActivateRoom(int roomIndex)
    {
        roomData[roomIndex].room.Activate();
    }

    public void DeactivateRoom(int roomIndex)
    {
        roomData[roomIndex].room.Deactivate();
    }

    public void ActivateAudio(int roomIndex)
    {
        roomData[roomIndex].room.PlayAudios();
    }

    public void DeactivateAudio(int roomIndex)
    {
        roomData[roomIndex].room.PauseAudios();
    }

	public void ActivateLight(int roomIndex)
	{
		roomData[roomIndex].room.ActivateLights();
	}

	public void DeactivateLight(int roomIndex)
	{
		roomData[roomIndex].room.DeactivateLights();
	}

	public void ActivateWater(int roomIndex)
	{
		roomData[roomIndex].room.ActivateWater();
	}

	public void DeactivateWater(int roomIndex)
	{
		roomData[roomIndex].room.DeactivateWater();
	}

	public void ActivateAnimator(int roomIndex)
	{
		roomData[roomIndex].room.ActivateAnimators();
	}

	public void DeactivateAnimator(int roomIndex)
	{
		roomData[roomIndex].room.DeactivateAnimators();
	}
}
