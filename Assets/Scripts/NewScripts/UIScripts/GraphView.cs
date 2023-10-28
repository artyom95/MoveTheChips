using System.Collections.Generic;
using UnityEngine;

namespace NewScripts.UIScripts
{
    public class GraphView : MonoBehaviour
    {
        [SerializeField] private Material _materialLineRenderer;
        private const int _scalePosition = 2;
        private const int _lineRendererWidth = 10;

        public void DisplayGraphs(List<Vector2> coordinatesPoints, List<Vector2> connectionsBetweenPointPairs,
            GameObject mainPanel, Vector3 newPosition = default)
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
                if (newPosition != Vector3.zero)
                {
                    firstPosition /= _scalePosition;
                    secondPosition /= _scalePosition;

                    line.startWidth = _lineRendererWidth / 2;
                    line.endWidth = _lineRendererWidth / 2;
                }

                line.SetPosition(index, firstPosition);
                index++;
                line.SetPosition(index, secondPosition);


                if (newPosition != Vector3.zero)
                {
                    SetLineRendererNewPosition(line, newPosition);
                }

                gameObject.transform.SetParent(mainPanel.transform);
            }
        }

        private void SetLineRendererNewPosition(LineRenderer line, Vector3 newPosition)
        {
            var amountPointLineRenderer = line.positionCount;
            for (int i = 0; i < amountPointLineRenderer; i++)
            {
                var localPosition = line.GetPosition(i);
                var globalPosition = newPosition + localPosition;
                line.SetPosition(i, globalPosition);
            }
        }
    }
}