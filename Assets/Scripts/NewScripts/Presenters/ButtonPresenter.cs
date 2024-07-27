using System;
using JetBrains.Annotations;
using UnityEngine;

namespace NewScripts.Presenters
{
    public class ButtonPresenter : MonoBehaviour
    {
        private Action _loadCurrentStage;
        private Action _loadNextStage;

        
        public void Initialize(Action loadNextStage, Action loadCurrentStage )
        {
            _loadNextStage = loadNextStage;
            _loadCurrentStage = loadCurrentStage;
        }
        [UsedImplicitly]
        public void LoadNextStage()
        {
            _loadNextStage?.Invoke();
        }

        [UsedImplicitly]
        public void LoadCurrentStage()
        {
            _loadCurrentStage?.Invoke();
        }
    }
}