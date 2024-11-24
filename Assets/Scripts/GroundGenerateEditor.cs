using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// 在 Inspector 中绘制按钮
[CustomEditor(typeof(GroundGenerate))]
public class GroundGenerateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // 绘制默认的 Inspector 界面
        GroundGenerate myScript = target as GroundGenerate;
        if (GUILayout.Button("生成地面"))
        {
            myScript.GenerateGround(); // 点击按钮时执行函数
        }
    }
}
