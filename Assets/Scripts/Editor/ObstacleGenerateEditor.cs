using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// 在 Inspector 中绘制按钮
[CustomEditor(typeof(ObstacleGenerate))]
public class ObstacleGenerateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // 绘制默认的 Inspector 界面
        ObstacleGenerate myScript = target as ObstacleGenerate;
        if (GUILayout.Button("生成障碍"))
        {
            myScript.GenerateObstacle(); // 点击按钮时执行函数
        }
    }
}
