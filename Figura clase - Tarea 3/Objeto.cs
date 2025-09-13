using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

public class Objeto
{
    public string Nombre { get; set; }

    // No usamos Vector3 para serialización
    public float CentroX { get; set; }
    public float CentroY { get; set; }
    public float CentroZ { get; set; }

    public List<Parte> Partes { get; private set; }

    public Objeto()
    {
        Partes = new List<Parte>();
        CentroX = 0f;
        CentroY = 0f;
        CentroZ = 0f;
    }

    public void AgregarParte(Parte parte)
    {
        Partes.Add(parte);
    }

    public void QuitarParte(Parte parte)
    {
        Partes.Remove(parte);
    }

    public Parte ObtenerParte(int index)
    {
        return Partes[index];
    }

    public void Dibujar()
    {

        foreach (var parte in Partes)
        {
            parte.Dibujar();
        }
    }
}
/*

- no usar vector3 por error en la serializacion
- agregar, quite, obtenga elemento de una lista (ya estan predefinidas partes.add,remove,etc)
- falta un metodo dibujar (centro de masa y dibujar de parte), no poner nada de opengl
- implementar un comportamiento de una superclase interface

*/
