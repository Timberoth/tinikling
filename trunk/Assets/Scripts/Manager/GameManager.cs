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
	public GameObject footObject;	
	
	// Real time game object references
	private GameObject []feet;
	
	// Feet positioning
	private Vector3[] feetPositions;
	
	// Input Code
	private bool fingerPressed;
	
	
	/*
	 * Functions 
	 */
	
	// Use this for initialization
	void Start () {
		feet = new GameObject[]{null, null};
		feetPositions = new Vector3[]{new Vector3(0,0,0), new Vector3(0,0,0)};
	}
	
	// Update is called once per frame
	void Update () {
		// Check for mouse input
		CheckForInput();
		
		
	}
	
	public GameObject SpawnFoot( Vector3 position ){
	
		if( footObject != null )
			return Instantiate( footObject, position, Quaternion.identity) as GameObject;
		else
			return null;
	}	
	
	
	private void CheckForInput()
	{
		// If there are no touches, double check all feet destroyed
		if( Input.touches.Length == 0 )
		{
			for( int i = 0; i < feet.Length; i++ )
			{
				if( feet[i] != null )
				{
					Destroy(feet[i]);
					feet[i] = null;
					feetPositions[i] = Vector3.zero;
				}
			}
			
			return;
		}
		
		
		foreach(Touch touch in Input.touches) {
			
			// Calculate touches world space position.
			Camera mainCamera = UnityEngine.Camera.mainCamera;
			Vector3 fingerPosition = new Vector3();
			fingerPosition.x = touch.position.x;
			fingerPosition.y = touch.position.y;
			fingerPosition.z = -mainCamera.transform.position.z;
			Vector3 worldPosition = mainCamera.ScreenToWorldPoint( fingerPosition );
			
			float fingerDelta = touch.deltaPosition.magnitude;
					
			// On first touch, spawn a foot if there aren't two already.
			if (touch.phase == TouchPhase.Began) {
				if( feet[0] == null )
				{
					// Spawn foot
					feet[0] = SpawnFoot( worldPosition );
					feetPositions[0] = worldPosition;
				}
				else if( feet[1] == null )
				{
					// Spawn foot
					feet[1] = SpawnFoot( worldPosition );
					feetPositions[1] = worldPosition;
				}
			}
			
			else if( touch.phase == TouchPhase.Ended || fingerDelta > 5.0 || touch.phase == TouchPhase.Canceled )
			{
				for( int i = 0; i < feetPositions.Length; i++ )
				{
					// Figure out which foot was removed and delete it
					if( Vector3.Distance( worldPosition, feetPositions[i] ) < 3.0 )
					{
						if( feet[i] != null )
						{
							Destroy( feet[i] );
							feet[i] = null;	
							
							// Zero out the foot position
							feetPositions[i] = Vector3.zero;
						}
					}
				}
			}
		}	
	}
}
