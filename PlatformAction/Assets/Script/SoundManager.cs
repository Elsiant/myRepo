using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;

    #region singleton
    void Awake()
    {
        if (null == _instance)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }
    #endregion singleton

    public AudioSource[]    _audioSourceEffects;
    public AudioSource      _audioSourceBgm;

    public string[]         _playSoundName;

    public Sound[]          _effectSounds;
    public Sound[]          _bgmSounds;

    void Start()
    {
        _playSoundName = new string[_audioSourceEffects.Length];
    }

    public void PlayBgm(string name)
    {
        foreach(var bgm in _bgmSounds)
        {
            if(name == bgm.name)
            {
                _audioSourceBgm.clip = bgm.clip;
                _audioSourceBgm.Play();
                return;
            }
        }
    }

    public void StopBgm()
    {
        _audioSourceBgm.Stop();
    }

    public void PlaySound(string name)
    {
        for (int i = 0; i < _effectSounds.Length; i++)
        {
            if (name == _effectSounds[i].name)
            {
                for (int j = 0; j < _audioSourceEffects.Length; j++)
                {
                    if (false == _audioSourceEffects[j].isPlaying)
                    {
                        _playSoundName[j] = _effectSounds[i].name;
                        _audioSourceEffects[j].clip = _effectSounds[i].clip;
                        _audioSourceEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 AudioSource가 사용중.");
                return;
            }
        }
        Debug.Log("등록되지 않은 사운드 호출 : " + name);
    }

    public void StopAllSound()
    {
        foreach(var audioSource in _audioSourceEffects)
        {
            audioSource.Stop();
        }
    }

    public void StopSound(string name)
    {
        for (int i = 0; i < _audioSourceEffects.Length; i++)
        {
            if (name == _playSoundName[i])
            {
                _audioSourceEffects[i].Stop();
                break;
            }
        }

        Debug.Log( name + "은 재생중이 아님");
    }

    public void SetVolumeBgm(float volume)
    {
        if(volume < 0 ||
            1.0f < volume)
        {
            Debug.Log("bgm volume 값이 범위를 벗어났습니다. 0 ~ 1");

            return;
        }

        _audioSourceBgm.volume = volume;
    }

    public void SetVolumeSound(float volume)
    {
        if (volume < 0 ||
            1.0f < volume)
        {
            Debug.Log("sound volume 값이 범위를 벗어났습니다. 0 ~ 1");

            return;
        }

        foreach(var audio in _audioSourceEffects)
        {
            audio.volume = volume;
        }
    }

    public float GetVolumeBGM()
    {
        return _audioSourceBgm.volume;
    }

    public float GetVolumeSound()
    {
        //볼륨은 모두 같으니 첫번째의 볼륨을 리턴한다.
        return _audioSourceEffects[0].volume;
    }
}
