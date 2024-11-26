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

    // 重写 GetHashCode 方法
    public override int GetHashCode()
    {
        // 使用 x 和 y 字段来生成哈希码
        return System.HashCode.Combine(x, y);
    }

    // 重写 Equals 方法
    public override bool Equals(object obj)
    {
        if (obj is Coord other)
        {
            return this.x == other.x && this.y == other.y;
        }
        return false;
    }

    //重载 `ToString` 方法，便于调试
    public override string ToString()
    {
        return $"({x}, {y})";
    }
}

