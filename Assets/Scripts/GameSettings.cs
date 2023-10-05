using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class GameSettings 
{
   [field:SerializeField]
   public List<ScriptableSettings> ScriptableSettings { get; private set; }
}
