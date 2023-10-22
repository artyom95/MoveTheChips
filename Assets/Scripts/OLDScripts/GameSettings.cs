using System.Collections.Generic;
using UnityEngine;

namespace OLDScripts
{
   public class GameSettings 
   {
      [field:SerializeField]
      public List<ScriptableSettings> ScriptableSettings { get; private set; }
   }
}

