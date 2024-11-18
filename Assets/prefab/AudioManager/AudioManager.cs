using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // ����Դ�����ڲ�����Ч�ͱ�������
    [Header("Audio Sources")]
    [SerializeField] private AudioSource efxSource;  // ��Ч��Դ
    [SerializeField] private AudioSource musicSource;  // ������Դ

    // ���߷�Χ��������Ƶ����ʱ���������
    [Header("Pitch Range")]
    [SerializeField] private float lowPitchRange = 0.95f;  // �������
    [SerializeField] private float highPitchRange = 1.05f;  // �������

    // �����������飬���Դ洢�����ʼ�ͽ�������
    [Header("Music Clips")]
    [SerializeField] private AudioClip[] startMusic;  // ��Ϸ��ʼʱ������
    [SerializeField] private AudioClip[] endingMusic;  // ��Ϸ����ʱ������

    // ��̬ʵ����ȷ����ȫ�ַ�Χ��ֻ��һ�� AudioManager ʵ��
    public static AudioManager Instance { get; private set; }

    // ����Ϸ���󱻼���ʱ����
    private void Awake()
    {
        // ȷ��ֻ��һ�� AudioManager ʵ��
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // ��֤��������ڳ����л�ʱ���ᱻ����
        }
        else if (Instance != this)
        {
            Destroy(gameObject);  // �����ظ���ʵ��
        }
    }

    #region ������Ƶ���ź���

    // ���ű������ֵĺ�������������Ƶ������Ϊ����
    public void PlayMusic(params AudioClip[] clips)
    {
        PlayAudio(musicSource, clips);
    }

    // ���������Ч�ĺ�������������ЧƬ����Ϊ����
    public void RandomizeSfx(params AudioClip[] clips)
    {
        PlayAudio(efxSource, clips);
    }

    #endregion
    #region ˽�и�������

    // һ��˽�еĺ��������𲥷�ָ����ƵԴ����Ƶ
    private void PlayAudio(AudioSource source, AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0) return;

        // ���ѡ��һ����ƵƬ��
        int randomIndex = Random.Range(0, clips.Length);
        AudioClip selectedClip = clips[randomIndex];

        // ���������Ƶ������
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        // ������ƵԴ����Ƶ����������
        source.clip = selectedClip;
        source.pitch = randomPitch;

        // ������Ƶ
        source.Play();
    }

    #endregion
}
