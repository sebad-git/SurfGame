using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace mountsix.runner{

	public class TileSpawner : MonoBehaviour {

		public TileCreatorConfig config;
		//PRIVATE
		private GameObject player;
		private float spawnZ;
		private List<GameObject> activeTiles;
		private List<GameObject> activePowerUps;
		private float powerInterval;
		private float timer;
		private int currentTile;

		void Awake() {
			this.player=(GameObject)GameObject.FindGameObjectWithTag("Player");
			this.activeTiles = new List<GameObject>(); this.activePowerUps = new List<GameObject>();
			for (int i=0; i<this.config.tilesSpawn.tilesPerScreen; i++) { this.spawnTile(); }
			this.powerInterval = Random.Range(this.config.itemSpawn.spawnTimeRange.x,this.config.itemSpawn.spawnTimeRange.y);
		}
		//TODO UPDATE???
		void FixedUpdate () {
			this.timer += Time.deltaTime;
			float spawnPos = (this.spawnZ - this.config.tilesSpawn.tilesPerScreen * this.config.tilesSpawn.tileLenght);
			if (player.transform.position.z > spawnPos ) { this.spawnTile(); }
			if (this.player.transform.position.z > spawnPos) { 
				this.StartCoroutine(this.deleteTile()); this.deletePWP();
			}
			if(this.timer>=this.powerInterval){ this.spawnPowerUp(); }
		}

		private void spawnTile(){
			GameObject go = null;
			if (this.activeTiles.Count < 2) { go = (GameObject)Instantiate(config.tilesSpawn.firstTile); this.currentTile=0;} 
			else {
				int tileNumber = this.currentTile;
				while(tileNumber == this.currentTile){
					tileNumber = Random.Range(0,this.config.tilesSpawn.tiles.Length);
				}
				go = (GameObject)Instantiate(config.tilesSpawn.tiles[tileNumber]);
				this.currentTile=tileNumber;
			}
			go.transform.parent = transform;
			go.transform.localPosition = Vector3.zero;
			go.transform.localRotation = Quaternion.Euler(Vector3.zero);
			go.transform.position = (Vector3.forward * this.spawnZ);
			this.spawnZ += this.config.tilesSpawn.tileLenght;
			this.activeTiles.Add(go);
		}

		private IEnumerator deleteTile(){
			yield return new WaitForSeconds(this.config.tilesSpawn.destroyTime);
			if (this.activeTiles.Count > 0) {
				Destroy (this.activeTiles[0]); this.activeTiles.RemoveAt(0);
			}
		}

		private void spawnPowerUp(){
			if(this.config.itemSpawn.powerUps!=null && this.config.itemSpawn.powerUps.Length>0){
				GameObject pwp = (GameObject)Instantiate(config.itemSpawn.powerUps[Random.Range(0,this.config.itemSpawn.powerUps.Length)]);
				pwp.transform.rotation = Quaternion.identity;
				float distance = Random.Range(this.config.itemSpawn.spawnDistanceRange.x,this.config.itemSpawn.spawnDistanceRange.y);
				Vector3 pos = Vector3.zero; pos.z = (this.player.transform.position.z+distance);
				pwp.transform.position = pos; this.activePowerUps.Add(pwp);
				this.powerInterval = Random.Range(this.config.itemSpawn.spawnTimeRange.x,this.config.itemSpawn.spawnTimeRange.y);
				this.timer = 0;	
			}
		}

		private void deletePWP(){
			if (this.activePowerUps.Count>0) {
				Destroy (this.activePowerUps[0]); this.activePowerUps.RemoveAt(0);
			}
		}

	}
}
