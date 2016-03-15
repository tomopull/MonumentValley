
using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using LitJson;



static public class Util  {

//	public struct ComponentType{
//		public string TEXT = "text_componet";
//		public string IMAGE = "image_component";
//	}

	static public void Shuffle (int[] deck) {
		for (int i = 0; i < deck.Length; i++) {
			int temp = deck[i];
			int randomIndex = UnityEngine.Random.Range(0, deck.Length);
			deck[i] = deck[randomIndex];
			deck[randomIndex] = temp;
		}
	}

	static public void Shuffle (JsonData data ) {
		for (int i = 0; i < data["object_data"].Count; i++) {
			JsonData temp = data["object_data"][i];
			int randomIndex = UnityEngine.Random.Range(0, data["object_data"].Count);
			data["object_data"][i] = data["object_data"][randomIndex];
			data["object_data"][randomIndex] = temp;
		}
	}


	//読み込み外部ファイルのベースのurlの決定
	static public string GetBaseURL(){

		string base_url = "";

		if (Application.platform == RuntimePlatform.OSXEditor) {
			//OSX Editor
			base_url = "file://" + Application.dataPath + "/StreamingAssets";

		} else if (Application.platform == RuntimePlatform.OSXPlayer) {
			//PC Mac & linux StandAlone
			base_url = "file://" + Application.dataPath + "/StreamingAssets";

		}else if(Application.platform == RuntimePlatform.IPhonePlayer){
			//Iphone
			base_url = "file://" + Application.dataPath + "/Raw";

		} else if(Application.platform == RuntimePlatform.OSXWebPlayer){
			//Web Player
			//絶対パス
			base_url = Application.dataPath;
		}

		return base_url;
	}
		
	/// <summary>
	/// リソースのprefabから複製
	/// </summary>
	static public GameObject InstantiateUtil(GameModel _game_model, string resource_path,Vector3 default_position,Quaternion default_quaernion){
		GameObject obj = (GameObject)GameObject.Instantiate(Resources.Load(_game_model.PrefabResourcePath + resource_path),default_position ,default_quaernion);
		//自動的につく(cloneの文字を除去処理 名称をresource_pathのみにする)
		obj.name = resource_path;
		return obj;
	}


	/// <summary>
	/// テキストコンポネーントを取得
	/// </summary>
	/// <returns>The text component.</returns>
	/// <param name="str">String.</param>
	static public Text FindTextComponentUtil(string str){
		Text return_str = GameObject.Find (str).GetComponent<Text> ();
		return return_str;
	}

	/// <summary>
	/// get text component and set the component's string to selected new text
	/// </summary>
	/// <returns>The text to text component util.</returns>
	/// <param name="_str">_str.</param>
	/// <param name="_new_text">_new_text.</param>
	static public Text SetTextToTextComponentUtil(string _str,Text _new_text){
		_new_text = GameObject.Find (_str).GetComponent<Text> ();
		return _new_text;
	}

	/// <summary>
	/// テキストのストリングを更新
	/// </summary>
	/// <returns>The text string util.</returns>
	static public string UpdateTextStringUtil(Text _text, string _string){
		_text.text = _string;
		return _string;
	}

	/// <summary>
	/// ゲームオブジェクトを取得
	/// </summary>
	/// <returns>The game object util.</returns>
	/// <param name="str">String.</param>
	static public GameObject FindGameObjectUtil(string str){
		GameObject obj = (GameObject)GameObject.Find (str);
		return obj;
	}

		
	/// <summary>
	/// ボタンコンポーネントの
	/// </summary>
	/// <returns>The button component util.</returns>
	static public Button FindButtonComponentUtil(string str){
		Button btn = GameObject.Find (str).GetComponent<Button> ();
		return btn;
	}
		
	/// <summary>
	/// Game Objectの有効化、無効化
	/// </summary>
	/// <param name="">.</param>
	static public void SetActivationOfGameObject(GameObject _game_object, bool _bool){
		_game_object.SetActive (_bool);
	}

	/// <summary>
	/// ボタンイベントの設定
	/// _obj:GameObject
	/// _unity_action:UIEventHandler.Instance.OnPointerClick
	/// _event_trigger_typ:EventTriggerType.PointerClick;
	/// </summary>
	/// <param name="_button">_button.</param>
	/// <param name="_event">_event.</param>
	static public void SetButtonEvent(GameObject _obj, UnityAction<BaseEventData> _action, EventTriggerType _event_trigger_type){

		//既にEventTriggerComponentが追加されボタン化されていなかったらボタン化
		if (!_obj.GetComponent<EventTrigger> ()) {
		
			//Debug.Log (_obj);
			EventTrigger _event_triger = _obj.AddComponent<EventTrigger> ();

			EventTrigger.Entry _entry = new EventTrigger.Entry ();

			_entry.callback.AddListener (_action);

			_entry.eventID = _event_trigger_type;

			_event_triger.triggers = new List<EventTrigger.Entry> ();
			_event_triger.triggers.Add (_entry);

			//_event_triger.triggers = UIEventHandler.Instance.EntryList;
			//UIEventHandler.Instance.EntryDict.Add(_obj.name,_entry);
			//_event_triger.triggers = new List<EventTrigger.Entry> ();
			//_event_triger.triggers.Add (_entry);
			//Debug.Log (_event_triger.triggers.Count);
			//Debug.Log (_event_triger.triggers.Count);
		}

	}

	/// <summary>
	/// remove button event
	/// </summary>
	/// <param name="_obj">_obj.</param>
	/// <param name="_action">_action.</param>
	/// <param name="_event_trigger_type">_event_trigger_type.</param>
	static public void RemoveButtonEvent(GameObject _obj, EventTrigger.Entry _entry, UnityAction<BaseEventData> _action){

		//既にEventTriggerComponentが追加されボタン化されていたらボタン無効化
		if (_obj.GetComponent<EventTrigger> ()) {

			EventTrigger _event_triger = _obj.GetComponent<EventTrigger> ();
			_entry.callback.RemoveListener (_action);
				
		}

	}


	static public void AnimateObjectScale(GameObject _obj, float _target_scale_x, float _target_scale_y, iTween.EaseType _ease_type, float _time){
	
		if (_obj != null) {
		
			//一番最後に洗濯しているオブジェクトをハイライト
			iTween.ScaleTo (_obj,iTween.Hash(
				"x", _target_scale_x,
				"y", _target_scale_y,
				"easeType", _ease_type,
				"time", _time
			));

		}
			
	}


}
