﻿using System;

namespace OpenPGP.Core
{
    /// <summary>
    /// Converts integer data types to arrays of bytes, and arrays of bytes to integer data types.
    /// </summary>
    public static class BigEndianBitConverter
    {
        /// <summary>
        /// Returns a 16-bit unsigned integer converted from two bytes at a specified position in a byte array.
        /// </summary>
        /// <param name="value">The array of bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="value" />.</param>
        /// <returns>A 32-bit unsigned integer formed by four bytes beginning at <paramref name="startIndex" />.</returns>
        [CLSCompliant(false)]
        public static ushort ToUInt16(byte[] value, int startIndex)
        {
            return (ushort) (value[startIndex] << 8 | value[startIndex + 1]);
        }

        /// <summary>
        /// Returns a 32-bit unsigned integer converted from four bytes at a specified position in a byte array.
        /// </summary>
        /// <param name="value">The array of bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="value" />.</param>
        /// <returns>A 32-bit unsigned integer formed by four bytes beginning at <paramref name="startIndex" />.</returns>
        [CLSCompliant(false)]
        public static uint ToUInt32(byte[] value, int startIndex)
        {
            return (uint)
                (value[startIndex] << 24 | 
                 value[startIndex+1] << 16 | 
                 value[startIndex + 2] << 8 |
                 value[startIndex + 3]);
        }

        /// <summary>
        /// Returns a 64-bit unsigned integer converted from eight bytes at a specified position in a byte array.
        /// </summary>
        /// <param name="value">The array of bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="value" />.</param>
        /// <returns>A 64-bit unsigned integer formed by four bytes beginning at <paramref name="startIndex" />.</returns>
        [CLSCompliant(false)]
        public static ulong ToUInt64(byte[] value, int startIndex)
        {
            return 
                ((ulong)ToUInt32(value, startIndex) << 32 | ToUInt32(value, startIndex + 4));
        }

        /// <summary>
        /// Returns the specified 16-bit unsigned integer value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 2.</returns>
        [CLSCompliant(false)]
        public static byte[] GetBytes(ushort value)
        {
            return new byte[]
                       {
                           (byte) (value >> 8),
                           (byte) (value & 0x00FF)
                       };
        }

        /// <summary>
        /// Returns the specified 32-bit unsigned integer value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 4.</returns>
        [CLSCompliant(false)]
        public static byte[] GetBytes(uint value)
        {
            return new byte[]
                       {
                           (byte) (value >> 24),
                           (byte) ((value & 0x00FF0000) >> 16),
                           (byte) ((value & 0x0000FF00) >> 8),
                           (byte) ((value & 0x000000FF))
                       };
        }
        
        /// <summary>
        /// Returns the specified 64-bit unsigned integer value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 8.</returns>
        [CLSCompliant(false)]
        public static byte[] GetBytes(ulong value)
        {
            var dword1 = GetBytes((uint) (value >> 32));
            var dword2 = GetBytes((uint) (value & 0x00000000FFFFFFFF));

            var result = new byte[8];
            Array.Copy(dword1, 0, result, 0, 4);
            Array.Copy(dword2, 0, result, 4, 4);

            return result;
        }
        
    }
}