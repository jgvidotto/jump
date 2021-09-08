using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveSceneData
{
    public string id;
    public List<SerializableTransform> transforms;
    public int score;

    public SaveSceneData()
    {
    }

    public SaveSceneData(string id, List<SerializableTransform> transforms, int score)
    {
        this.id = id;
        this.transforms = transforms;
        this.score = score;
    }

}
