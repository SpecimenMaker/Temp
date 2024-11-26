using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerate : MonoBehaviour
{
    // ������ӣ����ڴ�����Ƭ˳��
    public int seed;
    // �ϰ���Ƭ��Ԥ����
    public GameObject[] obstacleTilePrefabs;
    // ����ߴ�
    public Vector2 groundSize;
    //�ϰ���Ƭ�Ĵ�С
    public int obstacleTileSize;
    //���������С
    public int centerSize;
    // �ϰ�����ռ����
    [Range(0, 1)]
    public float obstaclePercent;
    // ������Ƭ������б�
    List<Coord> allTileCoords;
    // ���Һ����Ƭ�������
    Queue<Coord> shuffledTileCooeds;
    /// <summary>
    /// �����ϰ�������⽨��
    /// </summary>
    public void GenerateObstacle()
    {
        Utility.RemoveAllChildren(transform);
        // ��ʼ����Ƭ�����б�
        allTileCoords = new List<Coord>();
        for (int x = 0; x < groundSize.x; x++)
        {
            for (int y = 0; y < groundSize.y; y++)
            {
                // ���ÿ����Ƭ�����굽�б�
                allTileCoords.Add(new Coord(x, y));
            }
        }
        // ��������˳��
        shuffledTileCooeds = new Queue<Coord>(Utility.ShuffleArray(allTileCoords.ToArray(), seed));
        // ���ڴ洢�ϰ���λ�õĲ�������
        bool[,] obstacleMap = new bool[(int)groundSize.x, (int)groundSize.y];
        // ���ݱ���������ͨ�ϰ�����������
        int maxObstacleCount = (int)(groundSize.x * groundSize.y * obstaclePercent);
        // ��ǰ�ϰ������
        int currentObstaclCount = 0;
        for (int i = 0; i < maxObstacleCount; i++)
        {
            // ��ȡ�������
            Coord randomCoord = Utility.GetRandomCoord(shuffledTileCooeds);
            if (obstacleMap[randomCoord.x, randomCoord.y] == false)
            {
                obstacleMap[randomCoord.x, randomCoord.y] = true;
                currentObstaclCount++;
                // ����ϰ���λ���Ƿ��ʺ�
                if (!IsInCenter(randomCoord.x, randomCoord.y, centerSize) && MapIsFullyAccessible(obstacleMap, currentObstaclCount))
                {
                    Vector3 obstaclePos = Utility.CoordToPos(randomCoord.x, randomCoord.y, 0, groundSize, obstacleTileSize); // �����ϰ���λ��
                                                                                                                                                             // ���ѡ��һ��Ԥ����
                    GameObject selectedPrefab = obstacleTilePrefabs[Random.Range(0, obstacleTilePrefabs.Length)];
                    // ʵ����Ԥ���壬��������ԭʼ��ת
                    GameObject newObstacle = Instantiate(selectedPrefab, obstaclePos, selectedPrefab.transform.rotation);
                    // �����ϰ���ĸ�����
                    newObstacle.transform.SetParent(transform);
                }
                else
                {
                    obstacleMap[randomCoord.x, randomCoord.y] = false; // �����������ȡ�����
                    currentObstaclCount--; // �ϰ��������һ
                }
            }
        }
    }

    bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount)
    {
        // �����ͼ����
        Coord mapCenter = new Coord((int)groundSize.x / 2, (int)groundSize.y / 2);
        bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)]; // �ɴ��ͼ���
        Queue<Coord> queue = new Queue<Coord>(); // BFS����
        queue.Enqueue(mapCenter); // �����Ŀ�ʼ
        mapFlags[mapCenter.x, mapCenter.y] = true; // �������Ϊ�ѷ���
        int accesibleTileCount = 1; // �ɴ���Ƭ����
        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue(); // ȡ������
            // ������������ڽӵ���Ƭ
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int neighbourX = tile.x + x;
                    int neighbourY = tile.y + y;
                    if (x == 0 || y == 0) // ֻ������������
                    {
                        // ����ڽ���Ƭ�Ƿ���Ч���Ҳ���Ϊ�ϰ���
                        if (neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0) && neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1))
                            if (!mapFlags[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY])
                            {
                                mapFlags[neighbourX, neighbourY] = true; // ���Ϊ�ɷ���
                                queue.Enqueue(new Coord(neighbourX, neighbourY)); // �������
                                accesibleTileCount++; // ���ӿɴ����
                            }
                    }
                }
            }
        }
        int targetAcessibleTileCount = (int)(groundSize.x * groundSize.y - currentObstacleCount); // Ŀ��ɴ���Ƭ����
        return targetAcessibleTileCount == accesibleTileCount; // �����Ƿ���ȫ�ɴ�
    }

    /// <summary>
    /// ���ָ�������Ƿ��ڵ�ͼ����������
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool IsInCenter(int x, int y,int centerSize)
    {
        // �����ͼ����
        Coord mapCenter = new Coord((int)groundSize.x / 2, (int)groundSize.y / 2);
        return x >= mapCenter.x - centerSize && x <= mapCenter.x + centerSize && y >= mapCenter.y - centerSize && y <= mapCenter.y + centerSize;
    }
}
