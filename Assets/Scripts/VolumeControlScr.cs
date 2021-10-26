using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControlScr : MonoBehaviour
{

    private float lastVolume = 0.0f;
    const float volumeZero = -80.0f;

    #region Public Fields
    [SerializeField] string volumeParameter = "MasterVolume";
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;
    [SerializeField] float volumeMultiplier = 30f;
    [SerializeField] Toggle toggle;
    #endregion

    #region Unity Methods

    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderValueChanged);
        toggle.onValueChanged.AddListener(HandleToggleValueChanged);
    }

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat(volumeParameter, slider.value);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter, slider.value);
    }

    private void HandleToggleValueChanged(bool disableSound)
    {
        if (disableSound)
        {
            mixer.GetFloat(volumeParameter, out lastVolume);
            mixer.SetFloat(volumeParameter, volumeZero);
        }
        else
        {
            mixer.SetFloat(volumeParameter, lastVolume);
        }
    }

    private void HandleSliderValueChanged(float value)
    {
        if (toggle.isOn) {
            toggle.isOn = false;
        }
        mixer.SetFloat(volumeParameter, Mathf.Log10(value) * volumeMultiplier);
    }

    void Update()
    {
    }
 
    #endregion
 
    #region Private Methods
    #endregion
}
