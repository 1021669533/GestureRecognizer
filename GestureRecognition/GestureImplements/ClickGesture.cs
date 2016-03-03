using UnityEngine;

namespace GestureRecognition.GestureImplements
{
    public class GestureClick1 : NonRealTimeGestureParser
    {
        public GestureClick1()
        {
            Type = GestureType.Click1;
            ExpectPathCount = 1;
        }
        public override int Parse(GesturePath[] paths)
        {
            if (paths.Length != 1)
            {
                return -1;
            }
            if (paths[0].InflectionPoints.Count != 2)
            {
                return -1;
            }
            if (paths[0].Duration > 10000000)
            {
                return -1;
            }
            return 100;
        }
    }

    public class GestureLongClick1 : NonRealTimeGestureParser
    {
        public GestureLongClick1()
        {
            Type = GestureType.LongClick1;
            ExpectPathCount = 1;
        }
        public override int Parse(GesturePath[] paths)
        {
            if (paths.Length != 1)
            {
                return -1;
            }
            if (paths[0].InflectionPoints.Count > 1)
            {
                return -1;
            }
            if (paths[0].Duration < 10000000)
            {
                return -1;
            }
            return 100;
        }
    }
}
