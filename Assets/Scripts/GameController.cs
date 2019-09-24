using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public bool gamePaused, canSpawnFoodNorm, canSpawnGopher, canSpawnChicken, gopherExists, chickenExists;
	float tmpTimescale;
	public GameObject collisionSpawnChecker;
	//food objects
	public GameObject foodStandard;
	//animal objects
	public GameObject gopher, chicken;
	//border walls
	public Transform rWall, lWall, tWall, bWall;
	public Text scoreTxt, highScoreTxt;
	int currentScore;
	public int unlockNextStageScore = 10;
	public int maxNumLvls = 10;

	private static GameController gameController;
	public static GameController GamesController{
		get{
			if(gameController == null){
				gameController = GameObject.FindObjectOfType<GameController>();
			}
			return gameController;
		}
	}
	
	void Start () {
		if(!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_score")){
			Debug.Log("No High Score set. Reset Scores");

			scoreTxt.text = "0";
			highScoreTxt.text = "0";
		}else{
			scoreTxt.text = "0";
			highScoreTxt.text = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_score").ToString();
		}
	}
	
	void Update () {
		if(canSpawnFoodNorm){
			StartCoroutine(SpawnFoodNorm());
		}

		if(canSpawnGopher && !gopherExists){
			StartCoroutine(SpawnGopher());
			gopherExists = true;
		}

		if(canSpawnChicken && !chickenExists){
			StartCoroutine(SpawnChicken());
			chickenExists = true;
		}

		CalculateHighScore();

		CheckKeyboardShortcuts();
	}

	public void DeleteAllData(){
		PlayerPrefs.DeleteAll();
	}

	void CheckKeyboardShortcuts(){
		if(Input.GetKey(KeyCode.P)){
			PauseGame();
		}

		if(Input.GetKey(KeyCode.U)){
			UnpauseGame();
		}

		if(Input.GetKey(KeyCode.R)){
			RestartLevel();
		}

		if(Input.GetKey(KeyCode.M)){
			GoToLevelSelect();
		}

		if(Input.GetKey(KeyCode.X)){
			DeleteAllData();
		}
	}

	void CalculateHighScore(){
		int highScore = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_score");
		if(currentScore > highScore){
			highScore = currentScore;
		}

		if(highScore >= unlockNextStageScore){
			int currentIndex = SceneManager.GetActiveScene().buildIndex;
			Debug.Log("Current Index: "+currentIndex);
			//int nextIndex = SceneManager.GetActiveScene().buildIndex;
			//string nextSceneName = SceneManager.GetSceneAt(currentIndex + 1).name;
			string nextSceneName = "";

			if(currentIndex < maxNumLvls){
				nextSceneName = GetSceneNameFromScenePath(SceneUtility.GetScenePathByBuildIndex(currentIndex + 1));
			}else{
				nextSceneName = SceneManager.GetActiveScene().name;
			}
			Debug.Log("Next Scene Name: " + nextSceneName);

			StageCompleteUnlockNext(nextSceneName);
		}

		highScoreTxt.text = highScore.ToString();
		PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_score", highScore);
	}

	private static string GetSceneNameFromScenePath(string scenePath)
    {
        // Unity's asset paths always use '/' as a path separator
        var sceneNameStart = scenePath.LastIndexOf("/") + 1;
        var sceneNameEnd = scenePath.LastIndexOf(".");
        var sceneNameLength = sceneNameEnd - sceneNameStart;
        return scenePath.Substring(sceneNameStart, sceneNameLength);
    }

	public void CalculateScore(int eatVal){
		currentScore += eatVal;

		scoreTxt.text = currentScore.ToString();
	}

	void StageCompleteUnlockNext(string lvlName){
		if(PlayerPrefs.GetInt(lvlName) != 1){
			//Debug.Log("Setting " + lvlName + " to unlocked!");
			PlayerPrefs.SetInt(lvlName, 1);
		}else{
			//Debug.Log("Wait! " + lvlName + " is already unlocked!");
		}
	}

	IEnumerator SpawnFoodNorm(){
		canSpawnFoodNorm = false;
			
		int x = (int)Random.Range (lWall.position.x + 0.5f, rWall.position.x - 0.5f);
		int y = (int)Random.Range (bWall.position.y + 0.5f, tWall.position.y - 0.5f);
		Vector2 spawnPos = new Vector2 (x, y);

		yield return new WaitForSeconds(0.2f);
		collisionSpawnChecker.GetComponent<CollisionSpawnCheckBehaviour>().ObjectToSpawn(foodStandard);
		Instantiate (collisionSpawnChecker, spawnPos, Quaternion.identity);
	}

	IEnumerator SpawnGopher(){
			
		int x = (int)Random.Range (lWall.position.x + 0.5f, rWall.position.x - 0.5f);
		int y = (int)Random.Range (bWall.position.y + 0.5f, tWall.position.y - 0.5f);
		Vector2 spawnPos = new Vector2 (x, y);

		yield return new WaitForSeconds(Random.Range(5f,20f));
		//Instantiate(gopher, spawnPos, Quaternion.identity);
		collisionSpawnChecker.GetComponent<CollisionSpawnCheckBehaviour>().ObjectToSpawn(gopher);
		Instantiate (collisionSpawnChecker, spawnPos, Quaternion.identity);
		
	}

	IEnumerator SpawnChicken(){
			
		int x = (int)Random.Range (lWall.position.x + 2.5f, rWall.position.x - 2.5f);
		int y = (int)Random.Range (bWall.position.y + 2.5f, tWall.position.y - 2.5f);
		Vector2 spawnPos = new Vector2 (x, y);

		yield return new WaitForSeconds(Random.Range(5f,10f));
		collisionSpawnChecker.GetComponent<CollisionSpawnCheckBehaviour>().ObjectToSpawn(chicken);
		Instantiate(collisionSpawnChecker, spawnPos, Quaternion.identity);
		
	}

	public void PauseGame(){

		if(!gamePaused){
			gamePaused = true;
			Time.timeScale = 0.0f;
		}
	}

	public void UnpauseGame(){
		if(gamePaused){
			gamePaused = false;
			Time.timeScale = 1.0f;
		}
	}

	public void RestartLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void GoToLevelSelect(){
		SceneManager.LoadScene("LevelSelect");
	}
}