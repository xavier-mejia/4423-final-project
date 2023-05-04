using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Toggle fullscreen;
    public Toggle vsync;
    public List<Resolution> resolutions = new();
    public AudioMixer mixer;
    public TMP_Text resolutionText;
    public TMP_Text masterText, musicText, sfxText;
    public Slider masterSlider, musicSlider, sfxSlider;
    private int _resolutionIndex;

    private void Start()
    {
        fullscreen.isOn = Screen.fullScreen;
        vsync.isOn = QualitySettings.vSyncCount == 0 ? true : false;
        Resolution currentResolution = new Resolution
        {
            width = Screen.width,
            height = Screen.height
        };
        AddResolutionIfNotPresent(currentResolution);
        InitializeVolumeSliders();
    }

    private void DecreaseResolution()
    {
        if (_resolutionIndex > 0)
        {
            _resolutionIndex--;
        }
        
        UpdateResolutionText();
    }

    private void IncreaseResolution()
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
    
    private void ApplyGraphics()
    {
        QualitySettings.vSyncCount = vsync.isOn ? 1 : 0;
        Screen.SetResolution(
            resolutions[_resolutionIndex].width,
            resolutions[_resolutionIndex].height,
            fullscreen.isOn
            );
    }
    
    public void SetMasterVolume()
    {
        masterText.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();
        mixer.SetFloat("MasterVolume", masterSlider.value);
        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
    }
    
    public void SetMusicVolume()
    {
        musicText.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        mixer.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }
    
    public void SetSfxVolume()
    {
        sfxText.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();
        mixer.SetFloat("SfxVolume", sfxSlider.value);
        PlayerPrefs.SetFloat("SfxVolume", sfxSlider.value);
    }

    private void InitializeVolumeSliders()
    {
        float volume = 0f;
        mixer.GetFloat("MasterVolume", out volume);
        masterSlider.value = volume;
        masterText.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();

        
        mixer.GetFloat("MusicVolume", out volume);
        musicSlider.value = volume;
        musicText.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();

        mixer.GetFloat("SfxVolume", out volume);
        sfxSlider.value = volume;
        sfxText.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();
    }
    
    private void AddResolutionIfNotPresent(Resolution resolution)
    {
        bool hasCurrentResolution = false;
        for (int i = 0; i < resolutions.Count(); i++)
        {
            if ((resolution.width == resolutions[i].width) && (resolution.height == resolutions[i].height))
            {
                hasCurrentResolution = true;
                _resolutionIndex = i;
                UpdateResolutionText();
            }
        }

        if (!hasCurrentResolution)
        {
            resolutions.Add(resolution);
            
            /* Assuming all resolutions have a unique width & height,
             it should be safe to compare each resolution by width only.*/
            resolutions.Sort((res1, res2) => res1.width.CompareTo(res2.width));
            
            _resolutionIndex = resolutions.FindIndex(r =>
                r.width == resolution.width
                && r.height == resolution.height
            );

            UpdateResolutionText();
            Debug.Log("Index: " + _resolutionIndex);
        }
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