using UnityEngine;
using UnityEditor;

public class MovingObjectCreator {

	[MenuItem ("Assets/Create/Mountsix/Runner/Movement Creator")]
	public static void createConfig () {
		MovingObjectConfig config = ScriptableObject.CreateInstance<MovingObjectConfig>();
		ProjectWindowUtil.CreateAsset(config, "MovementConfig.asset");
	}
}
