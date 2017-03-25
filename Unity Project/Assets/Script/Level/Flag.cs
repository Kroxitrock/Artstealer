using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Flag : MonoBehaviour {

	public Text succeed;
	int coins;
	public int wait_seconds;
	public Text timer;

	static public int stop_timer;


	// Use this for initialization
	void Start () {
	
		stop_timer = 0;
		succeed.enabled = false;
		coins = PlayerPrefs.GetInt("coin");
	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "Finish") {

			stop_timer = 1;
			succeed.enabled = true;	
			coins += 10;
			PlayerPrefs.SetInt("coin", coins);
			PlayerPrefs.Save();

			StartCoroutine(Change_Scene());
		}
	}

	IEnumerator Change_Scene () {

		yield return new WaitForSeconds(wait_seconds);
		Application.LoadLevel(1);
	}
}
