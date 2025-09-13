using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Collections.Generic;

public class Cara
{
    public List<Vertice> Vertices { get; private set; }

    public Cara()
    {
        Vertices = new List<Vertice>();
    }

    public void AgregarVertice(Vertice v)
    {
        Vertices.Add(v);
    }

    public void QuitarVertice(Vertice v)
    {
        Vertices.Remove(v);
    }

    public Vertice ObtenerVertice(int index)
    {
        return Vertices[index];
    }

    public void Dibujar()
    {
        // CORRECCIÓN: Verificar que hay vértices
        if (Vertices.Count == 0) return;

        GL.UniformMatrix4(Game.uMVP, false, ref Game.MVP);

        // Arreglo de vértices [x,y,z] - TU CÓDIGO ORIGINAL
        float[] data = new float[Vertices.Count * 3];
        for (int i = 0; i < Vertices.Count; i++)
        {
            data[i * 3 + 0] = Vertices[i].X;
            data[i * 3 + 1] = Vertices[i].Y;
            data[i * 3 + 2] = Vertices[i].Z;
        }

        // TU CÓDIGO ORIGINAL - Sin cambios
        int vao = GL.GenVertexArray();
        int vbo = GL.GenBuffer();

        GL.BindVertexArray(vao);
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);

        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

        GL.DrawArrays(PrimitiveType.Triangles, 0, Vertices.Count);

        // TU CÓDIGO ORIGINAL - Sin cambios
        GL.DeleteBuffer(vbo);
        GL.DeleteVertexArray(vao);

        // CORRECCIÓN MENOR: Desenlazar para evitar problemas
        GL.BindVertexArray(0);
    }
}


/*

- instrucciones para cargar los vertices nada mas
- dibujar 
- aqui instrucciones de opengl cara y game

*/
