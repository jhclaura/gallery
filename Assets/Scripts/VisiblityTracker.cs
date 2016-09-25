using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisiblityTracker : MonoBehaviour {

    public System.Action BecameVisibleCallback;
    public System.Action BecameInvisibleCallback;

    private string currentVisibilityState;
    private Renderer[] renderers;
    private IEnumerator pendingUpdate;

    // Use this for initialization
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>(true);

        foreach (Renderer renderer in renderers)
        {
            VisibilityChangeCallback callback = renderer.gameObject.AddComponent<VisibilityChangeCallback>();
            callback.Callback = OnChildVisibilityChange;
        }

        currentVisibilityState = GetVisibilityState();
        Debug.Log(gameObject.name + " is initially " + currentVisibilityState);
    }

    void OnChildVisibilityChange() {
        UpdateVisibilityState();
    }

    void UpdateVisibilityState()
    {
        string visibilityState = "invisible";

        foreach (Renderer renderer in renderers)
        {
            if (renderer.isVisible)
            {
                visibilityState = "visible";
            }
        }

        if (visibilityState != currentVisibilityState)
        {
            Debug.Log(gameObject.name + " is now " + visibilityState + " and was " + currentVisibilityState);
            currentVisibilityState = visibilityState;

            if (pendingUpdate != null)
            {
                StopCoroutine(pendingUpdate);
                pendingUpdate = null;
            }

            if (visibilityState == "visible" && BecameVisibleCallback != null)
            {
                pendingUpdate = CallCallback(BecameVisibleCallback);
            } else if (visibilityState == "invisible" && BecameInvisibleCallback != null)
            {
                pendingUpdate = CallCallback(BecameInvisibleCallback);
            }

            if (pendingUpdate != null)
            {
                StartCoroutine(pendingUpdate);
            }
        }
    }

    IEnumerator CallCallback(System.Action Callback)
    {
        yield return new WaitForEndOfFrame();
        Callback();
    }

    string GetVisibilityState()
    {
        string visibilityState = "visible";

        foreach (Renderer renderer in renderers)
        {
            if (renderer.isVisible)
            {
                visibilityState = "invisible";
            }
        }

        return visibilityState;
    }

}
