using UnityEngine;
using UnityEngine.UI;

namespace InfiniteHopper
{
	/// <summary>
	/// IPH text by platform.
	/// </summary>
	public class IPHTextByPlatform : MonoBehaviour
	{

		// The text that will be displayed on PC/Mac/Webplayer
		public string computerText = "CLICK TO START";
	
		// The text that will be displayed on Android/iOS/WinPhone
		public string mobileText = "TAP TO START";
	
		// The text that will be displayed on Playstation, Xbox, Wii
		public string consoleText = "PRESS 'A' TO START";
	
		/// <summary>
		/// Start is only called once in the lifetime of the behaviour.
		/// The difference between Awake and Start is that Start is only called if the script instance is enabled.
		/// This allows you to delay any initialization code, until it is really needed.
		/// Awake is always called before any Start functions.
		/// This allows you to order initialization of scripts
		/// </summary>
		void Start()
		{
			if ( Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.LinuxPlayer )
			{
				GetComponent<Text>().text = computerText;
			}
            #pragma warning disable CS0618 // Type or member is obsolete
            else if ( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WP8Player)
            #pragma warning restore CS0618 // Type or member is obsolete
            {
                GetComponent<Text>().text = mobileText;
			}
            #pragma warning disable CS0618 // Type or member is obsolete
            else if ( Application.platform == RuntimePlatform.PS3 || Application.platform == RuntimePlatform.XBOX360 || Application.platform == RuntimePlatform.PS4 || Application.platform == RuntimePlatform.XboxOne)
            #pragma warning restore CS0618 // Type or member is obsolete
            {
                GetComponent<Text>().text = consoleText;
			}
		}
	}
}