using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApplication
{
    public class LegDriver
    {
        private List<Leg> legs;
        private float range;
        public bool MoveFinished { get; set; }
        public LegDriver(List<Leg> legs, float range)
        {
            this.legs = legs;
            this.range = range;

        }
        private LegDriver() { }
        public void PerformMove()
        {
            try
            {
                if (legs.Any())
                    legs.ForEach(x => x.PerformMove(range));
            }
            catch (Exception ex) // TODO obsłużyć odpowiedni wyjątek
            {
                MoveFinished = true;
            }

        }
    }
}
