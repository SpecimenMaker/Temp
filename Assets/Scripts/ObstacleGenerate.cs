using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerate : MonoBehaviour
{
    // 随机种子，用于打乱瓦片顺序
    public int seed;
    // 障碍瓦片的预制体
    public GameObject[] obstacleTilePrefabs;
    // 地面尺寸
    public Vector2 groundSize;
    //障碍瓦片的大小
    public int obstacleTileSize;
    //中兴区域大小
    public int centerSize;
    // 障碍物所占比例
    [Range(0, 1)]
    public float obstaclePercent;
    // 所有瓦片坐标的列表
    List<Coord> allTileCoords;
    // 打乱后的瓦片坐标队列
    Queue<Coord> shuffledTileCooeds;
    /// <summary>
    /// 生成障碍物和特殊建筑
    /// </summary>
    public void GenerateObstacle()
    {
        Utility.RemoveAllChildren(transform);
        // 初始化瓦片坐标列表
        allTileCoords = new List<Coord>();
        for (int x = 0; x < groundSize.x; x++)
        {
            for (int y = 0; y < groundSize.y; y++)
            {
                // 添加每个瓦片的坐标到列表
                allTileCoords.Add(new Coord(x, y));
            }
        }
        // 打乱坐标顺序
        shuffledTileCooeds = new Queue<Coord>(Utility.ShuffleArray(allTileCoords.ToArray(), seed));
        // 用于存储障碍物位置的布尔数组
        bool[,] obstacleMap = new bool[(int)groundSize.x, (int)groundSize.y];
        // 根据比例计算普通障碍物的最大数量
        int maxObstacleCount = (int)(groundSize.x * groundSize.y * obstaclePercent);
        // 当前障碍物计数
        int currentObstaclCount = 0;
        for (int i = 0; i < maxObstacleCount; i++)
        {
            // 获取随机坐标
            Coord randomCoord = Utility.GetRandomCoord(shuffledTileCooeds);
            if (obstacleMap[randomCoord.x, randomCoord.y] == false)
            {
                obstacleMap[randomCoord.x, randomCoord.y] = true;
                currentObstaclCount++;
                // 检查障碍物位置是否适合
                if (!IsInCenter(randomCoord.x, randomCoord.y, centerSize) && MapIsFullyAccessible(obstacleMap, currentObstaclCount))
                {
                    Vector3 obstaclePos = Utility.CoordToPos(randomCoord.x, randomCoord.y, 0, groundSize, obstacleTileSize); // 计算障碍物位置
                                                                                                                                                             // 随机选择一个预制体
                    GameObject selectedPrefab = obstacleTilePrefabs[Random.Range(0, obstacleTilePrefabs.Length)];
                    // 实例化预制体，并保持其原始旋转
                    GameObject newObstacle = Instantiate(selectedPrefab, obstaclePos, selectedPrefab.transform.rotation);
                    // 设置障碍物的父对象
                    newObstacle.transform.SetParent(transform);
                }
                else
                {
                    obstacleMap[randomCoord.x, randomCoord.y] = false; // 如果不合适则取消标记
                    currentObstaclCount--; // 障碍物计数减一
                }
            }
        }
    }

    bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount)
    {
        // 计算地图中心
        Coord mapCenter = new Coord((int)groundSize.x / 2, (int)groundSize.y / 2);
        bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)]; // 可达地图标记
        Queue<Coord> queue = new Queue<Coord>(); // BFS队列
        queue.Enqueue(mapCenter); // 从中心开始
        mapFlags[mapCenter.x, mapCenter.y] = true; // 标记中心为已访问
        int accesibleTileCount = 1; // 可达瓦片计数
        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue(); // 取出队首
            // 检查上下左右邻接的瓦片
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int neighbourX = tile.x + x;
                    int neighbourY = tile.y + y;
                    if (x == 0 || y == 0) // 只考虑上下左右
                    {
                        // 检查邻接瓦片是否有效并且不可为障碍物
                        if (neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0) && neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1))
                            if (!mapFlags[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY])
                            {
                                mapFlags[neighbourX, neighbourY] = true; // 标记为可访问
                                queue.Enqueue(new Coord(neighbourX, neighbourY)); // 加入队列
                                accesibleTileCount++; // 增加可达计数
                            }
                    }
                }
            }
        }
        int targetAcessibleTileCount = (int)(groundSize.x * groundSize.y - currentObstacleCount); // 目标可达瓦片数量
        return targetAcessibleTileCount == accesibleTileCount; // 返回是否完全可达
    }

    /// <summary>
    /// 检查指定坐标是否在地图中心区域内
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool IsInCenter(int x, int y,int centerSize)
    {
        // 计算地图中心
        Coord mapCenter = new Coord((int)groundSize.x / 2, (int)groundSize.y / 2);
        return x >= mapCenter.x - centerSize && x <= mapCenter.x + centerSize && y >= mapCenter.y - centerSize && y <= mapCenter.y + centerSize;
    }
}
