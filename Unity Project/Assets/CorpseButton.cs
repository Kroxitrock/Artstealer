using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseButton : MonoBehaviour {
    public GameObject Corpse;
    public GameObject Player;
    protected Vector3 myposition;
    public Transform warpTarget;

    public void killMe()
    {
        myposition = Player.transform.position;
        Instantiate(Corpse, myposition, Quaternion.identity);
        Player.transform.position = warpTarget.position;
    }
}
