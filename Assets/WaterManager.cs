using UnityEngine;
using System.Collections;

public class WaterManager : MonoBehaviour {

    public GameObject surfaceWater;
    public GameObject underWater;

    public GameObject ocean;
    AudioSource[] waterAudios;
    float waterNormalPitch = 0.9f;
    float waterUnderPitch = 0.3f;

    // Use this for initialization
    void Start () {
        surfaceWater.SetActive(true);
        underWater.SetActive(false);

        Room room = ocean.GetComponent<Room>();
        waterAudios = room.audios;
    }
	
	public void TurnOnSurfaceWater(bool yeaa)
    {
        surfaceWater.SetActive(yeaa);
        underWater.SetActive(!yeaa);

        // under water, muffle the sound, if above, reset the sound
        foreach ( AudioSource audio in waterAudios)
        {
            if (yeaa) audio.pitch = waterNormalPitch;
            else audio.pitch = waterUnderPitch;
        }

    }
}
