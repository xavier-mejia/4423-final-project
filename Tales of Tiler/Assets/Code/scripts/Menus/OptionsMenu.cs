using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Toggle fullscreen;
    public Toggle vsync;
    public List<Resolution> resolutions = new();
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

        bool hasCurrentResolution = false;
        for (int i = 0; i < resolutions.Count(); i++)
        {
            if ((Screen.width == resolutions[i].width) && (Screen.height == resolutions[i].height))
            {
                hasCurrentResolution = true;
                _resolutionIndex = i;
                UpdateResolutionText();
            }
        }

        if (!hasCurrentResolution)
        {
            Resolution newResolution = new Resolution()
            {
                width = Screen.width,
                height = Screen.height
            };
            resolutions.Add(newResolution);
            
            /* Assuming all resolutions have a unique width & height,
             it should be safe to compare each resolution by width only.*/
            resolutions.Sort((res1, res2) => res1.width.CompareTo(res2.width));
            
            _resolutionIndex = resolutions.FindIndex(r =>
                r.width == newResolution.width
                && r.height == newResolution.height
            );

            UpdateResolutionText();
            Debug.Log("Index: " + _resolutionIndex);
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
        resolutionText.text = resolutions[_resolutionIndex].ToString();
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

    public override string ToString()
    {
        return width + "x" + height;
    }
}