using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectManager : MonoBehaviour {

	private static GameObjectManager instance = null;
	public GameModel _game_model;

	public static GameObjectManager Instance {
		get {	
			return GameObjectManager.instance;	
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


