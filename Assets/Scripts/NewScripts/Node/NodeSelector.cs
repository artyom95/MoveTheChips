using System;
using NewScripts.Chip;
using NewScripts.Events;
using UniTaskPubSub;
using UnityEngine;
using VContainer;

namespace NewScripts.Node
{
    public class NodeSelector : MonoBehaviour
    {
        public event Action<NodeModel> FirstNodeModelSelected;
        public event Action SwitchedOfOutline ;
        public event Action<NodeModel> SecondNodeModelSelected;

        private int _layerMask;
        private bool _isSelectorLocked;
        private ChipModelSettings _chipWithColor;
        private AsyncMessageBus _messageBus;

        [Inject]
        public void Construct(AsyncMessageBus messageBus)
        {
            _messageBus = messageBus;
        }
        public void Start()
        {
            _layerMask = LayerMask.GetMask("ChipModel");
        }

        public void Update()
        {
            if (!_isSelectorLocked)
            {
                SelectNode();
            }
        }

        public void ChangeStateNodeSelector(bool state)
        {
            _isSelectorLocked = state;
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
                    hitInfo.collider.TryGetComponent<NodeModel>(out var nodeModel);
                    SelectMainNode(nodeModel);
                    SelectTargetNode(nodeModel);
                }
                else
                {
                    SwitchedOfOutline?.Invoke();
                }
            }
        }

        private void SelectMainNode(NodeModel nodeModel)
        {
            if (nodeModel != null && nodeModel.ChipModel != null)
            {
                CheckSelectionAnotherNodeModel();
                _chipWithColor = nodeModel.ChipModel;
                nodeModel.ChipModel.TurnOnOutline();
                FirstNodeModelSelected?.Invoke(nodeModel);
            }
        }

        private void SelectTargetNode(NodeModel nodeModel)
        {
            var outline = nodeModel.GetComponent<Outline>();
            if (nodeModel != null && outline.OutlineWidth > 0)
            {
                SecondNodeModelSelected?.Invoke(nodeModel);
                _messageBus.Publish(new IncreaseAttemptEvent());
            }
        }
    }
}