using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverController : MonoBehaviour
{
   [SerializeField] private GameObject _winPanel;
   [SerializeField] private GameObject _losePanel;

   public void ShowWinScreen()
   {
      _winPanel.gameObject.SetActive(true);
   }

   public void ShowLoseScreen()
   {
      _losePanel.gameObject.SetActive(true);
   }
}
