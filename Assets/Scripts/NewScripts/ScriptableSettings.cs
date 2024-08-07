using System.Collections.Generic;
using UnityEngine;

namespace NewScripts
{
    [CreateAssetMenu(menuName = "ScriptableObjectSettings", fileName = "SceneSettings", order = 1)]
    public class ScriptableSettings : ScriptableObject
    {
        [field: SerializeField] public int AmountAttempts { get; private set; }

        [field: SerializeField] public int AmountColumnsForGraph { get; private set; }

        [field: SerializeField] public int AmountChips { get; private set; }

        [field: SerializeField] public List<Color> ColorsChips { get; private set; }

        [field: SerializeField] public int AmountPoints { get; private set; }

        [field: SerializeField] public List<Vector2> CoordinatesPoints { get; private set; }

        [field: SerializeField] public List<int> InitialPointLocation { get; private set; }

        [field: SerializeField] public List<int> FinishPointLocation { get; private set; }

        [field: SerializeField] public int NumberConnections { get; private set; }

        [field: SerializeField] public List<Vector2> ConnectionsBetweenPointPairs { get; private set; }
    }
}