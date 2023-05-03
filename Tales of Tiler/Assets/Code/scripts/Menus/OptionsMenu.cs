using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Toggle fullscreen;
    public Toggle vsync;
    public List<Resolution> resolutions = new List<Resolution>();
    public TMP_Text resolutionText;
    private int _resolutionIndex;

    public void Start()
    {
        fullscreen.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vsync.isOn = false;
        }
        else
        {
            vsync.isOn = true;
        }
    }

    public void DecreaseResolution()
    {
        if (_resolutionIndex > 0)
        {
            _resolutionIndex--;
        }
        
        UpdateResolutionText();
    }

    public void IncreaseResolution()
    {
        if (_resolutionIndex < resolutions.Count - 1)
        {
            _resolutionIndex++;
        }
        
        UpdateResolutionText();
    }

    private void UpdateResolutionText()
    {
        resolutionText.text = resolutions[_resolutionIndex].width.ToString() + "x" +
                              resolutions[_resolutionIndex].height.ToString();
    }
    
    public void ApplyGraphics()
    {
        QualitySettings.vSyncCount = vsync.isOn ? 1 : 0;
        Screen.SetResolution(
            resolutions[_resolutionIndex].width,
            resolutions[_resolutionIndex].height,
            fullscreen.isOn
            );
    }
}

[System.Serializable]
public class Resolution
{
    public int width;
    public int height;
}