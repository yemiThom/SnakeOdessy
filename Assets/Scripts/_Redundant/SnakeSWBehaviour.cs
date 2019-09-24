using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSWBehaviour : MonoBehaviour {

	private Renderer[] renderers;

	private bool isWrappingX, isWrappingY;

	void Start () {
		renderers = GetComponentsInChildren<Renderer>();
	}
	
	void FixedUpdate () {
		ScreenWrap();
	}

	void ScreenWrap(){
		bool isVisible = CheckRenderers();

		if(isVisible){
			//Debug.Log("Is Visible");
			isWrappingX = isWrappingY = false;
			return;
		}

		if(isWrappingX && isWrappingY){
			//Debug.Log("Should be wrapping!");
			return;
		}

		Vector2 newPos = transform.position;
		//Debug.Log("New Pos X: " + newPos.x);
		if(newPos.x > 1 || newPos.x < 0){
			newPos.x = -newPos.x;
			isWrappingX = true;
		}

		//Debug.Log("New Pos Y: " + newPos.y);
		if(newPos.y > 1 || newPos.y < 0){
			newPos.y = -newPos.y;
			isWrappingY = true;
		}

		transform.position = newPos;
	}

	bool CheckRenderers(){
		foreach(Renderer rend in renderers){
			if(rend.isVisible){
				return true;
			}
		}

		return false;
	}


}
