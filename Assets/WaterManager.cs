using UnityEngine;
using System.Collections;

public class WaterManager : MonoBehaviour {

    public GameObject surfaceWater;
    public GameObject underWater;

	// Use this for initialization
	void Start () {
        surfaceWater.SetActive(true);
        underWater.SetActive(false);
	}
	
	public void TurnOnSurfaceWater(bool yeaa)
    {
        surfaceWater.SetActive(yeaa);
        underWater.SetActive(!yeaa);
    }
}
