using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// �� Inspector �л��ư�ť
[CustomEditor(typeof(ObstacleGenerate))]
public class ObstacleGenerateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // ����Ĭ�ϵ� Inspector ����
        ObstacleGenerate myScript = target as ObstacleGenerate;
        if (GUILayout.Button("�����ϰ�"))
        {
            myScript.GenerateObstacle(); // �����ťʱִ�к���
        }
    }
}
