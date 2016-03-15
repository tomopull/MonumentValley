using UnityEngine;
using System.Collections;

public class AbstractSinglton : MonoBehaviour {


	private static AbstractSinglton instance = null;

	public static AbstractSinglton Instance {

		get {	
			return AbstractSinglton.instance;	
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
