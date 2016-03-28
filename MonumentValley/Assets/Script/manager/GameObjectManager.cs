using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameObjectManager : MonoBehaviour {

	private static GameObjectManager instance = null;
	public GameModel _game_model;


	/// <summary>
	/// The _target_angle. mouse_pos and character
	/// </summary>
	private float _target_angle;

	public void SetRotationAngleByTargetPosition(GameObject _char,Vector3 _vec_3){
		//マウスポインターが何らかのEventSystem関連のUI用のGameObject上になかった場合
		if(!EventSystem.current.IsPointerOverGameObject()){

			Vector3 selfScreenPoint = Camera.main.WorldToScreenPoint(_char.transform.position);
			Vector3 diff =  Input.mousePosition - selfScreenPoint;

			float angle = Mathf.Atan2(diff.y,diff.x) * Mathf.Rad2Deg;

			float final_angle = angle -90f;

			//Debug.Log(final_angle);
			_char.transform.eulerAngles = new Vector3(0,final_angle,0);

			//Debug.Log(angle);
			//_target_angle = GetRotationAngleByTargetPosition(Input.mousePosition);

		}

	}


	private void GetRotationAngleByTargetPosition(Vector3 _pos){


	}


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


