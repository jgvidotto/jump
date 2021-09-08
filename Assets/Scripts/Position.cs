using System;
using UnityEngine;

[Serializable]
public class Position
{
	public float x;
    public float y;
    public float z;

	public Position(Transform transform)
	{
		x = transform.position.x;
		y = transform.position.y;
		z = transform.position.z;
	}

	public Position(Vector3 vector)
	{
		x = vector.x;
		y = vector.y;
		z = vector.z;
	}

	public Position(float posX, float posY, float posZ)
	{
		x = posX;
		y = posY;
		z = posZ;
	}
}