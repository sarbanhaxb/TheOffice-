using UnityEngine;


[System.Serializable]
public class RadioStation
{
    public string stationName;
    public AudioClip audioClip;

    [Range(0, 1)] public float volume = 1f;
    public bool loop = true;

    [TextArea] public string description;
}
