﻿namespace FurryFeast.Extensions
{
	public static class ToByteArrayExtension
	{
		public static byte[] ToByteArray(this string source)
		{
			byte[] result = null;

			if (!string.IsNullOrWhiteSpace(source))
			{
				var outputLength = source.Length / 2;
				var output = new byte[outputLength];

				for (var i = 0; i < outputLength; i++)
				{
					output[i] = Convert.ToByte(source.Substring(i * 2, 2), 16);
				}
				result = output;
			}

			return result;
		}
	}
}
