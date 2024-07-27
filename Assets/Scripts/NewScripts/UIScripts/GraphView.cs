using System.Collections.Generic;
using UnityEngine;

namespace NewScripts.UIScripts
{
    public class GraphView : MonoBehaviour
    {
        [SerializeField] private Material _materialLineRenderer;
        private const int _lineRendererWidth = 10;
        private List<LineRenderer> _listMainLines = new ();
        private List<LineRenderer> _listTargetLines = new ();
        
        public void DisplayGraphs(List<Vector2> coordinatesPoints, List<Vector2> connectionsBetweenPointPairs,
            GameObject panel, string nameListLines)
        {
            var numberLine = 0;
            foreach (var connection in connectionsBetweenPointPairs)
            {
                 DrawLine(coordinatesPoints, panel, numberLine, connection, nameListLines );
            }
        }

        public void Clear()
        {
            CLearList(_listMainLines);
            CLearList(_listTargetLines);
        }

        private void CLearList(List<LineRenderer> list)
        {
            foreach (var lineRenderer in list)
            {
                Destroy(lineRenderer.gameObject);
            }
            list.Clear();
        }
        private void DrawLine(List<Vector2> coordinatesPoints, GameObject panel, int numberLine, Vector2 connection,
            string nameListLines)
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

            var localScale = panel.transform.localScale;

            line.startWidth = _lineRendererWidth * localScale.x;
            line.endWidth = _lineRendererWidth * localScale.x;

            gameObject.transform.SetParent(panel.transform);
            line.useWorldSpace = false;
            line.SetPosition(index, firstPosition);
            index++;
            line.SetPosition(index, secondPosition);
            line.gameObject.transform.localScale = Vector3.one;
            
            if (nameListLines.Equals("main"))
            {
                _listMainLines.Add(line);
            }
            else
            {
                _listTargetLines.Add(line);
            }
            ResetLineRendererPosition(line);
        }

        private void ResetLineRendererPosition(LineRenderer line)
        {
            line.transform.localPosition = Vector3.zero;
        }
        
    }
}