using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 静态实例，确保在全局范围内只有一个 GameManager 实例
    public static GameManager Instance { get; private set; }

    // 控制游戏是否暂停
    private bool isPaused = false;

    // 在游戏对象被加载时调用
    private void Awake()
    {
        // 如果 GameManager 的实例为空，设置为当前实例
        if (Instance == null)
            Instance = this;
        // 如果实例已存在且不是当前实例，则销毁当前对象，确保只存在一个 GameManager
        else if (Instance != this)
            Destroy(gameObject);

        // 保证这个对象在场景切换时不会被销毁
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // 初始状态，游戏不暂停
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 处理游戏暂停和恢复
        HandlePauseToggle();

        // 处理退出游戏
        HandleQuit();
    }
    #region 退出游戏
    /// <summary>
    /// 处理退出游戏的操作
    /// </summary>
    private void HandleQuit()
    {
        // 按下 Escape 键时退出游戏
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    /// <summary>
    /// 退出游戏的方法（此方法在打包后的游戏中生效）
    /// </summary>
    private void QuitGame()
    {
        // 在编辑模式下不允许调用退出应用的功能
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // 在编辑器中停止游戏（此行在打包后不可用）
#else
        Application.Quit();  // 调用 Application.Quit() 来退出游戏（此方法在打包后的游戏中生效）
#endif
    }
    #endregion
    #region 暂停游戏
    /// <summary>
    /// 切换游戏的暂停/恢复状态
    /// </summary>
    private void HandlePauseToggle()
    {
        // 按下空格键时切换暂停状态
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;  // 暂停或恢复游戏
        }
    }
    #endregion
}
