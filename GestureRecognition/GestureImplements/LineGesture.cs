
using GestureRecognition.Tools;

namespace GestureRecognition.GestureImplements
{
    /// <summary>
    /// 一重向上直线
    /// </summary>
    public class GestureSingleLineUpward : NonRealTimeGestureParser
    {
        public GestureSingleLineUpward()
        {
            Type = GestureType.LineUpward;
            ExpectPathCount = 1;
        }
        public override int Parse(GesturePath[] paths)
        {
            var path = paths[0];
            var weight = 0;
            if (path.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path.AllNormalizedVectors[0]);
            if (k1 > -GestureConstant.tan67p5 || k1 < -GestureConstant.tan67p5)
            {
                weight += 50;
            }
            if (path.InflectionPoints[1].y > path.InflectionPoints[0].y)
            {
                weight += 50;
            }
            return weight;
        }
    }

    public class GestureDoubleLineUpward : NonRealTimeGestureParser
    {
        public GestureDoubleLineUpward()
        {
            Type = GestureType.DoubleLineUpward;
            ExpectPathCount = 2;
        }
        public override int Parse(GesturePath[] paths)
        {
            var path1 = paths[0];
            var path2 = paths[1];
            var weight = 0;
            if (path1.AllNormalizedVectors.Count != 1 || path2.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path1.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path2.AllNormalizedVectors[0]);
            if (k1 > -GestureConstant.tan67p5 || k1 < -GestureConstant.tan67p5)
            {
                weight += 25;
            }
            if (k2 > -GestureConstant.tan67p5 || k2 < -GestureConstant.tan67p5)
            {
                weight += 25;
            }
            if (path1.InflectionPoints[1].y > path1.InflectionPoints[0].y)
            {
                weight += 25;
            }
            if (path2.InflectionPoints[1].y > path2.InflectionPoints[0].y)
            {
                weight += 25;
            }
            return weight;
        }
    }

    public class GestureTripleLineUpward : NonRealTimeGestureParser
    {
        public GestureTripleLineUpward()
        {
            Type = GestureType.TripleLineUpward;
            ExpectPathCount = 3;
        }
        public override int Parse(GesturePath[] paths)
        {
            var path1 = paths[0];
            var path2 = paths[1];
            var path3 = paths[2];
            var weight = 0;
            if (path1.AllNormalizedVectors.Count != 1 || path2.AllNormalizedVectors.Count != 1 || path3.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path1.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path2.AllNormalizedVectors[0]);
            var k3 = MathExtension.Slope(path3.AllNormalizedVectors[0]);
            if (k1 > -GestureConstant.tan67p5 || k1 < -GestureConstant.tan67p5)
            {
                weight += 100;
            }
            if (k2 > -GestureConstant.tan67p5 || k2 < -GestureConstant.tan67p5)
            {
                weight += 100;
            }
            if (k3 > -GestureConstant.tan67p5 || k3 < -GestureConstant.tan67p5)
            {
                weight += 100;
            }
            if (path1.InflectionPoints[1].y > path1.InflectionPoints[0].y)
            {
                weight += 100;
            }
            if (path2.InflectionPoints[1].y > path2.InflectionPoints[0].y)
            {
                weight += 100;
            }
            if (path3.InflectionPoints[1].y > path3.InflectionPoints[0].y)
            {
                weight += 100;
            }
            weight /= 6;
            return weight;
        }
    }

    public class GestureSingleLineDownward : NonRealTimeGestureParser
    {
        public GestureSingleLineDownward()
        {
            Type = GestureType.LineDownward;
            ExpectPathCount = 1;
        }
        public override int Parse(GesturePath[] paths)
        {
            var path = paths[0];
            var weight = 0;
            if (path.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path.AllNormalizedVectors[0]);
            if (k1 > -GestureConstant.tan67p5 || k1 < -GestureConstant.tan67p5)
            {
                weight += 50;
            }
            if (path.InflectionPoints[1].y < path.InflectionPoints[0].y)
            {
                weight += 50;
            }
            return weight;
        }
    }

