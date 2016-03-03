using System.Collections.Generic;
using System.Linq;
using GestureRecognition.Tools;
using UnityEngine;

namespace GestureRecognition
{
    public abstract class GestureRecognizer
    {

        public static string ShowGestureType(GestureType type)
        {
            switch (type)
            {
                default:
                    return "未定义手势";
            }
        }
        public static string ShowRealTimeGestureType(RealTimeGestureType type)
        {
            switch (type)
            {
                case RealTimeGestureType.Void:
                    return "空手势";
                case RealTimeGestureType.Stay1:
                    return "单指停留";
                case RealTimeGestureType.Stay2:
                    return "双指停留";
                case RealTimeGestureType.Stay3:
                    return "三指停留";
                case RealTimeGestureType.Upwards1:
                    return "单指向上";
                case RealTimeGestureType.Upwards2:
                    return "双指向上";
                case RealTimeGestureType.Upwards3:
                    return "三指向上";
                case RealTimeGestureType.Downwards1:
                    return "单指向下";
                case RealTimeGestureType.Downwards2:
                    return "双指向下";
                case RealTimeGestureType.Downwards3:
                    return "三指向下";
                case RealTimeGestureType.Rightwards1:
                    return "单指向右";
                case RealTimeGestureType.Rightwards2:
                    return "双指向右";
                case RealTimeGestureType.Rightwards3:
                    return "三指向右";
                case RealTimeGestureType.Leftwards1:
                    return "单指向左";
                case RealTimeGestureType.Leftwards2:
                    return "双指向左";
                case RealTimeGestureType.Leftwards3:
                    return "三指向左";
                case RealTimeGestureType.Diagonal45_1:
                    return "单指斜45度";
                case RealTimeGestureType.Diagonal45_2:
                    return "双指斜45度";
                case RealTimeGestureType.Diagonal45_3:
                    return "三指斜45度";
                case RealTimeGestureType.Diagonal135_1:
                    return "单指斜135度";
                case RealTimeGestureType.Diagonal135_2:
                    return "双指斜135度";
                case RealTimeGestureType.Diagonal135_3:
                    return "三指斜135度";
                case RealTimeGestureType.Diagonal225_1:
                    return "单指斜225度";
                case RealTimeGestureType.Diagonal225_2:
                    return "双指斜225度";
                case RealTimeGestureType.Diagonal225_3:
                    return "三指斜225度";
                case RealTimeGestureType.Diagonal315_1:
                    return "单指斜315度";
                case RealTimeGestureType.Diagonal315_2:
                    return "双指斜315度";
                case RealTimeGestureType.Diagonal315_3:
                    return "三指斜315度";
                case RealTimeGestureType.Pinch:
                    return "双指聚拢";
                case RealTimeGestureType.Expand:
                    return "双指张开";
                default:
                    return "未定义手势";
            }
        }

        public static string ShowNonRealTimeGestureType(GestureType type)
        {
            switch (type)
            {
                case GestureType.ArrowUpward1:
                    return "顺序左箭头";
                case GestureType.ArrowUpward2:
                    return "逆序左箭头";
                default:
                    return "空手势";
            }
        }

        public static string ShowInflectionPointFeature(InflectionPointFeature feature)
        {
            switch (feature)
            {
                case InflectionPointFeature.Unknown:
                    return "未定义";
                case InflectionPointFeature.Start:
                    return "起点";
                case InflectionPointFeature.Straight:
                    return "直线";
                case InflectionPointFeature.ClockwiseAcuteAngle:
                    return "顺时针锐角";
                case InflectionPointFeature.ClockwiseRightAngle:
                    return "顺时针直角";
                case InflectionPointFeature.ClockwiseObtuseAngle:
                    return "顺时针钝角";
                case InflectionPointFeature.AntiClockwiseAcuteAngle:
                    return "逆时针锐角";
                case InflectionPointFeature.AntiClockwiseRightAngle:
                    return "逆时针直角";
                case InflectionPointFeature.AntiClockwiseObtuseAngle:
                    return "逆时针钝角";
                case InflectionPointFeature.Turn:
                    return "转向";
                case InflectionPointFeature.Break:
                    return "断开";
                case InflectionPointFeature.End:
                    return "终点";
                case InflectionPointFeature.TouchCountChanged:
                    return "触摸点改变";
            }
            return null;
        }
    }

    public class GestureRealTimeRecognizer : GestureRecognizer, IRealTimeGestureListener
    {
        private readonly IRealTimeGestureListener _listener;
        private readonly List<RealTimeGestureParser> _gestureParsers = new List<RealTimeGestureParser>();
        private readonly Queue<RealTimeGestureType> _gestureQueue = new Queue<RealTimeGestureType>();

