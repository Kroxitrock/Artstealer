using UnityEngine;
using System.Collections;

public class Earn_Coins : MonoBehaviour {

	public int game_num;
	public GameObject invisible;
	public GameObject visible;

	// Use this for initialization
	void Start () {
	
		game_num = PlayerPrefs.GetInt("game_num");
	}
	
	// Update is called once per frame
	void Update () {
	
		if (game_num < 3)
			Invisible ();
		else
			Visible ();
	}

	void Visible() {

		gameObject.transform.position = visible.transform.position;
	}

	void Invisible() {

		gameObject.transform.position = invisible.transform.position;
	}
}
