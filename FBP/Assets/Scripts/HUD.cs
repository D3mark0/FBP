using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

public class HUD : MonoBehaviour 
{
	public GUISkin skin;
	public gameController gameController;
	public GUIText coins;
	public GUIText deaths;
	
	bool pause;
	Rect windowPause, windowWin;
	
	public int playerScore = 0;
	public int highScore = 0;
	string highScoreKey = "HighScore";


	void Start() {

		pause = false;
		Time.timeScale = 1;
		windowPause = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 150);
		windowWin = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200);

		highScore = PlayerPrefs.GetInt(highScoreKey,0);
	}
	
	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			pause = !pause;
			if(pause)
				Time.timeScale = 0;
			else
				Time.timeScale = 1;
		}

		if(gameController.win)
		{
			Time.timeScale = 0;
		}

		coins.text = gameController.coins.ToString();
		deaths.text = gameController.deaths.ToString();
	}
	
	void OnGUI() {
		GUI.skin = skin;

		if(pause)
			windowPause = GUI.Window (0, windowPause, pauseFunctions, "Pause Menu");

		if (gameController.win)
			windowWin = GUI.Window (0, windowWin, winFunctions, "Finished");

	}
	
	void pauseFunctions(int id) {
		if(GUILayout.Button("Resume")){
			pause = false;
			Time.timeScale = 1;
		}
		if(GUILayout.Button("Respawn")){
			gameController.Die();
			pause = false;
			Time.timeScale = 1;
		}
		if(GUILayout.Button("Quit")){
			SceneManager.LoadScene ("Menu");
		}
	}

	void winFunctions(int id) {

		string message = "";

		playerScore = gameController.coins - gameController.deaths;

		if (playerScore < -100)
			message = "Disgusting!!!";
		else if (playerScore < -75)
			message = "Awful!";
		else if (playerScore < -50)
			message = "Miserable!";
		else if (playerScore < -25)
			message = "Unacceptable!";
		else if (playerScore < 0)
			message = "Eeeh...";
		else if (playerScore == 0)
			message = "Suspiciously...";
		else if (playerScore > 0)
			message = "Awesome!";

		GUILayout.Label (message);
		GUILayout.Label ("Your Score: " + playerScore.ToString ());

		if (playerScore > highScore) {

			GUILayout.Label("New record!");

			PlayerPrefs.SetInt(highScoreKey, playerScore);
			PlayerPrefs.Save();

			GUILayout.Space(5);
		}
		else
			GUILayout.Space (35);

		if(GUILayout.Button("Play")){

			string currentLevel = SceneManager.GetActiveScene().name;
			int level = 0;

			int.TryParse(currentLevel, out level);

			level++;

			if(level < 10)
				SceneManager.LoadScene(level.ToString());
			else
				SceneManager.LoadScene("Menu");
		}
		
		if(GUILayout.Button("Quit")){
			
			SceneManager.LoadScene("Menu");
		}
	}
}