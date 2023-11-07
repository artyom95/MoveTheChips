using System.Collections.Generic;
using NewScripts.Chip;
using UnityEngine;

namespace NewScripts.Node
{
    public class NodeModel : MonoBehaviour, IHighlightable
    {
        public List<NodeModel> Neighbours { get; } = new();
        public ChipModelSettings ChipModel { get; private set; }
        public int ID { get; private set; }
        public Vector3 Position { get; private set; }

        [SerializeField] private OutlineController _outlineController;

        public void SetID(int id)
        {
            ID = id;
        }

        public void SetNeighbours(NodeModel nodeModel)
        {
            Neighbours.Add(nodeModel);
        }

        public void SetChipModel(ChipModelSettings chipModel)
        {
            ChipModel = chipModel;
        }

        public void ResetChipModel()
        {
            ChipModel = default;
        }

        public void SetPosition(Vector3 position)
        {
            Position = position;
        }

        public void TurnOnOutline()
        {
            _outlineController.SetFocus();
        }

        public void TurnOffOutline()
        {
            _outlineController.RemoveFocus();
        }
    }
}