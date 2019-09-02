using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_eletron : MonoBehaviour {

    public GameObject atomo;
	public string parent;
	
	// Use this for initialization
	void Start () {
		parent = transform.parent.name;
        atomo = GameObject.Find(parent);
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(atomo.transform.position, transform.forward, 100 * Time.deltaTime);
    }
}
