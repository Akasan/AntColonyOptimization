#!/bin/bash

wget -w 1 -r -l 0 http://elib.zib.de/pub/mp-testdata/tsp/tsplib/tsp
mkdir dataset
cp elib.zib.de/pub/mp-testdata/tsp/tsplib/tsp/* dataset/
rm -rf elib.zib.de
