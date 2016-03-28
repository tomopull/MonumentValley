using UnityEngine;
using System.Collections;

public class CameraData : MonoBehaviour {
	
	public  GameObject  target;
	public  Vector3     startPosition       =   new Vector3(0, 1, 0);
	
	public  float       zoom                =   1f;
	public  float       stdDistance         =   40f;
	public  float       minZoom             =   .55f;
	public  float       maxZoom             =   .55f;
	public  float       rotation            =   -90f;
	public  float       panBuffer           =   .1f;
	private int         _panBufferLeft;
	private int         _panBufferRight;
	private int         _panBufferBottom;
	private int         _panBufferTop;
	public  int         panDistance         =   5;
	public  float       panSpeed            =   5f;
	//public  float       panSpeed            =   0f;
		
	private float       heightRayLength     =   100f;
	
	public  float       xDistance {
		get {
			return Mathf.Cos(Mathf.Deg2Rad * rotation) * zoom * stdDistance;
		}
	}
	
	public  float       yDistance {
		get {
			return zoom * stdDistance;
		}
	}
	
	public  float       zDistance {
		get {
			return Mathf.Sin(Mathf.Deg2Rad * rotation) * zoom * stdDistance;
		}
	}
	
	void Start () {
		storeTarget();
		
		_panBufferLeft = Mathf.RoundToInt(Screen.width * panBuffer);
		_panBufferRight = Mathf.RoundToInt(Screen.width - (Screen.width * panBuffer));
		_panBufferBottom = Mathf.RoundToInt(Screen.height * panBuffer);
		_panBufferTop = Mathf.RoundToInt(Screen.height - (Screen.height * panBuffer));
	}
	
	public void storeTarget () {
		//If we have no target (not a child of anything), create the target and make the camera its child
		if(transform.parent == null) {
			target = new GameObject();
			target.name = "Camera Target";
			target.transform.position = startPosition;
			transform.parent = target.transform;
		} else {
			target = transform.parent.gameObject;
		}
	}
	
	public bool shouldMoveLeft () {
		return Input.GetAxis("Horizontal") < 0 || Input.mousePosition.x < _panBufferLeft;
	}
	
	public bool shouldMoveRight () {
		return Input.GetAxis("Horizontal") > 0 || Input.mousePosition.x > _panBufferRight;
	}
	
	public bool shouldMoveBack () {
		return Input.GetAxis("Vertical") < 0 || Input.mousePosition.y < _panBufferBottom;
	}
	
	public bool shouldMoveForward () {
		return Input.GetAxis("Vertical") > 0 || Input.mousePosition.y > _panBufferTop;
	}
	
	public bool shouldRotateLeft () {
		return Input.GetAxis("Fire2") > 0;
	}
	
	public bool shouldRotateRight () {
		return Input.GetAxis("Fire1") > 0;
	}
	
	public float GetGroundHeight () {
		RaycastHit hit;
		int layerMask = 1 << 8; //bit shift the index of the 8th layer to get its bitmask so only terrain is considered the ground
		
		if(Physics.Raycast(new Ray(target.transform.position, Vector3.down), out hit, heightRayLength, layerMask)) {
			return hit.point.y + 1;
		}
		else if(Physics.Raycast(new Ray(target.transform.position, Vector3.up), out hit, heightRayLength, layerMask)) {
			return hit.point.y + 1;
		}
		
		//No hit? What the hell happened?! Throw an exception!
		throw new UnityException("Camera could not find any ground beneath it.");
	}
	
}