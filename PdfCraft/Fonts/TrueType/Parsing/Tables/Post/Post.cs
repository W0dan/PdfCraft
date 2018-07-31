using System;
using FWord = System.Int16;

namespace PdfCraft.Fonts.TrueType.Parsing.Tables.Post
{
    public struct Post
    {
        public Fixed Format;
        public Fixed ItalicAngle;
        public FWord UnderlinePosition;
        public FWord UnderlineThickness;
        public UInt32 IsFixedPitch;
        public UInt32 MinMemType42;
        public UInt32 MaxMemType42;
        public UInt32 MinMemType1;
        public UInt32 MaxMemType1;

        public PostFormat2 Format2 { get; set; }
    }
}