using UnityEngine;
using System.Collections;

public class RemoteManager : MonoBehaviour {

    public GameObject leftButton;
    public GameObject rightButton;

    Vector3 resetAngle = new Vector3();
    Vector3 leftDownAngle = new Vector3(0,0,-17);
    Vector3 rightDownAngle = new Vector3(0, 0, 17);

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
        leftButton.transform.localEulerAngles = leftDownAngle;

        //Debug.Log("Press left!");
    }

    public void PressRightButton()
    {
        rightButton.transform.localEulerAngles = rightDownAngle;

        //Debug.Log("Press right!");
    }

    public void ReleaseLeftButton()
    {
        leftButton.transform.localEulerAngles = resetAngle;

        oldRoomGifData.ShowPreviousGif();
    }

    public void ReleaseRightButton()
    {
        rightButton.transform.localEulerAngles = resetAngle;

        oldRoomGifData.ShowNextGif();
    }

}
