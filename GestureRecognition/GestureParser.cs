using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestureRecognition
{
    public abstract class GestureParser
    {
        public GestureType Type { get; set; }
    }

    public abstract class RealTimeGestureParser : GestureParser
    {
        
    }
    public class RealTimeGestureParserSinglePath : RealTimeGestureParser
    {

    }

    public class RealTimeGestureParserDoublePath : RealTimeGestureParser
    {

    }

    public abstract class NonRealTimeGestureParser : GestureParser
    {
        // 期望路径个数
        public int ExpectPathCount { get; set; }
        // 解析路径，返回权重
        public abstract int Parse(GesturePath[] paths);
    }
}
