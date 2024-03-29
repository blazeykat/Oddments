﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InControl;
using System.IO;
using UnityEngine;
namespace BindingAPI
{
	class BindingLoader : MonoBehaviour
	{
		public static string BindingDataLoc;
		public static void SaveBindings()
		{
			if (!File.Exists(BindingDataLoc))
				File.Create(BindingDataLoc).Dispose();

			File.WriteAllText(BindingDataLoc, BindingBuilder.Instance.Save());

		}


		public static void LoadBindings()
		{
			if (!File.Exists(BindingDataLoc))
				File.Create(BindingDataLoc).Dispose();

			BindingBuilder.Instance.Load(File.ReadAllText(BindingDataLoc));
		}

		public void OnApplicationQuit()
		{
			SaveBindings();
		}
	}
}
