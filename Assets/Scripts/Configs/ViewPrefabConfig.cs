using Assets.Scripts.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//TODO: сделать базовую вьюшку
[CreateAssetMenu(fileName = nameof(ViewPrefabConfig),
    menuName = "Configs/UI/ViewPrefabConfig")]
public class ViewPrefabConfig : ScriptableObject
{
    [SerializeField] private List<BaseView> _prefabs;

    public T GetPrefab<T>() where T : BaseView
    {
        return (T)_prefabs.FirstOrDefault(prefab => prefab.GetType() == typeof(T));
    }
}
