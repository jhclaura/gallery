// by steffkelsey
// https://github.com/googlevr/gvr-unity-sdk/issues/263#issuecomment-248739572

using System.Collections;
using UnityEngine;

public class EyeLayerCull : MonoBehaviour
{
	public LayerMask LeftEyeMask;
	public LayerMask RightEyeMask;

	void Start()
	{
		StartCoroutine(LateStart());
	}

	protected IEnumerator LateStart()
	{
		yield return new WaitWhile(() => GvrViewer.Controller == null);
		yield return new WaitWhile(() => GvrViewer.Controller.Eyes.Length < 2);
		foreach (GvrEye gvrEye in GvrViewer.Controller.Eyes)
		{
			switch (gvrEye.eye)
			{
			case GvrViewer.Eye.Left:
				gvrEye.toggleCullingMask = LeftEyeMask;
				break;
			case GvrViewer.Eye.Right:
				gvrEye.toggleCullingMask = RightEyeMask;
				break;
			}
		}
		GvrViewer.Controller.UpdateStereoValues();
	}
}