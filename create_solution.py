""" 
Joins given input_files to one file and reorganize usings.
Performs additional cleanings:
- none so far

Put the resulting code in output_path as well as in clipboard.
Usage:
- copy to solution folder
- set input_files and output_path
- run script
"""


input_files = ['MapRecoloring.cs', 'Program.cs', 'SolutionBase.cs', 'Solution.cs']
output_path = 'created_solution.cs'

def read_clean_file(path):
    data = open(path,"r").readlines()
    data[0] = data[0][3:]
    return data

def separate_using(lines):
    for i in range(len(lines)):
        l = lines[i].strip()
        if not (l.startswith("using") or l == ""):
            return lines[:i], lines[i:]
# polacz
data = [read_clean_file(f) for f in input_files]

using, code = zip(*[separate_using(p) for p in data])
usings = [k for u in using for k in u]
codes = [k for u in code for k in u]


# usun zbedne usingi
usings = list(set(usings))

all = usings + codes
open(output_path, "w").writelines(all)

from Tkinter import Tk
r = Tk()
r.withdraw()
r.clipboard_clear()
r.clipboard_append("".join(all))
r.destroy()