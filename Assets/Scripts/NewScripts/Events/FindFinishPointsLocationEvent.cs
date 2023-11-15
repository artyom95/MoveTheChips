using System.Collections.Generic;


public struct FindFinishPointsLocationEvent
{
   public List<int> FinishPointsLocation { get; }

   public FindFinishPointsLocationEvent(List<int> finishPointsLocation)
   {
      FinishPointsLocation = new List<int>();
      FinishPointsLocation = finishPointsLocation;
   }
}
