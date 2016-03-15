using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectManager : MonoBehaviour {

	private static GameObjectManager instance = null;
	public GameModel _game_model;

	public static GameObjectManager Instance {
		get {	
			return GameObjectManager.instance;	
		}
	}
		
	//カラーチェンジ
	public void SetColor(int _category_id,GameObject obj){

		if (obj != null) {


			//カラーチェンジ
			switch (_category_id) {

			case 1:
				obj.GetComponent<Renderer> ().material.color = Color.blue;
				break;
			case 2:
				obj.GetComponent<Renderer> ().material.color = Color.red;
				break;
			case 3:
				obj.GetComponent<Renderer> ().material.color = Color.yellow;
				break;
			case 4:
				obj.GetComponent<Renderer> ().material.color = Color.green;
				break;
			case 5:
				obj.GetComponent<Renderer> ().material.color = Color.cyan;
				break;
			}

		}
			
	}
		

		
	//ライン描画のリセット
	public void ResetLineObjectData(){
		_game_model.SelectedObjectDataDict = new Dictionary<string,ObjectData>();
		_game_model.FirstObjectSelectedCategory = ObjectData.NullCategory;
		_game_model.LastObjectSelected = null;
	}

	//選択中のオブジェクトをハイライトする
	public void HighLightSelectedData(Dictionary<string,ObjectData> _selected_dict){

		int i = 0;

		foreach (ObjectData value in _selected_dict.Values) {

			GameObject obj = (GameObject)value.Obj;

			if (obj != null) {

				obj.GetComponent<Renderer> ().material.color = Color.white;

				i += 1;


				if (i >= _selected_dict.Count) {

					//ハイライト
					Util.AnimateObjectScale (obj,1.25f,1.25f,iTween.EaseType.easeInExpo,0.1f);

				} else {

					//ハイライト解除
					Util.AnimateObjectScale(obj,1f,1f,iTween.EaseType.linear,0f);

				}

			}

		}
					
	}

	//選択中のオブジェクトをハイライトリセット元の色に戻す
	public void ResetHighLightSelectedData(Dictionary<string, ObjectData> _selected_dict){

		int i = 0;

		foreach (ObjectData value in _selected_dict.Values) {

			GameObject obj = (GameObject)value.Obj;

			SetColor (value.Category, obj);
			i += 1;

			//オブジェクトをハイライト解除
			Util.AnimateObjectScale(obj,1f,1f,iTween.EaseType.linear,0f);


		}

	}
		
	//マウスをドラッグ中なら近くのオブジェクト同時をつなぐ線の描画処理を継続
	public void DrawLine(Dictionary<string,ObjectData> _selected_dict){
	
		LineRenderer line = GameObject.Find ("LineContainer").GetComponent<LineRenderer>();
		line.sortingOrder = 10;
		line.SetWidth (0.1f, 0.1f);
		line.SetVertexCount (_selected_dict.Count);

		//ラインの頂点数のカウント
		int i = 0;
		//順番に配列が代入されると仮定。
		foreach (ObjectData value  in _selected_dict.Values) {

			if (value.Obj != null) {
				line.SetPosition (i,value.Obj.transform.position);
				i += 1;
			}
				
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














//	//	private GameObject line_particle_obj;
////	private ParticleSystem line_particle;
//
//	//マウスをドラッグ中なら近くのオブジェクト同時をつなぐ線の描画処理を継続
//	public void DrawLine(Dictionary<string,ObjectData> _selected_dict){
//
////		if (line_particle == null) {
////			line_particle_obj = (GameObject)Instantiate(Resources.Load("Prefabs/ParticleLine"),new Vector3(),Quaternion.identity);
////			line_particle = line_particle_obj.GetComponent<ParticleSystem> ();
////			line_particle.Stop ();
////		}
//
//		LineRenderer line = GameObject.Find ("LineContainer").GetComponent<LineRenderer>();
//		line.sortingOrder = 10;
//		line.SetWidth (0.1f, 0.1f);
//		line.SetVertexCount (_selected_dict.Count);
//
//		//ラインの頂点数のカウント
//		int i = 0;
//		//順番に配列が代入されると仮定。
//		foreach (ObjectData value  in _selected_dict.Values) {
//			line.SetPosition (i,value.Obj.transform.position);
//			i += 1;
//		}
//
//	}

