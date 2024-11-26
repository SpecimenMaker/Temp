using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Utility:MonoBehaviour
{
    /// <summary>
    /// ������Ҹ��������Ԫ��˳��
    /// </summary>
    /// <typeparam name="T">����Ԫ�ص����͡�</typeparam>
    /// <param name="array">Ҫ���ҵ����顣</param>
    /// <param name="seed">����������������ӣ�ȷ��ÿ����ͬ����ʱ�õ���ͬ�����</param>
    /// <returns>���Һ�����顣</returns>
    public static T[] ShuffleArray<T>(T[] array, int seed)
    {
        // ����һ���µ��������������ʹ��ָ��������ֵ
        System.Random prng = new System.Random(seed);

        // ���������ÿ��Ԫ�أ��������һ��
        for (int i = 0; i < array.Length - 1; i++)
        {
            // ����һ����Χ�ڵ�ǰ����������ĩβ���������
            int randomIndex = prng.Next(i, array.Length);
            // ������ǰԪ���������������Ԫ��
            T tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }

        // ���ش��Һ������
        return array;
    }
    /// <summary>
    /// ������ת��Ϊ��������
    /// </summary>
    /// <param name="x">����X</param>
    /// <param name="y">����Y</param>
    /// <param name="z">����Z</param>
    /// <param name="mapSize">��ͼ��С</param>
    /// <param name="tileSize">��Ƭ��С</param>
    /// <returns>������������</returns>
    public static Vector3 CoordToPos(int x, int y, int z, Vector2 mapSize, int tileSize)
    {
        return new Vector3(-mapSize.x / 2 + 0.5f + x, z, -mapSize.y / 2 + 0.5f + y) * tileSize;
    }
    public static Vector2Int PosToCoord(Vector3 pos, Vector2 mapSize, float tileSize)
    {
        // ������Ƭ����
        int x = Mathf.FloorToInt(pos.x / tileSize + mapSize.x / 2 - 0.5f);
        int y = Mathf.FloorToInt(pos.z / tileSize + mapSize.y / 2 - 0.5f);

        return new Vector2Int(x, y);
    }
    /// <summary>
    /// ����һ�����Һ���������,��ȡһ��������꣬�����䷵�ص�����ĩβ���Ա��������ʱ�����ٴ�ʹ�á�
    /// </summary>
    /// <returns>һ����������꣨����Ϊ Coord����</returns>
    public static Coord GetRandomCoord<Coord>(Queue<Coord> shuffledTileCooeds)
    {
        Coord randomCoord = shuffledTileCooeds.Dequeue(); // �Ӷ����л�ȡ����
        shuffledTileCooeds.Enqueue(randomCoord); // ���������¼������
        return randomCoord; // ���ػ�ȡ���������
    }
    /// <summary>
    /// �Ƴ�����������
    /// </summary>
    /// <param name="trans">��Ҫ�Ƴ�������ĸ�����trans</param>
    public static void RemoveAllChildren(Transform trans)
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
