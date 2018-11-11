using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamecontrolleur : MonoBehaviour {

	static public bool PauseState = false;
	private Canvas pauseMenu;

	void Start()
	{
		pauseMenu = GetComponent<Canvas>();
		pauseMenu.enabled = false;

	}
	
	public void LoadLevel(int level){
		SceneManager.LoadScene(level);
	}

	public void QuitGame(){
		Application.Quit();
	}

	public void Pause(){
		PauseState = !PauseState;
		if(PauseState){
			Time.timeScale = 0f;
			pauseMenu.enabled = true;
			print("Pause");
		}else{
			Time.timeScale = 1f;
			pauseMenu.enabled = false;
			print("UnPause");
		}
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape)){
			Pause();
		}
	}
}
