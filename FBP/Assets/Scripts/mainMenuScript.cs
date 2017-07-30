using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

public class mainMenuScript : MonoBehaviour {

	public GUISkin skin;

	Rect windowMain, windowLevels, windowHighScore;
	int switcher = 0;
	int levelSelector = 1;

	string highScoreKey = "HighScore";
	
	void Start () {

		windowMain = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200);
		windowLevels = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200);
		windowHighScore = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200);
	}

	void Update () {
	
	}

	void OnGUI()
	{
		GUI.skin = skin;

		if (switcher == 0) {

			windowMain = GUI.Window(0, windowMain, mainMenuFunctions, "Main Menu");
		}
		else if (switcher == 1) {

			windowLevels = GUI.Window(0, windowLevels, levelSelectFunctions, "Level " + levelSelector.ToString());
		}
		else if (switcher == 2) {

			windowHighScore = GUI.Window(0, windowHighScore, highScoreFunctions, "High Score");
		}
	}

	void mainMenuFunctions(int id) {

		if(GUILayout.Button("Play")){
			SceneManager.LoadScene(levelSelector.ToString());
		}
		if(GUILayout.Button("Level Select")){
			switcher = 1;
		}
		if(GUILayout.Button("High Score")){
			switcher = 2;
		}

		GUILayout.Space (20);

		if(GUILayout.Button("Quit")){
			Application.Quit();
		}
	}

	void levelSelectFunctions(int id) {

		if(GUILayout.Button("Previous Level")){
			if ((levelSelector - 1) >= 1)
				levelSelector--;
		}
		if(GUILayout.Button("Next Level")){
			if ((levelSelector + 1) <= 10)
				levelSelector++;
		}

		GUILayout.Space(56);

		if(GUILayout.Button("Back")){
			switcher = 0;
		}
	}

	void highScoreFunctions(int id) {

		int highScore;

		highScore = PlayerPrefs.GetInt(highScoreKey, -1);

		if(highScore == -1) {

			GUILayout.Label("There are no records yet");
			GUILayout.Space(42);
		}
		else {

			GUILayout.Label("Current high score:\n" + highScore.ToString());
			GUILayout.Space(42);
		}
		
		if(GUILayout.Button("Reset")){
			
			PlayerPrefs.SetInt(highScoreKey, -1);
			PlayerPrefs.Save();
		}

		if(GUILayout.Button("Back")){
			switcher = 0;
		}
	}
}
