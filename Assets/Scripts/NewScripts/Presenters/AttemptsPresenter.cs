using System;
using NewScripts.Events;
using NewScripts.Extensions;
using NewScripts.UIScripts;
using UniTaskPubSub;

namespace NewScripts.Presenters
{
    public class AttemptsPresenter : IDisposable
    {
        private readonly AttemptsView _attemptsView;
        private readonly IAsyncSubscriber _subscriber;
        private CompositeDisposable _subscription;
        private readonly AttemptsModel _attemptsModel;
        private Action _attemptsEnded;

        public AttemptsPresenter(AttemptsView attemptsView,
            AttemptsModel attemptsModel,
            IAsyncSubscriber subscriber)
        {
            _attemptsModel = attemptsModel;
            _subscriber = subscriber;
            _attemptsView = attemptsView;
        }

        public void Initialize(int amountAttempts, Action attemptsEnded)
        {
            _attemptsEnded = attemptsEnded;
            _subscription = new CompositeDisposable()
            {
                _subscriber.Subscribe<IncreaseAttemptEvent>(IncreaseAttemptHandler)
            };

            _attemptsModel.Initialize(amountAttempts);
            _attemptsView.gameObject.SetActive(true);
            _attemptsView.ShowAllAttempts(amountAttempts);
        }

        private void IncreaseAttemptHandler(IncreaseAttemptEvent arg1)
        {
            var currentAttempt = _attemptsModel.IncreaseAmountAttempt();
            if (_attemptsModel.IsGameOver())
            {
                _attemptsEnded?.Invoke();
            }

            _attemptsView.ShowCurrentAttempt(currentAttempt);
        }


        public void Dispose()
        {
            _subscription.Dispose();
        }

        public void ResetAttempts()
        {
            _attemptsModel.ResetAttempts();
            Dispose();
        }
    }
}