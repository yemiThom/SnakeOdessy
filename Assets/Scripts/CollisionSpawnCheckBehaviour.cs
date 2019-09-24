using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSpawnCheckBehaviour : MonoBehaviour {
	float lWallPos, rWallPos, tWallPos, bWallPos;
	public float padding;
	public bool justSpawnedIn, destroyingSelf, shouldRespawn, shouldSpawnObj;
	public GameObject thisObject, collisionSpawnCheckerAgain;

	void Start () {
		rWallPos = GameController.GamesController.rWall.position.x;
		lWallPos = GameController.GamesController.lWall.position.x;
		tWallPos = GameController.GamesController.tWall.position.y;
		bWallPos = GameController.GamesController.bWall.position.y;

		shouldSpawnObj = true;
	}

	void Update(){
		if(shouldSpawnObj){
			StartCoroutine(ShouldSpawnObj());
		}

		if(shouldRespawn){
			ShouldRespawn();
		}
	}

	public void ObjectToSpawn(GameObject objectToSpawn){
		thisObject = objectToSpawn;
	}

	IEnumerator ShouldSpawnObj(){
		yield return new WaitForSeconds(0.550f);

		Debug.Log("Should spawn something now");
		destroyingSelf = true;

		Instantiate (thisObject, transform.position, Quaternion.identity);

		Destroy(this.gameObject);
	}

	void ShouldRespawn(){
		Debug.Log("Stop The Presses!");
		StopAllCoroutines();
		Debug.Log("Reset object's position");
		
		int x = (int)Random.Range (lWallPos + padding, rWallPos - padding);
		int y = (int)Random.Range (bWallPos + padding, tWallPos - padding);

		Vector2 spawnPos = new Vector2 (x, y);

		collisionSpawnCheckerAgain.GetComponent<CollisionSpawnCheckBehaviour>().ObjectToSpawn(thisObject);
		collisionSpawnCheckerAgain.GetComponent<CollisionSpawnCheckBehaviour>().shouldRespawn = false;
		collisionSpawnCheckerAgain.GetComponent<CollisionSpawnCheckBehaviour>().shouldSpawnObj = true;
		Instantiate (collisionSpawnCheckerAgain, spawnPos, Quaternion.identity);

		Destroy(this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.gameObject.tag == "Wall" || coll.gameObject.tag == "Food" || coll.gameObject.tag == "Edible"){
			shouldRespawn = true;
			shouldSpawnObj = false;
		}
	}
}