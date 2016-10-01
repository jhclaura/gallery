using UnityEngine;
using System.Collections;

public class RemoteManager : MonoBehaviour {

    public GameObject topButton;
    public GameObject downButton;
    
    Vector3 resetAngle = new Vector3();
    Vector3 topDownAngle = new Vector3(-12.5f, 0, 0);
    Vector3 downDownAngle = new Vector3(0, 0, 12.5f);

    public OldRoomGifData oldRoomGifData;

    void OnEnable()
    {
        if (oldRoomGifData == null)
        {
            oldRoomGifData = GameObject.FindObjectOfType<OldRoomGifData>();
            Debug.Log("Got old room gif data");
        }
    }

    public void PressLeftButton()
    {
        downButton.transform.localEulerAngles = downDownAngle;

        //Debug.Log("Press left!");
    }

    public void PressRightButton()
    {
        topButton.transform.localEulerAngles = topDownAngle;

        //Debug.Log("Press right!");
    }

    public void ReleaseLeftButton()
    {
        downButton.transform.localEulerAngles = resetAngle;

        oldRoomGifData.ShowPreviousGif();
    }

    public void ReleaseRightButton()
    {
        topButton.transform.localEulerAngles = resetAngle;

        oldRoomGifData.ShowNextGif();
    }

}
