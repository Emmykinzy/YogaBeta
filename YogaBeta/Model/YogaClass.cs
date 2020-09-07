using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace YogaBeta.Model
{
    public class YogaClass
    {
        public int PoseDuration { get; set; }

        public int PrepDuration { get; set; }

        public string Shavasana { get; set; }

        public int ShavasanaLength { get; set; }

        public int TotalPoseMinutes
        { get { return PoseDuration * PoseList.Length; }}

        public int TotalPoseIntervalSeconds
        { get { return PrepDuration * (PoseList.Length -1); } }

        public int TotalClassTime
        { get { return (TotalPoseMinutes) + (TotalPoseIntervalSeconds / 60) + ShavasanaLength; }}

        public Poses[] PoseList { get; set; }

        public override string ToString() => JsonSerializer.Serialize<YogaClass>(this);
    }
}
