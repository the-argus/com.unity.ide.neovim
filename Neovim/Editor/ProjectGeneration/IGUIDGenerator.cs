namespace Packages.Neovim.Editor.ProjectGeneration
{
	internal interface IGUIDGenerator
	{
		string ProjectGuid(string name);
	}
}
