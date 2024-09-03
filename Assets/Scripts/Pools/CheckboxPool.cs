using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RitikUtils
{
    [CreateAssetMenu(fileName = "Checkbox Pool" , menuName = "Scriptable/CheckboxPool")]
    public class CheckboxPool : ObjectPool<CheckBox>
    {
        [SerializeField] protected GameObject prefab;
        public Transform ParentInScene {get; set;}
        protected override CheckBox CreateNewObject()
        {
            CheckBox cBox = Instantiate(prefab , ParentInScene, true).GetComponent<CheckBox>();
            objects.Add(cBox);
            SetParent(cBox);
            return cBox;
        }

        public override CheckBox GetObject()
        {
            CheckBox cBox = TryPickExistingObject(out CheckBox c) ? c : CreateNewObject();
            cBox.gameObject.SetActive(true);
            return cBox;
        }

        public override void ResetAll()
        {
            foreach(CheckBox box in objects)
            {
                box.Reset();
            }
        }

        public override void SetParent(CheckBox cBox)
        {
            if(ParentInScene != null){
                cBox.transform.parent = ParentInScene;
            }
            else 
            {
                Transform T = new GameObject($"{cBox.name}s Pool").transform;
                ParentInScene = T;
                cBox.transform.parent = T;
            }
        }

        protected override bool TryPickExistingObject(out CheckBox cBox)
        {
            foreach(var c in objects)
            {
                if(c.gameObject.activeSelf) continue;
                cBox = c;
                return true;
            }

            cBox = null;
            return false;
        }
    }
}