    public class GestureDoubleLineDownward : NonRealTimeGestureParser
    {
        public GestureDoubleLineDownward()
        {
            Type = GestureType.DoubleLineDownward;
            ExpectPathCount = 2;
        }
        public override int Parse(GesturePath[] paths)
        {
            var path1 = paths[0];
            var path2 = paths[1];
            var weight = 0;
            if (path1.AllNormalizedVectors.Count != 1 || path2.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path1.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path2.AllNormalizedVectors[0]);
            if (k1 > -GestureConstant.tan67p5 || k1 < -GestureConstant.tan67p5)
            {
                weight += 25;
            }
            if (k2 > -GestureConstant.tan67p5 || k2 < -GestureConstant.tan67p5)
            {
                weight += 25;
            }
            if (path1.InflectionPoints[1].y < path1.InflectionPoints[0].y)
            {
                weight += 25;
            }
            if (path2.InflectionPoints[1].y < path2.InflectionPoints[0].y)
            {
                weight += 25;
            }
            return weight;
        }
    }
    public class GestureTripleLineDownward : NonRealTimeGestureParser
    {
        public GestureTripleLineDownward()
        {
            Type = GestureType.TripleLineDownward;
            ExpectPathCount = 3;
        }
        public override int Parse(GesturePath[] paths)
        {
            var path1 = paths[0];
            var path2 = paths[1];
            var path3 = paths[2];
            var weight = 0;
            if (path1.AllNormalizedVectors.Count != 1 || path2.AllNormalizedVectors.Count != 1 || path3.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path1.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path2.AllNormalizedVectors[0]);
            var k3 = MathExtension.Slope(path3.AllNormalizedVectors[0]);
            if (k1 > -GestureConstant.tan67p5 || k1 < -GestureConstant.tan67p5)
            {
                weight += 100;
            }
            if (k2 > -GestureConstant.tan67p5 || k1 < -GestureConstant.tan67p5)
            {
                weight += 100;
            }
            if (k3 > -GestureConstant.tan67p5 || k3 < -GestureConstant.tan67p5)
            {
                weight += 100;
            }
            if (path1.InflectionPoints[1].y < path1.InflectionPoints[0].y)
            {
                weight += 100;
            }
            if (path2.InflectionPoints[1].y < path2.InflectionPoints[0].y)
            {
                weight += 100;
            }
            if (path3.InflectionPoints[1].y < path3.InflectionPoints[0].y)
            {
                weight += 100;
            }
            weight /= 6;
            return weight;
        }
    }
    public class GestureSingleLineLeftward : NonRealTimeGestureParser
    {
        public GestureSingleLineLeftward()
        {
            Type = GestureType.LineLeftward;
            ExpectPathCount = 1;
        }

        public override int Parse(GesturePath[] paths)
        {
            var path = paths[0];
            var weight = 0;
            if (path.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path.AllNormalizedVectors[0]);
            if (k1 > -GestureConstant.tan22p5 && k1 < GestureConstant.tan22p5)
            {
                weight += 50;
            }
            if (path.InflectionPoints[1].x < path.InflectionPoints[0].x)
            {
                weight += 50;
            }
            return weight;
        }
    }

    public class GestureDoubleLineLeftward : NonRealTimeGestureParser
    {
        public GestureDoubleLineLeftward()
        {
            Type = GestureType.DoubleLineLeftward;
            ExpectPathCount = 2;
        }
        public override int Parse(GesturePath[] paths)
        {
            var path1 = paths[0];
            var path2 = paths[1];
            var weight = 0;
            if (path1.AllNormalizedVectors.Count != 1 || path2.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path1.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path2.AllNormalizedVectors[0]);
            if (k1 > -GestureConstant.tan22p5 && k1 < GestureConstant.tan22p5)
            {
                weight += 25;
            }
            if (k2 > -GestureConstant.tan22p5 && k2 < GestureConstant.tan22p5)
            {
                weight += 25;
            }
            if (path1.InflectionPoints[1].x < path1.InflectionPoints[0].x)
            {
                weight += 25;
            }
            if (path2.InflectionPoints[1].x < path2.InflectionPoints[0].x)
            {
                weight += 25;
            }
            return weight;
        }
    }

    public class GestureTripleLineLeftward : NonRealTimeGestureParser
    {
        public GestureTripleLineLeftward()
        {
            Type = GestureType.TripleLineLeftward;
            ExpectPathCount = 3;
        }

