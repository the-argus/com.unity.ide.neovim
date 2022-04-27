#!/bin/bash

nvr_exec=/bin/nvr
# replace with your terminal
term_exec=/bin/kitty

if [[ -n `$nvr_exec --serverlist | grep unity` ]]; then
    $nvr_exec --servername unity --remote-silent $@
else
    $term_exec -e ${nvr_exec} --servername unity --remote-silent $@
fi
