#!/bin/sh

log=$HOME/.cache/nvim/unity-server.log

flatpak_substring="com.unity.UnityHub"

# the classic stackoverflow "how get directory of script??"
script_dir=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )

if [ "${FLATPAK_SANDBOX_DIR#*$flatpak_substring}" ]; then
    # remove the weird thing that precedes filenames
    file_arg=$(echo "$@" | sed -e "s/+normal -1G-1| //g")
    # you need to run "sudo flatpak override com.unity.UnityHub --talk-name=org.freedesktop.Flatpak" for this to work
    flatpak-spawn --host $script_dir/nvim-remote "__UNITY_FLATPAK__ $file_arg" >> $log 2>&1
else
    $script_dir/nvim-remote "$@"
fi
