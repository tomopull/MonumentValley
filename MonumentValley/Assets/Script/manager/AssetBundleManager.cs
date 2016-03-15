using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetBundleManager : MonoBehaviour {

	[SerializeField]
	private string _base_bundle_url = "/AssetBundles";
	public string Base_bundle_url
	{
	    get { return this._base_bundle_url; } 
	    set { this._base_bundle_url = value; }
	}

	private AssetBundle assetBundleCache;


	public IEnumerator LoadAssetBundleCoroutine(){
		string _url = Util.GetBaseURL () + Base_bundle_url +"/objectbundle";
		Debug.Log (_url);

		WWW www = WWW.LoadFromCacheOrDownload (_url, 1);

		while (!www.isDone) {
			yield return null;
		}

		//TODO error handling

		//Asset bundle cache
		assetBundleCache = www.assetBundle;

		//dispose request
		www.Dispose ();
	}

	public Sprite GetSpriteFromAssetBundle(string _asset_name){
		try {

			return assetBundleCache.LoadAsset<Sprite>(string.Format("{0}.png",_asset_name));

		} catch (System.Exception ex) {

			Debug.Log (ex.ToString());
			return null;

		}

	}


	private static AssetBundleManager instance = null;

	public static AssetBundleManager Instance {
		get {	
			return AssetBundleManager.instance;	
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
