using UnityEngine;
using InfiniteHopper;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private IPHGameController gameController;
    PlayerStats playerStats;
    [SerializeField]
    private AudioClip soundAchievement;
    

    void Awake()
    {
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

