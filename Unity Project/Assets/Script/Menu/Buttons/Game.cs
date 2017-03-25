using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public GameObject game;
	public GameObject coins;
	public GameObject upgrades;

	bool is_pressed;

	void Start() {

		is_pressed = false;
	}
	
	public void Play () {

		if (!is_pressed) {
		
			Show();
			is_pressed = true;
		}
		else {

			Hide();
			is_pressed = false;
		}
	}

	public void Show() {

		game.transform.Translate(0, 100, 0);
		coins.transform.Translate(0, 100, 0);
		upgrades.transform.Translate(0, 100, 0);
	}

	
	public void Hide() {
		
		game.transform.Translate(0, -100, 0);
		coins.transform.Translate(0, -100, 0);
		upgrades.transform.Translate(0, -100, 0);
	}

}
