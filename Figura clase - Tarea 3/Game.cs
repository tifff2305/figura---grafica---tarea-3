using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace Figura_Tarea2
{
    public class Game : GameWindow
    {
        private List<Objeto> objetos = new();

        // Shader variables
        private int _shaderProgram;
        private int _uMVP;

        // Cámara y proyección
        private Matrix4 _view;
        private Matrix4 _projection;

        public Game(GameWindowSettings gw, NativeWindowSettings nw) : base(gw, nw) { }

        private Parte CrearParteConCaras(Vector3 centro, List<List<Vector3>> carasRelativas)
        {
            Parte parte = new Parte { CentroDeMasa = centro };
            foreach (var caraVerts in carasRelativas)
            {
                Cara cara = new Cara();
                foreach (var v in caraVerts)
                    cara.AgregarVertice(new Vertice(v.X, v.Y, v.Z));
                parte.AgregarCara(cara);
            }
            parte.CrearBuffers();
            return parte;
        }

        protected override void OnLoad()
        {
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1f);
            GL.Enable(EnableCap.DepthTest);

            // Crear shader simple
            string vertexSrc = @"
                #version 330 core
                layout(location = 0) in vec3 aPosition;
                layout(location = 1) in vec3 aColor;
                out vec3 vColor;
                uniform mat4 uMVP;
                void main()
                {
                    vColor = aColor;
                    gl_Position = uMVP * vec4(aPosition, 1.0);
                }";
            string fragmentSrc = @"
                #version 330 core
                in vec3 vColor;
                out vec4 FragColor;
                void main()
                {
                    FragColor = vec4(vColor, 1.0);
                }";

            _shaderProgram = GL.CreateProgram();
            int vs = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vs, vertexSrc);
            GL.CompileShader(vs);
            GL.AttachShader(_shaderProgram, vs);

            int fs = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fs, fragmentSrc);
            GL.CompileShader(fs);
            GL.AttachShader(_shaderProgram, fs);

            GL.LinkProgram(_shaderProgram);
            GL.DeleteShader(vs);
            GL.DeleteShader(fs);

            _uMVP = GL.GetUniformLocation(_shaderProgram, "uMVP");

            // Configurar cámara y proyección
            _view = Matrix4.LookAt(new Vector3(0, 1, 3), Vector3.Zero, Vector3.UnitY);
            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60f), Size.X / (float)Size.Y, 0.1f, 100f);

            // Definir caras de la pantalla
            List<List<Vector3>> carasPantalla = new()
            {
                new List<Vector3> {
                    new Vector3(-0.5f, -0.5f, 0.0f),
                    new Vector3(0.5f, -0.5f, 0.0f),
                    new Vector3(0.5f, 0.5f, 0.0f),
                    new Vector3(-0.5f, 0.5f, 0.0f)
                }
            };

            // Crear pantalla
            Objeto pantalla = new Objeto { Nombre = "Pantalla", CentroDeMasa = new Vector3(-4, 0, -6) };
            objetos.Add(pantalla);

            // Crear CPU
            Objeto cpu = new Objeto { Nombre = "CPU", CentroDeMasa = new Vector3(0.5f, -0.4f, -1.0f) };
            objetos.Add(cpu);

            // Crear teclado
            Objeto teclado = new Objeto { Nombre = "Teclado", CentroDeMasa = new Vector3(-4, -0.8f, -4.6f) };
            objetos.Add(teclado);

        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(_shaderProgram);

            foreach (var obj in objetos)
            {
                foreach (var parte in obj.Partes)
                {
                    // Crear modelo para la parte
                    Matrix4 model = Matrix4.CreateTranslation(obj.CentroDeMasa + parte.CentroDeMasa);
                    Matrix4 mvp = model * _view * _projection;
                    GL.UniformMatrix4(_uMVP, false, ref mvp);

                    parte.Dibujar(obj.CentroDeMasa);
                }
            }

            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60f), Size.X / (float)Size.Y, 0.1f, 100f);
        }
    }
}

/*

- cargar los objetos, pero sin estar en una clase
- render: solo llamar al objeto y darle dibujar, se para ejecutando n-veces
- load: se ejecuta una sola vez, ahi esta los vertices.. pero luego lo sacamos y lo llevamos a serializar
- model, view, projection va en el load ya que solo una vez se ejecuta
- codigo opengl, aqui en el game y en la cara

*/
