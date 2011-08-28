using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	/*
	 * Enums 
	 */
	public enum Foot
	{
		Left,
		Right
	}
	
	
	/*
	 * Variables
	 */
	
	// Prefab References used to spawn objects at run time
	public GameObject leftFootObject;	
	public GameObject rightFootObject;
	
	// Real time game object references
	private GameObject leftFoot = null;
	private GameObject rightFoot = null;
	
	// Track the last foot spawned
	private Foot lastFoot = Foot.Right;
	
	// Input Code
	private bool mouseHeld = false;
	
	/*
	 * Functions 
	 */
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// Check for mouse input
		CheckForInput();
		
		
	}
	
	public GameObject SpawnFoot( Foot type, Vector3 position ){
		GameObject foot = null;
		
		if( type == Foot.Left )
		{
			if( leftFootObject != null )
				foot = Instantiate( leftFootObject, position, Quaternion.identity) as GameObject;
			
		}
		else if( type == Foot.Right )
		{
			if( leftFootObject != null )
				foot = Instantiate( leftFootObject, position, Quaternion.identity) as GameObject;
		}
		
		return foot;
	}	
	
	
	private void CheckForInput(){
		// Check if mouse is down for the first time.		
		bool mouseDown = Input.GetMouseButtonDown(0);
		if( mouseDown && !mouseHeld )
		{			
			// Position
			Camera mainCamera = UnityEngine.Camera.mainCamera;
			Vector3 mousePosition = Input.mousePosition;			
			mousePosition.z = -mainCamera.transform.position.z;
			Vector3 worldPosition = mainCamera.ScreenToWorldPoint( mousePosition );
						
			
			// Spawn foot
			leftFoot = SpawnFoot( Foot.Left, worldPosition );
			
			mouseHeld = true;			
			
			// Start particle effect
		}
		
		// Check if the mouse has just been released.
		bool mouseUp = Input.GetMouseButtonUp(0);
		if( mouseUp && mouseHeld )
		{
			mouseHeld = false;
			
			// Despawn foot
			if( leftFoot != null )
			{
				GameManager.Destroy( leftFoot );
				leftFoot = null;
			}
			
			// End particle effect			
		}
	}
}
