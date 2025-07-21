using UnityEngine;
using System.Collections.Generic;
using System;
using JetBrains.Annotations;

public class RadioSystem : MonoBehaviour
{
    [Header("Настройки радио")]
    [SerializeField] private List<RadioStation> stations = new List<RadioStation>();
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float frequencyChangeSpeed = 0.2f;

    [Header("Визуальные элементы")]
    [SerializeField] private TMPro.TMP_Text stationDisplay;
    [SerializeField] private GameObject radioDisplay;

    private int _currentStationIndex = -1;
    private bool _isRadioOn = false;
    private float _frequencyChangeCooldown = 0;

    private void Awake()
    {
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        TurnOff();
    }

    private void Update()
    {
        if (_frequencyChangeCooldown > 0)
        {
            _frequencyChangeCooldown -= Time.deltaTime;
        }
    }

    public void TurnOn()
    {
        _isRadioOn = true;
        radioDisplay.SetActive(true);

        if (stations.Count > 0)
        {
            _currentStationIndex = 0;
            PlayCurrentStation();
        }
    }

    public void TurnOff()
    {
        _isRadioOn = false;
        audioSource.Stop();
        radioDisplay.SetActive(false);
        _currentStationIndex = -1;
    }

    public void NextStation()
    {
        if (!_isRadioOn || stations.Count == 0 || _frequencyChangeCooldown > 0)
        {
            return;
        }
        _currentStationIndex = (_currentStationIndex + 1) % stations.Count;
        PlayCurrentStation();
        _frequencyChangeCooldown = frequencyChangeSpeed;
    }

    public void PreviousStation()
    {
        if (!_isRadioOn || stations.Count == 0 || _frequencyChangeCooldown > 0)
        {
            return;
        }

        _currentStationIndex = (_currentStationIndex - 1 + stations.Count) % stations.Count;
        PlayCurrentStation();
        _frequencyChangeCooldown = frequencyChangeSpeed;
    }

    private void PlayCurrentStation()
    {
        if (_currentStationIndex < 0 || _currentStationIndex >= stations.Count)
        {
            return;
        }

        RadioStation station = stations[_currentStationIndex];
        audioSource.clip = station.audioClip;
        audioSource.volume = station.volume;
        audioSource.loop = station.loop;
        audioSource.Play();

        if (stationDisplay != null)
        {
            stationDisplay.text = $"{station.stationName}\n<size=60%>{station.description}</size>";
        }
    }

    public string GetCurrentStationName()
    {
        if (!_isRadioOn || _currentStationIndex < 0)
        {
            return "RADIO OFF";
        }

        return stations[_currentStationIndex].stationName;
    }
}
