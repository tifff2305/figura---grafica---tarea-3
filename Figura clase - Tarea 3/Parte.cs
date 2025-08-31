using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Collections.Generic;

public class Parte
{
    public Vector3 CentroDeMasa;
    public List<Cara> Caras = new List<Cara>();

    // Datos GPU
    private int _vao, _vbo, _ebo;
    private bool _uploaded = false;
    private int _indiceCount;

    public void AgregarCara(Cara cara) => Caras.Add(cara);

    public void CrearBuffers()
    {
        List<float> vertices = new();
        List<uint> indices = new();
        uint offset = 0;

        foreach (var cara in Caras)
        {
            foreach (var vert in cara.Vertices)
            {
                // Posición absoluta
                vertices.Add(vert.Posicion.X + CentroDeMasa.X);
                vertices.Add(vert.Posicion.Y + CentroDeMasa.Y);
                vertices.Add(vert.Posicion.Z + CentroDeMasa.Z);

                // Color (podemos usar blanco por defecto)
                vertices.Add(1f);
                vertices.Add(1f);
                vertices.Add(1f);
            }

            // Crear índices para triangulación (convierte n-gon a triángulos)
            for (int i = 1; i < cara.Vertices.Count - 1; i++)
            {
                indices.Add(offset);
                indices.Add(offset + (uint)i);
                indices.Add(offset + (uint)(i + 1));
            }
            offset += (uint)cara.Vertices.Count;
        }

        _indiceCount = indices.Count;

        _vao = GL.GenVertexArray();
        _vbo = GL.GenBuffer();
        _ebo = GL.GenBuffer();

        GL.BindVertexArray(_vao);

        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * sizeof(float), vertices.ToArray(), BufferUsageHint.StaticDraw);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Count * sizeof(uint), indices.ToArray(), BufferUsageHint.StaticDraw);

        int stride = 6 * sizeof(float);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, stride, 0);
        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, stride, 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);

        GL.BindVertexArray(0);
    }

    public void Dibujar(Vector3 centroObjeto)
    {
        GL.BindVertexArray(_vao);
        GL.DrawElements(PrimitiveType.Triangles, _indiceCount, DrawElementsType.UnsignedInt, 0);
        GL.BindVertexArray(0);

    }
}

/*
 
- Debe tener un centro de masa
- Dibujar (centro de masa y dibujar)
- Igual tener add y demas

- viene a ser un copy de objeto

 */

