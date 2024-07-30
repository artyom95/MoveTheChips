using System.Collections.Generic;
using NewScripts.Node;
using UnityEngine;

namespace NewScripts
{
    public class PathFinder
    {
        private Vector2 _finishPosition;


        public List<NodeModel> FindHighlightingPath(NodeModel currentNodeModel)
        {
            var queueNodeModels = new Queue<NodeModel>();
            var listHighlightingNode = new List<NodeModel>();
            queueNodeModels.Enqueue(currentNodeModel);
            while (queueNodeModels.Count > 0)
            {
                var nodeModel = queueNodeModels.Dequeue();
                var neighbours = nodeModel.Neighbours;
                foreach (var neighbour in neighbours)
                {
                    if (neighbour.ChipModel == null && neighbour.ID != currentNodeModel.ID &&
                        !IsNodeModelInList(neighbour, listHighlightingNode))
                    {
                        queueNodeModels.Enqueue(neighbour);
                        listHighlightingNode.Add(neighbour);
                    }
                }
            }

            return listHighlightingNode;
        }

        public List<Vector3> FindMovingPath(int columns, List<NodeModel> nodeModelsList, NodeModel startNodeModel,
            NodeModel finishNodeModel)
        {
            int amountColumns = columns;
            int amountRows = nodeModelsList.Count / amountColumns;

            var nodeArray = CreateNodeArray(amountRows, amountColumns, nodeModelsList, finishNodeModel, startNodeModel);

            Queue<Vector2> points = new Queue<Vector2>();
            var labirint = CreateLabirintArray(amountRows, amountColumns, nodeArray);

            var countNumber = 1;
            for (var i = 0; i < nodeArray.GetLength(0); i++)
            {
                for (var i1 = 0; i1 < nodeArray.GetLength(1); i1++)
                {
                    if (nodeArray[i, i1] == -5)
                    {
                        points.Enqueue(new Vector2(i + 1, i1 + 1));
                    }

                    if (finishNodeModel.ID == countNumber)
                    {
                        _finishPosition = new Vector2(i + 1, i1 + 1);
                    }

                    countNumber++;
                }
            }

            FillArraySteps(points, labirint, nodeArray, nodeModelsList);

            var indexList = FillListIndexPoints(labirint, nodeArray, nodeModelsList);
            var rootArray = FindRootList(indexList, nodeArray, nodeModelsList);
            return rootArray;
        }

        private List<Vector3> FindRootList(LinkedList<Vector2> indexList, int[,] nodeArray,
            List<NodeModel> nodeModelsList)
        {
            var rootArray = new List<Vector3>();
            foreach (var vector2 in indexList)
            {
                var count = 0;
                for (var firstIndex = 0; firstIndex < nodeArray.GetLength(0); firstIndex++)
                {
                    for (var secondIndex = 0; secondIndex < nodeArray.GetLength(1); secondIndex++)
                    {
                        if (firstIndex == vector2.x && secondIndex == vector2.y)
                        {
                            var nodeModel = nodeModelsList[count];
                            rootArray.Add(nodeModel.Position);
                        }

                        count++;
                    }
                }
            }

            return rootArray;
        }

        private LinkedList<Vector2> FillListIndexPoints(int[,] labirint, int[,] nodeArray,
            List<NodeModel> nodeModelsList)
        {
            var indexList = new LinkedList<Vector2>();
            var points = new Queue<Vector2>();
            indexList.AddFirst(new Vector2(_finishPosition.x - 1, _finishPosition.y - 1));
            points.Enqueue(_finishPosition);

            var step = 1;
            var start = 1;
            var end = 1;
            var count = 1;

            while (points.Count > 0)
            {
                for (int i = start; i <= end; i++)
                {
                    var currentCoordinates = points.Dequeue();
                    var x = (int)currentCoordinates.x;
                    var y = (int)currentCoordinates.y;
                    var index = FindNodeModelID(x, y, nodeArray);
                    var currentNodeModel = nodeModelsList[index];
                    if (labirint[x - 1, y] == labirint[x, y] - 1 && IsNodesNeighbours(currentNodeModel,
                            nodeModelsList[FindNodeModelID(x - 1, y, nodeArray)]))
                    {
                        indexList.AddFirst(new Vector2(x - 2, y - 1));
                        points.Enqueue(new Vector2(x - 1, y));
                        count++;
                        continue;
                    }

                    if (labirint[x + 1, y] == labirint[x, y] - 1 && IsNodesNeighbours(currentNodeModel,
                            nodeModelsList[FindNodeModelID(x + 1, y, nodeArray)]))
                    {
                        indexList.AddFirst(new Vector2(x, y - 1));
                        points.Enqueue(new Vector2(x + 1, y));
                        count++;
                        continue;
                    }

                    if (labirint[x, y - 1] == labirint[x, y] - 1 && IsNodesNeighbours(currentNodeModel,
                            nodeModelsList[FindNodeModelID(x, y - 1, nodeArray)]))
                    {
                        indexList.AddFirst(new Vector2(x - 1, y - 2));
                        points.Enqueue(new Vector2(x, y - 1));
                        count++;
                        continue;
                    }

                    if (labirint[x, y + 1] == labirint[x, y] - 1 && IsNodesNeighbours(currentNodeModel,
                            nodeModelsList[FindNodeModelID(x, y + 1, nodeArray)]))
                    {
                        indexList.AddFirst(new Vector2(x - 1, y));
                        points.Enqueue(new Vector2(x, y + 1));
                        count++;
                    }
                }

                if (points.Count == 0)
                {
                    break;
                }

                start = end + 1;
                end = count;
                step++;
            }

            return indexList;
        }

