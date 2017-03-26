using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour {
	public int game_num;
	public int coins;
	public GameObject button;
	
	// Use this for initialization
	void Start () {

		Advertisement.Initialize ("1360539", true);
	}
	
	void Update(){

		coins = PlayerPrefs.GetInt ("coin");
	}
	
	public void Click() {
		if(Advertisement.IsReady()){
			Advertisement.Show();
			Destroy (button);
			game_num = 0;
			PlayerPrefs.SetInt("game_num", game_num);
			PlayerPrefs.Save();
			coins += 10;
			PlayerPrefs.SetInt("coin", coins);
			PlayerPrefs.Save();
		}
		
	}
	
}