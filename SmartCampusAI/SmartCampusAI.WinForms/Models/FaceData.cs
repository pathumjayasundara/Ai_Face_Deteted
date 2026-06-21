using System;

namespace SmartCampusAI.Models
{
    public class FaceData
    {
        public int      FaceID          { get; set; }
        public int      StudentID       { get; set; }
        public string   StudentName     { get; set; } = string.Empty;
        public string   FaceEncoding    { get; set; } = string.Empty;  // JSON float array
        public string   SampleImagePath { get; set; } = string.Empty;
        public DateTime CapturedAt      { get; set; }

        // Deserialized in memory only
        public float[]? EncodingArray   { get; set; }
    }
}
