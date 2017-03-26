using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Coins_Menu : MonoBehaviour {

	public Text coins_text;
	int coins;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		coins = PlayerPrefs.GetInt("coin");
		coins_text.text = "" + coins;
	}
}