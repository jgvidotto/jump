using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SceneData
{
    public List<SaveSceneData> scenes;

    public SceneData()
    {
        this.scenes = new List<SaveSceneData>();
    }

}