        public GestureRealTimeRecognizer(IRealTimeGestureListener listener)
        {
            _listener = listener;
        }
        public void AddGestureParser(RealTimeGestureParser gestureParser)
        {
            if (_gestureParsers.All(parser => parser.Type != gestureParser.Type))
            {
                _gestureParsers.Add(gestureParser);
            }
        }

        public void RemoveGestureParser(RealTimeGestureParser gestureParser)
        {
            foreach (var parser in _gestureParsers.Where(parser => parser.Type == gestureParser.Type))
            {
                _gestureParsers.Remove(parser);
                break;
            }
        }

        public void RemoveAllParsers()
        {
            _gestureParsers.Clear();
        }

        public void Recognize(GesturePoint[] lastPoints, GesturePoint[] currentPoints)
        {

        }
        public void Recognize(GesturePoint lastPoint, GesturePoint point)
        {
            if (GesturePoint.DistanceSquare(lastPoint, point) < GestureManager.MinRealTimeOffset)
            {
                NotifyType(RealTimeGestureType.Stay1);
            }
            else
            {
                var slope = MathExtension.Slope(point - lastPoint);
                if (slope > GestureConstant.tan67p5 || slope < -GestureConstant.tan67p5)
                {
                    NotifyType(point.y > lastPoint.y ? RealTimeGestureType.Upwards1 : RealTimeGestureType.Downwards1);
                }
                else if (slope > GestureConstant.tan22p5)
                {
                    NotifyType(point.y > lastPoint.y ? RealTimeGestureType.Diagonal45_1 : RealTimeGestureType.Diagonal225_1);
                }
                else if (slope > -GestureConstant.tan22p5)
                {
                    NotifyType(point.x > lastPoint.x ? RealTimeGestureType.Rightwards1 : RealTimeGestureType.Leftwards1);
                }
                else
                {
                    NotifyType(point.x > lastPoint.x ? RealTimeGestureType.Diagonal315_1 : RealTimeGestureType.Diagonal135_1);
                }
            }
        }
        public void Recognize(GesturePoint lastPoint1, GesturePoint point1, GesturePoint lastPoint2, GesturePoint point2)
        {
            if (MathExtension.DistanceSquare(lastPoint1, point1) < GestureManager.MinRealTimeOffset)
            {
                if (MathExtension.DistanceSquare(lastPoint2, point2) < GestureManager.MinRealTimeOffset)
                {
                    NotifyType(RealTimeGestureType.Stay2);
                    return;
                }
                NotifyType(RealTimeGestureType.Void);
                return;
            }
            var vector1 = point1 - lastPoint1;
            var vector2 = point2 - lastPoint2;
            var angle = Vector2.Angle(vector1, vector2);
            if (angle > 120)
            {
                NotifyType(MathExtension.DistanceSquare(point1, point2) > MathExtension.DistanceSquare(lastPoint1, lastPoint2) ? RealTimeGestureType.Expand : RealTimeGestureType.Pinch);
            }
            else
            {
                var direction1 = MathExtension.JudgeDirection(lastPoint1, point1);
                var direction2 = MathExtension.JudgeDirection(lastPoint2, point2);
                if (direction1 != direction2) return;
                switch (direction1)
                {
                    case Direction.Up:
                        NotifyType(RealTimeGestureType.Upwards2);
                        break;
                    case Direction.Down:
                        NotifyType(RealTimeGestureType.Downwards2);
                        break;
                    case Direction.Left:
                        NotifyType(RealTimeGestureType.Leftwards2);
                        break;
                    case Direction.Right:
                        NotifyType(RealTimeGestureType.Rightwards2);
                        break;
                    case Direction.Diagonal45:
                        NotifyType(RealTimeGestureType.Diagonal45_2);
                        break;
                    case Direction.Diagonal135:
                        NotifyType(RealTimeGestureType.Diagonal135_2);
                        break;
                    case Direction.Diagonal225:
                        NotifyType(RealTimeGestureType.Diagonal225_2);
                        break;
                    case Direction.Diagonal315:
                        NotifyType(RealTimeGestureType.Diagonal315_2);
                        break;
                    default:
                        NotifyType(RealTimeGestureType.Void);
                        break;
                }
            }
        }

