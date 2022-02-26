using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootRun
{
    public class UIController : SingletonBehaviour<UIController>
    {
        [SerializeField] private RectTransform _successPanel;
        [SerializeField] private RectTransform _failPanel;

        public void ActivateEndgamePanel(bool success)
        {
            if(success)
                _successPanel.gameObject.SetActive(true);
            else
                _failPanel.gameObject.SetActive(true);
        }

        public void LoadButton()
        {
            _successPanel.gameObject.SetActive(false);
            _failPanel.gameObject.SetActive(false);
            
            GameManager.Instance.LoadLevel();
        }
    }
}

