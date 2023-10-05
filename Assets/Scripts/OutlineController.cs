    using UnityEngine;

    [RequireComponent(typeof(Outline))]
    public class OutlineController : MonoBehaviour
    {
        [SerializeField]
        private int _highlightIntensity;
    
        private Outline _outline;

        private void Awake()
        {
            _outline = GetComponent<Outline>();
        }
        
        public void SetFocus()
        {
            _outline.OutlineWidth = _highlightIntensity;
        }

        public  void RemoveFocus()
        {
            _outline.OutlineWidth = 0;
        }
    }
