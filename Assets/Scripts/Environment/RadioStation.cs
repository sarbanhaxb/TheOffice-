using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class RadioStation
{
    public string stationName;
    public List<AudioClip> audioClips; // Теперь список клипов

    [Range(0, 1)] public float volume = 1f;
    public bool loop = true;
    public bool shuffle = true; // Включить случайный порядок

    [TextArea] public string description;

    private int currentClipIndex = 0;
    private List<int> playOrder = new List<int>();

    public AudioClip GetNextClip()
    {
        if (audioClips.Count == 0) return null;

        if (playOrder.Count == 0)
        {
            // Генерируем новый порядок воспроизведения
            playOrder = new List<int>();
            for (int i = 0; i < audioClips.Count; i++) playOrder.Add(i);

            if (shuffle)
            {
                // Алгоритм Фишера-Йетса для перемешивания
                for (int i = 0; i < playOrder.Count; i++)
                {
                    int temp = playOrder[i];
                    int r = Random.Range(i, playOrder.Count);
                    playOrder[i] = playOrder[r];
                    playOrder[r] = temp;
                }
            }
        }

        currentClipIndex = (currentClipIndex + 1) % playOrder.Count;
        return audioClips[playOrder[currentClipIndex]];
    }
}
