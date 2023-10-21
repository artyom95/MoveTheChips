using System;
using System.Collections.Generic;
using UnityEngine;

namespace NewScripts
{
   [Serializable]
   public class GameSettings: MonoBehaviour

   {
   [field: SerializeField] public List<ScriptableSettings> ScriptableSettings { get; private set; }
   }
}
