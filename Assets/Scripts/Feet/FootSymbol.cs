using UnityEngine;
using System.Collections;

public class FootSymbol : MonoBehaviour {
	
	/// <summary>
	/// Enums
	/// </summary>
	public enum FootState
	{
		Down,
		Up,
		Inactive
	}
		
	public enum Foot
	{
		Left,
		Right
	}
	
	// Publics
	// Starting Foot - Left, Right
	public Foot startingFoot = Foot.Left;
	
	// Starting Foot State - Up, Down, Inactive
	public FootState startingFootState = FootState.Inactive;
	
	public GameObject leftFootUpIcon = null;
	public GameObject leftFootDownIcon = null;
	public GameObject rightFootUpIcon = null;
	public GameObject rightFootDownIcon = null;
	
	/// <summary>
	/// Private Variables
	/// </summary>
	
	// Type of foot
	private Foot foot = Foot.Left;
	
	
	// Track if the foot symbol should be reversed
	private bool flipped = false;
	
	
	// Track if foot is up, down or inactive
	private FootState state = FootState.Inactive;
	
	private GameObject footObject = null;
	
	
	// Use this for initialization
	void Start () {
		// Double check that the foot icons are already set
		if( !leftFootUpIcon )
		{
			print("Left Foot Up Icon not found.");	
			Debug.Break();
		}
		
		if( !leftFootDownIcon )
		{
			print("Left Foot Down Icon not found.");	
			Debug.Break();
		}
		
		if( !rightFootUpIcon )
		{
			print("Right Foot Up Icon not found.");
			Debug.Break();
		}
		
		if( !rightFootDownIcon )
		{
			print("Right Foot Down Icon not found.");
			Debug.Break();
		}
		
		// Setup the foot based on the settings from the editor
		UpdateFoot( startingFoot, startingFootState, false );
	}
	
	
	// Update the foot values.
	public void UpdateFoot( Foot footType, FootState footState, bool footFlipped ) {
		
		bool needNewModel = false;	
		
		// If the foot type or state changes, swap out the model
		if( foot != footType || state != footState )		
			needNewModel = true;
		
		// Update variables
		foot = footType;
		state = footState;
		
		// Flip the foot if necessary.
		if( flipped != footFlipped && footObject != null )
		{
			flipped = footFlipped;
			
			if( flipped )
			{
				footObject.transform.RotateAround( new Vector3(0,0,1), 180 );					
			}
			else
			{
				footObject.transform.rotation = Quaternion.identity;				
			}
		}
		
		if( needNewModel  )
		{
			// Delete the old model
			if( footObject != null )
				GameObject.Destroy( footObject );
			
			// Left Foot
			if( foot == Foot.Left )
			{
				if( state == FootState.Down )
				{					
					footObject = Instantiate( leftFootDownIcon, this.transform.position, this.transform.rotation) as GameObject;
					
					// Update the foot boundaries
					if( GameManager.instance.feetBoundaries.Length > 1 )
					{
						GameManager.instance.feetBoundaries[0].x = footObject.transform.position.x - 1.5f;
						GameManager.instance.feetBoundaries[0].y = footObject.transform.position.x + 1.5f;
					}
				}
				else if( state == FootState.Up )
				{
					footObject = Instantiate( leftFootUpIcon, this.transform.position, this.transform.rotation) as GameObject;
					Vector3 newPosition = footObject.transform.position;
					newPosition.z -= 5.0f;
					footObject.transform.position = newPosition;
					
					// Clear foot boundaries
					if( GameManager.instance.feetBoundaries.Length > 1 )
					{
						GameManager.instance.feetBoundaries[0].x = 0.0f;
						GameManager.instance.feetBoundaries[0].y = 0.0f;
					}
				}
				else if( state == FootState.Inactive )
				{
					// Clear foot boundaries
					if( GameManager.instance.feetBoundaries.Length > 1 )
					{
						GameManager.instance.feetBoundaries[0].x = 0.0f;
						GameManager.instance.feetBoundaries[0].y = 0.0f;
					}
				}
			}
			
			// Right Foot
			else if( foot == Foot.Right )
			{
				if( state == FootState.Down )
				{
					footObject = Instantiate( rightFootDownIcon, this.transform.position, this.transform.rotation) as GameObject;	
					
					// Update the foot boundaries
					if( GameManager.instance.feetBoundaries.Length > 1 )
					{
						GameManager.instance.feetBoundaries[1].x = footObject.transform.position.x - 1.5f;
						GameManager.instance.feetBoundaries[1].y = footObject.transform.position.x + 1.5f;
					}
				}
				else if( state == FootState.Up )
				{
					footObject = Instantiate( rightFootUpIcon, this.transform.position, this.transform.rotation) as GameObject;
					Vector3 newPosition = footObject.transform.position;
					newPosition.z -= 5.0f;
					footObject.transform.position = newPosition;
					
					// Clear foot boundaries
					if( GameManager.instance.feetBoundaries.Length > 1 )
					{
						GameManager.instance.feetBoundaries[1].x = 0.0f;
						GameManager.instance.feetBoundaries[1].y = 0.0f;
					}
				}
				else if( state == FootState.Inactive )
				{
					// Clear foot boundaries
					if( GameManager.instance.feetBoundaries.Length > 1 )
					{
						GameManager.instance.feetBoundaries[1].x = 0.0f;
						GameManager.instance.feetBoundaries[1].y = 0.0f;
					}
				}
			}
		}
	}
}
