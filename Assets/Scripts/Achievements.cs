using System;
using UnityEngine;

[Serializable]
public class Achievements
{
	public int id;
    public string title;
    public int progression;
    public int goal;
    public bool unlocked;
    public enum Category
    {
        OneShot,
        Jump,
        PowerUps,
        Points,
        Matches,
        Tokens,
        UnlockCharacter
    }
    public Category category;
}
