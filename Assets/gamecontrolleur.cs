﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamecontrolleur : MonoBehaviour {

	static public bool PauseState = false;
	private GameObject pauseMenu;
	private GameObject looseScreen;
	public bool intro = false;
	public bool main_menu = false;

	void Start()
	{
		if(main_menu == false && intro == false){
			pauseMenu = GameObject.Find("Pause");
			looseScreen = GameObject.Find("Loose Screen");
			pauseMenu.SetActive(false);
			looseScreen.SetActive(false);
			Time.timeScale = 1f;
		}

	}
	
	public void LoadLevel(int level){
		SceneManager.LoadScene(level);
	}
	public void LoadNextLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

	public void LooseGame(){
		Time.timeScale = 0f;
		looseScreen.SetActive(true);
	}

	void Update()
	{
		if(main_menu == false){
			if((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause")) && intro == false){
				Pause();
			}
		}

		if(intro){
			if(Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause") || Input.GetButtonDown("Fire1")){
			LoadNextLevel();
		}

		}
	}
}
