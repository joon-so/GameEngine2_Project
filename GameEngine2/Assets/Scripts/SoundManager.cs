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
    public static SoundManager instance;

    [Header("사운드 등록")]
    [SerializeField] Sound[] bgmSounds;
    [SerializeField] Sound[] sfxSounds;

    [Header("브금 플레이어")]
    [SerializeField] AudioSource bgmPlayer;

    [Header("효과음 플레이어")]
    [SerializeField] AudioSource sfxPlayer;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        PlayInGameBGM();
    }

    public void PlayTitleBGM()
    {
        bgmPlayer.clip = bgmSounds[0].clip;
        bgmPlayer.Play();
    }

    public void PlayShootingEffect()
    {
        //for(int i = 0; i< sfxSounds.Length; i++)
        //{
        //    if(_soundName == sfxSounds[i].soundName)
        //    {
        //        for(int x = 0; x<sfxSounds.Length; x++)
        //        {
        //            if(!sfxPlayer[x].isPlaying)
        //        }
        //    }
        //}

        //0 : PlayerSE
        sfxPlayer.clip = sfxSounds[0].clip;
        sfxPlayer.Play();
    }

    public void EnemyShootingEffect()
    {
        //1 : EnemySE
        sfxPlayer.clip = sfxSounds[1].clip;
        sfxPlayer.Play();
    }

    public void PlayInGameBGM()
    {
        bgmPlayer.clip = bgmSounds[1].clip;
        bgmPlayer.Play();
    }
}
