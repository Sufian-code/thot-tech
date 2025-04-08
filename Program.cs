// Program.cs
using System;
using SplashKitSDK;
using System.IO; // Required for Directory

public class Program
{
    public static void Main()
    {
        Controls controls = new Controls();  // Create instance from separate file
        Json controlsJson = controls.GetControlsJson(); // Use instance method

        Game game = new Game(); // Create instance from separate file
        Console.WriteLine("Current Directory: " + Directory.GetCurrentDirectory());

        Window gameWindow = new Window("Arcade Machine - Single Combat", 800, 600);

        // --- Farthest Point on Circle Demonstration ---
        Point2D circleCenter = new Point2D() { X = 400, Y = 300 };
        double circleRadius = 50;
        Point2D givenPoint = new Point2D() { X = 100, Y = 100 };

        Vector2D vectorToCenter = SplashKit.VectorFromPointToPoint(givenPoint, circleCenter);
        double headingToCenter = SplashKit.VectorHeading(vectorToCenter);
        Point2D farthestPoint = SplashKit.DistantPointOnCircleHeading(circleCenter, circleRadius, headingToCenter + 180);
        // --- End of Farthest Point Demonstration ---

        while (!gameWindow.CloseRequested)
        {
            SplashKit.ProcessEvents();
            game.Update();
            game.Draw();

            // --- Drawing for Farthest Point Demonstration ---
            SplashKit.DrawCircle(Color.Blue, circleCenter.X, circleCenter.Y, circleRadius);
            SplashKit.FillRectangle(Color.Red, givenPoint.X - 5, givenPoint.Y - 5, 10, 10);
            SplashKit.FillRectangle(Color.Green, farthestPoint.X - 5, farthestPoint.Y - 5, 10, 10);
            SplashKit.DrawLine(Color.Yellow, givenPoint.X, givenPoint.Y, farthestPoint.X, farthestPoint.Y);
            // --- End of Drawing for Farthest Point Demonstration ---
        }

        gameWindow.Close();
    }
}
