using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]

public class Foot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider other) {
		Destroy( this.gameObject );		
		
		// Play sound
		
		// Play particle FX		
	}
}