using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectScatter : MonoBehaviour {
    public int count = 10;
    public float scatterX = 10f;
    public float scatterY = 10f;
    public float scatterZ = 10f;

    public int seed = 0;

    public GameObject[] prefabs;

	// Use this for initialization
	void Start () {
        Random.State randState = Random.state;
        Random.InitState(seed);

        for (int i = 0; i < count; ++i)
        {
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
            float x = Random.Range(-scatterX, scatterX);
            float y = Random.Range(-scatterY, scatterY);
            float z = Random.Range(-scatterZ, scatterZ);
            Quaternion angle = Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
            GameObject createdObject = Instantiate(prefab, Vector3.zero, angle) as GameObject;
            createdObject.transform.parent = transform;
            createdObject.transform.localPosition = new Vector3(x, y, z);
        }

        Random.state = randState;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
