using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    // 静态实例，确保在全局范围内只有一个 UImanager 实例
    public static UImanager Instance { get; private set; }

    // 场景名称常量
    private static readonly string StartSceneName = "StartScene";
    private static readonly string GameSceneName = "GameScene";
    private static readonly string HowToPlaySceneName = "HowToPlay";

    // 在游戏对象被加载时调用
    private void Awake()
    {
        // 确保只存在一个实例
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        // 保证这个对象在场景切换时不会被销毁
        DontDestroyOnLoad(gameObject);
    }

    // 开始游戏
    public void StartGame()
    {
        LoadScene(GameSceneName);
        Time.timeScale = 1f;
    }

    // 返回到开始界面
    public void BackToStart()
    {
        LoadScene(StartSceneName);
    }

    // 显示游戏规则
    public void HowToPlay()
    {
        LoadScene(HowToPlaySceneName);
    }

    // 通用的加载场景方法
    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
