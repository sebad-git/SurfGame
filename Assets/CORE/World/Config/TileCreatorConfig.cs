using UnityEngine;

public class TileCreatorConfig : ScriptableObject {
	[System.Serializable]
	public class TilesSpawn{
		public float tileLenght=30f;
		[Range(1,10)]public int tilesPerScreen=5;
		public float destroyTime=15f;
		public GameObject firstTile;
		public GameObject[] tiles;
	}
	public TilesSpawn tilesSpawn;

	[System.Serializable]
	public class ItemSpawn{
		public Vector2 spawnTimeRange = new Vector2(10f,30f);
		public Vector2 spawnDistanceRange = new Vector2(10f,40f);
		public GameObject[] powerUps;	
	}
	public ItemSpawn itemSpawn;
}
	

