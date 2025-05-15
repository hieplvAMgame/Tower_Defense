/*
	Select the main menu scene by plataform
*/

using UnityEngine;
using System.Collections;

public class PlataformSelection : MonoBehaviour {

	void Start () {
		if (Application.platform == RuntimePlatform.Android){
			Application.LoadLevel("MainMenuPhone");
		}else{
			Application.LoadLevel("MainMenu");
		}
	}

}
