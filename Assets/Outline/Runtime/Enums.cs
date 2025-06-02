using System;
using NUnit.Framework;
using UnityEngine;

public enum SignType
{
    East,
    West
}
public enum FloorType
{
    Grass,
    Dirt,
    Rock,
    Wood
}

public enum GameState
{
    MainMenu,
    PauseMenu,
    InGame,
    Inventory,
    Minigame,
    SceneLoading,
    None
}

public enum InventorySize
{
    Small = 5,
    Medium = 10,
    Large = 15,
}

public enum MinesweeperSize
{
    Small,
    Medium,
    Large,
}

public enum DayCycles
{
    Sunrise = 0,
    Day = 1,
    Sunset = 2,
    Midnight = 3,
    Night = 4,
}

public enum MinigameType
{
    FlappyCake,
    Minesweeper,
    Books,
    BedroomCleaning,
    KitchenMinigame
}

public enum VCAType
{
    Master,
    Ambient,
    SFX
}

 
