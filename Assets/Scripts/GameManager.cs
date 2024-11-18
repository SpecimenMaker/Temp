using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ��̬ʵ����ȷ����ȫ�ַ�Χ��ֻ��һ�� GameManager ʵ��
    public static GameManager Instance { get; private set; }

    // ������Ϸ�Ƿ���ͣ
    private bool isPaused = false;

    // ����Ϸ���󱻼���ʱ����
    private void Awake()
    {
        // ��� GameManager ��ʵ��Ϊ�գ�����Ϊ��ǰʵ��
        if (Instance == null)
            Instance = this;
        // ���ʵ���Ѵ����Ҳ��ǵ�ǰʵ���������ٵ�ǰ����ȷ��ֻ����һ�� GameManager
        else if (Instance != this)
            Destroy(gameObject);

        // ��֤��������ڳ����л�ʱ���ᱻ����
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // ��ʼ״̬����Ϸ����ͣ
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ������Ϸ��ͣ�ͻָ�
        HandlePauseToggle();

        // �����˳���Ϸ
        HandleQuit();
    }
    #region �˳���Ϸ
    /// <summary>
    /// �����˳���Ϸ�Ĳ���
    /// </summary>
    private void HandleQuit()
    {
        // ���� Escape ��ʱ�˳���Ϸ
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    /// <summary>
    /// �˳���Ϸ�ķ������˷����ڴ�������Ϸ����Ч��
    /// </summary>
    private void QuitGame()
    {
        // �ڱ༭ģʽ�²���������˳�Ӧ�õĹ���
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // �ڱ༭����ֹͣ��Ϸ�������ڴ���󲻿��ã�
#else
        Application.Quit();  // ���� Application.Quit() ���˳���Ϸ���˷����ڴ�������Ϸ����Ч��
#endif
    }
    #endregion
    #region ��ͣ��Ϸ
    /// <summary>
    /// �л���Ϸ����ͣ/�ָ�״̬
    /// </summary>
    private void HandlePauseToggle()
    {
        // ���¿ո��ʱ�л���ͣ״̬
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;  // ��ͣ��ָ���Ϸ
        }
    }
    #endregion
}
