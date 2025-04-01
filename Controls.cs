using System;
using System.IO;
using System.Collections.Generic;
using SplashKitSDK;

public class Controls
{
    private Json? _controlsJson;
    private Dictionary<string, KeyCode> _keyMappings;

    public Controls()
    {
        string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Controls.json");
    Console.WriteLine($"Looking for file at: {jsonPath}");

    if (File.Exists(jsonPath))
    {
        Console.WriteLine("Controls.json FOUND ✅");

        try
        {
            _controlsJson = SplashKit.JsonFromFile(jsonPath);
            Console.WriteLine("Controls.json loaded successfully. ✅");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading JSON: {ex.Message}");
        }
    }
    else
    {
        Console.WriteLine("Error: Controls.json file is missing!");
        _controlsJson = new Json(); // Prevents null reference errors
    }

        LoadKeyMappings();
    }

    private void LoadKeyMappings()
    {
        _keyMappings = new Dictionary<string, KeyCode>
        {
            { "MoveLeft", KeyCode.LeftKey },
            { "MoveRight", KeyCode.RightKey },
            { "Rotate", KeyCode.UpKey },
            { "Drop", KeyCode.DownKey }
        };

        foreach (var action in _keyMappings.Keys)
        {
            if (_controlsJson.HasKey(action))
            {
                _keyMappings[action] = (KeyCode)Enum.Parse(typeof(KeyCode), _controlsJson.ReadString(action));
            }
        }
    }

    public KeyCode KeyLookup(string action)
    {
        if (_keyMappings.ContainsKey(action))
        {
            return _keyMappings[action];
        }
        return KeyCode.UnknownKey; // Return default unknown key if not found
    }

    private void SetDefaultControls()
    {
        _controlsJson = new Json();
        _controlsJson.AddString("MoveLeft", "LeftKey");
        _controlsJson.AddString("MoveRight", "RightKey");
        _controlsJson.AddString("Rotate", "UpKey");
        _controlsJson.AddString("Drop", "DownKey");

        SplashKit.JsonToFile(_controlsJson, "Controls.json");
        Console.WriteLine("Default Controls.json file created.");
    }

    public Json GetControlsJson()
    {
        return _controlsJson ?? new Json();
    }
}
