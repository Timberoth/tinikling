using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BeatManager : MonoBehaviour {
		
	// This is filled out through the editor.
	public GameObject[] beatPatterns;	
	
	private int beatIndex = 0;
		
	/*
	 * Singleton Code
	 */	
	public static BeatManager instance;
	void Awake(){		
		if(instance == null){
			instance = this;
		}
		else{
			Debug.LogWarning("There should only be one of these");
			print("There should only be one of these");
			Debug.Break();			
		}
	}
	

	// Use this for initialization
	void Start () {
	
	}
	
	
	public BeatPattern GetBeat( string name )
	{
		for( int i = 0; i < beatPatterns.Length; i++ )
		{			
			GameObject beatObject = beatPatterns[i];
			if( beatObject && beatObject.name == name )
			{
				BeatPattern pattern = beatObject.GetComponent<BeatPattern>();
				if( pattern )
				{
					beatIndex = i;
					return pattern;
				}
				
				print("ERROR - Failed to retrieve BeatPattern component with name "+name);
				Debug.Break();
				return null;
			}
		}
		
		print("ERROR - Failed to find BeatPattern GameObject with name "+name);
		Debug.Break();
		return null;
	}	
	
	public BeatPattern GetBeat( int index )
	{
		if( index < beatPatterns.Length )
		{
			BeatPattern pattern = beatPatterns[index].GetComponent<BeatPattern>();
			if( pattern )
			{
				beatIndex = index;
				return pattern;
			}
			else
			{
				print("ERROR - Failed to retrieve BeatPattern component with index "+index);
				Debug.Break();
				return null;
			}
		}		
		else
		{		
			return null;
		}
	}
	
	public BeatPattern GetNextBeat()
	{
		beatIndex++;
		
		// Roll over condition
		if( beatIndex >= beatPatterns.Length )
		{
			beatIndex = 0;			
		}
		
		return GetBeat( beatIndex );
	}
}
