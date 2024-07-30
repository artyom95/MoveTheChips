using System.Collections.Generic;
using UnityEngine;

namespace NewScripts.UIScripts
{
    public class GraphView : MonoBehaviour
    {
        [SerializeField] private Material _materialLineRenderer;
        private const int _lineRendererWidth = 10;
        private List<LineRenderer> _listMainLines = new();
        private List<LineRenderer> _listTargetLines = new();

        public void DisplayGraphs(List<Vector2> coordinatesPoints, List<Vector2> connectionsBetweenPointPairs,
            GameObject panel, string nameListLines)
        {
            var numberLine = 0;
            foreach (var connection in connectionsBetweenPointPairs)
            {
                DrawLine(coordinatesPoints, panel, numberLine, connection, nameListLines);
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
            numberLine++;
            var firstPosition = GetAdjustedPosition(coordinatesPoints[(int)(connection.x - 1)]);
            var secondPosition = GetAdjustedPosition(coordinatesPoints[(int)(connection.y - 1)]);

            var lineObject = CreateLineObject(numberLine, panel);
            var lineRenderer = lineObject.GetComponent<LineRenderer>();

            SetupLineRenderer(lineRenderer, firstPosition, secondPosition, panel.transform.localScale);

            AddLineToList(lineRenderer, nameListLines);
            ResetLineRendererPosition(lineRenderer);
        }

        private Vector3 GetAdjustedPosition(Vector2 point)
        {
            return new Vector3(point.x, -point.y, 0);
        }

        private GameObject CreateLineObject(int numberLine, GameObject panel)
        {
            GameObject lineObject = new GameObject("line" + numberLine);
            lineObject.transform.SetParent(panel.transform);
            lineObject.AddComponent<LineRenderer>();
            return lineObject;
        }

        private void SetupLineRenderer(LineRenderer lineRenderer, Vector3 firstPosition, Vector3 secondPosition,
            Vector3 scale)
        {
            lineRenderer.startWidth = _lineRendererWidth * scale.x;
            lineRenderer.endWidth = _lineRendererWidth * scale.x;
            lineRenderer.material = _materialLineRenderer;
            lineRenderer.useWorldSpace = false;
            lineRenderer.SetPosition(0, firstPosition);
            lineRenderer.SetPosition(1, secondPosition);
            lineRenderer.gameObject.transform.localScale = Vector3.one;
        }

        private void AddLineToList(LineRenderer lineRenderer, string nameListLines)
        {
            if (nameListLines.Equals("main"))
            {
                _listMainLines.Add(lineRenderer);
            }
            else
            {
                _listTargetLines.Add(lineRenderer);
            }
        }

        private void ResetLineRendererPosition(LineRenderer line)
        {
            line.transform.localPosition = Vector3.zero;
        }
    }
}