using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using LitJson;

public class MainScene : MonoBehaviour {

	//ゲームのデータ管理
	private GameModel _game_model;

	//ゲームオブジェクト管理
	public GameObjectManager _game_object_manager;

	//キャンバスオブジェクト管理
	public CanvasObjectManager _canvas_object_manager;

	//パーティクル管理
	public ParticleManager _particle_manager;

	//ユーザーインプット管理
	private GameModel.SimpleTouch ActiveTouch;

	//state  set default state
	private GameModel.GameState _game_state;

	//Asset Bundle manager
	private AssetBundleManager _asset_bundle_manager;

	private StageManager _stage_manager;

	//UI Event Handler
	private UIEventHandler _ui_event_handler;
	
	private Canvas _canvas_game_info;
	
	//Hero
	private GameObject _hero;

	private Timer _timer;

	private bool time_up = false;

	Ray ray;
	RaycastHit hit;

	private NavMeshAgent _agent;

	private Animator _animator;


	// Use this for initialization
	void Start () {
		//init all managers
		InitManager ();
		Init ();
		//PlayerPrefs.DeleteAll ();
	}

	//スワイプかタッチか判別
	private void CaluculateTouchInput(GameModel.SimpleTouch CurrentTouch){
		Vector2 touchDirection  = (CurrentTouch.CurrentTouchLocation - CurrentTouch.StartTouchLocation).normalized;
		float touchDistance     = (CurrentTouch.StartTouchLocation - CurrentTouch.CurrentTouchLocation).magnitude;
		TimeSpan timeGap        = System.DateTime.Now - CurrentTouch.StartTime;
		double touchTimeSpan    = timeGap.TotalSeconds;
		string touchType        = ( touchDistance > _game_model.SwipeDistance && touchTimeSpan > _game_model.SwipeTime ) ? "Swipe" : "Tap";
	}

	//各マネージャー、モデル初期化
	private void InitManager(){

		_game_model = GameModel.Instance;
		_game_object_manager = GameObjectManager.Instance;
		_canvas_object_manager = CanvasObjectManager.Instance;
		_particle_manager = ParticleManager.Instance;
		_stage_manager = StageManager.Instance;
		_asset_bundle_manager = AssetBundleManager.Instance;

		_game_object_manager._game_model = _game_model;
		_canvas_object_manager._game_model = _game_model;
		_particle_manager._game_model = _game_model;
		_stage_manager._game_model = _game_model;

		_ui_event_handler = UIEventHandler.Instance;
		_ui_event_handler.EntryDict = new Dictionary<string, EventTrigger.Entry> ();
		_ui_event_handler.EntryList = new List<EventTrigger.Entry> ();

		_game_model.ParticleDataList = new List<List<GameObject>> ();
		_game_model.Init ();
	}

	//初期化
	private void Init(){
		LoadFile ();
	}

	//外部ファイルのロード
	private void LoadFile(){
		StartCoroutine("LoadFileCorutine",_game_model.Json_Path);
	}

	private IEnumerator LoadFileCorutine(string _file_path){

		WWW file = new WWW (_file_path);

		yield return file;

		JsonData data = LitJson.JsonMapper.ToObject(file.text);

		//ローカルにオリジナルjsonデータ保存
		_game_model.OriginalJsonData = data;

		//CreateStage
		_stage_manager.CreateStage();

		//InitCanvasInfo
		InitCanvasInfo();


		//InitHero
		InitCharacter();
	}

	/// <summary>
	/// Inits the character.
	/// </summary>
	private void InitCharacter(){
		_hero = Util.InstantiateUtil(_game_model,"Hero",new Vector3(_game_model.BaseBlockList[0].Obj.transform.position.x,-1,_game_model.BaseBlockList[0].Obj.transform.position.z),Quaternion.identity);
		_agent = _hero.GetComponent<NavMeshAgent>();
		_animator = GameObject.Find("Hero/SD_unitychan_humanoid").GetComponent<Animator>();
	}

	private  void InitCanvasInfo(){
	
		_canvas_game_info = GameObject.Find ("CanvasGameInfo").GetComponent<Canvas> ();

		_game_state.GAME_END_STATE = "game_end_state";
		_game_state.GAME_PLAY_STATE = "game_play_state";
		_game_state.GAME_START_STATE = "game_start_state";
		_game_state.GAME_IDLE_STATE = "game_idle_state";

		SetGameState (_game_state.GAME_PLAY_STATE);

	}

	/// <summary>
	/// init data
	/// </summary>
	private void ResetPlayerPref(BaseEventData _base_event_data){
		PlayerPrefs.DeleteAll ();
	}



	private void SetGameState(string str){
		_game_model.NowState = str;
	}
	

	// Update is called once per frame
	void Update () {

		if(Application.isEditor){
			if (Input.GetMouseButton (0)) {

				if (ActiveTouch.Phase == TouchPhase.Canceled) {

					ActiveTouch.CurrentTouchLocation = Input.mousePosition;
					ActiveTouch.StartTouchLocation = Input.mousePosition;
					ActiveTouch.StartTime = System.DateTime.Now;
					ActiveTouch.Phase = TouchPhase.Began;
					_game_model.IsButtonDown = true;

				} else {

					ActiveTouch.CurrentTouchLocation = Input.mousePosition;

				}

			} else {

				if (ActiveTouch.Phase == TouchPhase.Began) {

					CaluculateTouchInput (ActiveTouch);
					ActiveTouch.Phase = TouchPhase.Canceled;
					_game_model.IsButtonDown = false;

				}

			}

		}else{

			if (Input.touches.Length > 0) {

				_game_model.DeviceTouch = Input.GetTouch (0);

				if (ActiveTouch.Phase == TouchPhase.Canceled) {

					ActiveTouch.Phase = _game_model.DeviceTouch.phase;
					ActiveTouch.StartTime = System.DateTime.Now;
					ActiveTouch.StartTouchLocation = _game_model.DeviceTouch.position;
					ActiveTouch.CurrentTouchLocation = _game_model.DeviceTouch.position;
					_game_model.IsButtonDown = true;

				} else {

					ActiveTouch.CurrentTouchLocation = _game_model.DeviceTouch.position;

				}

			} else {

				if(ActiveTouch.Phase != TouchPhase.Canceled){
					CaluculateTouchInput (ActiveTouch);
					ActiveTouch.Phase = TouchPhase.Canceled;
					_game_model.IsButtonDown = false;
				}

			}
		}

		//ゲームプレイ時間中にボタンをダウンしていたら、していなかったら
		if (Input.GetMouseButtonDown(0) &&  _game_model.NowState == _game_state.GAME_PLAY_STATE) {
		
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			hit = new RaycastHit();
			
			// "Run"アニメーションに遷移
			if(Physics.Raycast(ray,out hit) ){
				_agent.SetDestination(hit.point);
				_animator.SetBool("is_running",true);
				//Debug.Log("アニメーションに遷移");
				Debug.Log(Vector3.Distance(hit.point, transform.position) );
			}

		}

		// 目的地とプレイヤーとの距離が1以下になったら、
		if(Vector3.Distance(hit.point,_hero.transform.position )  < 0.1f){
			// "Run"アニメーションから抜け出す
			_animator.SetBool("is_running",false);
			//Debug.Log("アニメーションから抜け出す");
		}
		
		//再生終了したパーティクルデータを削除
		if(_game_model.NowState == _game_state.GAME_PLAY_STATE){
			if(_particle_manager != null)_particle_manager.RemoveParticleData ();
		}

		_game_object_manager.SetRotationAngleByTargetPosition(_hero,Input.mousePosition);
		
	
	}




}

