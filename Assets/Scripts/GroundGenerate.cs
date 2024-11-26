using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerate : MonoBehaviour
{
    // 地面瓦片的预制体
    public GameObject[] groundTilePrefabs;
    // 地面尺寸
    public Vector2 groundSize;
    //地面瓦片的大小
    public int groundTileSize;
    public void GenerateGround()
    {
        Utility.RemoveAllChildren(transform);
        // 用于存储实例化的瓦片的临时变量
        GameObject temptile;

        // 循环遍历地图的宽度
        for (int x = 0; x < groundSize.x; x++)
        {
            // 循环遍历地图的高度
            for (int y = 0; y < groundSize.y; y++) 
            {
                // 计算每个瓦片的位置
                Vector3 tilePos = Utility.CoordToPos(x, y, -1, groundSize, groundTileSize) ;
                // 随机选择一个预制体
                GameObject selectedPrefab = groundTilePrefabs[Random.Range(0, groundTilePrefabs.Length)];
                // 实例化瓦片
                temptile = Instantiate(selectedPrefab, tilePos, selectedPrefab.transform.rotation);
                // 设置瓦片的父对象
                temptile.transform.SetParent(transform);
            }
        }
        Debug.Log("地面生成成功");
    }


}
