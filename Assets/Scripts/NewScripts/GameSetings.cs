using System;
using System.Collections.Generic;
using UnityEngine;

namespace NewScripts
{
    [Serializable]
    public class GameSetings : MonoBehaviour

    {
        [field: SerializeField] public List<ScriptableSettings> ScriptableSettings { get; private set; }
    }
}