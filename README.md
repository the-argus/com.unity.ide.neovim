# Neovim in Unity

This package provides integration of neovim as an external editor in Unity.

# Installation

### Clone this repo
``git clone https://github.com/the-argus/com.unity.ide.neovim.git``

### Install the package
Open Unity. Go to Window > Package Manager and select the "+" sign at the top
left of the window. From the dropdown, select "Add package from disk". Browse
and select the ``package.json`` file at the root of this repo.

### Select Neovim as the editor
Go to Edit > Preferences > External Tools.
Set "External Script Editor" to "Neovim" in the dropdown menu. If it's not
present, then select "browse" and select the file ``run.sh`` at the root of
this repo.

### Customize run.sh
It's necessary to customize the ``run.sh`` file to fit your needs. Open it and
make sure the ``term_exec`` variable matches your own terminal emulator.
