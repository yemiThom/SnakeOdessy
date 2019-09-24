using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedFood : FoodBehaviour {

	void OnTriggerEnter2D(Collider2D coll){
		Debug.Log("Trigger speed food!");

		//if(coll.transform.tag == "Wall" || coll.transform.tag == "Food"){
		if(coll.gameObject.tag == "Player"){
			Debug.Log("Initiate WarpDrive");

			//coll.GetComponent<SnakeBehaviour>().speed = coll.GetComponent<SnakeBehaviour>().speed + 23;
			//coll.GetComponent<SnakeBehaviour>().ChangeSpeed();
		}
	}
}
