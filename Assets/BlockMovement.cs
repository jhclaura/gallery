using UnityEngine;
using System.Collections;

public class BlockMovement : MonoBehaviour {

    public GameObject[] blockPrefabs;
    public int blocksTotalAmount = 10;
    public float travelDistance = 200;
    public float taravelSpeed = 20f;
    public GameObject spawnPoint;
    public float scatterX = 4;
    public float scatterY = 2;
    public float scatterZ = 4;
    public float blockScale = 0.5f;
    public float minDuration = 10f;
    public float maxDuration = 20f;

    GameObject[] allBlocks;
    int[] moveTweenIds;
    int[] rotateTweenIds;

    bool initiated = false;

    void Start () {
        allBlocks = new GameObject[blocksTotalAmount];
        moveTweenIds = new int[blocksTotalAmount];
        rotateTweenIds = new int[blocksTotalAmount];

        int prefabLength = blockPrefabs.Length;

        for (int i=0; i<blocksTotalAmount; i++)
        {
            Vector3 pos = spawnPoint.transform.position;
            pos.x += Random.Range(-scatterX, scatterX);
            pos.y += Random.Range(-scatterY, scatterY);
            pos.z += Random.Range(-scatterZ, scatterZ);

            GameObject block = (GameObject)Instantiate(blockPrefabs[i%prefabLength], pos, Quaternion.identity);
            allBlocks[i] = block;
            block.transform.SetParent(gameObject.transform, true);
            block.transform.localScale = new Vector3(blockScale, blockScale, blockScale);

            // TWEEN
            moveTweenIds[i] = LeanTween.moveZ(block, pos.z - travelDistance, Random.Range(minDuration, maxDuration)).setRepeat(-1).id;
            rotateTweenIds[i] = LeanTween.rotate(block, new Vector3(180f, 180f, 180f), Random.Range(3f, 5f)).setRepeat(-1).id;
        }

        initiated = true;

    }

    void OnEnable()
    {
        if (initiated)
        {
            for (int i = 0; i < moveTweenIds.Length; i++)
            {
                LeanTween.resume(moveTweenIds[i]);
                LeanTween.resume(rotateTweenIds[i]);
            }
        }
    }

    void OnDisable()
    {
        for(int i=0; i<moveTweenIds.Length; i++)
        {
            LeanTween.pause(moveTweenIds[i]);
            LeanTween.pause(rotateTweenIds[i]);
        }
    }
}
