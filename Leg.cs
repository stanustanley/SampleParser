using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SampleApplication
{
    public abstract class Leg : MonoBehaviour
    {
        public float Range { get; set; }
        public float CurrentPosition { get; set; }
        public abstract void PerformMove(float step);
        public bool moveFinished { get; set; }
    }
}
