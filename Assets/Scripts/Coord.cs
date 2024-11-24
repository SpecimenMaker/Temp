// Coord.cs
using UnityEngine;
public class Coord
{
    public int x; // x坐标
    public int y; // y坐标

    public Coord(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public static bool operator !=(Coord a, Coord b) // 重载不等于运算符
    {
        return !(a == b);
    }

    public static bool operator ==(Coord a, Coord b) // 重载等于运算符
    {
        return a.x == b.x && a.y == b.y;
    }

    //重载 `ToString` 方法，便于调试
    public override string ToString()
    {
        return $"({x}, {y})";
    }
    /// <summary>
    /// 将坐标转换为世界坐标
    /// </summary>
    /// <param name="x">坐标X</param>
    /// <param name="y">坐标Y</param>
    /// <param name="mapSize">地图大小</param>
    /// <param name="tileSize">瓦片大小</param>
    /// <returns>返回世界坐标</returns>
    public static Vector3 CoordToPos(int x, int y,int z, Vector2 mapSize, int tileSize)
    {
        return new Vector3(-mapSize.x / 2 + 0.5f + x, z, -mapSize.y / 2 + 0.5f + y) * tileSize;
    }
}

