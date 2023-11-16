using System.Collections;
using System.Collections.Generic;
using NewScripts.StateMachine;
using UnityEngine;

namespace NewScripts.Chip
{
    public class ChipMover : MonoBehaviour
    {
        [SerializeField] private float _duration;

        public void StartMove(IReadOnlyList<Vector3> path,
            ChipModelSettings chip,
            GameContext gameContext,
            StateMachine<GameContext> stateMachine)
        {
            if (path.Count != 0)
            {
                StartCoroutine(MoveAlongPath(path, chip, gameContext, stateMachine));
            }
            else
            {
                return;
            }
        }

        private IEnumerator MoveAlongPath(IReadOnlyList<Vector3> path,
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

                    yield return null;
                }

                currentWaypointIndex++;
            }

            gameContext.FinishNodeModel.SetChipModel(chip);
            stateMachine.Enter<FinishGameState>();
        }
    }
}