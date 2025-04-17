using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    Debug.Log("instance " + typeof(T).Name + "null");
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        Time.timeScale = 1f;
        if (_instance == null) _instance = this as T;
        else Destroy(gameObject);
    }
}