        private void FillArraySteps(Queue<Vector2> points, int[,] labirint, int[,] nodeArray,
            List<NodeModel> nodeModelsList)
        {
            var step = 1;
            var start = 1;
            var end = 1;
            var count = 1;

            while (points.Count > 0)
            {
                for (int i = start; i <= end; i++)
                {
                    var currentCoordinates = points.Dequeue();
                    var x = (int)currentCoordinates.x;
                    var y = (int)currentCoordinates.y;
                    var index = FindNodeModelID(x, y, nodeArray);
                    var currentNodeModel = nodeModelsList[index];
                    if (labirint[x - 1, y] == 0 && IsNodesNeighbours(currentNodeModel,
                            nodeModelsList[FindNodeModelID(x - 1, y, nodeArray)]))
                    {
                        labirint[x - 1, y] = step;
                        points.Enqueue(new Vector2(x - 1, y));
                        count++;
                    }

                    if (labirint[x + 1, y] == 0 && IsNodesNeighbours(currentNodeModel,
                            nodeModelsList[FindNodeModelID(x + 1, y, nodeArray)]))
                    {
                        labirint[x + 1, y] = step;
                        points.Enqueue(new Vector2(x + 1, y));
                        count++;
                    }

                    if (labirint[x, y - 1] == 0 && IsNodesNeighbours(currentNodeModel,
                            nodeModelsList[FindNodeModelID(x, y - 1, nodeArray)]))
                    {
                        labirint[x, y - 1] = step;
                        points.Enqueue(new Vector2(x, y - 1));
                        count++;
                    }

                    if (labirint[x, y + 1] == 0 && IsNodesNeighbours(currentNodeModel,
                            nodeModelsList[FindNodeModelID(x, y + 1, nodeArray)]))
                    {
                        labirint[x, y + 1] = step;
                        points.Enqueue(new Vector2(x, y + 1));
                        count++;
                    }
                }

                if (points.Count == 0)
                {
                    break;
                }

                start = end + 1;
                end = count;
                step++;
            }
        }

        private int FindNodeModelID(int x, int y, int[,] nodeArray)
        {
            var id = 0;
            for (var i = 0; i < nodeArray.GetLength(0); i++)
            {
                for (var i1 = 0; i1 < nodeArray.GetLength(1); i1++)
                {
                    if (x - 1 == i && y - 1 == i1)
                    {
                        return id;
                    }

                    id++;
                }
            }

            return default;
        }

        private bool IsNodesNeighbours(NodeModel currentNodeModel, NodeModel nextNodeModel)
        {
            var neighbours = currentNodeModel.Neighbours;
            foreach (var nodeModel in neighbours)
            {
                if (nodeModel.ID == nextNodeModel.ID)
                {
                    return true;
                }
            }

            return false;
        }

        private int[,] CreateNodeArray(int rows, int columns, List<NodeModel> nodeModelsList, NodeModel finishNodeModel,
            NodeModel startNodeModel)
        {
            int[,] nodeModelArray = new int[rows, columns];
            var count = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (nodeModelsList[count].ChipModel != null)
                    {
                        nodeModelArray[i, j] = -1;
                    }
                    else
                    {
                        nodeModelArray[i, j] = 0;
                    }

                    count++;
                    if (finishNodeModel.ID == count)
                    {
                        nodeModelArray[i, j] = 0;
                    }
                    else if (startNodeModel.ID == count)
                    {
                        nodeModelArray[i, j] = -5;
                    }
                }
            }

            return nodeModelArray;
        }

        private int[,] CreateLabirintArray(int rows, int columns, int[,] nodeArray)
        {
            var labirint = new int[rows + 2, columns + 2];


            for (int i = 0; i <= rows + 1; i++)
            {
                for (int j = 0; j <= columns + 1; j++)
                {
                    labirint[i, j] = -1;
                }
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (nodeArray[i, j] < 0)
                    {
                        labirint[i + 1, j + 1] = -1;
                    }

                    if (nodeArray[i, j] == 0)
                    {
                        labirint[i + 1, j + 1] = 0;
                    }

                    if (nodeArray[i, j] == 1)
                    {
                        labirint[i + 1, j + 1] = 1;
                    }

                    if (nodeArray[i, j] == 2)
                    {
                        labirint[i + 1, j + 1] = 2;
                    }
                }
            }

            return labirint;
        }

        private bool IsNodeModelInList(NodeModel nodeModel, List<NodeModel> nodeModelsList)
        {
            foreach (var node in nodeModelsList)
            {
                if (nodeModel.ID == node.ID)
                {
                    return true;
                }
            }

            return false;
        }
    }
}