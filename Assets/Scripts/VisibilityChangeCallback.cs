using UnityEngine;
using System.Collections;

public class VisibilityChangeCallback : MonoBehaviour {

    public System.Action Callback;

    void OnBecameVisible()
    {
        CallCallback();
    }

    void OnBecameInvisible()
    {
        CallCallback();
    }

    void CallCallback()
    {
        Callback();
    }
}
