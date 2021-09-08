using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InfiniteHopper;
using UnityEngine;

public class LoadSceneData : MonoBehaviour
{
	private List<SerializableTransform> serializableTransforms;
    private SceneData data;
    public SceneData Data
    {
        get
        {
            return data;
        }
    }
    bool addScene;
    // Start is called before the first frame update
    void Start()
    {
        serializableTransforms = new List<SerializableTransform>();
        LoadScene();

        GetComponent<IPHGameController>().OnSaveTransform += OnSaveSceneData;
        GetComponent<IPHGameController>().OnSceneEnter += ShouldAddScene;
        GetComponent<IPHGameController>().OnSceneSave += SceneDataObj;


    }

	public void LoadScene()
	{
		data = SceneSaveLoad.LoadData();
        if (data == null)
            data = new SceneData();
	}

	void OnSaveSceneData(SerializableTransform sData, bool sameScene)
	{
        bool isSameScene = sameScene;

        if(!isSameScene) serializableTransforms.Add(sData);
        else
        {
            var addScenedata = Data.scenes.Single(id => id.id == PlayerPrefs.GetString("id"));
            addScenedata.transforms.Add(sData);
            if (addScenedata.score < GetComponent<IPHGameController>().score)
            {
                addScenedata.score = GetComponent<IPHGameController>().score;
                
            }
                
        }

    }

    void ShouldAddScene(bool shouldAdd)
    {
        addScene = shouldAdd;
    }

    private void OnDisable()
    {
        
        SceneSaveLoad.Save(data);
    }

    public void SceneDataObj(SceneData scene)
	{
        if (addScene)
        {
            PlayerPrefs.SetString("id", System.Guid.NewGuid().ToString());
            scene.scenes.Add(new SaveSceneData(PlayerPrefs.GetString("id"), serializableTransforms, GetComponent<IPHGameController>().score));
        }

	}
}
