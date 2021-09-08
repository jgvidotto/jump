using UnityEngine;
using System;

namespace InfiniteHopper.Types
{
	[Serializable]
    public abstract class PlayerUnlock: ScriptableObject 
	{
        private GameObject instantiated;
        //Get and Set the instantiated object
        public GameObject Instantiated
        {
            get
            {
                return instantiated;
            }
            set
            {
                instantiated = value;
            }
        }
        //The icon/avatar of the player
        public GameObject playerIcon;

        //How fast the player's jump power increases when we are holding the jump button
        public float jumpChargeSpeed;

        //The maximum jump power of the player
        public float jumpChargeMax;

        //The *horizontal* movement speed of the player when it is jumping
        public float moveSpeed;

        //How many tokens are needed to unlock this player
        public int tokensToUnlock;

        //Set the head Sprite
        public Sprite head;

        //Set the tail Sprite
        public Sprite tail;

        //Choose if the Sprite has a second element and where it should be
        public enum TailPosition
        {
            Top,
            Bottom,
            NoTail
        }

        //Set the color of the tail
        public Color colorOfTheTail;

        //Choose this player as a Bot
        public bool IsBot;

        public TailPosition tailPosition;

        //Instantiate Player
        public abstract void InstPlayer(GameObject parent);
        //Set the correct position and rotation of the player, also apply the tail color
        public abstract void SetTransform(SpriteRenderer[] sprites);
        //Check the player's choice
        public abstract void CheckPlayerNumber(int index, int playerNumber);
    }
    
}


