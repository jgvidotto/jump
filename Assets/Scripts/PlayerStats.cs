using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats: MonoBehaviour
{
    public int totalTokens;
    public int numberOfMatches;
    public int longestDistance;
    public int longestStreak;
    public int totalPowerUps;
    public int longestPowerUp;
    public int currentPlayer;
    public int charactersUnlocked;
    public Dictionary<int, Achievements> achievement;
    public GameObject achievementEarned;
    public Dictionary<string, int> highscoreScene;

    public delegate void OnTokenChangeDelegate(int newValue);
    public event OnTokenChangeDelegate OnVariableTokenChange;

    private void Awake()
    {
        achievement = new Dictionary<int, Achievements>();
        highscoreScene = new Dictionary<string, int>();
        LoadPlayer();
    }

    public void SavePlayer()
    {
        SavingAndLoading.SaveData(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SavingAndLoading.LoadData();
        if(data != null)
        {
            this.highscoreScene = data.highscoreScene;
            this.numberOfMatches = data.numberOfMatches;
            this.totalTokens = data.totalTokens;
            this.longestDistance = data.longestDistance;
            this.longestStreak = data.longestStreak;
            this.totalPowerUps = data.totalPowerUps;
            this.longestPowerUp = data.longestPowerUp;
            this.currentPlayer = data.currentPlayer;
            this.charactersUnlocked = data.charactersUnlocked;
            this.achievement = data.achievement;

        }

    }

    public void AchievementProgress(int key, int progress, AudioClip audio)
    {
        if(!achievement[key].unlocked)
        {
            if (achievement[key].progression < progress)
            {
                achievement[key].progression = progress;
                if (achievement[key].progression == achievement[key].goal || progress > achievement[key].goal)
                {
                    achievement[key].progression = achievement[key].goal;
                    achievement[key].unlocked = true;
                    achievementEarned.SetActive(true);
                    achievementEarned.GetComponent<Animator>().Play("AchievementEarned", -1, 0);
                    achievementEarned.GetComponent<AudioSource>().PlayOneShot(audio);
                    var achievementEarnedText = achievementEarned.GetComponentsInChildren<Text>();
                    achievementEarnedText[1].text = achievement[key].title.ToUpper();

                    if (achievement[key].category == Achievements.Category.UnlockCharacter) charactersUnlocked++;

                }
            }
        }

    }

    public void EarnAchievement(int key, int progress, AudioClip audio)
    {
        if (achievement.ContainsKey(key))
        {
            AchievementProgress(key, progress, audio);
            SavePlayer();
        }
        else
        {
            return;
        }
        
    }

    public void Clear()
    {
        if(highscoreScene.Count > 0) highscoreScene.Clear();
        numberOfMatches = 0;
        totalTokens = 0;
        longestDistance = 0;
        longestStreak = 0;
        totalPowerUps = 0;
        longestPowerUp = 0;
        currentPlayer = 0;
        charactersUnlocked = 0;
        OnVariableTokenChange(totalTokens);
        ResetAchievement();
    }

    public void ResetAchievement()
    {
        foreach (var key in achievement.ToArray())
        {
            key.Value.progression = 0;
            key.Value.unlocked = false;
        }
        SavePlayer();
    }


}
