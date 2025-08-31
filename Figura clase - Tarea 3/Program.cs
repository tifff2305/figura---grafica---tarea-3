using System;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace Figura_Tarea2
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameWindowSettings = new GameWindowSettings
            {
                UpdateFrequency = 60.0
            };

            var nativeWindowSettings = new NativeWindowSettings
            {
                ClientSize = new Vector2i(800, 600),
                Title = "Figura",
                Flags = OpenTK.Windowing.Common.ContextFlags.ForwardCompatible
            };

            using (var game = new Game(gameWindowSettings, nativeWindowSettings))
            {
                game.Run();
            }
        }
    }
}
