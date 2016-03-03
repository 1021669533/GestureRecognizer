using UnityEngine;

namespace GestureRecognition
{
    public class GestureConstant
    {
        public const float tan5 = 0.0874886635f;
        public const float tan10 = 0.1763269807f;
        public const float tan15 = 0.2679491924f;
        public const float tan20 = 0.3639702343f;
        public const float tan22p5 = 0.4142135624f;
        public const float tan25 = 0.4663076582f;
        public const float tan30 = 0.5773502692f;
        public const float tan35 = 0.7002075382f;
        public const float tan40 = 0.8390996312f;
        public const float tan45 = 1f;
        public const float tan50 = 1.1917535926f;
        public const float tan55 = 1.4281480067f;
        public const float tan60 = 1.7320508076f;
        public const float tan65 = 2.1445069205f;
        public const float tan67p5 = 2.4142135624f;
        public const float tan70 = 2.7474774195f;
        public const float tan75 = 3.7320508076f;
        public const float tan80 = 5.6712818196f;
        public const float tan85 = 11.4300523028f;

        public const float coss5 = 0.9924038765f;
        public const float coss10 = 0.9698463104f;
        public const float coss22p5 = 0.8535533906f;
        public const float coss30 = 0.75f;
        public const float coss45 = 0.5f;
        public const float coss60 = 0.25f;
        public const float coss67p5 = 0.1464466094f;
        public const float coss75 = 0.0669872981f;
        public const float coss90 = 0;
        public const float coss105 = -0.0669872981f;
        public const float coss112p5 = -0.1464466094f;
        public const float coss120 = -0.25f;
        public const float coss135 = -0.5f;
        public const float coss150 = -0.75f;
        public const float coss157p5 = -0.8535533906f;
        public const float coss170 = -0.9698463104f;

        public static readonly float ScreenHeightDiv2 = Screen.height / 2f;
        public static readonly float ScreenHeightDiv5 = Screen.height / 5f;
        public static readonly float ScreenHeightDiv10 = Screen.height / 10f;
        public static readonly float ScreenWidthDiv2 = Screen.height / 2f;
        public static readonly float ScreenWidthDiv5 = Screen.width / 5f;
        public static readonly float ScreenWidthDiv10 = Screen.width / 10f;
    }

    public class InputConstant
    {
        public const string MouseScrollWheel = "Mouse ScrollWheel";
    }
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        Diagonal45,
        Diagonal135,
        Diagonal225,
        Diagonal315
    }
}
