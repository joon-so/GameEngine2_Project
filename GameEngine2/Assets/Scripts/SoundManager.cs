using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    [Header("사운드 등록")]
    [SerializeField] Sound[] bgmSounds;

    [Header("브금 플레이어")]
    [SerializeField] AudioSource bgmPlayer;

    // Start is called before the first frame update
    void Start()
    {
        PlayInGameBGM();
    }

    public void PlayTitleBGM()
    {
        bgmPlayer.clip = bgmSounds[0].clip;
        bgmPlayer.Play();
    }

    public void PlayInGameBGM()
    {
        bgmPlayer.clip = bgmSounds[1].clip;
        bgmPlayer.Play();
    }
}
