using UnityEngine;
using System.Collections;

public class Timer_Upgrade : MonoBehaviour {


	int have_timer;
	int coins;

	void Start () {

		have_timer = PlayerPrefs.GetInt("timer_upgrade");
		coins = PlayerPrefs.GetInt("coin");
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
