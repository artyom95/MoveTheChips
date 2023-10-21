using System;
using System.Collections.Generic;
using NewScripts;
using UnityEngine;


   [Serializable]
   public class GameSettings 
   {
      [field:SerializeField]
      public List<ScriptableSettings> ScriptableSettings { get; private set; }
   }

