using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]

public class Foot : MonoBehaviour {
	
	private AudioSource footHit = null;
	
	// Use this for initialization
	void Start () {		
		
		InitAudio();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider other) {
		// Play sound
		if( footHit != null )
		{
			AudioSource.PlayClipAtPoint(footHit.clip, Vector3.zero);			
		}
		
		// Play particle FX
		
		
		// Destroy object
		Destroy( this.gameObject );
	}
	
	
	void InitAudio()
	{
		// Create references to the attached audio sources.
		foreach( AudioSource source in this.GetComponents<AudioSource>() )
		{			
			if( source.clip.name == "FootHit" )
			{
				footHit = source;				
			}			
		}		
	}
}
