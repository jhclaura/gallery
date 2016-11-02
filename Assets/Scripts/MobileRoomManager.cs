using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MobileRoomData {
    public bool loaded;
    public string name;
    public Room room;

	public MobileRoomData(string roomName) {
        name = roomName;
        loaded = false;
        room = null;
    }
}
	
public class MobileRoomManager : MonoBehaviour {

	public Room[] room;
	MobileRoomData[] roomData;

	public void PairRoomByType() {
		roomData = new MobileRoomData[room.Length];

		for (int i=0; i<room.Length; ++i) {
			MobileRoomData r_data = new MobileRoomData( room[i].roomName );
			r_data.room = room [i];
			roomData [i] = r_data;
//			Debug.Log(room[i].roomName + " == " + roomData[i].room.roomName);
		}
	}

//    public IEnumerator LoadAll() {
//        foreach (RoomData room in roomData)
//        {
//            yield return SceneManager.LoadSceneAsync(room.name, LoadSceneMode.Additive);
//        }
//
//        // Pair up rooms with room objects
//        // Not a great way of doing this...
//        Room[] rooms = GameObject.FindObjectsOfType<Room>();
//
//        for (int i = 0; i < roomData.Length; ++i) {
//            RoomData roomDataItem = roomData[i];
//
//            foreach (Room room in rooms)
//            {
//
//                if (roomDataItem.name == room.roomName) {
//                    roomDataItem.room = room;
////                    Debug.Log(room.roomName + " " + roomDataItem.name);
//					Debug.Log(room.roomName + " loaded!");
//                }
//            }
//        }
//    }
    
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

	public void ActivateArt(int roomIndex)
	{
		roomData[roomIndex].room.ActivateArts();
	}

	public void DeactivateArt(int roomIndex)
	{
		roomData[roomIndex].room.DeactivateArts();
	}
}
