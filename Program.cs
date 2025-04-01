using System;
using SplashKitSDK;

public class Program
{
    public static void Main()
    {
        Controls controls = new Controls();  // Create instance
        Json controlsJson = controls.GetControlsJson(); // Use instance method

        Game game = new Game();
        Console.WriteLine("Current Directory: " + Directory.GetCurrentDirectory());

        Window gameWindow = new Window("Arcade Machine - Single Combat", 800, 600);

        while (!gameWindow.CloseRequested)
        {
            SplashKit.ProcessEvents();
            game.Update();
            game.Draw();
        }

        gameWindow.Close();
    }
}
