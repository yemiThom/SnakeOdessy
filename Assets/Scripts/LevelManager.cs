using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	[System.Serializable]
	public class Level{
		public string levelTxt;
		public int levelUnlocked;
		public bool isInteractable;

		//public Button.ButtonClickedEvent onClickEvent;
	}

	public GameObject button;
	public Transform spacerPanel;
	public List<Level> levelList;

	void Start () {
		FillList();
	}
	
	void FillList(){
		foreach(Level lvl in levelList){
			GameObject newbtn = Instantiate(button);

			newbtn.transform.SetParent(spacerPanel,false);

			LevelButton btn = newbtn.GetComponent<LevelButton>();
			btn.levelNumTxt.text = lvl.levelTxt;

			if((PlayerPrefs.GetInt(btn.levelNumTxt.text) == 1)){
				lvl.levelUnlocked = 1;
				lvl.isInteractable = true;
			}

			btn.levelUnlocked = lvl.levelUnlocked;
			btn.GetComponent<Button>().interactable = lvl.isInteractable;
			btn.GetComponent<Button>().onClick.AddListener(() => LevelToLoad(btn.levelNumTxt.text));

			//Debug.Log("Level Num Text: " + btn.levelNumTxt.text);

			if(PlayerPrefs.GetInt(btn.levelNumTxt.text + "_score") > 0){
				btn.star1.SetActive(true);
			}
			if(PlayerPrefs.GetInt(btn.levelNumTxt.text + "_score") >= 10){
				btn.star2.SetActive(true);
			}
			if(PlayerPrefs.GetInt(btn.levelNumTxt.text + "_score") >= 20){
				btn.star3.SetActive(true);
			}
		}

		SaveLevelInfo();
	}

	void SaveLevelInfo(){
		GameObject[] allBtns = GameObject.FindGameObjectsWithTag("LvlBtns");

		/*if(PlayerPrefs.HasKey("Stage1")){
			return;
		}else{*/
			foreach(GameObject btns in allBtns){
				LevelButton btn = btns.GetComponent<LevelButton>();
				
				PlayerPrefs.SetInt(btn.levelNumTxt.text, btn.levelUnlocked);
			}
		//}
	}

	void LevelToLoad(string levelName){
		SceneManager.LoadScene(levelName);
	}
}
