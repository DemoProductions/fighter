using UnityEngine;
using System.Collections;

public class MenuBgMusic : MonoBehaviour 
{
	private static MenuBgMusic _instance;
	
	public static MenuBgMusic instance

	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<MenuBgMusic>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}

	AudioSource bg;

	void Start() {
		bg = this.gameObject.GetComponent<AudioSource> ();
	}
	
	void Awake() 
	{
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(this.gameObject);
		}
	}
	
	public void Play()
	{
		bg.Play();
	}
}