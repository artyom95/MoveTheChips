using System.Collections;
using System.Collections.Generic;
using NewScripts;
using NewScripts.Chip;
using NewScripts.Node;
using UnityEngine;

public class GameContext
{
  
   public NodeModel StartNodeModel { get; set; }
   public NodeModel FinishNodeModel { get; set; }
   public List<NodeModel> NodeModelsList { get; set; } = new ();
   
   public List<NodeModel> HighlightingNodesList { get; set; } = new ();

   public List<int> FinishPointLocation { get; set; } = new();
   public List<Vector3> Path { get; set; } = new();
   
   public ChipModel Chip { get; set; }
   public int [] FinishIndexChips { get; set; } 
}
