using UnityEngine;
using System.Collections;

public class OldRoomGifData : MonoBehaviour {

    public GameObject[] allTheGifs;

    public Sprite[] gifRenderSource;
    public RuntimeAnimatorController[] gifAnimatorSource;

    SpriteRenderer[] gifRenders;
    Animator[] gifAnimators;
    int gifIndex = 0;
    int gifObjectCount;
    int gifSourceCount;

    // Use this for initialization
    void Start () {
        gifObjectCount = allTheGifs.Length;
        gifSourceCount = gifRenderSource.Length;

        gifRenders = new SpriteRenderer[gifObjectCount];
        gifAnimators = new Animator[gifObjectCount];

        for(int i=0; i< gifObjectCount; i++)
        {
            gifRenders[i] = allTheGifs[i].GetComponent<SpriteRenderer>();
            gifAnimators[i] = allTheGifs[i].GetComponent<Animator>();
        }
    }
	
	public void ShuffleGifs()
    {
        gifIndex++;
      
        for (int i = 0; i < gifObjectCount; i++)
        {
            int gifIndexEdit = (i+gifIndex) % gifSourceCount;
            gifRenders[i].sprite = gifRenderSource[gifIndexEdit];
            gifAnimators[i].runtimeAnimatorController = gifAnimatorSource[gifIndexEdit];
        }
    }
}
