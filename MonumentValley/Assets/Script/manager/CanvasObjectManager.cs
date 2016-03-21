using UnityEngine;
using System.Collections;

public class CanvasObjectManager : MonoBehaviour {

	public GameModel _game_model;

	private static CanvasObjectManager instance = null;

	public static CanvasObjectManager Instance {
		get {	
			return CanvasObjectManager.instance;	
		}
	}


	void Awake()
	{
		if( instance == null)

		{

			instance = this;

		}else{

			Destroy( this );

		}

	}
		
		
}
