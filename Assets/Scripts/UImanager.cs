using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    // ��̬ʵ����ȷ����ȫ�ַ�Χ��ֻ��һ�� UImanager ʵ��
    public static UImanager Instance { get; private set; }

    // �������Ƴ���
    private static readonly string StartSceneName = "StartScene";
    private static readonly string GameSceneName = "GameScene";
    private static readonly string HowToPlaySceneName = "HowToPlay";

    // ����Ϸ���󱻼���ʱ����
    private void Awake()
    {
        // ȷ��ֻ����һ��ʵ��
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        // ��֤��������ڳ����л�ʱ���ᱻ����
        DontDestroyOnLoad(gameObject);
    }

    // ��ʼ��Ϸ
    public void StartGame()
    {
        LoadScene(GameSceneName);
        Time.timeScale = 1f;
    }

    // ���ص���ʼ����
    public void BackToStart()
    {
        LoadScene(StartSceneName);
    }

    // ��ʾ��Ϸ����
    public void HowToPlay()
    {
        LoadScene(HowToPlaySceneName);
    }

    // ͨ�õļ��س�������
    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
