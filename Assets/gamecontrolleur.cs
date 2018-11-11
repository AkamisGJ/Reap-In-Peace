using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamecontrolleur : MonoBehaviour {

	static public bool PauseState = false;
	private GameObject pauseMenu;

	void Start()
	{
		pauseMenu = GameObject.Find("Pause");
		pauseMenu.SetActive(false);

	}
	
	public void LoadLevel(int level){
		SceneManager.LoadScene(level);
	}

	public void QuitGame(){
		Application.Quit();
	}

	public void RestartLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Pause(){
		PauseState = !PauseState;
		if(PauseState){
			pauseMenu.SetActive(true);
			Time.timeScale = 0f;
			print("Pause");
		}else{
			pauseMenu.SetActive(false);
			Time.timeScale = 1f;
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
