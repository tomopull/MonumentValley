using UnityEngine;
using System.Collections;

public class ObjectData : MonoBehaviour{


	[SerializeField]
	private int _point;

	//カテゴリーヌル
	public static int NullCategory = 99;

	//date state

	public static string Idle = "idle";
	public static string SELECTED = "selected";


	//取得ポイント
	public int Point
	{
	    get { return this._point; } 
	    set { this._point = value; }
	}

	//種類
	[SerializeField]
	private int _category = ObjectData.NullCategory;
	public int Category
	{
		get { return this._category; } 
		set { this._category = value; }
	}

	//自身のGameObject
	[SerializeField]
	private GameObject obj;
	public GameObject Obj
	{
	    get { return this.obj; } 
	    set { this.obj = value; }
	}

	//データの状態
	[SerializeField]
	private string _data_state = ObjectData.Idle;
	public string DataState
	{
	    get { return this._data_state; } 
	    set { this._data_state = value; }
	}

	//データが格納される連想配列のkey
	[SerializeField]
	private string key;
	public string Key
	{
	    get { return this.key; } 
	    set { this.key = value; }
	}


}
