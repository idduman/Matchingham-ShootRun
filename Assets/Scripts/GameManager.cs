using UnityEngine;

namespace ShootRun
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        public void FinishGame(bool success)
        {
            Debug.Log("Finish");
        }
    }
}

