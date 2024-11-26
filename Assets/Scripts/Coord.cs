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

    // ��д GetHashCode ����
    public override int GetHashCode()
    {
        // ʹ�� x �� y �ֶ������ɹ�ϣ��
        return System.HashCode.Combine(x, y);
    }

    // ��д Equals ����
    public override bool Equals(object obj)
    {
        if (obj is Coord other)
        {
            return this.x == other.x && this.y == other.y;
        }
        return false;
    }

    //���� `ToString` ���������ڵ���
    public override string ToString()
    {
        return $"({x}, {y})";
    }
}

