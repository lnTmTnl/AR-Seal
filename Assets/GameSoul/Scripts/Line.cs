using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line
{
    public float Size { get; }

    public List<Vector2> Points { get; }

    public Line(float size, List<Vector2> points)
    {
        this.Size = size;
        this.Points = points;
    }
}
