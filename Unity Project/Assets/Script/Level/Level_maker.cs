using UnityEngine;
using System.Collections;

public class Level_maker : MonoBehaviour {

	public int level_lenght;
	public GameObject terrain_02;
	GameObject new_obj;
	public GameObject child;
	static public float level_time;  

	// Use this for initialization
	void Start () {
	
		level_lenght = Random.Range (5, 8);
		level_time = level_lenght * 8;

		//terrain_02 = (GameObject) Resources.Load("Terrain_02");
	}
	
	// Update is called once per frame
	void Update () {
	
		for (; level_lenght > 0; level_lenght--) {
			new_obj = (GameObject) Instantiate(terrain_02, gameObject.transform.position, gameObject.transform.rotation);
			child = new_obj.transform.GetChild(0).GetChild(0).gameObject;
			gameObject.transform.position = child.transform.position;

			if (level_lenght == 1) {


			}
		}
	}
}
