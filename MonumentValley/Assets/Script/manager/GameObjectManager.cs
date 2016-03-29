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

			Animator _animator = _char.GetComponent<Animator>();

			Vector3 selfScreenPoint = Camera.main.WorldToScreenPoint(_char.transform.position);

			//マウスの位置とキャラの位置の座標の差分
//			Vector3 diff =  Input.mousePosition - selfScreenPoint;

			Quaternion _rotate = Quaternion.LookRotation(Input.mousePosition -selfScreenPoint);
			_rotate.x = _rotate.z = 0;
			_char.transform.rotation = _rotate;

//			float angle = Mathf.Atan2(diff.y,diff.x) * Mathf.Rad2Deg;
//			float final_angle = angle -90f;

//			Quaternion _tmp_rotation = Quaternion.LookRotation(diff);
//			_char.transform.rotation = _tmp_rotation;

//			Quaternion rotate = Quaternion.LookRotation(target.transform.position - transform.position);
//			rotate.x = rotate.z = 0;
//			transform.rotation = rotate;

//			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//			RaycastHit hit = new RaycastHit();

			//if(Physics.Raycast(ray,out hit)){
				//_char.transform.LookAt(selfScreenPoint);
				//string selected = hit.collider.gameObject.name;
				//Debug.Log(selected);
			//}



//			Vector3 selfScreenPoint = Camera.main.WorldToScreenPoint(_char.transform.position);
//			Vector3 diff =  Input.mousePosition - selfScreenPoint;
//			float angle = Mathf.Atan2(diff.y,diff.x) * Mathf.Rad2Deg;
//			float final_angle = angle -90f;
//			Debug.Log(_char.transform.rotation + "rotation");
//			Debug.Log(final_angle + "final_angle");

		

		}

	}


	private void GetRotationAngleByTargetPosition(Vector3 _pos){

//		Vector3 selfScreenPoint = Camera.main.WorldToScreenPoint(_char.transform.position);
//		Vector3 diff =  Input.mousePosition - selfScreenPoint;
//		
//		float angle = Mathf.Atan2(diff.y,diff.x) * Mathf.Rad2Deg;
//		
//		float final_angle = angle -90f;
//		
//		//Debug.Log(final_angle);
//		_char.transform.rotation = Quaternion.Euler(0,final_angle,0);
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


