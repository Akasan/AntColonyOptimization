# How to prepare dataset

## Download from source
You can download benchmark problem from TSPLIB(http://elib.zib.de/pub/mp-testdata/tsp/tsplib/tsplib.html)

## Use wget
You can download all benchmark problem using `download_dataset.sh`
Please execute following command
```bash
bash download_dataset.sh
```
After executing this command, `dataset` folder will be generated.
After that, you can get dataset as csv format by calling below.
```bash
python convert_dataset_to_csv.py
```
After this, `csv_dataset` folder will be generated.
