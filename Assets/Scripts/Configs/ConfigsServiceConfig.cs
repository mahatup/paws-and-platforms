using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ConfigsServiceConfig),
    menuName = "Configs/Infrastructure/ConfigsServiceConfig")]
public class ConfigsServiceConfig : ScriptableObject
{
    [field: SerializeField] public ScriptableObject[] Configs {  get; private set; }

}