        public override int Parse(GesturePath[] paths)
        {
            var path1 = paths[0];
            var path2 = paths[1];
            var path3 = paths[2];
            var weight = 0;
            if (path1.AllNormalizedVectors.Count != 1 || path2.AllNormalizedVectors.Count != 1 || path3.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path1.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path2.AllNormalizedVectors[0]);
            var k3 = MathExtension.Slope(path3.AllNormalizedVectors[0]);
            if (k1 > -GestureConstant.tan22p5 && k1 < GestureConstant.tan22p5)
            {
                weight += 100;
            }
            if (k2 > -GestureConstant.tan22p5 && k2 < GestureConstant.tan22p5)
            {
                weight += 100;
            }
            if (k3 > -GestureConstant.tan22p5 && k3 < GestureConstant.tan22p5)
            {
                weight += 100;
            }
            if (path1.InflectionPoints[1].x < path1.InflectionPoints[0].x)
            {
                weight += 100;
            }
            if (path2.InflectionPoints[1].x < path2.InflectionPoints[0].x)
            {
                weight += 100;
            }
            if (path3.InflectionPoints[1].x < path3.InflectionPoints[0].x)
            {
                weight += 100;
            }
            weight /= 6;
            return weight;
        }
    }
    public class GestureSingleLineRightward : NonRealTimeGestureParser
    {
        public GestureSingleLineRightward()
        {
            Type = GestureType.LineRightward;
            ExpectPathCount = 1;
        }

        public override int Parse(GesturePath[] paths)
        {
            var path = paths[0];
            var weight = 0;
            if (path.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path.AllNormalizedVectors[0]);
            if (k1 > -GestureConstant.tan22p5 && k1 < GestureConstant.tan22p5)
            {
                weight += 50;
            }
            if (path.InflectionPoints[1].x > path.InflectionPoints[0].x)
            {
                weight += 50;
            }
            return weight;
        }
    }

    public class GestureDoubleLineRightward : NonRealTimeGestureParser
    {
        public GestureDoubleLineRightward()
        {
            Type = GestureType.DoubleLineRightward;
            ExpectPathCount = 2;
        }
        public override int Parse(GesturePath[] paths)
        {
            var path1 = paths[0];
            var path2 = paths[1];
            var weight = 0;
            if (path1.AllNormalizedVectors.Count != 1 || path2.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path1.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path2.AllNormalizedVectors[0]);
            if (k1 > -GestureConstant.tan22p5 && k1 < GestureConstant.tan22p5)
            {
                weight += 25;
            }
            if (k2 > -GestureConstant.tan22p5 && k2 < GestureConstant.tan22p5)
            {
                weight += 25;
            }
            if (path1.InflectionPoints[1].x > path1.InflectionPoints[0].x)
            {
                weight += 25;
            }
            if (path2.InflectionPoints[1].x > path2.InflectionPoints[0].x)
            {
                weight += 25;
            }
            return weight;
        }
    }

    public class GestureTripleLineRightward : NonRealTimeGestureParser
    {
        public GestureTripleLineRightward()
        {
            Type = GestureType.TripleLineRightward;
            ExpectPathCount = 3;
        }

        public override int Parse(GesturePath[] paths)
        {
            var path1 = paths[0];
            var path2 = paths[1];
            var path3 = paths[2];
            var weight = 0;
            if (path1.AllNormalizedVectors.Count != 1 || path2.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path1.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path2.AllNormalizedVectors[0]);
            var k3 = MathExtension.Slope(path3.AllNormalizedVectors[0]);
            if (k1 > -GestureConstant.tan22p5 && k1 < GestureConstant.tan22p5)
            {
                weight += 100;
            }
            if (k2 > -GestureConstant.tan22p5 && k2 < GestureConstant.tan22p5)
            {
                weight += 100;
            }
            if (k3 > -GestureConstant.tan22p5 && k3 < GestureConstant.tan22p5)
            {
                weight += 100;
            }
            if (path1.InflectionPoints[1].x > path1.InflectionPoints[0].x)
            {
                weight += 100;
            }
            if (path2.InflectionPoints[1].x > path2.InflectionPoints[0].x)
            {
                weight += 100;
            }
            if (path3.InflectionPoints[1].x > path3.InflectionPoints[0].x)
            {
                weight += 100;
            }
            weight /= 6;
            return weight;
        }
    }
    public class GestureSingleLineDiagonal45 : NonRealTimeGestureParser
    {
        public GestureSingleLineDiagonal45()
        {
            Type = GestureType.LineDiagonal45;
            ExpectPathCount = 1;
        }

