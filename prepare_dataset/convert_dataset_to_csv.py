from glob import glob
import csv
import subprocess as sp


EXTENSION = [".tsp", ".tour"]

save_folder = "csv_dataset"
mkdir_command = f"mkdir {save_folder}"


def get_file_list():
    file_list = glob("./dataset/*")
    return file_list


def convert_to_csv(file_list):
    for fname in file_list:
        split_fname = fname.split(".")

        # optimal answer
        if len(split_fname) == 4:
            continue

        if split_fname[-1] == "tsp":
            position_data = []
            with open(fname, "r") as f:
                lines = f.readlines()
                for i, line in enumerate(lines):
                    if i < 6:
                        continue

                    split_line = line.split(" ")
                    split_line[-1] = split_line[-1].strip()

                    if not len(split_line) == 3:
                        break

                    try: 
                        x, y = float(split_line[1]), float(split_line[2])
                    except:
                        continue
                    position_data.append([x, y])
            
            new_filename = save_folder + "/" + split_fname[1].split("/")[-1] + ".csv"
            with open(new_filename, 'w') as f:
                writer = csv.writer(f)
                writer.writerows(position_data)

if __name__ == "__main__":
    file_list = get_file_list()
    #sp.call(mkdir_command.split())
    convert_to_csv(file_list)
