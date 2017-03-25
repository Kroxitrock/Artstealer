using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsInstaKill : MonoBehaviour {
    public GameObject Corpse;
    public GameObject Player;
    public Transform warpTarget;
    protected Vector3 myposition;


    void OnCollisionEnter2D(Collision2D col)
    {
        myposition = Player.transform.position;
        Instantiate(Corpse, myposition, Quaternion.identity);
        Player.transform.position = warpTarget.position;
    }
}
