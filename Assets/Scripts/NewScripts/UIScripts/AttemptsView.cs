using System;
using TMPro;
using UnityEngine;

namespace NewScripts.UIScripts
{
    public class AttemptsView : MonoBehaviour

    {
        [SerializeField] private TextMeshProUGUI _currentAttempt;
        [SerializeField] private TextMeshProUGUI _amountAttempts;

        public void ShowAllAttempts(int amountAttempts)
        {
            var attempt = amountAttempts.ToString();
            _currentAttempt.text = "0";
            _amountAttempts.text = string.Concat("/" +" " + attempt);
        }

        public void ShowCurrentAttempt(int currentAttempt)
        {
            _currentAttempt.text = Convert.ToString(currentAttempt);
        }
    }
}