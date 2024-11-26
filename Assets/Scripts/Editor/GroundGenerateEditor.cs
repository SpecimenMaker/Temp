using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// �� Inspector �л��ư�ť
[CustomEditor(typeof(GroundGenerate))]
public class GroundGenerateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // ����Ĭ�ϵ� Inspector ����
        GroundGenerate myScript = target as GroundGenerate;
        if (GUILayout.Button("���ɵ���"))
        {
            myScript.GenerateGround(); // �����ťʱִ�к���
        }
    }
}
