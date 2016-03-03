using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GestureRecognition.GestureImplements
{
    public class GestureArrowUpward1 : NonRealTimeGestureParser
    {
        public GestureArrowUpward1()
        {
            Type = GestureType.ArrowUpward1;
            ExpectPathCount = 1;
        }
        public override int Parse(GesturePath[] paths)
        {
            var path = paths[0];
            var weight = 0;
            if (path.AllNormalizedVectors.Count != 2)
            {
                return -1;
            }
            if (path.InflectionPoints[2].x < path.InflectionPoints[0].x)
            {
                return -1;
            }
            // 权重归零
            weight = 0;
            var k1 = path.AllNormalizedVectors[0].y / path.AllNormalizedVectors[0].x;
            if (k1 > GestureConstant.tan30)
            {
                weight += 100;
            }
            var k2 = path.AllNormalizedVectors[1].y / path.AllNormalizedVectors[1].x;
            if (k2 < -GestureConstant.tan30)
            {
                weight += 100;
            }
            if (path.InflectionPoints[1].x > path.InflectionPoints[0].x && path.InflectionPoints[1].x < path.InflectionPoints[2].x)
            {
                weight += 100;
            }
            if (path.InflectionPoints[1].y > path.InflectionPoints[0].y && path.InflectionPoints[1].y > path.InflectionPoints[2].y)
            {
                weight += 100;
            }
            weight /= 4;
            return weight;
        }

        public override string ToString()
        {
            return "上箭头";
        }

    }
    public class GestureArrowBottom1 : NonRealTimeGestureParser
    {
        public GestureArrowBottom1()
        {
            Type = GestureType.ArrowDownward1;
            ExpectPathCount = 1;
        }

        public override int Parse(GesturePath[] paths)
        {
            var path = paths[0];
            var weight = 0;
            if (path.AllNormalizedVectors.Count != 2)
            {
                return -1;
            }
            if (path.InflectionPoints[2].x < path.InflectionPoints[0].x)
            {
                return -1;
            }
            // 权重归零
            weight = 0;
            var k1 = path.AllNormalizedVectors[0].y / path.AllNormalizedVectors[0].x;
            if (k1 < -GestureConstant.tan30)
            {
                weight += 100;
            }
            var k2 = path.AllNormalizedVectors[1].y / path.AllNormalizedVectors[1].x;
            if (k2 > GestureConstant.tan30)
            {
                weight += 100;
            }
            if (path.InflectionPoints[1].x > path.InflectionPoints[0].x && path.InflectionPoints[1].x < path.InflectionPoints[2].x)
            {
                weight += 100;
            }
            if (path.InflectionPoints[1].y < path.InflectionPoints[0].y && path.InflectionPoints[1].y < path.InflectionPoints[2].y)
            {
                weight += 100;
            }
            weight /= 4;
            return weight;
        }
        public override string ToString()
        {
            return "下箭头";
        }
    }
    public class GestureArrowLeftward1 : NonRealTimeGestureParser
    {
        public GestureArrowLeftward1()
        {
            Type = GestureType.ArrowLeftward1;
            ExpectPathCount = 1;
        }

        public override int Parse(GesturePath[] paths)
        {
            var path = paths[0];
            var weight = 0;
            if (path.AllNormalizedVectors.Count != 2)
            {
                return -1;
            }
            if (path.InflectionPoints[2].y > path.InflectionPoints[0].y)
            {
                return -1;
            }
            // 权重归零
            weight = 0;
            var k1 = path.AllNormalizedVectors[0].y / path.AllNormalizedVectors[0].x;
            if (k1 < GestureConstant.tan60 && k1 > 0)
            {
                weight += 100;
            }
            var k2 = path.AllNormalizedVectors[1].y / path.AllNormalizedVectors[1].x;
            if (k2 > -GestureConstant.tan60 && k2 < 0)
            {
                weight += 100;
            }
            if (path.InflectionPoints[1].y < path.InflectionPoints[0].y && path.InflectionPoints[1].y > path.InflectionPoints[2].y)
            {
                weight += 100;
            }
            if (path.InflectionPoints[1].x < path.InflectionPoints[0].x && path.InflectionPoints[1].x < path.InflectionPoints[2].x)
            {
                weight += 100;
            }
            weight /= 4;
            return weight;
        }

        public override string ToString()
        {
            return "左箭头";
        }
    }
    // 顺序右箭头
    public class GestureArrowRightward1 : NonRealTimeGestureParser
    {
        public GestureArrowRightward1()
        {
            this.Type = GestureType.ArrowRightward1;
            ExpectPathCount = 1;
        }

        public override int Parse(GesturePath[] paths)
        {
            // 权重归零
            var info = paths[0];
            var weight = 0;
            if (info.AllNormalizedVectors.Count != 2)
            {
                return -1;
            }
            if (info.InflectionPoints[2].y > info.InflectionPoints[0].y)
            {
                return -1;
            }
            var k1 = info.AllNormalizedVectors[0].y / info.AllNormalizedVectors[0].x;
            if (k1 > -GestureConstant.tan60 && k1 < 0)
            {
                weight += 100;
            }
            var k2 = info.AllNormalizedVectors[1].y / info.AllNormalizedVectors[1].x;
            if (k2 < GestureConstant.tan60 && k2 > 0)
            {
                weight += 100;
            }
            if (info.InflectionPoints[1].y < info.InflectionPoints[0].y && info.InflectionPoints[1].y > info.InflectionPoints[2].y)
            {
                weight += 100;
            }
            if (info.InflectionPoints[1].x > info.InflectionPoints[0].x && info.InflectionPoints[1].x > info.InflectionPoints[2].x)
            {
                weight += 100;
            }
            weight /= 4;
            return weight;
        }

        public override string ToString()
        {
            return "右箭头";
        }
    }
}
