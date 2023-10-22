using System;
using Unity.VisualScripting;
using UnityEngine;

namespace NewScripts
{
    public class NodeSelector : MonoBehaviour
    {
        public event Action<NodeModel> FirstNodeModelSelected;

        public event Action<NodeModel> SecondNodeModelSelected;
     
        private int _layerMask;
        private bool _isFirstNodeSelect = true;
        private bool _isSecondNodeSelect = true;
        private ChipModel _chipWithColor;

        private void Awake()
        {
            _layerMask = LayerMask.GetMask("ChipModel");
        }

        private void Update()
        {
            SelectFirstNode();

            SelectSecondNode();
        }

        private void SelectSecondNode()
        {
            if (Input.GetMouseButtonDown(0) && !_isSecondNodeSelect)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, ~_layerMask))
                {
                    var nodeModel = hitInfo.collider.GetComponent<NodeModel>();
                    var outline = nodeModel.GetComponent<Outline>();
                    if (nodeModel != null && outline.OutlineWidth > 0)
                    {
                        _isFirstNodeSelect = true;
                        _isSecondNodeSelect = true;
                        SecondNodeModelSelected?.Invoke(nodeModel);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private void SelectFirstNode()
        {
            if (Input.GetMouseButtonDown(0) && !_isFirstNodeSelect)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, ~_layerMask))
                {
                    var nodeModel = hitInfo.collider.GetComponent<NodeModel>();


                    if (nodeModel != null && nodeModel.ChipModel != null)
                    {
                        CheckSelectionAnotherNodeModel();
                        _chipWithColor = nodeModel.ChipModel;
                        nodeModel.ChipModel.TurnOnOutline();
                        FirstNodeModelSelected?.Invoke(nodeModel);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private void CheckSelectionAnotherNodeModel()
        {
            if (_chipWithColor != null)
            {
                _chipWithColor.TurnOffOutline();
            }
        }

        public void ChangeStateNodeSelector(int countStateSelector)
        {
            if (countStateSelector == 1)
            {
                _isFirstNodeSelect = false;
            }
            else if (countStateSelector == 2)
            {
                _isSecondNodeSelect = false;
            }
        }
    }
}