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
	 * Publics
	 */	
	// Prefab References used to spawn objects at run time
	public GameObject footObject;
		
	// Speed and Distance Variables
	public float upDownDist = 3.0f;
	public float inOutDist = 5.0f;
		
	public float UPDOWN_SPEED_MAX = 40.0f;	
	public float INOUT_SPEED_MAX = 40.0f;
	
	// Current Beat being used.
	public BeatPattern beat;
	
	// The left and right foot symbol boundaries for active symbols.
	// x = left boundary on the x-axis
	// y = right boundary on the x-axis
	public Vector2[] feetBoundaries;
	
	// Speed Sliders
	public UISlider upDownSpeedSlider;
	public UISlider inOutSpeedSlider;	
		
	
	/*
	 * Privates
	 */
	// Real time game object references
	private GameObject []feet;
	
	// Feet positioning
	private Vector3[] feetPositions;
	
	// Track if any fingers are touching the screen.
	private bool fingerPressed;	
#if UNITY_EDITOR
	private bool mouseHeld = false;
#endif
		
	
	// Score ticks up from 0
	private int score = 0;
	
	
	// GUI Code
	private SpriteText scoreText;
	
	
	/*
	 * Singleton Code
	 */	
	public static GameManager instance;
	void Awake(){		
		if(instance == null){
			instance = this;
		}
		else{
			Debug.LogWarning("There should only be one of these");
		}
	}
	
	
	
	/*
	 * Unity Functions 
	 */
	
	// Use this for initialization
	void Start () {
		
		// Initialize everything to the defaults.		
		score = 0;		
		
		// Feet
		feet = new GameObject[]{null, null};
		feetPositions = new Vector3[]{new Vector3(0,0,0), new Vector3(0,0,0)};		
		
		feetBoundaries = new Vector2[]{new Vector2(0,0), new Vector2(0,0)};		
		
		
		// Beat - Start with the first beat
		beat = BeatManager.instance.GetBeat(0);
		
		// Register our delegate with both controls:
        upDownSpeedSlider.AddValueChangedDelegate(SpeedSliderChange);
		inOutSpeedSlider.AddValueChangedDelegate(SpeedSliderChange);
		
		
		// Cache object references
		
		// Score Text
		GameObject scoreTextObject = GameObject.Find("ScoreText");
		scoreText = scoreTextObject.GetComponent<SpriteText>();
		if( scoreText )
		{
			scoreText.Text = "Score: "+score;	
		}
		
		// Create the StickManager
		StickManager.Instance.Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		// Check for mouse input
		CheckForInput();	
	}
	
	/*
	 * Gameplay Functions
	 */
	public GameObject SpawnFoot( Vector3 position ){
	
		if( footObject != null )
			return Instantiate( footObject, position, Quaternion.identity) as GameObject;
		else
			return null;
	}
	
	// Restart the level
	public void RestartLevel()
	{
		// Reset Score
		score = 0;
		UpdateScore();
		
		// Reset Combo Multipliers
		
		
		// Reset speed
	}	
	
	
	
	/*
	 * GUI Functions
	 */
	
	// Update Score Text
	public void UpdateScore()
	{
		if( scoreText )
		{
			scoreText.Text = "Score: "+score;	
		}		
	}
	

	// TODO Debug Functions
	
	public void NextBeat()
	{
		beat = BeatManager.instance.GetNextBeat();
	}	
	
	public void IncreaseFootPatternSpeed()
	{
		// Increase foot pattern speed
		GameObject patternObject = GameObject.Find("FootPattern1") as GameObject;
		FootPattern1 pattern = patternObject.GetComponent<FootPattern1>();
		pattern.currentSpeed += 0.2f;
		print( pattern.currentSpeed );
	}	
	
	public void PausePattern()
	{
		GameObject patternObject = GameObject.Find("FootPattern1") as GameObject;
		FootPattern1 pattern = patternObject.GetComponent<FootPattern1>();
		pattern.PausePattern();
	}
	
	public void ResumePattern()
	{
		GameObject patternObject = GameObject.Find("FootPattern1") as GameObject;
		FootPattern1 pattern = patternObject.GetComponent<FootPattern1>();
		pattern.ResumePattern();
	}
	
	public void StopPattern()
	{
		GameObject patternObject = GameObject.Find("FootPattern1") as GameObject;
		FootPattern1 pattern = patternObject.GetComponent<FootPattern1>();
		pattern.StopPattern();
	}
	
	private float debugStickSpeed = 0.0f;
	public void IncreaseStickSpeed()
	{
		debugStickSpeed += 0.5f;
		StickManager.Instance.ChangeSpeed( debugStickSpeed );
	}
	
	
	
	// Scoring Functions
	
	public void CheckFootBounds( float x )
	{
		// As long as the foot exists within boundaries, it gains points
		if( x >= feetBoundaries[0].x && x <= feetBoundaries[0].y ||
		   x >= feetBoundaries[1].x && x <= feetBoundaries[1].y )
		{
			score++;
			UpdateScore();	
		}	
	}
	
	// GUI Events
	
	// Called when the slider moves
	void SpeedSliderChange( IUIObject obj )
    {
        if(obj == upDownSpeedSlider)
		{
			beat.upDownSpeed = upDownSpeedSlider.Value * UPDOWN_SPEED_MAX;
			if( beat.upDownSpeed <= 0.0 )
				beat.upDownSpeed = 1.0f;
			
			print("Updown Speed: "+beat.upDownSpeed);            
		}
		
		else if(obj == inOutSpeedSlider)
		{
			beat.inOutSpeed = inOutSpeedSlider.Value * INOUT_SPEED_MAX;			
			if( beat.inOutSpeed <= 0.0 )
				beat.inOutSpeed = 1.0f;
			
			print("InOut Speed: "+beat.inOutSpeed);            
		}
    }	
	
	
	/*
	 * Input Functions
	 */
	private void CheckForInput()
	{
#if UNITY_EDITOR				
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
			feet[0] = SpawnFoot( worldPosition );
			
			mouseHeld = true;			
			
			// Start particle effect
		}		
		
		// Check if the mouse has just been released.
		bool mouseUp = Input.GetMouseButtonUp(0);
		if( mouseUp && mouseHeld )
		{
			mouseHeld = false;
			
			// Despawn foot
			if( feet[0] != null )
			{
				GameManager.Destroy( feet[0] );
				feet[0] = null;
			}
			
			// End particle effect			
		}
				
#elif UNITY_ANDROID
		// DO ANDROID STUFF
		
#elif UNITY_IPHONE		
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
			
			// Check if the foot has been moved
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
#endif
	}	
}
