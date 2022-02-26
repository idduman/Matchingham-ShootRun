using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootRun
{
    public interface IObstacle
    {
        public void Damage(Vector3 point);
    }
}

