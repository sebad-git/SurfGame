using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public string[] channels;

	public void openChannel(int channelId){ Application.OpenURL(this.channels[channelId]); }

}
