using UnityEngine;

namespace RitikUtils
{
    public class MPersistentSingleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance;
        public virtual void Awake()
        {
            if(Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}