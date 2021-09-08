using InfiniteHopper.Types;
using UnityEngine;

/// <summary>
///This script handles the creation of a new player
/// </summary>
[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class Character : PlayerUnlock
{
    //Instantiate the player
    public override void InstPlayer(GameObject parent)
    {
        Instantiated = Instantiate(playerIcon) as GameObject;
        Instantiated.transform.SetParent(parent.transform);
        Instantiated.transform.localPosition = new Vector3(0, -0.5f, 0);
        Instantiated.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        SetTransform(Instantiated.GetComponentsInChildren<SpriteRenderer>());
    }

    //Set the correct position and rotation of the player, also apply the tail color
    public override void SetTransform(SpriteRenderer[] sprites)
    {

        sprites[0].sprite = head;
        sprites[1].sprite = tail;

        sprites[1].color = colorOfTheTail;

        if (tailPosition == TailPosition.Top)
        {
            sprites[1].transform.localPosition = new Vector3(0, 0.5f, 0);
            sprites[1].transform.localRotation = Quaternion.Euler(0, 0, 75);
        }
        else if (tailPosition == TailPosition.Bottom)
        {
            sprites[1].transform.localPosition = new Vector3(-0.4f, -0.4f, 0);
            sprites[1].transform.localRotation = Quaternion.Euler(0, 0, 75);
        } else
        {
            sprites[1].enabled = false;
        }
        
    }

    //Check the player's choice
    public override void CheckPlayerNumber(int index, int playerNumber)
    {
        if (index != playerNumber)
        {
            Instantiated.SetActive(false);
        }
        else Instantiated.SetActive(true);
    }
}
