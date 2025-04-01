using System;
using System.Collections.Generic;
using SplashKitSDK;

public class Game
{
    private List<Block> _blocks = new List<Block>();
    private Controls _controls;
    private SplashKitSDK.Timer _gameTimer; // Explicitly specify SplashKitSDK.Timer
    private double _lastDropTime;
    private const double NormalDropInterval = 500; // 500ms per drop
    private const double FastDropInterval = 100;   // 100ms per drop when holding Down

    public Game()
    {
        _controls = new Controls();
        _gameTimer = new SplashKitSDK.Timer("GameTimer"); // Explicitly use SplashKitSDK.Timer
        _gameTimer.Start();
        SpawnNewBlocks();
        _lastDropTime = _gameTimer.Ticks;
    }

    private void SpawnNewBlocks()
    {
        _blocks.Clear();
        _blocks.Add(new Block(200, 0, Color.Red));
        _blocks.Add(new Block(220, 0, Color.Blue));
    }

    public void HandleInput()
    {
        if (SplashKit.KeyTyped(_controls.KeyLookup("MoveLeft")))
        {
            foreach (var block in _blocks) block.MoveLeft();
        }

        if (SplashKit.KeyTyped(_controls.KeyLookup("MoveRight")))
        {
            foreach (var block in _blocks) block.MoveRight();
        }

        if (SplashKit.KeyTyped(_controls.KeyLookup("Rotate")))
        {
            foreach (var block in _blocks) block.Rotate();
        }
    }

    private bool HasBlockLanded()
    {
        foreach (var block in _blocks)
        {
            if (block.Y >= SplashKit.ScreenHeight() - block.Height)
            {
                return true;
            }
        }
        return false;
    }

    public void Update()
    {
        HandleInput();

        // Get current time in milliseconds
        double currentTime = _gameTimer.Ticks;
        double dropInterval = SplashKit.KeyDown(_controls.KeyLookup("Drop")) ? FastDropInterval : NormalDropInterval;

        if (currentTime - _lastDropTime >= dropInterval)
        {
            foreach (var block in _blocks) block.Drop();
            _lastDropTime = currentTime;
        }

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
