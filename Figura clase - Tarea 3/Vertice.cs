using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

public class Vertice
{
    // Posición
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public Vertice(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Vertice(float x, float y, float z, float r, float g, float b)
    {
        X = x;
        Y = y;
        Z = z;
    }

}
