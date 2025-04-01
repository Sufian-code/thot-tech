using System;
using System.Collections.Generic;
using SplashKitSDK;

public class Game
{
    private List<Block> _blocks = new List<Block>();
    private Controls _controls;

    public Game()
    {
        _controls = new Controls(); // Initialize controls
        SpawnNewBlocks(); // Initialize first blocks
    }

    public void HandleInput()
    {
        if (SplashKit.KeyDown(_controls.KeyLookup("MoveLeft")))
        {
            foreach (var block in _blocks) block.MoveLeft();
        }

        if (SplashKit.KeyDown(_controls.KeyLookup("MoveRight")))
        {
            foreach (var block in _blocks) block.MoveRight();
        }

        if (SplashKit.KeyTyped(_controls.KeyLookup("Rotate")))
        {
            foreach (var block in _blocks) block.Rotate();
        }

        if (SplashKit.KeyDown(_controls.KeyLookup("Drop")))
        {
            foreach (var block in _blocks) block.Drop();
        }
    }

    private bool HasBlockLanded()
    {
        foreach (var block in _blocks)
        {
            if (block.Y >= SplashKit.ScreenHeight() - block.Height) // Ensure Block has a Height property
            {
                return true;
            }
        }
        return false;
    }

    private void SpawnNewBlocks()
    {
        _blocks.Clear(); // Remove old blocks

        // Create new block pair at the top
        _blocks.Add(new Block(200, 0, Color.Red));
        _blocks.Add(new Block(220, 0, Color.Blue));
    }

    public void Update()
    {
        HandleInput();

        // Move blocks down
        foreach (var block in _blocks) block.Drop();

        // Respawn new blocks when landed
        if (HasBlockLanded())
        {
            SpawnNewBlocks();
        }
    }

    public void Draw()
    {
        SplashKit.ClearScreen();
        foreach (var block in _blocks) block.Draw();
        SplashKit.RefreshScreen();
    }
}
