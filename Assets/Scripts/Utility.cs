using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Utility:MonoBehaviour
{
    /// <summary>
    /// 随机打乱给定数组的元素顺序。
    /// </summary>
    /// <typeparam name="T">数组元素的类型。</typeparam>
    /// <param name="array">要打乱的数组。</param>
    /// <param name="seed">随机数生成器的种子，确保每次相同输入时得到相同结果。</param>
    /// <returns>打乱后的数组。</returns>
    public static T[] ShuffleArray<T>(T[] array, int seed)
    {
        // 创建一个新的随机数生成器，使用指定的种子值
        System.Random prng = new System.Random(seed);

        // 遍历数组的每个元素，除了最后一个
        for (int i = 0; i < array.Length - 1; i++)
        {
            // 生成一个范围在当前索引到数组末尾的随机索引
            int randomIndex = prng.Next(i, array.Length);
            // 交换当前元素与随机索引处的元素
            T tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }

        // 返回打乱后的数组
        return array;
    }
    /// <summary>
    /// 将坐标转换为世界坐标
    /// </summary>
    /// <param name="x">坐标X</param>
    /// <param name="y">坐标Y</param>
    /// <param name="z">坐标Z</param>
    /// <param name="mapSize">地图大小</param>
    /// <param name="tileSize">瓦片大小</param>
    /// <returns>返回世界坐标</returns>
    public static Vector3 CoordToPos(int x, int y, int z, Vector2 mapSize, int tileSize)
    {
        return new Vector3(-mapSize.x / 2 + 0.5f + x, z, -mapSize.y / 2 + 0.5f + y) * tileSize;
    }
    public static Vector2Int PosToCoord(Vector3 pos, Vector2 mapSize, float tileSize)
    {
        // 计算瓦片坐标
        int x = Mathf.FloorToInt(pos.x / tileSize + mapSize.x / 2 - 0.5f);
        int y = Mathf.FloorToInt(pos.z / tileSize + mapSize.y / 2 - 0.5f);

        return new Vector2Int(x, y);
    }
    /// <summary>
    /// 传入一个打乱后的坐标队列,获取一个随机坐标，并将其返回到队列末尾，以便后续调用时可以再次使用。
    /// </summary>
    /// <returns>一个随机的坐标（类型为 Coord）。</returns>
    public static Coord GetRandomCoord<Coord>(Queue<Coord> shuffledTileCooeds)
    {
        Coord randomCoord = shuffledTileCooeds.Dequeue(); // 从队列中获取坐标
        shuffledTileCooeds.Enqueue(randomCoord); // 将坐标重新加入队列
        return randomCoord; // 返回获取的随机坐标
    }
    /// <summary>
    /// 移除所有子物体
    /// </summary>
    /// <param name="trans">需要移除子物体的父物体trans</param>
    public static void RemoveAllChildren(Transform trans)
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
