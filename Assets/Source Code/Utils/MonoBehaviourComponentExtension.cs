using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MonoBehaviourComponentExtension
{
    //soluções copiadas de alguns stackOverflows que achei muito interessantes. não sei se chegarão a ser utilizadas
    public static T AddComponentWithSetup<T>(this GameObject obj, Action<T> onInit) where T : Component
    {
        bool oldState = obj.activeSelf;
        obj.SetActive(false);
        T comp = obj.AddComponent<T>();
        onInit?.Invoke(comp);
        obj.SetActive(oldState);
        return comp;
    }

    public static T GetInterface<T>(this GameObject obj) where T : class
    {
        return obj.GetComponents<Component>().OfType<T>().FirstOrDefault();
    }

    public static IEnumerable<T> GetInterfaces<T>(this GameObject obj) where T : class
    {
        return obj.GetComponents<Component>().OfType<T>();
    }

    public static bool ContainsLayer(this LayerMask mask, int layer)
    {
        return (mask.value & 1 << layer) == 1 << layer;
    }
}