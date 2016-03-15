using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;

public class TopSceneModel : MonoBehaviour {

	//キャンバストップシーン
	[SerializeField]
	private GameObject canvas_top_scene;
	public GameObject CanvasTopScene
	{
	    get { return this.canvas_top_scene; } 
	    set { this.canvas_top_scene = value; }
	}
		

	private static TopSceneModel instance = null;

	public static TopSceneModel Instance {
		get {	
			return TopSceneModel.instance;	
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
	