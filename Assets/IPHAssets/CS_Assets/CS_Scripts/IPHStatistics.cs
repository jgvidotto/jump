using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IPHStatistics : MonoBehaviour
{
    public GameObject controller;
    private PlayerStats player;
    // Use this for initialization
    void Start() 
	{
        player = controller.GetComponent<PlayerStats>();
        // Set the statistics values in the statistics canvas
        GameObject.Find("TextDistance").GetComponent<Text>().text = $"LONGEST DISTANCE: {player.longestDistance}";
        GameObject.Find("TextStreak").GetComponent<Text>().text = $"LONGEST STREAK: {player.longestStreak}";
        GameObject.Find("TextTokens").GetComponent<Text>().text = $"TOTAL FEATHERS: {player.totalTokens}";
        GameObject.Find("TextPowerups").GetComponent<Text>().text = $"TOTAL POWERUPS: {player.totalPowerUps}";
        GameObject.Find("TextPowerupStreak").GetComponent<Text>().text = $"LONGEST POWERUP: {player.longestPowerUp}";
        GameObject.Find("TextCharacters").GetComponent<Text>().text = $"CHARACTERS UNLOCKED: {player.charactersUnlocked}";

    }

    // Reset statistics values and Player data
    public void Reset()
    {
        SavingAndLoading.Reset();
        SceneSaveLoad.Reset();
        player.Clear();
        GameObject.Find("TextDistance").GetComponent<Text>().text = $"LONGEST DISTANCE: {player.longestDistance}";
        GameObject.Find("TextStreak").GetComponent<Text>().text = $"LONGEST STREAK: {player.longestStreak}";
        GameObject.Find("TextTokens").GetComponent<Text>().text = $"TOTAL FEATHERS: {player.totalTokens}";
        GameObject.Find("TextPowerups").GetComponent<Text>().text = $"TOTAL POWERUPS: {player.totalPowerUps}";
        GameObject.Find("TextPowerupStreak").GetComponent<Text>().text = $"LONGEST POWERUP: {player.longestPowerUp}";
        GameObject.Find("TextCharacters").GetComponent<Text>().text = $"CHARACTERS UNLOCKED: {player.charactersUnlocked}";
    }

}
