using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Collections.Generic;

public class Parte
{
    // Centro de masa separado por coordenadas
    public float CentroX { get; set; }
    public float CentroY { get; set; }
    public float CentroZ { get; set; }

    public List<Cara> Caras { get; private set; }

    public Parte()
    {
        Caras = new List<Cara>();
        CentroX = 0f;
        CentroY = 0f;
        CentroZ = 0f;
    }

    public void AgregarCara(Cara cara)
    {
        Caras.Add(cara);
    }

    public void QuitarCara(Cara cara)
    {
        Caras.Remove(cara);
    }

    public Cara ObtenerCara(int index)
    {
        return Caras[index];
    }

    public void Dibujar()
    {

        foreach (var cara in Caras)
        {
            cara.Dibujar();
        }

    }
}

/*
 
- Debe tener un centro de masa
- Dibujar (centro de masa y dibujar)
- Igual tener add y demas
- no usar vector3 por la serializacion
- viene a ser un copy de objeto en teoria

 */
