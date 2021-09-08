using System.Collections;
using InfiniteHopper.Types;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InfiniteHopper
{
	/// <summary>
	/// This script defines the player, its movement, as well as its death
	/// </summary>
	public class IPHPlayer:MonoBehaviour 
	{
		internal Transform thisTransform;
		internal GameObject gameController;
        public PlayerUnlock playerUnlock;
		
		//The current jump power of the player
		internal float jumpPower = 2;

		//Should the player automatically jump when reaching the maximum power?
		internal bool autoJump = true;

		//A power bar that shows how high the player will jump.
		private Transform powerBar;
		
		//The particle effects that will play when the player jumps and lands on a column
		public ParticleSystem jumpEffect;
		public ParticleSystem landEffect;
		public ParticleSystem perfectEffect;
		
		//Various animations for the player
		public AnimationClip animationJumpStart;
		public AnimationClip animationJumpEnd;
		public AnimationClip animationFullPower;
		public AnimationClip animationFalling;
		public AnimationClip animationLanded;

		//Various sounds and their source
		public AudioClip soundStartJump;
		public AudioClip soundEndJump;
		public AudioClip soundLand;
		public AudioClip soundCrash;
		public AudioClip soundPerfect;
		public string soundSourceTag = "GameController";
		internal GameObject soundSource;
		
		//Did the player start the jump process ( powering up and then releasing )
		internal bool  startJump = false;
		
		//Is the player jumping now ( is in mid-air )
		internal bool  isJumping = false;
		
		//Has the player landed on a column?
		internal bool  isLanded = false;

		// Is the player falling now?
		internal bool  isFalling = false;

		// Is the player dead?
		internal bool isDead = false;

        // Is the player dead?
        bool playerBot = false;

        // Column's index
        int n;
       

        void  Start()
		{
            n = 1;
			thisTransform = transform;
            playerBot = playerUnlock.IsBot;
            powerBar = GameObject.FindGameObjectWithTag("PowerBar").GetComponent<Transform>();
            //Assign the game controller for easier access
            gameController = GameObject.FindGameObjectWithTag("GameController");
            playerUnlock.SetTransform(GetComponentsInChildren<SpriteRenderer>());

            if (playerBot)
            {
                gameController.GetComponent<IPHGameController>().SendMessage("Unpause");
                GameObject.FindWithTag("ButtonJump").GetComponent<EventTrigger>().enabled = false;
            }

			//Set the particle effects to the "RenderInFront" sorting layer so that the effects appear in front of the player object
			if ( jumpEffect )    jumpEffect.GetComponent<Renderer>().sortingLayerName = "RenderInFront";
			if ( landEffect )    landEffect.GetComponent<Renderer>().sortingLayerName = "RenderInFront";
			if ( perfectEffect )    perfectEffect.GetComponent<Renderer>().sortingLayerName = "RenderInFront";
			
			//Assign the sound source for easier access
			if ( GameObject.FindGameObjectWithTag(soundSourceTag) )    soundSource = GameObject.FindGameObjectWithTag(soundSourceTag);
		}
		
		void  Update()
		{

            if ( isDead == false )
			{
                //If we are starting to jump, charge up the jump power as long as we are holding the jump button down
                if ( startJump == true )
				{
					//Charge up the jump power
					if ( jumpPower < playerUnlock.jumpChargeMax)
					{
                        if (playerBot)
                        {
                            if (jumpPower < VelocityY(n))
                            {
                                if (GetComponent<Animation>() && animationJumpStart)
                                {
                                    //Play the animation
                                    GetComponent<Animation>().Play(animationJumpStart.name);
                                }
                                //Add to the jump power based on charge speed
                                jumpPower += Time.deltaTime * playerUnlock.jumpChargeSpeed;

                                //Update the power bar fill amount
                                powerBar.Find("Base/FillAmount").GetComponent<Image>().fillAmount = jumpPower / playerUnlock.jumpChargeMax;

                                //Play the charge sound and change the pitch based on the jump power
                                if (soundSource) soundSource.GetComponent<AudioSource>().pitch = 0.3f + jumpPower * 0.1f;
                            }
                            else
                            {
                                EndJump();
                            }
                        }
                        else
                        {
                            //Add to the jump power based on charge speed
                            jumpPower += Time.deltaTime * playerUnlock.jumpChargeSpeed;
                            GetComponent<Animation>().Play(animationJumpStart.name);
                            //Update the power bar fill amount
                            powerBar.Find("Base/FillAmount").GetComponent<Image>().fillAmount = jumpPower / playerUnlock.jumpChargeMax;

                            //Play the charge sound and change the pitch based on the jump power
                            if (soundSource) soundSource.GetComponent<AudioSource>().pitch = 0.3f + jumpPower * 0.1f;
                        }


                    }
					else if ( autoJump == true )
					{
						//If the jump power exceeds the maximum allowed jump power, end charging, and launch the player
						EndJump();
					}
					else
					{
						//Play the full power animation
						if ( GetComponent<Animation>() && animationFullPower )    GetComponent<Animation>().Play(animationFullPower.name);
					}
				}
				
				//If the player is moving down, then he is falling
				if ( isFalling == false && GetComponent<Rigidbody2D>().velocity.y < 0 )
				{
					isFalling = true;
					
					//Play the falling animation
					if ( GetComponent<Animation>() && animationFalling )
					{
						//Play the animation
						GetComponent<Animation>().PlayQueued(animationFalling.name, QueueMode.CompleteOthers);
					}
				}
			}
		}
		
		//This function adds score to the gamecontroller
		void  ChangeScore(Transform landedObject)
		{
			gameController.SendMessage("ChangeScore", landedObject);
		}


        //This function destroys the player, and triggers the game over event
        void  Die()
		{
			if ( isDead == false )
			{
				//Call the game over function from the game controller
				gameController.SendMessage("GameOver", 0.5f);
				
				//Play the death sound
				if ( soundSource )
				{
					soundSource.GetComponent<AudioSource>().pitch = 1;
					
					//If there is a sound source and a sound assigned, play it from the source
					if ( soundCrash )    soundSource.GetComponent<AudioSource>().PlayOneShot(soundCrash);
				}
				
				// The player is dead
				isDead = true;
                n--;
            }
		}

		// This function resets the player's dead status
		public void NotDead()
		{
			isDead = false;
		}
		
		//This function starts the jumping process, allowing the player to charge up the jump power as long as he is holding the jump button down
		void  StartJump( bool playerAutoJump )
		{

            if ( isDead == false )
			{
				//Set the player auto jump state based on the GameController playerAutoJump value
				autoJump = playerAutoJump;

				//You can only jump if you are on land
				if ( isLanded == true )
				{	
					startJump = true;
					
					//Reset the jump power
					jumpPower = 0;


                    if (!playerBot)
                    {
                        //Play the jump start animation ( charging up the jump power )
                        if (GetComponent<Animation>() && animationJumpStart)
                        {
                            //Stop the animation
                            GetComponent<Animation>().Stop();

                            //Play the animation
                            GetComponent<Animation>().Play(animationJumpStart.name);
                        }
                    }

					//Align the power bar to the player and activate it
					if ( powerBar )
					{
						powerBar.position = thisTransform.position;

						powerBar.gameObject.SetActive(true);
					}
					
					if ( soundSource )
					{
						//If there is a sound source and a sound assigned, play it from the source
						if ( soundStartJump )    soundSource.GetComponent<AudioSource>().PlayOneShot(soundStartJump);
					}
				}

            }
		}
		
		//This function ends the jump process, and launches the player with the jump power we charged
		void  EndJump()
		{
			if ( isDead == false )
			{
				//You can only jump if you are on land, and you already charged up the jump power ( jump start )
				if ( isLanded == true && startJump == true )
				{
					thisTransform.parent = null;

					startJump = false;
					isJumping = true;
					isLanded = false;
					isFalling = false;


                    if (playerBot)
                    {
                        //Give the player's velocity.y
                        GetComponent<Rigidbody2D>().velocity = new Vector2(playerUnlock.moveSpeed, VelocityY(n));
                        //Increase the column's index
                        n++;
                    }
                    else
                    {
                        //Give the player velocity based on jump power and move speed
                        GetComponent<Rigidbody2D>().velocity = new Vector2(playerUnlock.moveSpeed, jumpPower);
                       
                    }
                    //Play the jump ( launch ) animation
                    if (GetComponent<Animation>() && animationJumpEnd)
                    {
                        //Stop the animation
                        GetComponent<Animation>().Stop();

                        //Play the animation
                        GetComponent<Animation>().Play(animationJumpEnd.name);
                    }

                    //Deactivate the power bar
                    if ( powerBar )    powerBar.gameObject.SetActive(false);

					//Play the jump particle effect
					if ( jumpEffect )   jumpEffect.Play(); 

					//Play the jump sound ( launch )
					if ( soundSource )
					{
						soundSource.GetComponent<AudioSource>().Stop();
						
						soundSource.GetComponent<AudioSource>().pitch = 0.6f + jumpPower * 0.05f;
						
						//If there is a sound source and a sound assigned, play it from the source
						if ( soundEndJump )    soundSource.GetComponent<AudioSource>().PlayOneShot(soundEndJump);
					}
					
				}
			}
		}

        //This function calculates the velocity needed on y-axis to jump to the next column
        float VelocityY(int index)
        {
            var columnX = gameController.GetComponent<IPHGameController>().vectorsColumn[index].position.x;
            var columnY = gameController.GetComponent<IPHGameController>().vectorsColumn[index].position.y;

            float distanceX = columnX - transform.position.x;
            float distanceYC = columnY - gameController.GetComponent<IPHGameController>().vectorsColumn[index -1].position.y;
            float time = distanceX / playerUnlock.moveSpeed;

            bool moving = gameController.GetComponent<IPHGameController>().vectorsColumn[index].GetComponent<IPHColumn>().movingColumn;
            float predictedLand = gameController.GetComponent<IPHGameController>().vectorsColumn[index].GetComponent<IPHColumn>().Predicted(time) - gameController.GetComponent<IPHGameController>().vectorsColumn[index - 1].position.y;


            var vy = (distanceYC - (0.5f * Physics2D.gravity.y * time * time)) / time;

            if (moving)
            {
                vy = (predictedLand - (0.5f * Physics2D.gravity.y * time * time)) / time;
            }

            return vy;
        }


        //This function runs when the player succesfully lands on a column
        void  PlayerLanded()
		{

            isLanded = true;
            isJumping = false;
            if(playerBot) StartJump(false);

            //Play the landing animation
            if ( GetComponent<Animation>() && animationLanded )
			{
				//Stop the animation
				GetComponent<Animation>().Stop();
				
				//Play the animation
				GetComponent<Animation>().Play(animationLanded.name);
			}
			
			//Play the landing particle effect
			if ( landEffect )    landEffect.Play();
			
			//Play the landing sound
			if ( soundSource )
			{
				soundSource.GetComponent<AudioSource>().pitch = 1;
				
				//If there is a sound source and a sound assigned, play it from the source
				if ( soundLand )    soundSource.GetComponent<AudioSource>().PlayOneShot(soundLand);
			}
		}
		
		//This function runs when the player executes a perfect landing ( closest to the middle )
		void  PerfectLanding(int streak)
		{
			//Play the perfect landing particle effect
			if ( perfectEffect )    perfectEffect.Play();
			
			//If there is a sound source and a sound assigned, play it from the source
			if ( soundSource && soundPerfect )    soundSource.GetComponent<AudioSource>().PlayOneShot(soundPerfect);
		}

		//This function rescales this object over time
		IEnumerator Rescale( float targetScale )
		{
			//Perform the scaling action for 1 second
			float scaleTime = 1;
			
			while ( scaleTime > 0 )
			{
				//Count down the scaling time
				scaleTime -= Time.deltaTime;
				
				//Wait for the fixed update so we can animate the scaling
				yield return new WaitForFixedUpdate();

				float tempScale = thisTransform.localScale.x;

				//Scale the object up or down until we reach the target scale
				tempScale -= ( thisTransform.localScale.x - targetScale ) * 5 * Time.deltaTime;
				
				thisTransform.localScale = Vector3.one * tempScale;
			}
			
			//Rescale the object to the target scale instantly, so we make sure that we got the target
			thisTransform.localScale = Vector3.one * targetScale;
		}
	}
}