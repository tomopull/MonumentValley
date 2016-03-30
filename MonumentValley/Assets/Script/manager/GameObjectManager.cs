using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameObjectManager : MonoBehaviour {

	private static GameObjectManager instance = null;
	public GameModel _game_model;


	private Vector3 _next;
	private bool _isClockwise; // _checkClockwise の結果を保存しておく

	public void Start(){

	}

	public void SetRotationAngleByTargetPosition(GameObject _char,Vector3 _vec_3){

		//マウスポインターが何らかのEventSystem関連のUI用のGameObject上になかった場合
		if(!EventSystem.current.IsPointerOverGameObject()){

			//Animator _animator = _char.GetComponent<Animator>();
			Vector3 selfScreenPoint = Camera.main.WorldToScreenPoint(_char.transform.position);
			//Vector3 selfScreenPoint = _char.transform.position;
			Vector3 diff = Input.mousePosition - selfScreenPoint;
			float angle = Mathf.Atan2(diff.y,diff.x) * Mathf.Rad2Deg;
			float final_angle = angle -90f;
			_char.transform.eulerAngles = new Vector3(0,-final_angle,0);

//			if(Physics.Raycast(ray,out hit)){
//				string selected = hit.collider.gameObject.name;
//				Quaternion _rotate = Quaternion.LookRotation((Input.mousePosition - selfScreenPoint)* Mathf.Deg2Rad);
//				_rotate.x = _rotate.z = 0;
//				_char.transform.rotation = _rotate;
//			}



			//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//RaycastHit hit = new RaycastHit();
			
//			if(Physics.Raycast(ray,out hit)){
//
//				string selected = hit.collider.gameObject.name;
//				Quaternion _rotate = Quaternion.LookRotation((Input.mousePosition - selfScreenPoint)* Mathf.Deg2Rad);
//				_rotate.x = _rotate.z = 0;
//				_char.transform.rotation = _rotate;
//			}

			//Quaternion _rotate = Quaternion.LookRotation((Input.mousePosition - selfScreenPoint));

			//_char.transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));

			//_rotate.x = _rotate.z = 0;
			//_char.transform.rotation = _rotate;
			//_char.transform.rotation = Quaternion.Slerp(_char.transform.rotation,_rotate,Time.deltaTime * 1f);

			//Debug.Log(_char.transform.rotation.y * Mathf.Rad2Deg);

			//Debug.Log(Input.mousePosition + "mouse pos");
			//Debug.Log(_char.transform.rotation + "rotate");

//			float angle = Mathf.Atan2(diff.y,diff.x) * Mathf.Rad2Deg;
//			float final_angle = angle -90f;

//			Quaternion _tmp_rotation = Quaternion.LookRotation(diff);
//			_char.transform.rotation = _tmp_rotation;

//			Quaternion rotate = Quaternion.LookRotation(target.transform.position - transform.position);
//			rotate.x = rotate.z = 0;
//			transform.rotation = rotate;




//			Vector3 selfScreenPoint = Camera.main.WorldToScreenPoint(_char.transform.position);
//			Vector3 diff =  Input.mousePosition - selfScreenPoint;
//			float angle = Mathf.Atan2(diff.y,diff.x) * Mathf.Rad2Deg;
//			float final_angle = angle -90f;
//			Debug.Log(_char.transform.rotation + "rotation");
//			Debug.Log(final_angle + "final_angle");

		

		}

	}

	private bool _checkClockwise(float current, float target)
	{
		return target>current ? !( target  - current > 180f)
			:    current - target  > 180f;
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


