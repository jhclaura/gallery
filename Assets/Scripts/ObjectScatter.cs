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

    public bool animateObjects = false;
    int[] rotateTweenIds;
    bool initiated = false;

    void Start () {
        Random.State randState = Random.state;
        Random.InitState(seed);
        rotateTweenIds = new int[count];

        for (int i = 0; i < count; ++i)
        {
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
            float x = Random.Range(-scatterX, scatterX);
            float y = Random.Range(-scatterY, scatterY);
            float z = Random.Range(-scatterZ, scatterZ);
            GameObject createdObject;

            if (!animateObjects)
            {
                Quaternion angle = Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
                createdObject = Instantiate(prefab, Vector3.zero, angle) as GameObject;
            }
            else
            {
                createdObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
            }
                
            createdObject.transform.parent = transform;
            createdObject.transform.localPosition = new Vector3(x, y, z);

            if (animateObjects)
            {
                rotateTweenIds[i] = LeanTween.rotate(createdObject, new Vector3(180f, 180f, 180f), Random.Range(5f, 8f)).setRepeat(-1).id;
            }
        }

        Random.state = randState;
        initiated = true;
    }

    void OnEnable()
    {
        if (initiated)
        {
            for (int i = 0; i < rotateTweenIds.Length; i++)
            {
                LeanTween.resume(rotateTweenIds[i]);
            }
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < rotateTweenIds.Length; i++)
        {
            LeanTween.pause(rotateTweenIds[i]);
        }
    }
}
