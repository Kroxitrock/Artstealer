using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {
	
	public void Upgrades_Button () {
	
		Application.LoadLevel(2);
	}

	public void Play_Button () {
		
		Application.LoadLevel(3);
	}
}
