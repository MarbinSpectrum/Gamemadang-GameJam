using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj;
                obj = GameObject.Find(typeof(T).Name);
                instance = obj.GetComponent<T>();
            }
        
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance != null && instance.gameObject != this)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}