using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehaviour : MonoBehaviour {

	public GameObject groundBIGObstacle;
	public int eatenVal = 10;
	
	void Start () {
		StartCoroutine(DoYourThing());
	}

	IEnumerator DoYourThing(){
		yield return new WaitForSeconds(10f);

		Instantiate(groundBIGObstacle, transform.position, Quaternion.identity);

		Destroy(gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		//Debug.Log("Trigger egg!");
	}
}
