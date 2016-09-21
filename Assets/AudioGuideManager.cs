using UnityEngine;
using System.Collections;

public class AudioGuideManager : MonoBehaviour {

    public GameObject leftButton;
    public GameObject rightButton;

    Vector3 resetAngle = new Vector3();
    Vector3 leftDownAngle = new Vector3(0, 0, -20);
    Vector3 rightDownAngle = new Vector3(0, 0, 20);

    int currentAudioIndex = 0;
    public AudioSource[] audioGuides = new AudioSource[3];

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

        currentAudioIndex--;
        SwitchAudios(currentAudioIndex);
    }

    public void ReleaseRightButton()
    {
        rightButton.transform.localEulerAngles = resetAngle;

        currentAudioIndex++;
        SwitchAudios(currentAudioIndex);
    }

    void SwitchAudios(int audioIndex)
    {
        int currentOnIndex = Mathf.Abs(audioIndex % 3);

        for (var i = 0; i < audioGuides.Length; i++)
        {
            if (i == currentOnIndex)
                audioGuides[i].enabled = true;
            else
                audioGuides[i].enabled = false;
        }
    }
}
