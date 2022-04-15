using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Packages.Neovim.Editor.ProjectGeneration;
using Unity.CodeEditor;
using UnityEditor;
using UnityEngine;

namespace Packages.Neovim.Editor
{
	[InitializeOnLoad]
	public class NeovimScriptEditor : IExternalCodeEditor
	{
		public CodeEditor.Installation[] Installations => installations;
        private static CodeEditor.Installation[] installations;

		private static IGenerator projectGeneration;
        
        private const string PackageName = "com.unity.ide.neovim";
        private const string NeovimLauncher = "run.sh";

        private static string launcherPath;

        static NeovimScriptEditor()
		{
		    CodeEditor.Register(new NeovimScriptEditor());
			NeovimScriptEditor.projectGeneration = new ProjectGeneration.ProjectGeneration();

            launcherPath = Path.GetFullPath(Path.Combine("Packages", PackageName, NeovimLauncher));
            installations = new CodeEditor.Installation[] 
            {
                new CodeEditor.Installation { Name = "Neovim", Path = launcherPath }
            };
		}

		public void OnGUI()
		{
			EditorGUILayout.LabelField("Generate .csproj files for:");
			EditorGUI.indentLevel++;
			SettingsButton(ProjectGenerationFlag.Embedded, "Embedded packages", "");
			SettingsButton(ProjectGenerationFlag.Local, "Local packages", "");
			SettingsButton(ProjectGenerationFlag.Registry, "Registry packages", "");
			SettingsButton(ProjectGenerationFlag.Git, "Git packages", "");
			SettingsButton(ProjectGenerationFlag.BuiltIn, "Built-in packages", "");
#if UNITY_2019_3_OR_NEWER
			SettingsButton(ProjectGenerationFlag.LocalTarBall, "Local tarball", "");
#endif
			SettingsButton(ProjectGenerationFlag.Unknown, "Packages from unknown sources", "");
			SettingsButton(ProjectGenerationFlag.PlayerAssemblies, "Player projects", "For each player project generate an additional csproj with the name 'project-player.csproj'");
			RegenerateProjectFiles();
			EditorGUI.indentLevel--;
		}

		void RegenerateProjectFiles()
		{
			var rect = EditorGUI.IndentedRect(EditorGUILayout.GetControlRect(new GUILayoutOption[] {}));
			rect.width = 252;
			if (GUI.Button(rect, "Regenerate project files"))
			{
				projectGeneration.Sync();
			}
		}

		void SettingsButton(ProjectGenerationFlag preference, string guiMessage, string toolTip)
		{
			var prevValue = projectGeneration.AssemblyNameProvider.ProjectGenerationFlag.HasFlag(preference);
			var newValue = EditorGUILayout.Toggle(new GUIContent(guiMessage, toolTip), prevValue);

			if (newValue != prevValue)
			{
				projectGeneration.AssemblyNameProvider.ToggleProjectGeneration(preference);
			}
		}

		public void SyncIfNeeded(string[] addedFiles, string[] deletedFiles, string[] movedFiles, string[] movedFromFiles,
			string[] importedFiles)
		{
			projectGeneration.SyncIfNeeded(addedFiles.Union(deletedFiles).Union(movedFiles).Union(movedFromFiles),
				importedFiles);
		}
		
		public void SyncAll()
		{
			AssetDatabase.Refresh();
			projectGeneration.SyncIfNeeded(new string[] { }, new string[] { });
		}
		
		public static void SyncIfNeeded(bool checkProjectFiles)
		{
			AssetDatabase.Refresh();
			projectGeneration.SyncIfNeeded(new string[] { }, new string[] { }, checkProjectFiles);
		}
		
		public void Initialize(string editorInstallationPath) { }

		public bool OpenProject(string path, int line, int column)
		{
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
              Arguments = $"\"+normal {line}G{column}|\" {path}",
              FileName = launcherPath,
              UseShellExecute = false,
              RedirectStandardOutput = true,
            };

            Process.Start(startInfo);

            return true;
        }

        public bool TryGetInstallationForPath(string editorPath, out CodeEditor.Installation installation)
		{
            installation = Installations.FirstOrDefault(install => install.Path == editorPath);
            return !string.IsNullOrEmpty(installation.Name);
		}
	}
}
