using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAchievementAnimation : MonoBehaviour
{
    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public IEnumerator Hold()
	{
        GetComponent<Animator>().speed = 0;
		yield return new WaitForSeconds(3);
        GetComponent<Animator>().speed = 1;
    }
}
