﻿using System;
using System.Collections.Generic;

namespace PlantHunter.Mobile.Core.Controls
{
	public static class EnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
		{
			foreach (var item in enumeration)
			{
				action(item);
			}
		}
	}
}