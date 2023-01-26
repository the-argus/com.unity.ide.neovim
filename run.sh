#!/bin/sh

echo "running with args ${file_arg}" >> $log

nvim-remote "$@"
