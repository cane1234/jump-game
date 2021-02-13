using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    #region Properties
    public static T Instance
    {
        get { return instance; }
    }

    public bool IsInstantiated
    {
        get { return instance != null; }
    }

    #endregion

    #region Unity Methods
    protected void Awake()
    {
        if (instance == null)
        {
            instance = (T)this;
        }
        else
        {
            Debug.LogError("Can't have two instances of a singleton.");
        }
    }

    void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }

    #endregion
}