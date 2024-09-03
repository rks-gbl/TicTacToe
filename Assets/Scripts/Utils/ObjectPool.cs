using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RitikUtils
{
    public abstract class ObjectPool<T> : ScriptableObject
    {
        [NonSerialized] protected List<T> objects = new();
        public abstract T GetObject();
        protected abstract bool TryPickExistingObject(out T obj);
        protected abstract T CreateNewObject();
        public abstract void SetParent(T obj);
        public abstract void ResetAll();
    }
}
