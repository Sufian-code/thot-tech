using System;
using SplashKitSDK;

public class Block
{
    public float X { get; private set; }
    public float Y { get; private set; }
    public float Height { get; } = 20; // Define height
    public Color BlockColor { get; private set; }
    
    private const float Speed = 2.0f;

    public Block(float x, float y, Color color)
    {
        X = x;
        Y = y;
        BlockColor = color;
    }

    public void MoveLeft()
    {
        X -= 20;
    }

    public void MoveRight()
    {
        X += 20;
    }

    public void Drop()
    {
        Y += Speed;
    }

    public void Rotate()
    {
        // Simple rotation effect (not full matrix transformation)
        float temp = X;
        X = Y;
        Y = temp;
    }

    public void Draw()
    {
        SplashKit.FillRectangle(BlockColor, X, Y, 20, 20);
    }
}
