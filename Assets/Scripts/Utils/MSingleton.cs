using UnityEngine;

namespace RitikUtils
{
    public class MSingleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    T[] allInstances = FindObjectsByType<T>(FindObjectsInactive.Exclude , FindObjectsSortMode.None);
                    if(allInstances.Length == 1){
                        instance = allInstances[0];
                    }
                    else if(allInstances.Length > 1)
                    {           
                        Debug.LogWarning($"MULTIPLE INSTANCES found for {typeof(T)} , setting the first one");
                        instance = allInstances[0];
                    }
                }
                if (instance == null) {
                    GameObject obj = new GameObject ();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    instance = obj.AddComponent<T> ();
                }
                return instance;
            }
        }
    }
}
