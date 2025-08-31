using OpenTK.Mathematics;
using System.Collections.Generic;

public class Objeto
{
    public string Nombre { get; set; }
    public Vector3 CentroDeMasa { get; set; } // Centro del objeto
    public List<Parte> Partes { get; private set; } = new List<Parte>();

    public void AgregarParte(Parte parte)
    {
        Partes.Add(parte);
    }
}

/*

- no usar vector3 por error en la serializacion
- agregar, quite, obtenga elemento de una lista (ya estan predefinidas partes.add,remove,etc)
- falta un metodo dibujar (centro de masa y dibujar de parte), no poner nada de opengl
- implementar un comportamiento de una superclase interface

*/