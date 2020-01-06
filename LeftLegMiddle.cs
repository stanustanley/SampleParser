using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SampleApplication
{
    public class LeftLegMiddle : Leg
    {

        private SpriteRenderer CurrLeg { get; set; }
        private float CurrColorPosition { get; set; } // 0...255
        private float expectedPosition { get; set; } // 40...160 160->LewaDol
        private float MinPosition { get; } = 40;
        private float MaxPosition { get; } = 160;
        private bool MoveStarted { get; set; } = false;
        private float Step { get; set; } = 0;

        public override void PerformMove(float expectedPosition) 
        {
            if (inRange(expectedPosition))
            {
                if (this.expectedPosition != expectedPosition) 
                {
                    this.expectedPosition = expectedPosition;
                    Step = GetColorStep(expectedPosition); 
                }
                else
                {
                    if (CurrLeg.color.g >= 1 || CurrLeg.color.g <= 0)
                    {
                        throw new System.Exception("Ruch poza zakresem");
                    }
                }
                CurrColorPosition += Step;
                SetNewColor(CurrColorPosition);
            }
        }

        private void Start()
        {
            CurrLeg = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
            CurrColorPosition = CurrLeg.color.g;
        }
        private void Update()
        {

        }
        private bool inRange(float position)
        {
            if (position <= MaxPosition && position >= MinPosition) return true;
            else return false;
        }
        private void SetNewColor(float newColorValue)
        {
            Debug.Log(newColorValue);
            var color = CurrLeg.color;
            color = new Color(0, newColorValue, 0);
            CurrLeg.color = color;
        }
        private float GetColorStep(float expectedPosition)
        {
            float expectedColorPosition = ServoToColorPosition(expectedPosition);
            return expectedColorPosition - CurrColorPosition / 60; // 60 kroków

        }
        private float ServoToColorPosition(float servoPosition)
        {
            float fromMinDistace = servoPosition - MinPosition;
            float range = MaxPosition - MinPosition;
            float colorPosition = fromMinDistace / range;
            return colorPosition;
        }
    }
}
