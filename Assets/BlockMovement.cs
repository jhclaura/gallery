using UnityEngine;
using System.Collections;

public class BlockMovement : MonoBehaviour {

    public GameObject[] blockPrefabs;
    public int blocksTotalAmount = 10;
    public float travelDistance = 200;
    public float taravelSpeed = 20f;
    public GameObject spawnPoint;

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
            pos.x += Random.Range(-4f, 2f);
            pos.y += Random.Range(-4f, 2f);
            GameObject block = (GameObject)Instantiate(blockPrefabs[i%prefabLength], pos, Quaternion.identity);
            allBlocks[i] = block;
            block.transform.SetParent(gameObject.transform, true);

            // TWEEN
            moveTweenIds[i] = LeanTween.moveZ(block, pos.z + travelDistance, taravelSpeed).setDelay(i*2).setRepeat(-1).id;
            rotateTweenIds[i] = LeanTween.rotate(block, new Vector3(180f, 180f, 180f), 5f).setDelay(i).setRepeat(-1).id;
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
