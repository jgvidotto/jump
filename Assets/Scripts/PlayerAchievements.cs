using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAchievements : MonoBehaviour
{
	public Achievements[] achievements;
	public GameObject achievementsPrefab;
    public GameObject parentPrefab;
    private PlayerStats playerStats;
    private int n;
    private float sliderValue;
    private List<GameObject> instantiatedGameObjects = new List<GameObject>();

    void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Stats").GetComponent<PlayerStats>();
        CheckDeletedAchievement();
        CheckUpdatedAchievement();
        InstantiateAchievement();
        playerStats.SavePlayer();
    }

    //Instantiate achievements according to the player's dictonary values
    void InstantiateAchievement()
	{
		for (int i = 0; i < achievements.Length; i++)
		{
            SaveAchievement(achievements[i]);

            GameObject go = Instantiate(achievementsPrefab) as GameObject;
            go.transform.SetParent(parentPrefab.transform);
            go.transform.localScale = new Vector3(1, 1, 1);

            var texts = go.GetComponentsInChildren<Text>();
            texts[0].text = achievements[i].title.ToUpper();
            texts[1].text = $"{playerStats.achievement[achievements[i].id].progression} / {achievements[i].goal.ToString()}";
            sliderValue = (float)playerStats.achievement[achievements[i].id].progression / achievements[i].goal;
            go.GetComponentInChildren<Slider>().value = sliderValue;
            instantiatedGameObjects.Add(go);
        }
        
    }

    //Add achievements to player's dictionary
    public void SaveAchievement(Achievements achievement)
    {
        if (!playerStats.achievement.ContainsKey(achievement.id))
        {
            playerStats.achievement.Add(achievement.id, achievement);
            Debug.Log("The achievement " + achievement.id + " has been added");
        }
    }

    //Clear if any achievement has been updated, if so, update its values fot the player dictionary
    public void CheckUpdatedAchievement()
    {
        foreach (var key in playerStats.achievement.ToArray())
        {
            if (!key.Value.title.Equals(achievements[n].title) || key.Value.goal != achievements[n].goal)
            {
                playerStats.achievement[key.Key].title = achievements[n].title;
                playerStats.achievement[key.Key].goal = achievements[n].goal;
                Debug.Log("The achievement " + key.Key + " has been Updated");
                n++;
            }
            else
            {
                n++;
            }
        }
       
    }

    //Clear the values of instantiated achievements 
    public void ResetProgress()
    {
        if(instantiatedGameObjects.Count == playerStats.achievement.Count)
        {
            for (int i = 0; i < instantiatedGameObjects.Count; i++)
            {
                instantiatedGameObjects[i].GetComponentInChildren<Slider>().value = playerStats.achievement[achievements[i].id].progression;
                instantiatedGameObjects[i].GetComponentsInChildren<Text>()[1].text = $"{playerStats.achievement[achievements[i].id].progression} / {achievements[i].goal.ToString()}";
            }
        }

    }

    //Clear if any achievement has been deleted, if so, delete it from the dictionary
    public void CheckDeletedAchievement()
    {
        if(playerStats.achievement.Count > achievements.Length)
        {
            foreach (var key in playerStats.achievement.ToArray())
            {
                if(key.Key != achievements[n].id)
                {
                    playerStats.achievement.Remove(key.Key);
                    Debug.Log("The achievement " + key.Key + " has been removed");
                }
                else
                {
                    n++;
                }
                
            }
        }
    }
}
