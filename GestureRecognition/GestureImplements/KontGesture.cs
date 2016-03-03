
using System.Linq;
using GestureRecognition.Tools;

namespace GestureRecognition.GestureImplements
{
    public class GestureSingleKnotUpward1 : NonRealTimeGestureParser
    {
        public GestureSingleKnotUpward1()
        {
            this.Type = GestureType.SingleKnotUpward1;
            ExpectPathCount = 1;
        }

        public override int Parse(GesturePath[] paths)
        {
            var path = paths[0];
            var weight = 0;
            if (path.CrossingCount != 1)
            {
                return -1;
            }
            switch (path.AllNormalizedVectors.Count)
            {
                case 3:
                case 4:
                    break;
                case 5:
                    weight -= 500;
                    break;
                default:
                    return -1;
            }
            if (path.AllNormalizedVectors.Count != 3 && path.AllNormalizedVectors.Count != 4)
            {
                if (path.AllNormalizedVectors.Count != 5)
                {
                    return -1;
                }
                else
                {
                    weight -= 500;
                }
            }
            if (path.MiddlePoint.y + GestureConstant.ScreenHeightDiv5 < path.InflectionPoints[0].y || path.MiddlePoint.y + GestureConstant.ScreenHeightDiv5 < path.InflectionPoints.Last().y)
            {
                return -1;
            }
            if (path.MiddlePoint.x < path.InflectionPoints[0].x && path.MiddlePoint.x < path.InflectionPoints.Last().x)
            {
                return -1;
            }
            if (path.MutationPoints.Count == 0)
            {
                weight += 500;
            }
            if (path.MiddlePoint.x + GestureConstant.ScreenWidthDiv5 < path.InflectionPoints[0].x || path.MiddlePoint.x > path.InflectionPoints.Last().x + GestureConstant.ScreenWidthDiv5)
            {
                weight -= 500;
            }
            weight += path.AllNormalizedVectors.Count == 3 ? 300 : 150;
            //if (info.ArcCount == 3)
            //{
            //    weight += 200;
            //}
            if (MathExtension.Slope(path.AllNormalizedVectors[0]) < GestureConstant.tan75 && MathExtension.Slope(path.AllNormalizedVectors[0]) > GestureConstant.tan15)
            {
                weight += 100;
            }
            if (MathExtension.Slope(path.AllNormalizedVectors[path.AllNormalizedVectors.Count - 1]) < -GestureConstant.tan15 && MathExtension.Slope(path.AllNormalizedVectors[path.AllNormalizedVectors.Count - 1]) > -GestureConstant.tan75)
            {
                weight += 100;
            }
            if (path.InflectionPoints[0].x < path.InflectionPoints[1].x)
            {
                weight += 100;
            }
            if (path.InflectionPoints[0].y < path.InflectionPoints[1].y)
            {
                weight += 100;
            }
            if (path.InflectionPoints.Last().x > path.InflectionPoints[path.InflectionPoints.Count - 2].x)
            {
                weight += 100;
            }
            if (path.InflectionPoints.Last().y < path.InflectionPoints[path.InflectionPoints.Count - 2].y)
            {
                weight += 100;
            }
            weight /= 14;
            return weight;
        }

        public override string ToString()
        {
            return "单结向上";
        }
    }

    public class GestureSingleKontDownward : NonRealTimeGestureParser
    {
        public override int Parse(GesturePath[] paths)
        {
            var weight = 0;
            var path = paths[0];

            return weight;
        }
    }
}