        public override int Parse(GesturePath[] paths)
        {
            var path = paths[0];
            var weight = 0;
            if (path.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path.AllNormalizedVectors[0]);
            if (k1 > GestureConstant.tan22p5 && k1 < GestureConstant.tan67p5)
            {
                weight += 50;
            }
            if (path.InflectionPoints[1].x > path.InflectionPoints[0].x)
            {
                weight += 50;
            }
            return weight;
        }
    }

    public class GestureDoubleLineDiagonal45 : NonRealTimeGestureParser
    {
        public GestureDoubleLineDiagonal45()
        {
            Type = GestureType.DoubleLineDiagonal45;
            ExpectPathCount = 2;
        }
        public override int Parse(GesturePath[] paths)
        {
            var path1 = paths[0];
            var path2 = paths[1];
            var weight = 0;
            if (path1.AllNormalizedVectors.Count != 1 || path2.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path1.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path2.AllNormalizedVectors[0]);
            if (k1 > GestureConstant.tan22p5 && k1 < GestureConstant.tan67p5)
            {
                weight += 25;
            }
            if (k2 > GestureConstant.tan22p5 && k2 < GestureConstant.tan67p5)
            {
                weight += 25;
            }
            if (path1.InflectionPoints[1].x > path1.InflectionPoints[0].x)
            {
                weight += 25;
            }
            if (path2.InflectionPoints[1].x > path2.InflectionPoints[0].x)
            {
                weight += 25;
            }
            return weight;
        }
    }

    public class GestureTripleLineDiagonal45 : NonRealTimeGestureParser
    {
        public GestureTripleLineDiagonal45()
        {
            Type = GestureType.TripleLineDiagonal45;
            ExpectPathCount = 3;
        }
        public override int Parse(GesturePath[] paths)
        {
            var path1 = paths[0];
            var path2 = paths[1];
            var path3 = paths[2];
            var weight = 0;
            if (path1.AllNormalizedVectors.Count != 1 || path2.AllNormalizedVectors.Count != 1 || path3.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path1.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path2.AllNormalizedVectors[0]);
            var k3 = MathExtension.Slope(path3.AllNormalizedVectors[0]);
            if (k1 > GestureConstant.tan22p5 && k1 < GestureConstant.tan67p5)
            {
                weight += 100;
            }
            if (k2 > GestureConstant.tan22p5 && k2 < GestureConstant.tan67p5)
            {
                weight += 100;
            }
            if (k3 > GestureConstant.tan22p5 && k3 < GestureConstant.tan67p5)
            {
                weight += 100;
            }
            if (path1.InflectionPoints[1].x > path1.InflectionPoints[0].x)
            {
                weight += 100;
            }
            if (path2.InflectionPoints[1].x > path2.InflectionPoints[0].x)
            {
                weight += 100;
            }
            if (path3.InflectionPoints[1].x > path3.InflectionPoints[0].x)
            {
                weight += 100;
            }
            return weight;
        }
    }
    public class GestureSingleLineDiagonal225 : NonRealTimeGestureParser
    {
        public GestureSingleLineDiagonal225()
        {
            Type = GestureType.LineDiagonal225;
            ExpectPathCount = 1;
        }

        public override int Parse(GesturePath[] paths)
        {
            var path = paths[0];
            var weight = 0;
            if (path.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path.AllNormalizedVectors[0]);
            if (k1 > GestureConstant.tan22p5 && k1 < GestureConstant.tan67p5)
            {
                weight += 50;
            }
            if (path.InflectionPoints[1].x < path.InflectionPoints[0].x)
            {
                weight += 50;
            }
            return weight;
        }
    }

    public class GestureDoubleLineDiagonal225 : NonRealTimeGestureParser
    {
        public GestureDoubleLineDiagonal225()
        {
            Type = GestureType.DoubleLineDiagonal225;
            ExpectPathCount = 2;
        }
        public override int Parse(GesturePath[] paths)
        {
            var path1 = paths[0];
            var path2 = paths[1];
            var weight = 0;
            if (path1.AllNormalizedVectors.Count != 1 || path2.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path1.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path2.AllNormalizedVectors[0]);
            if (k1 > GestureConstant.tan22p5 && k1 < GestureConstant.tan67p5)
            {
                weight += 25;
            }
            if (k2 > GestureConstant.tan22p5 && k2 < GestureConstant.tan67p5)
            {
                weight += 25;
            }
            if (path1.InflectionPoints[1].x < path1.InflectionPoints[0].x)
            {
                weight += 25;
            }
            if (path2.InflectionPoints[1].x < path2.InflectionPoints[0].x)
            {
                weight += 25;
            }
            return weight;
        }
    }

