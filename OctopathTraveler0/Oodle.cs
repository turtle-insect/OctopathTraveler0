using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace OctopathTraveler0
{
	// Special Thank's
	// UE_5.7\Engine\Source\Programs\Shared\EpicGames.Oodle\Oodle.cs

	internal class Oodle
	{
		[DllImport("oo2core_9_win64.dll", CallingConvention = CallingConvention.Cdecl)]
		// return file size?
		private static extern long OodleLZ_Compress(
			int compressor,
			byte[] rawBuf,
			long rawLen,
			byte[] compBuf,
			int level,
			long option,
			IntPtr dictionaryBase,
			IntPtr lrm,
			IntPtr scratchMem,
			long scratchSize
		);

		[DllImport("oo2core_9_win64.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern long OodleLZ_Decompress(
			byte[] compBuf,
			long compBufSize,
			byte[] rawBuf,
			long rawLen,
			int fuzzSafe,
			int checkCRC,
			int verbosity,
			IntPtr decBufBase,
			long decBufSize,
			long fpCallback,
			long callbackUserData,
			IntPtr decoderMemory,
			long decoderMemorySize,
			int threadPhase
		);

		[DllImport("oo2core_9_win64.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern long OodleLZ_GetCompressedBufferSizeNeeded(
			int compressor,
			long rawSize
		);

		public byte[] Compress(byte[] src)
		{
			const int compressor = 8;
			long size = OodleLZ_GetCompressedBufferSizeNeeded(compressor, src.Length);
			byte[] dest = new byte[size];
			size = OodleLZ_Compress(compressor, src, src.Length, dest, 9, 0, 0, 0, 0, 0);
			return dest[..(int)size];
		}

		public byte[] Decompress(byte[] src, int dest_length)
		{
			byte[] dest = new byte[dest_length];
			OodleLZ_Decompress(src, src.Length, dest, dest.Length, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3);
			return dest;
		}
	}
}
