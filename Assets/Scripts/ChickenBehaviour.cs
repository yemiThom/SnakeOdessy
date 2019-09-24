using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBehaviour : MonoBehaviour {
	/*public Transform[] pwalkPoints;
	float currentPos;
    public float speed,ToMove;
    private float direction = 1.0f;*/
	public float pwalkSpeed = 1.0f;
    public float pwalkLeft;
    public float pwalkRight;
	public float pwalkPadding;
    float pwalkingDirection = 1.0f;
    Vector2 pwalkAmount;
    float originalX; // Original float value
	public GameObject eggObject;
	public int eatenVal = 20;

	// Use this for initialization
	void Start () {
        originalX = this.transform.position.x;
		pwalkLeft = transform.position.x - 2.5f;
		pwalkRight = transform.position.x + 2.5f;

		StartCoroutine(DoYourThing());
		StartCoroutine(LayYourThing());
	}
	
	// Update is called once per frame
	void Update () {
		Walk();
	}

	void Walk(){
		/*currentPos = Mathf.Clamp01(currentPos + speed * Time.deltaTime * direction);
        if(direction == 1.0 && currentPos > 0.99) direction = -1.0f;
        if(direction == -1.0 && currentPos < 0.01) direction = 1.0f;
       
        transform.position = Vector3.Lerp(pwalkPoints[0].position, pwalkPoints[1].position, currentPos);*/

		pwalkAmount.x = pwalkingDirection * pwalkSpeed * Time.deltaTime;
        if (pwalkingDirection > 0.0f && transform.position.x >= pwalkRight) {
            pwalkingDirection = -1.0f;
        } else if (pwalkingDirection < 0.0f && transform.position.x <= pwalkLeft) {
            pwalkingDirection = 1.0f;
        }
        transform.Translate(pwalkAmount);
	}

	IEnumerator DoYourThing(){
		yield return new WaitForSeconds(Random.Range(2f, 10f));

		GameController.GamesController.chickenExists = false;
		Destroy(gameObject);
	}

	IEnumerator LayYourThing(){
		yield return new WaitForSeconds(Random.Range(2f, 10f));

		Instantiate(eggObject, transform.position, Quaternion.identity);
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		//Debug.Log("Trigger chicken!");
		if (coll.gameObject.tag == "Wall") {
            if ((this.transform.position.x - coll.transform.position.x) > 0) {
                Debug.Log("hit left");
				pwalkLeft = this.transform.position.x;
            } else if ((this.transform.position.x - coll.transform.position.x) < 0) {
                Debug.Log("hit right");
				pwalkRight = this.transform.position.x;
            }
        }

		if(coll.gameObject.tag == "Player"){
			//Debug.Log("Initiate chichen don't exist");

			GameController.GamesController.chickenExists = false;
		}
	}
}
