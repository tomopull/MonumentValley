using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;

public class GameModel : MonoBehaviour {

	//score key
	public const string SCORE_KEY =  "score_key";

	//high_score_key
	public const string  HIGH_SCORE_KEY = "high_score_key";


	//prefab resource path
	[SerializeField]
	private string prefab_resource_path = "Prefabs/";
	public string PrefabResourcePath
	{
	    get { return this.prefab_resource_path; } 
	    set { this.prefab_resource_path = value; }
	}

	//シンプルタッチストラクト
	public struct GameState{
		public string GAME_START_STATE;
		public string GAME_PLAY_STATE;
		public string GAME_END_STATE;
		public string GAME_IDLE_STATE;
	}

	[SerializeField]
	private string now_state;
	public string NowState
	{
	    get { return this.now_state; } 
	    set { this.now_state = value; }
	}

	private static ulong _uniqe_index = 0;
	
			//ゲームタイム
	[SerializeField]
	private float game_time = 45.0f;
	public float GameTime
	{
	    get { return this.game_time; } 
	    set { this.game_time = value; }
	}

	//オリジナルjson data
	[SerializeField]
	private JsonData original_json_data;
	public JsonData OriginalJsonData
	{
		get { return this.original_json_data; } 
		set { this.original_json_data = value; }
	}

	//行数
	[SerializeField]
	private int rowCount = 0;
	public int RowCount
	{
		get { return this.rowCount; } 
		set { this.rowCount = value; }
	}

	//列数
	[SerializeField]
	private int columnCount = 0;
	public int ColumnCount
	{
		get { return this.columnCount; } 
		set { this.columnCount = value; }
	}

	//ゲームオブジェクトデータ
	[SerializeField]
	private Dictionary<string,DataObject> object_data_dict;
	public Dictionary<string,DataObject> ObjectDataDict
	{
		get { return this.object_data_dict;} 
		set { this.object_data_dict = value;}
	}


	//今インタラクティブかどうか
	[SerializeField]
	private bool isInteractive = false;
	public bool IsInteractive
	{
		get { return this.isInteractive; } 
		set { this.isInteractive = value; }
	}

	//ネイティブデバイスタッチ
	[SerializeField]
	private Touch deviceTouch;
	public Touch DeviceTouch
	{
		get { return this.deviceTouch; } 
		set { this.deviceTouch = value; }
	}

	//スワイプの距離
	[SerializeField]
	private float swipeTime;
	public float SwipeTime
	{
		get { return this.swipeTime; } 
		set { this.swipeTime = value; }
	}

	//スワイプ時間
	[SerializeField]
	private float swipeDistance;
	public float SwipeDistance
	{
		get { return this.swipeDistance; } 
		set { this.swipeDistance = value; }
	}

	//今ボタンが押された状態かどうか
	[SerializeField]
	private bool isButtonDown = false;
	public bool IsButtonDown
	{
		get { return this.isButtonDown; } 
		set { this.isButtonDown = value; }
	}
	

	[SerializeField]
	private List<Block> _block_list;
	public List<Block> BlockList
	{
	    get { return this._block_list; } 
	    set { this._block_list = value; }
	}

	//Base Tile data
	[SerializeField]
	private List<BlockDataObject> _base_block_list;
	public List<BlockDataObject> BaseBlockList
	{
		get { return this._base_block_list; } 
		set { this._base_block_list = value; }
	}

	//パーティクルのデータの管理
	[SerializeField]
	private List<List<GameObject>>_particle_data_list;
	public List<List<GameObject>> ParticleDataList
	{
	    get { return this._particle_data_list; } 
	    set { this._particle_data_list = value; }
	}
		
	//オブジェクトが消えるパーティクルのデータ
	[SerializeField]
	private List<GameObject> vanish_particle_list;
	public List<GameObject> VanishParticleList
	{
		get { return this.vanish_particle_list; } 
		set { this.vanish_particle_list = value; }
	}

	//オブジェクトが加点されるパーティクルのデータ
	[SerializeField]
	private List<GameObject> get_point_particle_list;
	public List<GameObject> GetPointParticleList
	{
		get { return this.get_point_particle_list; } 
		set { this.get_point_particle_list = value; }
	}


	//ユニークな整数の取得
	public static ulong GetUniqueIndex(){
		_uniqe_index += 1;
		//print (_uniqe_index);
		return _uniqe_index;
	}


	//シンプルタッチストラクト
	public struct SimpleTouch{
		public Vector2 StartTouchLocation;
		public Vector2 CurrentTouchLocation;
		public DateTime StartTime;
		public TouchPhase Phase;
	}
		
	//ローカルのjsonのパス
	[SerializeField]
	private string json_path;
	public string Json_Path {

		get { 
			json_path = Util.GetBaseURL() + "/Json/data.json";
			return json_path;
		}
			
	}

	private static GameModel instance = null;
	public static GameModel Instance {
		get {	
				return GameModel.instance;	
			}
	}
		
	//初期化
	public void Init(){
		_uniqe_index = 0;
	}

	void Awake()
	{
		if( instance == null)

		{

			instance = this;

		}else{

			Destroy( this );

		}


		DontDestroyOnLoad(this); // シーン読み込みの際に破棄されなくなる

	}
		
}
	