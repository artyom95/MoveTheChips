using System.Collections.Generic;
using UnityEngine;

namespace NewScripts.UIScripts
{
    public class GraphView : MonoBehaviour
    {
        [SerializeField] private Material _materialLineRenderer;
        private const int _lineRendererWidth = 10;

        public void DisplayGraphs(List<Vector2> coordinatesPoints, List<Vector2> connectionsBetweenPointPairs,
            GameObject panel, bool isItAnotherInstancePosition)
        {
            var numberLine = 0;
            foreach (var connection in connectionsBetweenPointPairs)
            {
                var index = 0;
                numberLine++;
                var firstIndexPosition = (int)(connection.x - 1);
                var firstPosition = new Vector3(coordinatesPoints[firstIndexPosition].x,
                    -coordinatesPoints[firstIndexPosition].y, 0);

                var secondIndexPosition = (int)(connection.y - 1);
                var secondPosition = new Vector3(coordinatesPoints[secondIndexPosition].x,
                    -coordinatesPoints[secondIndexPosition].y, 0);


                var gameObject = new GameObject
                {
                    name = "line" + numberLine
                };
                var line = gameObject.AddComponent<LineRenderer>();
                line.startWidth = _lineRendererWidth;
                line.endWidth = _lineRendererWidth;
                line.material = _materialLineRenderer;
                if (isItAnotherInstancePosition)
                {
                    var localScale = panel.transform.localScale;
                    firstPosition *= localScale.x;
                    secondPosition *= localScale.x;

                    line.startWidth = _lineRendererWidth * localScale.x;
                    line.endWidth = _lineRendererWidth * localScale.x;
                }

                gameObject.transform.SetParent(panel.transform);

                line.useWorldSpace = false;
                line.gameObject.transform.localPosition = Vector3.zero;
                line.SetPosition(index, firstPosition);
                index++;
                line.SetPosition(index, secondPosition);
                ResetLineRendererPosition(line);
            }
        }


        private void ResetLineRendererPosition(LineRenderer line)
        {
            line.transform.localPosition = Vector3.zero;
        }
    }
}