using UnityEngine;
using System.Collections;

public class Level_maker : MonoBehaviour {

	public int level_lenght;
	int choose_terrain;

	public GameObject terrain_02;
	public GameObject terrain_03;
	public GameObject terrain_04;
	public GameObject terrain_05;
	public GameObject final;
	
	GameObject new_obj;
	public GameObject child;
	static public float level_time; 
	int have_timer;

	// Use this for initialization
	void Start () {
	
		have_timer = PlayerPrefs.GetInt("timer_upgrade");

		level_lenght = Random.Range (5, 8);
		level_time = level_lenght * 8;

		if (have_timer != 0) {
			level_time = level_time + (level_time / 5); 
			have_timer = 0;
			PlayerPrefs.SetInt("timer_upgrade", have_timer);
			PlayerPrefs.Save();
		}

		//terrain_02 = (GameObject) Resources.Load("Terrain_02");
	}
	
	// Update is called once per frame
	void Update () {
	
		for (; level_lenght > 0; level_lenght--) {

			choose_terrain = Random.Range(1,5);

			if (choose_terrain == 1)
				new_obj = (GameObject) Instantiate(terrain_02, gameObject.transform.position, gameObject.transform.rotation);

			if (choose_terrain == 2)
				new_obj = (GameObject) Instantiate(terrain_03, gameObject.transform.position, gameObject.transform.rotation);

			if (choose_terrain == 3)
				new_obj = (GameObject) Instantiate(terrain_04, gameObject.transform.position, gameObject.transform.rotation);

			if (choose_terrain == 4)
				new_obj = (GameObject) Instantiate(terrain_05, gameObject.transform.position, gameObject.transform.rotation);

			child = new_obj.transform.GetChild(0).GetChild(0).gameObject;
			gameObject.transform.position = child.transform.position;

			if (level_lenght == 1) {

				child = new_obj.transform.GetChild(1).gameObject;
				Instantiate(final, child.transform.position, child.transform.rotation);
			}
		}
	}
}
