using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using NewScripts.Events;
using NewScripts.StateMachine;
using UniTaskPubSub;
using UnityEngine;
using VContainer;

namespace NewScripts.Chip
{
    public class ChipMover : MonoBehaviour
    {
        [SerializeField] private float _duration;
        private AsyncMessageBus _messageBus;

        [Inject]
        public void Construct(AsyncMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async UniTask StartMove(IReadOnlyList<Vector3> path,
            ChipModelSettings chip,
            GameContext gameContext,
            StateMachine<GameContext> stateMachine)
        {
            if (path.Count != 0)
            {
                await MoveAlongPath(path, chip, gameContext, stateMachine);
            }
        }

        private async UniTask MoveAlongPath(IReadOnlyList<Vector3> path,
            ChipModelSettings chip,
            GameContext gameContext,
            StateMachine<GameContext> stateMachine)
        {
            var currentWaypointIndex = 0;

            gameContext.StartNodeModel.ResetChipModel();
            while (currentWaypointIndex < path.Count)
            {
                var targetWaypoint = path[currentWaypointIndex];
                var currentTime = 0f;
                while (!Mathf.Approximately(chip.transform.localPosition.x, targetWaypoint.x) ||
                       !Mathf.Approximately(chip.transform.localPosition.y, targetWaypoint.y) ||
                       !Mathf.Approximately(chip.transform.localPosition.z, targetWaypoint.z))
                {
                    chip.transform.localPosition =
                        Vector3.MoveTowards(chip.transform.localPosition, targetWaypoint, _duration * Time.deltaTime);
                    currentTime += Time.deltaTime;

                    await UniTask.Yield();
                }

                currentWaypointIndex++;
            }

            gameContext.FinishNodeModel.SetChipModel(chip);
            _messageBus.Publish(new FinishChipMovingEvent());
        }
    }
}