using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeBehaviour : MonoBehaviour {
	GameObject food;
	public GameObject tailPrefab;

	Transform rWall, lWall, tWall, bWall;
	
	bool eat = false;
	bool canMakeMove = true;
	public bool canGoVert = false;
	public bool canGoHor = true;

	private float speed = 0.1f;
	public float altSpeed;
	float moveX, moveY;

	Vector2 vector = Vector2.up;
	Vector2 moveVector;

	List<GameObject> tail = new List<GameObject>();

	// Use this for initialization
	void Start () {
		rWall = GameController.GamesController.rWall;
		lWall = GameController.GamesController.lWall;
		tWall = GameController.GamesController.tWall;
		bWall = GameController.GamesController.bWall;
		food = GameController.GamesController.foodStandard;

		InvokeRepeating("MoveSnake", 0.3f, speed);
		SpawnFood();
	}
	
	// Update is called once per frame
	void Update () {
		moveX = Input.GetAxis("Horizontal");
		moveY = Input.GetAxis("Vertical");

		CheckPlayerInput();
	}

	void CheckPlayerInput(){
		if(moveX < -0.1 && canGoHor && canMakeMove){
			//Debug.Log("Moving Left");
			canGoHor = canMakeMove = false;
			StartCoroutine(MakeMoveSwitch());
			canGoVert = true;
			vector = -Vector2.right;
		}else if(moveX > 0.1 && canGoHor && canMakeMove){
			//Debug.Log("Moving Right");
			canGoHor = canMakeMove = false;
			StartCoroutine(MakeMoveSwitch());
			canGoVert = true;
			vector = Vector2.right;
		}else if(moveY < -0.1 && canGoVert && canMakeMove){
			//Debug.Log("Moving Down");
			canGoVert = canMakeMove = false;
			StartCoroutine(MakeMoveSwitch());
			canGoHor = true;
			vector = -Vector2.up;
		}else if(moveY > 0.1 && canGoVert && canMakeMove){
			//Debug.Log("Moving Up");
			canGoVert = canMakeMove = false;
			StartCoroutine(MakeMoveSwitch());
			canGoHor = true;
			vector = Vector2.up;
		}

		moveVector = vector / 3f;
		//Debug.Log("Move Vector: " + moveVector);
	}

	public void ChangeSpeed(){
		CancelInvoke();
		InvokeRepeating("MoveSnake", 0.3f, altSpeed);
	}

	void MoveSnake(){
		Vector2 ta = transform.position;
		if (eat) {
			if (speed > 0.002){
				speed = speed - 0.002f;
			}
			GameObject g = (GameObject)Instantiate(tailPrefab, ta, Quaternion.identity);
			tail.Insert(0, g.gameObject);
			//Debug.Log(speed);
			eat = false;
		}
		else if (tail.Count > 0) {
			tail.Last().transform.position = ta;
			tail.Insert(0, tail.Last());
			tail.RemoveAt(tail.Count-1);
		}

		transform.Translate(moveVector);
	}
	
	public void SpawnFood() {
		//int x = (int)Random.Range (lWall.position.x + 0.5f, rWall.position.x - 0.5f);
		//int y = (int)Random.Range (bWall.position.y + 0.5f, tWall.position.y - 0.5f);
		//Vector2 spawnPos = new Vector2 (x, y);

		//if(Physics2D.OverlapCircle(spawnPos,1f,0,0)== null){
		//	Instantiate (food, spawnPos, Quaternion.identity);	
		//}

		GameController.GamesController.canSpawnFoodNorm = true;
	}
 
	void OnTriggerEnter2D(Collider2D coll) {
		//Debug.Log("Triggerd!");

		if (coll.gameObject.tag == "Food") {
			//Debug.Log("Eaten Food");
			int eatVal = coll.gameObject.GetComponent<FoodBehaviour>().eatenVal;
			GameController.GamesController.CalculateScore(eatVal);

			eat = true;
			Destroy(coll.gameObject);
			SpawnFood();
		}else if (coll.gameObject.tag == "Edible"){
			//Debug.Log("Eaten Food");
			int eatVal = 0;
			if(coll.gameObject.GetComponent<ChickenBehaviour>() != null){
				eatVal = coll.gameObject.GetComponent<ChickenBehaviour>().eatenVal;
			}else if(coll.gameObject.GetComponent<GopherBehaviour>() != null){
				eatVal = coll.gameObject.GetComponent<GopherBehaviour>().eatenVal;
			}
			GameController.GamesController.CalculateScore(eatVal);

			eat = true;
			Destroy(coll.gameObject);
		}else if(coll.gameObject.tag == "ColSpwnChkr"){
			//Do nothing
		}else{
			Debug.Log("End Game");
			CancelInvoke("MoveSnake");
		}
	}

	IEnumerator MakeMoveSwitch(){
		yield return new WaitForSeconds(0.2f);
		canMakeMove = true;
	}
}
