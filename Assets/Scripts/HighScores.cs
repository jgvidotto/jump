using System.Collections.Generic;
using System.Linq;
using InfiniteHopper;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script handles the instantiate function to show the player's top 10 scores.
/// </summary>
public class HighScores : MonoBehaviour
{
    public GameObject controller;
    public GameObject letsplay;
    public GameObject highScoreText;
    private PlayerStats player;
    private int i = 1;
    private void Awake()
    {
        player = controller.GetComponent<PlayerStats>();
    }

    private void Start()
    {
        //if there is data to show, instantiate it
        if(player.highscoreScene.Count > 0)
        {
            var sortedDict = (from entry in player.highscoreScene orderby entry.Value descending select entry)
                     .Take(10)
                     .ToDictionary(pair => pair.Key, pair => pair.Value);

            letsplay.SetActive(false);

            foreach(KeyValuePair<string, int> pair in sortedDict)
            {
                GameObject childText = Instantiate(highScoreText) as GameObject;
                childText.transform.SetParent(gameObject.transform, false);

                var newText = childText.GetComponentsInChildren<Text>();
                newText[0].text = (i ++).ToString();
                newText[2].text = player.highscoreScene[pair.Key].ToString();
                AddListener(childText.GetComponentInChildren<Button>(), pair.Key);
            }
           
        }
        //else, show the player that he has to play the game first.
        else
        {
            letsplay.SetActive(true);
        }
    }

    void AddListener(Button b, string value)
    {
        b.onClick.AddListener(() => PlaySceneScore(value));
    }

    public void PlaySceneScore(string idScene)
    {
        SceneData sceneData = SceneSaveLoad.LoadData();
        var sceneScore = sceneData.scenes.Single(id => id.id == idScene);
        PlayerPrefs.SetString("id", sceneScore.id);
        PlayerPrefs.SetInt("playagain", 1);
        GetComponent<IPHLoadLevel>().LoadLevel("CS_Game");
    }

}
