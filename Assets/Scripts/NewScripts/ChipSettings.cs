using UnityEngine;

namespace NewScripts
{
    public class ChipSettings :  ILuminable
    {
        public int ID { get; private set; }
        [SerializeField] private OutlineController _outlineController;
        [SerializeField] private MeshRenderer _meshRenderer;

        public void SetID(int id)
        {
            ID = id;
        }


        public void TurnOnOutline()
        {
            _outlineController.SetFocus();
        }

        public void TurnOffOutline()
        {
            _outlineController.RemoveFocus();
        }

        public void SetColor(Color color)
        {
            _meshRenderer.material.color = color;
        }
    }
}