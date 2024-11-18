using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // 声音源，用于播放音效和背景音乐
    [Header("Audio Sources")]
    [SerializeField] private AudioSource efxSource;  // 音效音源
    [SerializeField] private AudioSource musicSource;  // 音乐音源

    // 音高范围，控制音频播放时的随机音高
    [Header("Pitch Range")]
    [SerializeField] private float lowPitchRange = 0.95f;  // 最低音高
    [SerializeField] private float highPitchRange = 1.05f;  // 最高音高

    // 背景音乐数组，可以存储多个起始和结束音乐
    [Header("Music Clips")]
    [SerializeField] private AudioClip[] startMusic;  // 游戏开始时的音乐
    [SerializeField] private AudioClip[] endingMusic;  // 游戏结束时的音乐

    // 静态实例，确保在全局范围内只有一个 AudioManager 实例
    public static AudioManager Instance { get; private set; }

    // 在游戏对象被加载时调用
    private void Awake()
    {
        // 确保只有一个 AudioManager 实例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 保证这个对象在场景切换时不会被销毁
        }
        else if (Instance != this)
        {
            Destroy(gameObject);  // 销毁重复的实例
        }
    }

    #region 控制音频播放函数

    // 播放背景音乐的函数，传入多个音频剪辑作为参数
    public void PlayMusic(params AudioClip[] clips)
    {
        PlayAudio(musicSource, clips);
    }

    // 播放随机音效的函数，传入多个音效片段作为参数
    public void RandomizeSfx(params AudioClip[] clips)
    {
        PlayAudio(efxSource, clips);
    }

    #endregion
    #region 私有辅助函数

    // 一个私有的函数，负责播放指定音频源的音频
    private void PlayAudio(AudioSource source, AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0) return;

        // 随机选择一个音频片段
        int randomIndex = Random.Range(0, clips.Length);
        AudioClip selectedClip = clips[randomIndex];

        // 随机设置音频的音高
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        // 设置音频源的音频剪辑和音高
        source.clip = selectedClip;
        source.pitch = randomPitch;

        // 播放音频
        source.Play();
    }

    #endregion
}
