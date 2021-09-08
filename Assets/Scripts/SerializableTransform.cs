using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableTransform 
{
    public Position column;
    public Position item;
    public bool movingColumn;
    public string id;

    public SerializableTransform(Position column, Position item, bool movingColumn, string id)
    {
        this.column = column;
        if (item != null) this.item = item;
        this.movingColumn = movingColumn;
        this.id = id;
    }
}
