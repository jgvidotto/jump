using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using InfiniteHopper.Types;

namespace InfiniteHopper
{
	/// <summary>
	///This script handles player selection. You can navigate through the available players using buttons. 
	/// </summary>
	public class IPHPlayerSelection: MonoBehaviour
    {
		//A list of players that can be unlocked with tokens
		public PlayerUnlock[] playerList;

        //The currently selected player
        public int currentPlayer = 0;

        //The text to appear if player selected is a bot
        public GameObject textBot;

        //The number of tokens we have
        internal float tokens = 0;
			

		internal int index;
		
		//The icon that displays the number of tokens needed to unlock a character
		public Transform tokenIcon;

        public PlayerStats playerStats;

        private void Awake()
        {
            InstantiatePlayer();
        }

        void Start()
		{
            tokens = playerStats.totalTokens;
            playerStats.OnVariableTokenChange += TokenAmount;
            SetPlayer(currentPlayer);
        }

        public void TokenAmount(int amount)
        {
            tokens = amount;
        }

        //This function Instantiate scriptable players objects
        void InstantiatePlayer()
        {
            for(int i = 0; i < playerList.Length; i++)
            {
                playerList[i].InstPlayer(this.gameObject);
            }
        }

        //This function changes the current player
        public void  ChangePlayer(int changeValue)
		{

			currentPlayer += changeValue;
			
			if ( currentPlayer > playerList.Length - 1 )    currentPlayer = 0;
			if ( currentPlayer < 0 )    currentPlayer = playerList.Length - 1;
			
			SetPlayer(currentPlayer);
		}
		
		//This function activates the selected player, while deactivating all the others
		void  SetPlayer(int playerNumber)
		{
            if(playerList[playerNumber].IsBot) textBot.SetActive(true);
            else textBot.SetActive(false);
            //Go through all the players, and hide each one except the current player
            for ( index = 0; index < playerList.Length; index++ )
			{
                playerList[index].CheckPlayerNumber(index, playerNumber);
            }

            //Get all the sprite renderers in this player icon
            SpriteRenderer[] playerParts = playerList[playerNumber].Instantiated.GetComponentsInChildren<SpriteRenderer>();

            //If the player is unlocked, set this as the current player
            if (tokens >= playerList[playerNumber].tokensToUnlock)
            {
                //Go through all parts of the player and turn them opaque
                foreach (SpriteRenderer part in playerParts) part.color = new Color(part.color.r, part.color.g, part.color.b, 1);

                if (tokenIcon)
                {
                    //Deactivate the token icon
                    tokenIcon.gameObject.SetActive(false);
                }

                playerStats.currentPlayer = currentPlayer;
                playerStats.SavePlayer();

            }
            else //Otherwise, display the number of tokens needed before this character is unlocked
            {
                //Go through all parts of the player and turn them transparent
                foreach (SpriteRenderer part in playerParts) part.color = new Color(part.color.r, part.color.g, part.color.b, 0.3f);

                if (tokenIcon)
                {
                    //Activate the token icon
                    tokenIcon.gameObject.SetActive(true);

                    //Display the number of tokens needed to unlock this player
                    tokenIcon.Find("Text").GetComponent<Text>().text = (playerList[playerNumber].tokensToUnlock - tokens).ToString();
                }
            }


        }

        //This function is called when the stats has been reseted
        //When it happens, if the current player on the screen requires more token than 0, make the tokenIcon appear and change the player's alpha color
        public void SetPlayerTransparent()
        {
            if (tokens < playerList[currentPlayer].tokensToUnlock)
            {
                var parts = playerList[currentPlayer].Instantiated.GetComponentsInChildren<SpriteRenderer>();
                //Go through all parts of the player and turn them opaque
                foreach (SpriteRenderer part in parts) part.color = new Color(part.color.r, part.color.g, part.color.b, 0.3f);

                if (tokenIcon)
                {
                    //Activate the token icon
                    tokenIcon.gameObject.SetActive(true);

                    //Display the number of tokens needed to unlock this player
                    tokenIcon.Find("Text").GetComponent<Text>().text = (playerList[currentPlayer].tokensToUnlock - tokens).ToString();
                }
            }
        }

    }

   
}