        public void Recognize(GesturePoint lastPoint1, GesturePoint point1, GesturePoint lastPoint2, GesturePoint point2, GesturePoint lastPoint3, GesturePoint point3)
        {
            if (MathExtension.DistanceSquare(lastPoint1, point1) < GestureManager.MinRealTimeOffset)
            {
                if (MathExtension.DistanceSquare(lastPoint2, point2) < GestureManager.MinRealTimeOffset)
                {
                    if (MathExtension.DistanceSquare(lastPoint3, point3) < GestureManager.MinRealTimeOffset)
                    {
                        NotifyType(RealTimeGestureType.Stay3);
                        return;
                    }
                }
                NotifyType(RealTimeGestureType.Void);
                return;
            }
            var direction1 = MathExtension.JudgeDirection(lastPoint1, point1);
            var direction2 = MathExtension.JudgeDirection(lastPoint2, point2);
            var direction3 = MathExtension.JudgeDirection(lastPoint3, point3);
            if (direction1 != direction2 || direction2 != direction3) return;
            switch (direction1)
            {
                case Direction.Up:
                    NotifyType(RealTimeGestureType.Upwards3);
                    break;
                case Direction.Down:
                    NotifyType(RealTimeGestureType.Downwards3);
                    break;
                case Direction.Left:
                    NotifyType(RealTimeGestureType.Leftwards3);
                    break;
                case Direction.Right:
                    NotifyType(RealTimeGestureType.Rightwards3);
                    break;
                case Direction.Diagonal45:
                    NotifyType(RealTimeGestureType.Diagonal45_3);
                    break;
                case Direction.Diagonal135:
                    NotifyType(RealTimeGestureType.Diagonal135_3);
                    break;
                case Direction.Diagonal225:
                    NotifyType(RealTimeGestureType.Diagonal225_3);
                    break;
                case Direction.Diagonal315:
                    NotifyType(RealTimeGestureType.Diagonal315_3);
                    break;
                default:
                    NotifyType(RealTimeGestureType.Void);
                    break;
            }
        }
        public void NotifyType(RealTimeGestureType type)
        {
            _listener.NotifyType(type);
        }

        public void NotifyState(InflectionPointFeature state)
        {
            _listener.NotifyState(state);
        }
    }

    public class GestureSemiRealTimeRecognizer : GestureRecognizer, ISemiRealTimeGestureListener
    {
        private readonly ISemiRealTimeGestureListener _listener;

        public GestureSemiRealTimeRecognizer(ISemiRealTimeGestureListener listener)
        {
            _listener = listener;
        }
        // 半实时手势通知
        public void SemiNotify(GesturePath[] paths, int touchCount)
        {
            _listener.SemiNotify(paths, touchCount);
        }

        public void OnTouchBegin(int touchCount)
        {
            _listener.OnTouchBegin(touchCount);
        }

        public void OnTouchStartMove(Vector2[] vectors, int touchCount)
        {
            _listener.OnTouchStartMove(vectors, touchCount);
        }

        public void OnTouchesCountChanged(int lastTouchesCount, int currentTouchesCount)
        {
            _listener.OnTouchesCountChanged(lastTouchesCount, currentTouchesCount);
        }

        public void OnTouchEnd()
        {
            _listener.OnTouchEnd();
        }
    }

    // 非实时手势识别器
    public class GestureNonRealTimeRecognizer : GestureRecognizer
    {
        private readonly INonRealTimeGestureListener _listener;

        // 存储解析器的字典，以解析器的期望路径数量作为键
        private readonly Dictionary<int, List<NonRealTimeGestureParser>> _parsers = new Dictionary<int, List<NonRealTimeGestureParser>>();

        // 最大期望向量数
        public const int MaxExpectVectorCount = 20;
        // 期望向量数组，根据解析器而定义，该集合在添加解析器时自动维护
        private readonly List<GestureType>[] _expectGestures = new List<GestureType>[MaxExpectVectorCount];

        public GestureNonRealTimeRecognizer(INonRealTimeGestureListener listener)
        {
            _listener = listener;
        }
        // 添加解析器
        public void AddParser(NonRealTimeGestureParser parser)
        {
            if (!_parsers.ContainsKey(parser.ExpectPathCount))
            {
                _parsers.Add(parser.ExpectPathCount, new List<NonRealTimeGestureParser>() { parser });
            }
            else
            {
                _parsers[parser.ExpectPathCount].Add(parser);
            }
        }
        // 移除解析器
        public void RemoveParser(GestureType type)
        {
            if (!_parsers.ContainsKey(GestureManager.GetExpectGestureCount(type)))
            {
                return;
            }
            _parsers[GestureManager.GetExpectGestureCount(type)].RemoveAll(parser => parser.Type == type);
        }

        public void NotifyInfo(GesturePath[] paths)
        {
            if (!_parsers.ContainsKey(paths.Length))
            {
                return;
            }
            NonRealTimeGestureParser result = null;
            var maxWeight = 0;
            foreach (var parser in _parsers[paths.Length])
            {
                var weight = parser.Parse(paths);
                if (weight < 60 || weight < maxWeight)
                {
                    continue;
                }
                maxWeight = weight;
                result = parser;
            }
            if (result != null) _listener.NonNotify(paths, result.Type);
        }

        // 在这个方法中对手势路径进行解析
        public GestureType Parse(GesturePath path)
        {
            return GestureType.Void;
        }
    }
}
