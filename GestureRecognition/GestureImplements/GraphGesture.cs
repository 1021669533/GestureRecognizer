using System.Linq;
using GestureRecognition.Tools;

namespace GestureRecognition.GestureImplements
{
    public class GestureTriangularUpward : NonRealTimeGestureParser
    {
        public GestureTriangularUpward()
        {
            Type = GestureType.TriangularUpward;
            ExpectPathCount = 1;
        }
        public override int Parse(GesturePath[] paths)
        {
            var path = paths[0];
            if (path.AllNormalizedVectors.Count != 3)
            {
                return -1;
            }
            var weight = 0;
            var k1 = MathExtension.Slope(path.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path.AllNormalizedVectors[1]);
            var k3 = MathExtension.Slope(path.AllNormalizedVectors[2]);
            if (k1 > GestureConstant.tan22p5 && k1 < GestureConstant.tan67p5)
            {
                weight += 25;
            }
            if (k2 > -GestureConstant.tan15 && k2 < GestureConstant.tan15)
            {
                weight += 25;
            }
            if (k3 > -GestureConstant.tan67p5 && k3 < -GestureConstant.tan22p5)
            {
                weight += 25;
            }
            if (MathExtension.DistanceSquare(path.InflectionPoints.Last(), path.InflectionPoints[0]) < GestureConstant.ScreenHeightDiv5 * GestureConstant.ScreenHeightDiv5)
            {
                weight += 25;
            }
            return weight;
        }
    }

    public class GestureTriangularDownward : NonRealTimeGestureParser
    {
        public GestureTriangularDownward()
        {
            Type = GestureType.TriangularDownward;
            ExpectPathCount = 1;
        }
        public override int Parse(GesturePath[] paths)
        {
            var path = paths[0];
            if (path.AllNormalizedVectors.Count != 3)
            {
                return -1;
            }
            var weight = 0;
            var k1 = MathExtension.Slope(path.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path.AllNormalizedVectors[1]);
            var k3 = MathExtension.Slope(path.AllNormalizedVectors[2]);
            if (k1 > -GestureConstant.tan15 && k1 < GestureConstant.tan15)
            {
                weight += 25;
            }
            if (k2 > -GestureConstant.tan67p5 && k2 < -GestureConstant.tan22p5)
            {
                weight += 25;
            }
            if (k3 > GestureConstant.tan22p5 && k3 < GestureConstant.tan22p5)
            {
                weight += 25;
            }
            if (MathExtension.DistanceSquare(path.InflectionPoints.Last(), path.InflectionPoints[0]) < GestureConstant.ScreenHeightDiv5 * GestureConstant.ScreenHeightDiv5)
            {
                weight += 25;
            }
            return weight;
        }
    }

    public class GestureTriangularLeftward : NonRealTimeGestureParser
    {
        public GestureTriangularLeftward()
        {
            Type = GestureType.TriangularLeftward;
            ExpectPathCount = 1;
        }
        public override int Parse(GesturePath[] paths)
        {
            var path = paths[0];
            if (path.AllNormalizedVectors.Count != 3)
            {
                return -1;
            }
            var weight = 0;
            var k1 = MathExtension.Slope(path.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path.AllNormalizedVectors[1]);
            var k3 = MathExtension.Slope(path.AllNormalizedVectors[2]);
            if (k1 > GestureConstant.tan22p5 && k1 < GestureConstant.tan67p5)
            {
                weight += 25;
            }
            if (k2 > -GestureConstant.tan67p5 && k2 < -GestureConstant.tan22p5)
            {
                weight += 25;
            }
            if (k3 > GestureConstant.tan75 && k3 < -GestureConstant.tan75)
            {
                weight += 25;
            }
            if (MathExtension.DistanceSquare(path.InflectionPoints.Last(), path.InflectionPoints[0]) < GestureConstant.ScreenHeightDiv5 * GestureConstant.ScreenHeightDiv5)
            {
                weight += 25;
            }
            return weight;
        }
    }

    public class GestureTriangularRightward : NonRealTimeGestureParser
    {
        public GestureTriangularRightward()
        {
            Type = GestureType.TriangularRightward;
            ExpectPathCount = 1;
        }
        public override int Parse(GesturePath[] paths)
        {
            var path = paths[0];
            if (path.AllNormalizedVectors.Count != 3)
            {
                return -1;
            }
            var weight = 0;
            var k1 = MathExtension.Slope(path.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path.AllNormalizedVectors[1]);
            var k3 = MathExtension.Slope(path.AllNormalizedVectors[2]);
            if (k1 > -GestureConstant.tan67p5 && k1 < -GestureConstant.tan22p5)
            {
                weight += 25;
            }
            if (k2 > GestureConstant.tan22p5 && k2 < GestureConstant.tan67p5)
            {
                weight += 25;
            }
            if (k3 > GestureConstant.tan75 && k3 < -GestureConstant.tan75)
            {
                weight += 25;
            }
            if (MathExtension.DistanceSquare(path.InflectionPoints.Last(), path.InflectionPoints[0]) < GestureConstant.ScreenHeightDiv5 * GestureConstant.ScreenHeightDiv5)
            {
                weight += 25;
            }
            return weight;
        }
    }
}
