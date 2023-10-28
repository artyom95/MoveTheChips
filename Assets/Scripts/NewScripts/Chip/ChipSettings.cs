using UnityEngine;

namespace NewScripts.Chip
{
    public class ChipSettings : MonoBehaviour,  ILuminable
    {
        public int ID { get; private set; }
        [SerializeField] private global::OutlineController _outlineController;
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
