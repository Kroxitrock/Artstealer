using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer_Upgrade : MonoBehaviour {


	int have_timer;
	int coins;
	public Sprite full_bar;
	public Sprite empty_bar;

	public Image bar;

	void Start () {

		have_timer = PlayerPrefs.GetInt("timer_upgrade");
		coins = PlayerPrefs.GetInt("coin");
	}

	void Update () {

		if (have_timer == 0)
			bar.GetComponent<Image> ().sprite = empty_bar;
		else 
			bar.GetComponent<Image> ().sprite = full_bar;
	}

	public void Timer () {

		if (coins >= 10 && have_timer == 0) {
			have_timer = 1;
			PlayerPrefs.SetInt("timer_upgrade", have_timer);
			PlayerPrefs.Save();
			coins -= 10;
			PlayerPrefs.SetInt("coin", coins);
			PlayerPrefs.Save();
		}
	}
}
