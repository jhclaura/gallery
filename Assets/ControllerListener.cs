using UnityEngine;
using System.Collections;
using VRTK;

public class ControllerListener : MonoBehaviour {
    VRTK_ControllerEvents controllerEvents;

    public RemoteManager remoteManager;
    public AudioGuideManager audioGuideManager;
    bool isLeft = false;

    void Start () {
        controllerEvents = GetComponent<VRTK_ControllerEvents>();
        if (controllerEvents == null)
        {
            Debug.LogError("It is required to be attached to a SteamVR Controller that has the VRTK_ControllerEvents script attached to it");
            return;
        }

        controllerEvents.TouchpadPressed += new ControllerInteractionEventHandler(DoTouchpadPressed);
        controllerEvents.TouchpadReleased += new ControllerInteractionEventHandler(DoTouchpadReleased);

        //controllerEvents.TouchpadTouchStart += new ControllerInteractionEventHandler(DoTouchpadTouchStart);
        //controllerEvents.TouchpadTouchEnd += new ControllerInteractionEventHandler(DoTouchpadTouchEnd);

        //controllerEvents.TouchpadAxisChanged += new ControllerInteractionEventHandler(DoTouchpadAxisChanged);
    }

    private void DebugLogger(uint index, string button, string action, ControllerInteractionEventArgs e)
    {
        Debug.Log("Controller on index '" + index + "' " + button + " has been " + action
                + " with a pressure of " + e.buttonPressure + " / trackpad axis at: " + e.touchpadAxis + " (" + e.touchpadAngle + " degrees)");        
    }

    private void DoTouchpadPressed(object sender, ControllerInteractionEventArgs e)
    {
        //DebugLogger(e.controllerIndex, "TOUCHPAD", "pressed down", e);

        if (e.touchpadAxis.y > 0)
            isLeft = false;
        else
            isLeft = true;

        // REMOTE
        if (remoteManager.gameObject.activeSelf)
        {
            if(isLeft) remoteManager.PressLeftButton();
            else remoteManager.PressRightButton();
        }

        // AUDIOGUIDE
        if(audioGuideManager.gameObject.activeSelf)
        {
            if (isLeft) audioGuideManager.PressLeftButton();
            else audioGuideManager.PressRightButton();
        }
    }

    private void DoTouchpadReleased(object sender, ControllerInteractionEventArgs e)
    {
        //DebugLogger(e.controllerIndex, "TOUCHPAD", "released", e);

        // REMOTE
        if (remoteManager.gameObject.activeSelf)
        {
            if (isLeft) remoteManager.ReleaseLeftButton();
            else remoteManager.ReleaseRightButton();
        }

        // AUDIOGUIDE
        if (audioGuideManager.gameObject.activeSelf)
        {
            if (isLeft) audioGuideManager.ReleaseLeftButton();
            else audioGuideManager.ReleaseRightButton();
        }
    }

    private void DoTouchpadTouchStart(object sender, ControllerInteractionEventArgs e)
    {
        //DebugLogger(e.controllerIndex, "TOUCHPAD", "touched", e);
    }

    private void DoTouchpadTouchEnd(object sender, ControllerInteractionEventArgs e)
    {
        //DebugLogger(e.controllerIndex, "TOUCHPAD", "untouched", e);
    }

    private void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
    {
        //DebugLogger(e.controllerIndex, "TOUCHPAD", "axis changed", e);
    }
}
