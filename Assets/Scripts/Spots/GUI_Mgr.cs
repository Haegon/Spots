using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUI_Mgr : MonoBehaviour {

	public Dictionary<GameState,GameObject> gameObjects = new Dictionary<GameState, GameObject>();
	public bool isInitialize = false;


	private static GUI_Mgr _instance;
	public static GUI_Mgr Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(GUI_Mgr)) as GUI_Mgr;
				if (_instance == null)
				{
					GameObject go = new GameObject("GUI_Mgr");
				}
			}
			return _instance;
		}
	}

	// Use this for initialization
	void Start () {
		InitUI();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitUI() {
		gameObjects.Add(GameState.HOME,GameObject.Find("HomeUI"));
		gameObjects.Add(GameState.INGAME,GameObject.Find("GameUI"));
		gameObjects.Add(GameState.FINISH,GameObject.Find("ResultUI"));
		gameObjects.Add(GameState.PAUSE,GameObject.Find("PauseUI"));
		gameObjects.Add(GameState.OPTION,GameObject.Find("OptionUI"));

		isInitialize = true;
	}

	public void NewGame() {
		gameObjects[GameState.HOME].SetActive(false);	
		gameObjects[GameState.INGAME].SetActive(true);	
		gameObjects[GameState.FINISH].SetActive(false);	
		gameObjects[GameState.PAUSE].SetActive(false);	
		gameObjects[GameState.OPTION].SetActive(false);	
	}
	
	public void GoHome() {
		gameObjects[GameState.HOME].SetActive(true);	
		gameObjects[GameState.INGAME].SetActive(false);	
		gameObjects[GameState.FINISH].SetActive(false);	
		gameObjects[GameState.PAUSE].SetActive(false);	
		gameObjects[GameState.OPTION].SetActive(false);	
	}
	
	public void ShowResult() {
		gameObjects[GameState.HOME].SetActive(false);	
		gameObjects[GameState.INGAME].SetActive(true);	
		gameObjects[GameState.FINISH].SetActive(true);	
		gameObjects[GameState.PAUSE].SetActive(false);	
		gameObjects[GameState.OPTION].SetActive(false);	
	}
	
	public void PauseGame() {
		gameObjects[GameState.HOME].SetActive(false);	
		gameObjects[GameState.INGAME].SetActive(true);	
		gameObjects[GameState.FINISH].SetActive(false);	
		gameObjects[GameState.PAUSE].SetActive(true);	
		gameObjects[GameState.OPTION].SetActive(false);	
	}
	
	public void ResumeGame() {
		gameObjects[GameState.HOME].SetActive(false);	
		gameObjects[GameState.INGAME].SetActive(true);	
		gameObjects[GameState.FINISH].SetActive(false);	
		gameObjects[GameState.PAUSE].SetActive(false);	
		gameObjects[GameState.OPTION].SetActive(false);	
	}

	public void Option() {
		gameObjects[GameState.HOME].SetActive(true);	
		gameObjects[GameState.INGAME].SetActive(false);	
		gameObjects[GameState.FINISH].SetActive(false);	
		gameObjects[GameState.PAUSE].SetActive(false);	
		gameObjects[GameState.OPTION].SetActive(true);	
	}
}
