using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]

public class Foot : MonoBehaviour {
	
	private AudioSource footHit = null;
	
	// Used to make sure the foot isn't triggered twice.
	private bool stillAlive = true;
	
	// Use this for initialization
	void Start () {		
		
		InitAudio();
	}
	
	// Update is called once per frame
	void Update () {
		// As long as the foot exists, it gains points
		GameManager.instance.score++;
		GameManager.instance.UpdateScore();	
	}
	
	void OnTriggerEnter (Collider other) {
		
		if( stillAlive )
		{
			stillAlive = false;
				
			// Play sound
			if( footHit != null )
			{
				AudioSource.PlayClipAtPoint(footHit.clip, Vector3.zero);			
			}
			
			// Play particle FX
			
			// Lose a life
			GameManager.instance.lives--;
			GameManager.instance.UpdateLives();		
			
			// Stop the sticks from moving
			GameManager.instance.StopSticks();
			
			// Destroy object
			Destroy( this.gameObject );			
		}
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
