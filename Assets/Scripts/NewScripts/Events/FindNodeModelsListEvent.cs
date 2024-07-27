using System.Collections.Generic;
using NewScripts.Node;

public struct FindNodeModelsListEvent
{
    public List<NodeModel> NodeModelsList { get; }

    public FindNodeModelsListEvent(List<NodeModel> nodeModelsList)
    {
        NodeModelsList = new List<NodeModel>();
        NodeModelsList = nodeModelsList;
    }
}