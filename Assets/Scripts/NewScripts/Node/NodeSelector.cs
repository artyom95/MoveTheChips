using System;
using NewScripts.Chip;
using UnityEngine;

namespace NewScripts.Node
{
    public class NodeSelector : MonoBehaviour
    {
        public event Action<NodeModel> FirstNodeModelSelected;

        public event Action<NodeModel> SecondNodeModelSelected;

        private int _layerMask;
        private bool _isCurrentNodeSelect = true;
        private bool _isTargetNodeSelect = true;
        private ChipModelSettings _chipWithColor;

        public void Start()
        {
            _layerMask = LayerMask.GetMask("ChipModel");
        }

        public void Update()
        {
            SelectNode();
        }

        public void ChangeStateNodeSelector(int typeStateSelector)
        {
            if (typeStateSelector == 1)
            {
                _isCurrentNodeSelect = false;
            }
            else if (typeStateSelector == 2)
            {
                _isTargetNodeSelect = false;
            }
        }

        private void CheckSelectionAnotherNodeModel()
        {
            if (_chipWithColor != null)
            {
                _chipWithColor.TurnOffOutline();
            }
        }

        private void SelectNode()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, ~_layerMask))
                {
                    var nodeModel = hitInfo.collider.GetComponent<NodeModel>();
                    SelectMainNode(nodeModel);
                    SelectTargetNode(nodeModel);
                }
            }
        }

        private void SelectMainNode(NodeModel nodeModel)
        {
            if (!_isCurrentNodeSelect)
            {
                if (nodeModel != null && nodeModel.ChipModel != null)
                {
                    CheckSelectionAnotherNodeModel();
                    _chipWithColor = nodeModel.ChipModel;
                    nodeModel.ChipModel.TurnOnOutline();
                    FirstNodeModelSelected?.Invoke(nodeModel);
                }
            }
        }

        private void SelectTargetNode(NodeModel nodeModel)
        {
            if (!_isTargetNodeSelect)
            {
                var outline = nodeModel.GetComponent<Outline>();
                if (nodeModel != null && outline.OutlineWidth > 0)
                {
                    _isCurrentNodeSelect = true;
                    _isTargetNodeSelect = true;
                    SecondNodeModelSelected?.Invoke(nodeModel);
                }
            }
        }
    }
}