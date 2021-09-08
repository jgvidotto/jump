using UnityEngine;
using InfiniteHopper;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    IPHGameController gameController;
    PlayerStats playerStats;
    public AudioClip soundAchievement;
    

    void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<IPHGameController>();
        playerStats = gameController.playerStats;
    }

    private void Start()
    {
        gameController.OnVariableChange += TrackProgression;
        
    }

    private void TrackProgression(int key, int newVal)
    {
        playerStats.EarnAchievement(key, newVal, soundAchievement);
    }

    void OnDestroy()
    {
        gameController.OnVariableChange -= TrackProgression;
    }

}

