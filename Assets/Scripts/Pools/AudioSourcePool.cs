using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RitikUtils
{
    [CreateAssetMenu(fileName = "Audio Source Pool" , menuName = "Scriptable/AudioSourcePool")]
    public class AudioSourcePool : ObjectPool<AudioSource>
    {
        [SerializeField] protected GameObject prefab;
        public Transform ParentInScene {get; set;}
        protected override AudioSource CreateNewObject()
        {
            AudioSource ASource = Instantiate(prefab , ParentInScene, true).GetComponent<AudioSource>();
            objects.Add(ASource);
            SetParent(ASource);
            return ASource;
        }

        public override AudioSource GetObject()
        {
            AudioSource ASource = TryPickExistingObject(out AudioSource a) ? a : CreateNewObject();
            ASource.gameObject.SetActive(true);
            return ASource;
        }

        public override void ResetAll()
        {
            foreach(AudioSource ASource in objects)
            {
                ASource.Stop();
                ASource.clip = null;
                ASource.gameObject.SetActive(false);
            }
        }

        public override void SetParent(AudioSource ASource)
        {
            if(ParentInScene != null){
                ASource.transform.parent = ParentInScene;
            }
            else 
            {
                Transform T = new GameObject($"{ASource.name}s Pool").transform;
                ParentInScene = T;
                ASource.transform.parent = T;
            }
        }

        protected override bool TryPickExistingObject(out AudioSource ASource)
        {
            foreach(var c in objects)
            {
                if(c.isPlaying) continue;
                ASource = c;
                return true;
            }

            ASource = null;
            return false;
        }
    }
}