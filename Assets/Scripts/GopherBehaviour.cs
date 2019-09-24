using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GopherBehaviour : MonoBehaviour {

	public int eatenVal = 15;
	public GameObject groundObstacle;
	
	void Start () {
		StartCoroutine(DoYourThing());
	}

	IEnumerator DoYourThing(){
		yield return new WaitForSeconds(10f);

		Instantiate(groundObstacle, transform.position, Quaternion.identity);

		GameController.GamesController.gopherExists = false;
		Destroy(gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		//Debug.Log("Trigger gopher!");

		if(coll.gameObject.tag == "Player"){
			//Debug.Log("Initiate gopher don't exist");

			GameController.GamesController.gopherExists = false;
		}
	}
}
