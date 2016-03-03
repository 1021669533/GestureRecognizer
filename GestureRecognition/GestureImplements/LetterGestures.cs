using GestureRecognition.Tools;

namespace GestureRecognition.GestureImplements
{

    public class GestureM11 : NonRealTimeGestureParser
    {
        public GestureM11()
        {
            Type = GestureType.M1;
            ExpectPathCount = 1;
        }

        public override int Parse(GesturePath[] paths)
        {
            var path = paths[0];
            var weight = 0;
            if (path.AllNormalizedVectors.Count != 4 && path.InflectionPoints.Count != 5)
            {
                return -1;
            }
            if (path.AllNormalizedVectors.Count == 5)
            {
                weight -= -25;
            }
            if (path.InflectionPoints[1].y < path.InflectionPoints[0].y || path.InflectionPoints[1].y < path.InflectionPoints[2].y || path.InflectionPoints[1].y < path.InflectionPoints[path.InflectionPoints.Count - 1].y || path.InflectionPoints[path.InflectionPoints.Count - 2].y < path.InflectionPoints[0].y || path.InflectionPoints[path.InflectionPoints.Count - 2].y < path.InflectionPoints[2].y || path.InflectionPoints[path.InflectionPoints.Count - 2].y < path.InflectionPoints[path.InflectionPoints.Count - 1].y)
            {
                return -1;
            }
            if (path.InflectionPoints[1].x < path.InflectionPoints[0].x || path.InflectionPoints[2].x < path.InflectionPoints[1].x || path.InflectionPoints[path.InflectionPoints.Count - 1].x < path.InflectionPoints[2].x || path.InflectionPoints[path.InflectionPoints.Count - 1].x < path.InflectionPoints[path.InflectionPoints.Count - 2].x)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path.AllNormalizedVectors[0]);
            if (k1 > GestureConstant.tan45)
            {
                weight += 100;
            }
            var k2 = MathExtension.Slope(path.AllNormalizedVectors[1]);
            if (k2 < -GestureConstant.tan45)
            {
                weight += 100;
            }
            var k3 = MathExtension.Slope(path.AllNormalizedVectors[path.AllNormalizedVectors.Count - 2]);
            if (k3 > GestureConstant.tan45)
            {
                weight += 100;
            }
            var k4 = MathExtension.Slope(path.AllNormalizedVectors[path.AllNormalizedVectors.Count - 1]);
            if (k4 < -GestureConstant.tan45)
            {
                weight += 100;
            }
            weight /= 4;
            return weight;
        }

        public override string ToString()
        {
            return "M";
        }
    }
}
