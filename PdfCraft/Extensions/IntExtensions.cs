using System;

namespace PdfCraft.Extensions
{
    public static class IntExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="length">in inches</param>
        /// <returns></returns>
        public static int ToMillimeters(this int length)
        {
            return (int)(length * 2.54);
        }

        /// <summary>
        /// make divisible by 4, eg. to calculate the length of a 4byte aligned byte array
        /// </summary>
        public static UInt32 MakeAlignedBy4(this UInt32 value)
        {
            return (value + 0b0000_0000_0000_0000_0000_0000_0000_0011) & 0b1111_1111_1111_1111_1111_1111_1111_1100;
        }

        /// <summary>
        /// make divisible by 4, eg. to calculate the length of a 4byte aligned byte array
        /// </summary>
        public static UInt32 MakeAlignedBy4(this Int32 value)
        {
            var casted = (UInt32)value;

            return casted.MakeAlignedBy4();
        }
    }
}