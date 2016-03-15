using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/**
 * UI管理。
 */
public class UIManager : MonoBehaviour {

	private static UIManager instance = null;

	/**
	 * イベントの用意。
	 */
	public event EventHandler OnClick;

	/**
	 *OnGUI.
	 */
	public void OnGUI() {
		// イベント実行
		if (this.OnClick != null) this.OnClick(this, EventArgs.Empty);
	}
		

	public static UIManager Instance {
		get {	
			return UIManager.instance;	
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
