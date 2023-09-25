using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class SingletonObj<T> : MonoBehaviour where T : SingletonObj<T>
{
	#region Fields
	private static T instance = null;
	#endregion

	#region Properties
	public static T Instance
	{
		get
		{
			return instance;
		}
	}
	#endregion

	#region Methods
	protected virtual void OnEnable()
	{
		//instance = (T)this;
	}
	protected virtual void OnDisable()
	{
		//instance = null;
	}
	protected virtual void OnApplicationQuit()
	{
		instance = null;
	}
	public void Reload()
	{
		OnEnable();
	}
	public void Awake()
	{
		if(instance)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = (T)this;
			DontDestroyOnLoad(gameObject);
		}
	}
	#endregion
}