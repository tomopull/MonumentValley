using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleManager : MonoBehaviour {

	private static ParticleManager instance = null;

	public GameModel _game_model;

	public static ParticleManager Instance {
		get {	
			return ParticleManager.instance;	
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


	/// <summary>
	/// 再生終了して不要になった
	/// パーティクルの削除
	/// </summary>
	public void RemoveParticleData(){

		if (_game_model.ParticleDataList != null && _game_model != null) {
		
			for (int i = 0; i < _game_model.ParticleDataList.Count; i++) {

				List<GameObject> _child_list = _game_model.ParticleDataList [i];

				for (int j = 0; j < _child_list.Count; j++) {

					GameObject _child_obj = _child_list [j];

					ParticleSystem _root_particle = _child_obj.GetComponent<ParticleSystem> ();

				if (!_root_particle.IsAlive ()) {
						_root_particle.Clear ();
						_game_model.ParticleDataList [i].Remove (_root_particle.gameObject);
						Destroy (_root_particle.gameObject);
					}

				}

			}

		}

	}
				
		
	public void InitParticleList(List<GameObject> _list, GameModel _model){
		_model.ParticleDataList.Add (_list);
	}


	public void AddParticleList(GameObject _obj,List<GameObject> _object_list){
		_object_list.Add (_obj);

	}




		
}
