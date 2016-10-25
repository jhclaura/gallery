using UnityEngine;
using System.Collections;

public class WaterManager : MonoBehaviour {

    public GameObject surfaceWater;
    public GameObject underWater;

    public GameObject ocean;

	#if UNITY_STANDALONE_WIN
	AudioSource[] waterAudios;
	#else
	GvrAudioSource[] waterAudios;
	#endif

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
		#if UNITY_STANDALONE_WIN
        foreach ( AudioSource audio in waterAudios)
		#else
		foreach ( GvrAudioSource audio in waterAudios)
		#endif
        {
            if (yeaa) audio.pitch = waterNormalPitch;
            else audio.pitch = waterUnderPitch;
        }

    }
}
