using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public Dictionary<string, int> highscoreScene;
    public int numberOfMatches;
    public int totalTokens;
    public int longestDistance;
    public int longestStreak;
    public int totalPowerUps;
    public int longestPowerUp;
    public int currentPlayer;
    public int charactersUnlocked;
    public Dictionary<int, Achievements> achievement;

    public PlayerData (PlayerStats player)
    {
        this.highscoreScene = player.highscoreScene;
        this.numberOfMatches = player.numberOfMatches;
        this.totalTokens = player.totalTokens;
        this.longestDistance = player.longestDistance;
        this.longestStreak = player.longestStreak;
        this.totalPowerUps = player.totalPowerUps;
        this.longestPowerUp = player.longestPowerUp;
        this.currentPlayer = player.currentPlayer;
        this.charactersUnlocked = player.charactersUnlocked;
        this.achievement = player.achievement;
    }
}
