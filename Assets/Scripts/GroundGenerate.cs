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
        RemoveAllChildren(transform);
        // 用于存储实例化的瓦片的临时变量
        GameObject temptile;

        // 循环遍历地图的宽度
        for (int x = 0; x < groundSize.x; x++)
        {
            // 循环遍历地图的高度
            for (int y = 0; y < groundSize.y; y++) 
            {
                // 计算每个瓦片的位置
                Vector3 tilePos = Coord.CoordToPos(x, y, -1, groundSize, groundTileSize) ;
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
    /// <summary>
    /// 移除所有子物体
    /// </summary>
    /// <param name="trans">需要移除子物体的父物体trans</param>
    public void RemoveAllChildren(Transform trans)
    {
        Transform transform;
        if (trans.childCount == 0)
        {
            Debug.Log("没有地面瓦片存在");
            return;
        }

        // 从后向前遍历
        for (int i = trans.childCount - 1; i >= 0; i--)
        {
            // 获取子物体
            transform = trans.transform.GetChild(i);
            // 销毁子物体
#if UNITY_EDITOR
            DestroyImmediate(transform.gameObject);
#else
            Destroy(transform.gameObject);
#endif
        }
        Debug.Log("地面删除成功");
    }
}
