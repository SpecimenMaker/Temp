// Coord.cs
using UnityEngine;
public class Coord
{
    public int x; // x����
    public int y; // y����

    public Coord(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public static bool operator !=(Coord a, Coord b) // ���ز����������
    {
        return !(a == b);
    }

    public static bool operator ==(Coord a, Coord b) // ���ص��������
    {
        return a.x == b.x && a.y == b.y;
    }

    //���� `ToString` ���������ڵ���
    public override string ToString()
    {
        return $"({x}, {y})";
    }
    /// <summary>
    /// ������ת��Ϊ��������
    /// </summary>
    /// <param name="x">����X</param>
    /// <param name="y">����Y</param>
    /// <param name="mapSize">��ͼ��С</param>
    /// <param name="tileSize">��Ƭ��С</param>
    /// <returns>������������</returns>
    public static Vector3 CoordToPos(int x, int y,int z, Vector2 mapSize, int tileSize)
    {
        return new Vector3(-mapSize.x / 2 + 0.5f + x, z, -mapSize.y / 2 + 0.5f + y) * tileSize;
    }
}

