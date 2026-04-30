using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace OctopathTraveler0
{
	// Special Thank's
	// https://github.com/Tamely/Oodle-Tools
	// https://github.com/mistydemeo/quickbms/blob/master/included/oodle.c

	internal class Oodle
	{
		[DllImport("oo2core_9_win64.dll", CallingConvention = CallingConvention.Cdecl)]
		// return file size?
		private static extern int OodleLZ_Compress(
			int algo,
			byte[] src_buffer,
			int src_length,
			byte[] dest_buffer,
			int comp_level,
			uint a,
			uint b,
			uint c
		);

		[DllImport("oo2core_9_win64.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int OodleLZ_Decompress(
			byte[] src_buffer,
			int src_length,
			byte[] dest_buffer,
			int dest_length,
			int a,
			int b,
			int c,
			IntPtr d,
			int e,
			IntPtr f,
			IntPtr g,
			IntPtr h,
			int i,
			int j
		);

		public byte[] Compress(byte[] src)
		{
			int size = getMaxSize(src.Length);
			byte[] dest = new byte[size];
			size = OodleLZ_Compress(8, src, src.Length, dest, 9, 0, 0, 0);
			return dest[..size];
		}

		public byte[] Decompress(byte[] src, int dest_length)
		{
			byte[] dest = new byte[dest_length];
			OodleLZ_Decompress(src, src.Length, dest, dest.Length, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3);
			return dest;
		}

		private int getMaxSize(int size)
		{
			return size + 274 * ((size + 0x3FFFF) / 0x400000);
		}
	}
}
