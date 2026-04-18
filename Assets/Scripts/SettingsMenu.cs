using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider mainVolSlider;
    [SerializeField] private Slider musicVolSlider;
    [SerializeField] private Slider effectsVolSlider;
    [SerializeField] private Slider uiEffectsVolSlider;
    
    void Start()
    {
        LoadAudio();
    }

    private void LoadAudio()
    {
        Assert.IsTrue(mainVolSlider && musicVolSlider  && effectsVolSlider && uiEffectsVolSlider, 
            "No audio slider in SettingsMenu game object");
            
        float mainVolLvl = PlayerPrefs.GetFloat("mainVolume", 0.75f);
        float musicVolLvl = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        float effectsVolLvl = PlayerPrefs.GetFloat("effectsVolume", 0.50f);
        float uiEffectsVolLvl = PlayerPrefs.GetFloat("uiEffectsVolume", 0.50f);
            
        mainVolSlider.value = mainVolLvl;
        musicVolSlider.value = musicVolLvl;
        effectsVolSlider.value = effectsVolLvl;
        uiEffectsVolSlider.value = uiEffectsVolLvl;
            
        audioMixer.SetFloat ("mainVolume", Mathf.Log10(mainVolLvl) * 20);
        audioMixer.SetFloat ("musicVolume", Mathf.Log10(musicVolLvl) * 20);
        audioMixer.SetFloat ("soundEffectsVolume", Mathf.Log10(effectsVolLvl) * 20);
        audioMixer.SetFloat ("uiEffectsVolume", Mathf.Log10(uiEffectsVolLvl) * 20);
    }
    
    public void SetMainVolumeLevel(float masterVolLvl)
    {
        audioMixer.SetFloat ("mainVolume", Mathf.Log10(masterVolLvl) * 20);
        PlayerPrefs.SetFloat("mainVolume", masterVolLvl);
    }

    public void SetMusicVolumeLevel(float musicVolLvl)
    {
        audioMixer.SetFloat ("musicVolume", Mathf.Log10(musicVolLvl) * 20);
        PlayerPrefs.SetFloat("musicVolume", musicVolLvl);
    }
        
    public void SetEffectsVolumeLevel(float effectsVolLvl)
    {
        audioMixer.SetFloat ("soundEffectsVolume", Mathf.Log10(effectsVolLvl) * 20);
        PlayerPrefs.SetFloat("effectsVolume", effectsVolLvl);
    }
     
    public void SetUIEffectsVolumeLevel(float uiEffectsVolLvl)
    {
        audioMixer.SetFloat ("uiEffectsVolume", Mathf.Log10(uiEffectsVolLvl) * 20);
        PlayerPrefs.SetFloat("uiEffectsVolume", uiEffectsVolLvl);
    }
}
