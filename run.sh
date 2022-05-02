#!/bin/bash

# replace with your terminal
term_exec=/bin/kitty

server_path=$HOME/.cache/nvim/com.unity.server.pipe

if [ -e $server_path ]; then
    # open file in server
    $term_exec -e nvim --server $server_path --remote "$@"
else
    # start the server if its pipe doesn't exist
    $term_exec -e nvim --listen $server_path "$@"
fi
