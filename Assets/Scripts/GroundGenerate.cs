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
        Utility.RemoveAllChildren(transform);
        // ���ڴ洢ʵ��������Ƭ����ʱ����
        GameObject temptile;

        // ѭ��������ͼ�Ŀ��
        for (int x = 0; x < groundSize.x; x++)
        {
            // ѭ��������ͼ�ĸ߶�
            for (int y = 0; y < groundSize.y; y++) 
            {
                // ����ÿ����Ƭ��λ��
                Vector3 tilePos = Utility.CoordToPos(x, y, -1, groundSize, groundTileSize) ;
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


}
