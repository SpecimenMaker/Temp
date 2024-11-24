using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerate : MonoBehaviour
{
    // ������Ƭ��Ԥ����
    public GameObject[] groundTilePrefabs;
    // ����ߴ�
    public Vector2 groundSize;
    //������Ƭ�Ĵ�С
    public int groundTileSize;
    public void GenerateGround()
    {
        RemoveAllChildren(transform);
        // ���ڴ洢ʵ��������Ƭ����ʱ����
        GameObject temptile;

        // ѭ��������ͼ�Ŀ��
        for (int x = 0; x < groundSize.x; x++)
        {
            // ѭ��������ͼ�ĸ߶�
            for (int y = 0; y < groundSize.y; y++) 
            {
                // ����ÿ����Ƭ��λ��
                Vector3 tilePos = Coord.CoordToPos(x, y, -1, groundSize, groundTileSize) ;
                // ���ѡ��һ��Ԥ����
                GameObject selectedPrefab = groundTilePrefabs[Random.Range(0, groundTilePrefabs.Length)];
                // ʵ������Ƭ
                temptile = Instantiate(selectedPrefab, tilePos, selectedPrefab.transform.rotation);
                // ������Ƭ�ĸ�����
                temptile.transform.SetParent(transform);
            }
        }
        Debug.Log("�������ɳɹ�");
    }
    /// <summary>
    /// �Ƴ�����������
    /// </summary>
    /// <param name="trans">��Ҫ�Ƴ�������ĸ�����trans</param>
    public void RemoveAllChildren(Transform trans)
    {
        Transform transform;
        if (trans.childCount == 0)
        {
            Debug.Log("û�е�����Ƭ����");
            return;
        }

        // �Ӻ���ǰ����
        for (int i = trans.childCount - 1; i >= 0; i--)
        {
            // ��ȡ������
            transform = trans.transform.GetChild(i);
            // ����������
#if UNITY_EDITOR
            DestroyImmediate(transform.gameObject);
#else
            Destroy(transform.gameObject);
#endif
        }
        Debug.Log("����ɾ���ɹ�");
    }
}
