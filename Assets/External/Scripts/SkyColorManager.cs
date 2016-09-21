using UnityEngine;
using System.Collections;

public class SkyColorManager : MonoBehaviour {

    [System.Serializable]
    public class SkyboxColorSet
    {
        public Color32[] colors = new Color32[2];
    }
    public SkyboxColorSet[] skyboxColors;
    public Material skyboxMaterial;

    public Color[] ambientColors;

    float brightnessFrom = 0.1f;
    float brightnessTo = 0.1f;

    public void UpdateSkyboxColor(int floorIndex) {
        skyboxMaterial.SetColor("_Color1", skyboxColors[floorIndex].colors[1]);
        skyboxMaterial.SetColor("_Color2", skyboxColors[floorIndex].colors[0]);
    }

    public void UpdateAmbientColor(int floorIndex)
    {
        RenderSettings.ambientLight = ambientColors[floorIndex];
    }

    public void UpdateAmbientColorBrightness(int floorIndex) {
        if (floorIndex == 0)
        {
            brightnessFrom = 0.6f;
            brightnessTo = 0.1f;
        }
        else
        {
            brightnessFrom = 0.1f * (floorIndex);
            brightnessTo = 0.1f * (floorIndex + 1);
        }
        Debug.Log("change ac brightness from " + brightnessFrom + "to " + brightnessTo);
        //LeanTween.value( transform.gameObject, UpdateAC, brightnessFrom, brightnessTo, 1f);
    }

    
}
