using UnityEngine;
using System.Collections;

public class OldRoomGifData : MonoBehaviour {

    public GameObject[] allTheGifs;

    public Sprite[] gifRenderSource;
    public RuntimeAnimatorController[] gifAnimatorSource;

    public GameObject wallPaper;
    public Texture weirdWallPaper;
    Texture originalWallPaper;
    Renderer wallPaperRenderer;

    SpriteRenderer[] gifRenders;
    Animator[] gifAnimators;
    int gifIndex = 0;
    int gifObjectCount;
    int gifSourceCount;

    bool roomTextureNormal;

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

        wallPaperRenderer = wallPaper.transform.GetComponent<Renderer>();
        originalWallPaper = wallPaperRenderer.material.mainTexture;
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

        if (gifIndex % 7 == 0)
        {
            // change room texture!
            wallPaperRenderer.material.SetTexture("_MainTex", weirdWallPaper);
            roomTextureNormal = false;
        } else
        {
            if (!roomTextureNormal)
            {
                // change room texture back!
                wallPaperRenderer.material.SetTexture("_MainTex", originalWallPaper);
                roomTextureNormal = true;
            }
        }
    }
}
