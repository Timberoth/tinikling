using UnityEngine;
using System.Collections;

public class FootSymbol : MonoBehaviour {
	
	/// <summary>
	/// Enums
	/// </summary>
	enum FootState
	{
		Down,
		Up,
		Inactive
	}
		
	enum Foot
	{
		Left,
		Right
	}
	
	// Publics
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
		
		// All symbols are inactive, non-flipped, left feet by default.
		UpdateFoot( Foot.Left, FootState.Down, false );
	}
	
	private int DEBUG_TIMER = 0;
	// Update is called once per frame
	void Update () {
	
		DEBUG_TIMER++;
		
		if( DEBUG_TIMER >= 90 )
		{
			DEBUG_TIMER = 0;
			
			if( state == FootState.Down )
			{
				UpdateFoot( Foot.Left, FootState.Up, false );
			}
			
			else if( state == FootState.Up )
			{
				UpdateFoot( Foot.Left, FootState.Inactive, false );
			}
			
			else if( state == FootState.Inactive )
			{
				UpdateFoot( Foot.Left, FootState.Down, false );
			}
		}
	}
	
	
	// Update the foot values.
	void UpdateFoot( Foot footType, FootState footState, bool footFlipped ) {
		
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
				}
				else if( state == FootState.Up )
				{
					footObject = Instantiate( leftFootUpIcon, this.transform.position, this.transform.rotation) as GameObject;
				}
			}
			
			// Right Foot
			else if( foot == Foot.Right )
			{
				if( state == FootState.Down )
				{
					footObject = Instantiate( rightFootDownIcon, this.transform.position, this.transform.rotation) as GameObject;	
				}
				else if( state == FootState.Up )
				{
					footObject = Instantiate( rightFootUpIcon, this.transform.position, this.transform.rotation) as GameObject;
				}
			}
		}
	}
}
