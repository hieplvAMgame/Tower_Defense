/*
 	Return to main menu scene
*/

using UnityEngine;
using System.Collections;

public class Go_Main : MonoBehaviour {
	void OnMouseDown() {
		Invoke("CrossfadeDelayed",0.5f);
	}
		
	private void CrossfadeDelayed(){
		GameObject.Find("Crossfade").GetComponent<Animator>().SetBool("out",true);
		Invoke("ExitDelayed",2);
	}

	private void ExitDelayed(){
		if (Application.platform == RuntimePlatform.Android){
			Application.LoadLevel("MainMenuPhone");
		}else{
			Application.LoadLevel("MainMenu");
		}
	}

}
