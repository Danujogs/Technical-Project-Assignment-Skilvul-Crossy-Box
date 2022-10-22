using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] AudioMixer SFXMixer;
    private AudioSource AudioSource;
    //public GameObject objectMusic;
    public Slider SFXSlider;
    [SerializeField] float _multiplier = 50f;

    private void Awake()
    {
        SFXSlider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void HandleSliderValueChanged(float value)
    {
        SFXMixer.SetFloat("SFX_Volume", MathF.Log10(value) * _multiplier);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("SFX_Volume", SFXSlider.value);
    }

    void Start()
    {
        SFXSlider.value = PlayerPrefs.GetFloat("SFX_Volume", SFXSlider.value);

        //objectMusic = GameObject.FindWithTag("SFX");
        //AudioSource = objectMusic.GetComponent<AudioSource>();


        // SFXVolume = PlayerPrefs.GetFloat("Volume");
        // SFXMixer.volume = SFXVolume;
        // SFXSlider.value = SFXVolume;
    }

    void Update()
    {
        // AudioMixer

        // SFXMixer.volume = SFXVolume;
        // PlayerPrefs.SetFloat("Volume", SFXVolume);
    }

    public void UpdateVolume(float volume)
    {
        // SFXVolume = volume;
    }

}

