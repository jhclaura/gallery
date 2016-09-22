using UnityEngine;
using System.Collections;

public class AnimateSpriteSheet : MonoBehaviour
{
    //public int _uvTieX = 1;
    //public int _uvTieY = 1;
    //public int _fps = 10;
    //public Material myMaterial;

    //private Vector2 _size;
    //private Renderer _myRenderer;
    //private int _lastIndex = -1;

    //void Start()
    //{
    //    _size = new Vector2(1.0f / _uvTieX, 1.0f / _uvTieY);
    //    //_myRenderer = GetComponent<Renderer>();
    //    //if (_myRenderer == null)
    //    //enabled = false;

    //    if (myMaterial == null)
    //        enabled = false;
    //}
    //// Update is called once per frame
    //void Update()
    //{
    //    // Calculate index
    //    int index = (int)(Time.timeSinceLevelLoad * _fps) % (_uvTieX * _uvTieY);
    //    if (index != _lastIndex)
    //    {
    //        // split into horizontal and vertical index
    //        int uIndex = index % _uvTieX;
    //        int vIndex = index / _uvTieY;

    //        // build offset
    //        // v coordinate is the bottom of the image in opengl so we need to invert.
    //        Vector2 offset = new Vector2(uIndex * _size.x, 1.0f - _size.y - vIndex * _size.y);

    //        //_myRenderer.material.SetTextureOffset("_MainTex", offset);
    //        //_myRenderer.material.SetTextureScale("_MainTex", _size);

    //        myMaterial.SetTextureOffset("_MainTex", offset);
    //        myMaterial.SetTextureScale("_MainTex", _size);

    //        _lastIndex = index;
    //    }
    //}

    public int Columns = 4;
    public int Rows = 4;
    public float FramesPerSecond = 10f;
    public bool RunOnce = false;

    public float RunTimeInSeconds
    {
        get
        {
            return ((1f / FramesPerSecond) * (Columns * Rows));
        }
    }

    public Material myMaterial;

    void Start()
    {
        Vector2 size = new Vector2(1f / Columns, 1f / Rows);
        //renderer.sharedMaterial.SetTextureScale("_MainTex", size);
    }

    void OnEnable()
    {
        StartCoroutine(UpdateTiling());
    }

    private IEnumerator UpdateTiling()
    {
        float x = 0f;
        float y = 0f;
        Vector2 offset = Vector2.zero;

        while (true)
        {
            for (int i = Rows - 1; i >= 0; i--) // y
            {
                y = (float)i / Rows;

                for (int j = 0; j <= Columns - 1; j++) // x
                {
                    x = (float)j / Columns;

                    offset.Set(x, y);

                    myMaterial.SetTextureOffset("_MainTex", offset);
                    yield return new WaitForSeconds(1f / FramesPerSecond);
                }
            }

            if (RunOnce)
            {
                yield break;
            }
        }
    }
}
