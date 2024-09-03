using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RitikUtils
{
    [CreateAssetMenu(fileName = "GameObject Pool" , menuName = "Scriptable/GameObjectPool")]
    public class GameObjectPool : ObjectPool<GameObject>
    {
        [SerializeField] protected GameObject prefab;
        protected Transform ParentInScene {get; set;}
        protected override GameObject CreateNewObject()
        {
            GameObject obj = Instantiate(prefab , ParentInScene, true);
            objects.Add(obj);
            SetParent(obj);
            return obj;
        }

        public override GameObject GetObject()
        {
            GameObject obj = TryPickExistingObject(out GameObject c) ? c : CreateNewObject();
            obj.gameObject.SetActive(true);
            return obj;
        }

        public override void ResetAll()
        {
            foreach(GameObject obj in objects)
            {
                obj.SetActive(false);
            }
        }

        public override void SetParent(GameObject obj)
        {
            if(ParentInScene != null){
                obj.transform.parent = ParentInScene;
            }
            else 
            {
                Transform T = new GameObject($"{obj.name}s Pool").transform;
                ParentInScene = T;
                obj.transform.parent = T;
            }
        }

        protected override bool TryPickExistingObject(out GameObject obj)
        {
            foreach(var c in objects)
            {
                if(c.gameObject.activeSelf) continue;
                obj = c;
                return true;
            }

            obj = null;
            return false;
        }
    }
}