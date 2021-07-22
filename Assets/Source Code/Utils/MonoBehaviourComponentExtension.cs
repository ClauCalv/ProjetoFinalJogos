using System;
using UnityEngine;

public static class MonoBehaviourComponentExtension
{
    public static T AddComponentWithSetup<T>(this GameObject obj, Action<T> onInit) where T : Component
    {
        bool oldState = obj.activeSelf;
        obj.SetActive(false);
        T comp = obj.AddComponent<T>();
        onInit?.Invoke(comp);
        obj.SetActive(oldState);
        return comp;
    }
}