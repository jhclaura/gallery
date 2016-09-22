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
        // TODO - not a good wayyyyyyy
        if (oldRoomGifData == null)
        {
            oldRoomGifData = GameObject.Find("Floor_4").GetComponent<OldRoomGifData>();
            Debug.Log("Got old room gif data");
        }
    }

    public void PressLeftButton()
    {
        leftButton.transform.localEulerAngles = leftDownAngle;
    }

    public void PressRightButton()
    {
        rightButton.transform.localEulerAngles = rightDownAngle;
    }

    public void ReleaseLeftButton()
    {
        leftButton.transform.localEulerAngles = resetAngle;

        oldRoomGifData.ShuffleGifs();
    }

    public void ReleaseRightButton()
    {
        rightButton.transform.localEulerAngles = resetAngle;

        oldRoomGifData.ShuffleGifs();
    }

}
