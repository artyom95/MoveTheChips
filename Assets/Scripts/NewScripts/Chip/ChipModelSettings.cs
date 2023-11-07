using UnityEngine;

namespace NewScripts.Chip
{
    public class ChipModelSettings : MonoBehaviour, IHighlightable
    {
        public Vector3 Position { get; private set; }
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


        public void SetPosition(Vector3 position)
        {
            Position = position;
        }
    }
}