#!/bin/bash


nvr_exec= $HOME/.local/bin/nvr

if [[ -n `$nvr_exec --serverlist | grep unity` ]]; then
    $nvr_exec --servername unity --remote-silent $@
else
    # Replace `tilix -e` with your terminal
    tilix -e ${nvr_exec} --servername unity --remote-silent $@
fi
