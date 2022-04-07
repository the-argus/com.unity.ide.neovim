using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace Packages.Neovim.Editor.Util
{
	internal static class FileSystemUtil
	{
		public static string FileNameWithoutExtension(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return "";
			}

			var indexOfDot = -1;
			var indexOfSlash = 0;
			for (var i = path.Length - 1; i >= 0; i--)
			{
				if (indexOfDot == -1 && path[i] == '.')
				{
					indexOfDot = i;
				}

				if (indexOfSlash == 0 && path[i] == '/' || path[i] == '\\')
				{
					indexOfSlash = i + 1;
					break;
				}
			}

			if (indexOfDot == -1)
			{
				indexOfDot = path.Length;
			}

			return path.Substring(indexOfSlash, indexOfDot - indexOfSlash);
		}
		
		public static bool EditorPathExists(string editorPath)
		{
			return SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX && new DirectoryInfo(editorPath).Exists 
						 || SystemInfo.operatingSystemFamily != OperatingSystemFamily.MacOSX && new FileInfo(editorPath).Exists;
		}
	}
}
