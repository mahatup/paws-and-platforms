using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFactory
{
    private ViewPrefabConfig _config;
    private Transform _parent;

    public ViewFactory(Transform parent)
    {
        _parent = parent;
    }

    public T CreateView<T>(T prefab) where T: MonoBehaviour
    {
        return Object.Instantiate(prefab, _parent);
    }
}
