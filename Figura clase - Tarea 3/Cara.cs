using System.Collections.Generic;

public class Cara
{
    public List<Vertice> Vertices { get; private set; } = new List<Vertice>();

    public void AgregarVertice(Vertice v)
    {
        Vertices.Add(v);
    }
}

/*

- instrucciones para cargar los vertices nada mas
- dibujar 
- aqui instrucciones de opengl cara y game

*/