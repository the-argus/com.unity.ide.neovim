using System;
using UnityEditor;
using UnityEngine;

namespace Packages.Neovim.Editor
{
#if UNITY_2020_1_OR_NEWER // API doesn't exist in 2019.4
	[FilePath("ProjectSettings/NeovimScriptEditorPersistedState.asset", FilePathAttribute.Location.ProjectFolder)]
#endif
	internal class NeovimScriptEditorPersistedState : ScriptableSingleton<NeovimScriptEditorPersistedState>
	{
		[SerializeField] private long lastWriteTicks;

		public DateTime? LastWrite
		{
			get => DateTime.FromBinary(lastWriteTicks);
			set
			{
				if (!value.HasValue) return;
				lastWriteTicks = value.Value.ToBinary();
				Save(true);
			}
		}
	}
}
