using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Menu : MonoBehaviour
{
    public GameObject title;
    public GameObject play;
    public GameObject loadingScreen;
    public GameObject optionsMenu;
    public TMP_Dropdown resolution;
    public TMP_Dropdown quality;
    public TMP_Dropdown texture;
    public TMP_Dropdown aa;
    public Slider volumeSlider;
    public AudioSource audioSource;
    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolution.ClearOptions();

        List<string> options = new();
        
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                
                currentResolutionIndex = i;
        }

        resolution.AddOptions(options);
        resolution.value = currentResolutionIndex;
        resolution.RefreshShownValue();

        LoadSettings(currentResolutionIndex);

        title.SetActive(true);
        play.SetActive(true);
        optionsMenu.SetActive(false);
        loadingScreen.SetActive(false);
    }

    public void Play()
    {
        title.SetActive(false);
        play.SetActive(false);
        optionsMenu.SetActive(false);
        loadingScreen.SetActive(true);
    }

    public void Options()
    {
        title.SetActive(false);
        play.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Back()
    {
        title.SetActive(true);
        play.SetActive(true);
        optionsMenu.SetActive(false);

        PlayerPrefs.SetInt("QualitySettingPreference", quality.value);
        PlayerPrefs.SetInt("ResolutionPreference", resolution.value);
        PlayerPrefs.SetInt("TextureQualityPreference", texture.value);
        PlayerPrefs.SetInt("AntiAliasingPreference", aa.value);
        PlayerPrefs.SetInt("FullscreenPreference", Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("VolumePreference", audioSource.volume);
    }

    public void exit()
    {
        Application.Quit();
    }

    void OnEnable()
    {
        volumeSlider.onValueChanged.AddListener(delegate { changeVolume(volumeSlider.value); });
    }

    void changeVolume(float sliderValue)
    {
        audioSource.volume = sliderValue;
    }

    void OnDisable()
    {
        volumeSlider.onValueChanged.RemoveAllListeners();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        if (qualityIndex != 6)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
            switch (qualityIndex)
            {
                case 0:
                    texture.value = 0;
                    aa.value = 0;
                    break;

                case 1:
                    texture.value = 0;
                    aa.value = 0;
                    break;

                case 2:
                    texture.value = 0;
                    aa.value = 0;
                    break;

                case 3:
                    texture.value = 1;
                    aa.value = 0;
                    break;

                case 4:
                    texture.value = 2;
                    aa.value = 1;
                    break;

                case 5:
                    texture.value = 3;
                    aa.value = 2;
                    break;
            }

            quality.value = qualityIndex;
        }
    }

    public void SetTextureQuality(int textureIndex)
    {
        QualitySettings.masterTextureLimit = textureIndex;
        quality.value = 6;
    }
    public void SetAntiAliasing(int aaIndex)
    {
        QualitySettings.antiAliasing = aaIndex;
        quality.value = 6;
    }
    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("QualitySettingPreference"))

            quality.value = PlayerPrefs.GetInt("QualitySettingPreference");
       
        else
            quality.value = 3;

        if (PlayerPrefs.HasKey("ResolutionPreference"))
            
            resolution.value = PlayerPrefs.GetInt("ResolutionPreference");
        
        else
            
            resolution.value = currentResolutionIndex;
        
        if (PlayerPrefs.HasKey("TextureQualityPreference"))
           
            texture.value = PlayerPrefs.GetInt("TextureQualityPreference");
        
        else
         
            texture.value = 0;
        
        if (PlayerPrefs.HasKey("AntiAliasingPreference"))
          
            aa.value = PlayerPrefs.GetInt("AntiAliasingPreference");
        
        else
          
            aa.value = 1;
        
        if (PlayerPrefs.HasKey("FullscreenPreference"))
          
            Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        
        else
         
            Screen.fullScreen = true;
        
        if (PlayerPrefs.HasKey("VolumePreference"))
          
            volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
        
        else
           
            volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
    }
}