    public class GestureTripleLineDiagonal225 : NonRealTimeGestureParser
    {
        public GestureTripleLineDiagonal225()
        {
            Type = GestureType.TripleLineDiagonal225;
            ExpectPathCount = 3;
        }
        public override int Parse(GesturePath[] paths)
        {
            var path1 = paths[0];
            var path2 = paths[1];
            var path3 = paths[2];
            var weight = 0;
            if (path1.AllNormalizedVectors.Count != 1 || path2.AllNormalizedVectors.Count != 1 || path3.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path1.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path2.AllNormalizedVectors[0]);
            var k3 = MathExtension.Slope(path3.AllNormalizedVectors[0]);
            if (k1 > GestureConstant.tan22p5 && k1 < GestureConstant.tan67p5)
            {
                weight += 100;
            }
            if (k2 > GestureConstant.tan22p5 && k2 < GestureConstant.tan67p5)
            {
                weight += 100;
            }
            if (k3 > GestureConstant.tan22p5 && k3 < GestureConstant.tan67p5)
            {
                weight += 100;
            }
            if (path1.InflectionPoints[1].x < path1.InflectionPoints[0].x)
            {
                weight += 100;
            }
            if (path2.InflectionPoints[1].x < path2.InflectionPoints[0].x)
            {
                weight += 100;
            }
            if (path3.InflectionPoints[1].x < path3.InflectionPoints[0].x)
            {
                weight += 100;
            }
            weight /= 6;
            return weight;
        }
    }
    public class GestureSingleLineDiagonal135 : NonRealTimeGestureParser
    {
        public GestureSingleLineDiagonal135()
        {
            Type = GestureType.LineDiagonal135;
            ExpectPathCount = 1;
        }

        public override int Parse(GesturePath[] paths)
        {
            var path = paths[0];
            var weight = 0;
            if (path.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path.AllNormalizedVectors[0]);
            if (k1 < -GestureConstant.tan22p5 && k1 > -GestureConstant.tan67p5)
            {
                weight += 50;
            }
            if (path.InflectionPoints[1].x < path.InflectionPoints[0].x)
            {
                weight += 50;
            }
            return weight;
        }
    }

    public class GestureDoubleLineDiagonal135 : NonRealTimeGestureParser
    {
        public GestureDoubleLineDiagonal135()
        {
            Type = GestureType.DoubleLineDiagonal135;
            ExpectPathCount = 2;
        }

        public override int Parse(GesturePath[] paths)
        {
            var path1 = paths[0];
            var path2 = paths[1];
            var weight = 0;
            if (path1.AllNormalizedVectors.Count != 1 || path2.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path1.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path2.AllNormalizedVectors[0]);
            if (k1 < -GestureConstant.tan22p5 && k1 > -GestureConstant.tan67p5)
            {
                weight += 25;
            }
            if (k2 < -GestureConstant.tan22p5 && k2 > -GestureConstant.tan67p5)
            {
                weight += 25;
            }
            if (path1.InflectionPoints[1].x < path1.InflectionPoints[0].x)
            {
                weight += 25;
            }
            if (path2.InflectionPoints[1].x < path2.InflectionPoints[0].x)
            {
                weight += 25;
            }
            return weight;
        }
    }

