using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehaviour : MonoBehaviour {
	public int eatenVal = 1;
	float lWallPos, rWallPos, tWallPos, bWallPos;
	GameObject foodNew;

	void Start () {
		rWallPos = GameController.GamesController.rWall.position.x;
		lWallPos = GameController.GamesController.lWall.position.x;
		tWallPos = GameController.GamesController.tWall.position.y;
		bWallPos = GameController.GamesController.bWall.position.y;
		foodNew = GameController.GamesController.foodStandard;

		int x = (int)Random.Range (lWallPos, rWallPos);
		int y = (int)Random.Range (bWallPos, tWallPos);

		/*Vector2 spawnPos = new Vector2 (x, y);
		Collider2D coll = Physics2D.OverlapCircle(spawnPos,2f,0,0);

		if(coll != null){
			Debug.Log("Overlap detected...?");

			if(coll.tag != "Food"){
				Debug.Log("Reset food position");
			
				Instantiate (foodNew, new Vector2 (x, y), Quaternion.identity);

				Destroy(this.gameObject);
			}
		}*/
	}

	/*void OnTriggerEnter2D(Collider2D coll){
		Debug.Log("Trigger food!");

		//if(coll.transform.tag == "Wall" || coll.transform.tag == "Food"){
		if(coll.gameObject.tag == "Wall" || coll.gameObject.tag == "Food"){
			Debug.Log("Reset food position");

			int x = (int)Random.Range (lWallPos, rWallPos);
			int y = (int)Random.Range (bWallPos, tWallPos);
		
			Instantiate (foodNew, new Vector2 (x, y), Quaternion.identity);

			Destroy(this.gameObject);
		}
	}*/
}
