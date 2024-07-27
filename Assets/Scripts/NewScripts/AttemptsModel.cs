namespace NewScripts
{
    public class AttemptsModel
    {
        private int _amountAttempts;
        private int _currentAttempt;

        public void Initialize(int amountAttempts)
        {
            _amountAttempts = amountAttempts;
        }
        public int IncreaseAmountAttempt()
        {
            return _currentAttempt +=1;
        }

        public bool IsGameOver()
        {
            return _currentAttempt == _amountAttempts;
        }

        public void ResetAttempts()
        {
            _currentAttempt = 0;
        }
    }
}