    public class GestureTripleLineDiagonal135 : NonRealTimeGestureParser
    {
        public GestureTripleLineDiagonal135()
        {
            Type = GestureType.TripleLineDiagonal135;
            ExpectPathCount = 3;
        }
        public override int Parse(GesturePath[] paths)
        {
            var path1 = paths[0];
            var path2 = paths[1];
            var path3 = paths[2];
            var weight = 0;
            if (path1.AllNormalizedVectors.Count != 1 || path2.AllNormalizedVectors.Count != 1 || path3.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path1.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path2.AllNormalizedVectors[0]);
            var k3 = MathExtension.Slope(path3.AllNormalizedVectors[0]);
            if (k1 < -GestureConstant.tan22p5 && k1 > -GestureConstant.tan67p5)
            {
                weight += 100;
            }
            if (k2 < -GestureConstant.tan22p5 && k2 > -GestureConstant.tan67p5)
            {
                weight += 100;
            }
            if (k3 < -GestureConstant.tan22p5 && k3 > -GestureConstant.tan67p5)
            {
                weight += 100;
            }
            if (path1.InflectionPoints[1].x < path1.InflectionPoints[0].x)
            {
                weight += 100;
            }
            if (path2.InflectionPoints[1].x < path2.InflectionPoints[0].x)
            {
                weight += 100;
            }
            if (path3.InflectionPoints[1].x < path3.InflectionPoints[0].x)
            {
                weight += 100;
            }
            weight /= 6;
            return weight;
        }
    }
    public class GestureSingleLineDiagonal315 : NonRealTimeGestureParser
    {
        public GestureSingleLineDiagonal315()
        {
            Type = GestureType.LineDiagonal315;
            ExpectPathCount = 1;
        }

        public override int Parse(GesturePath[] paths)
        {
            var path = paths[0];
            var weight = 0;
            if (path.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path.AllNormalizedVectors[0]);
            if (k1 < -GestureConstant.tan22p5 && k1 > -GestureConstant.tan67p5)
            {
                weight += 50;
            }
            if (path.InflectionPoints[1].x > path.InflectionPoints[0].x)
            {
                weight += 50;
            }
            return weight;
        }
    }

    public class GestureDoubleLineDiagonal315 : NonRealTimeGestureParser
    {
        public GestureDoubleLineDiagonal315()
        {
            Type = GestureType.DoubleLineDiagonal315;
            ExpectPathCount = 2;
        }
        public override int Parse(GesturePath[] paths)
        {
            var path1 = paths[0];
            var path2 = paths[1];
            var weight = 0;
            if (path1.AllNormalizedVectors.Count != 1 || path2.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path1.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path2.AllNormalizedVectors[0]);
            if (k1 < -GestureConstant.tan22p5 && k1 > -GestureConstant.tan67p5)
            {
                weight += 25;
            }
            if (k2 < -GestureConstant.tan22p5 && k2 > -GestureConstant.tan67p5)
            {
                weight += 25;
            }
            if (path1.InflectionPoints[1].x > path1.InflectionPoints[0].x)
            {
                weight += 25;
            }
            if (path2.InflectionPoints[1].x > path2.InflectionPoints[0].x)
            {
                weight += 25;
            }
            return weight;
        }
    }

    public class GestureTripleLineDiagonal315 : NonRealTimeGestureParser
    {
        public GestureTripleLineDiagonal315()
        {
            Type = GestureType.TripleLineDiagonal315;
            ExpectPathCount = 3;
        }

        public override int Parse(GesturePath[] paths)
        {
            var path1 = paths[0];
            var path2 = paths[1];
            var path3 = paths[2];
            var weight = 0;
            if (path1.AllNormalizedVectors.Count != 1 || path2.AllNormalizedVectors.Count != 1)
            {
                return -1;
            }
            var k1 = MathExtension.Slope(path1.AllNormalizedVectors[0]);
            var k2 = MathExtension.Slope(path2.AllNormalizedVectors[0]);
            var k3 = MathExtension.Slope(path2.AllNormalizedVectors[0]);
            if (k1 < -GestureConstant.tan22p5 && k1 > -GestureConstant.tan67p5)
            {
                weight += 100;
            }
            if (k2 < -GestureConstant.tan22p5 && k2 > -GestureConstant.tan67p5)
            {
                weight += 100;
            }
            if (k3 < -GestureConstant.tan22p5 && k3 > -GestureConstant.tan67p5)
            {
                weight += 100;
            }
            if (path1.InflectionPoints[1].x > path1.InflectionPoints[0].x)
            {
                weight += 100;
            }
            if (path2.InflectionPoints[1].x > path2.InflectionPoints[0].x)
            {
                weight += 100;
            }
            if (path3.InflectionPoints[1].x > path3.InflectionPoints[0].x)
            {
                weight += 100;
            }
            weight /= 6;
            return weight;
        }
    }
}
