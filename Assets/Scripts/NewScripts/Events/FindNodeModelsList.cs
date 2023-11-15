using System.Collections.Generic;
using NewScripts.Node;

public struct FindNodeModelsList 
{
    public List<NodeModel> NodeModelsList { get; }

    public FindNodeModelsList(List<NodeModel> nodeModelsList)
    {
       NodeModelsList = new List<NodeModel>();
        NodeModelsList = nodeModelsList;
    }
}
