using UnityEngine;

using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class UIEventHandler : MonoBehaviour {

	private static UIEventHandler instance = null;

	//Entry Dict
	//どっち使うか検討中
	[SerializeField]
	private Dictionary<string, EventTrigger.Entry> _entry_dict;
	public Dictionary<string, EventTrigger.Entry> EntryDict
	{
		get { return this._entry_dict; } 
		set { this._entry_dict = value; }
	}

	//どっち使うか検討中
	//Entry List
	private List<EventTrigger.Entry> _entry_list;
	public List<EventTrigger.Entry> EntryList
	{
		get { return this._entry_list; } 
		set { this._entry_list = value; }
	}

	public static UIEventHandler Instance {
		get {	
			return UIEventHandler.instance;	
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