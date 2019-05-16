import sys, re, argparse


def new_search(search):
    return {
        "search": search,
        "count": 0
    }

def new_block():
    return {
        "name": "",
        "description": "",
        "searches": map(new_search, searches),
        "lines": 0,
        "matches": []
    }


def scan_file(file_name, block_end, line_process):
    block = None

    line_number = 1
    function_count = 1

    src_file = open(file_name, "r")

    for src_line in src_file:
        trimmed_src_line = src_line.lstrip()

        block_start = False

        if trimmed_src_line.startswith("function") or trimmed_src_line.startswith(".function"):
            block_start = True
        if trimmed_src_line.startswith("subroutine") or trimmed_src_line.startswith(".subroutine"):
            block_start = True

        if block_start and None != block:
            block_end(block)
            function_count += 1
 
        if block_start:
            block = new_block()
            block["description"] = "%s - %s" % (line_number, src_line)

 
        if None != block and None != line_process:
            line_process(block, line_number, trimmed_src_line)
 
        line_number += 1

    src_file.close()


parser = argparse.ArgumentParser()
parser.add_argument("mode", help="mode - outline, findall, findany")
parser.add_argument("file_name")
parser.add_argument("search_terms", nargs="*")
parser.add_argument("-o", "--output", help="output a breakpoints file")
args = parser.parse_args()

mode = args.mode
file_name = args.file_name
searches = args.search_terms

print("file = %s" % file_name)
print("searches = %s" % searches)

global function_match_count

function_match_count = 0

def outline_display(block):
    print("\nO:%s (%s)" % (block["description"], block["lines"]))

def search_display(block):
    global function_match_count
    if len(block["matches"]) > 0:
        print("\nS:%s (%s)" % (block["description"], block["lines"]))
        block["matches"].insert(0, "")
        print("\n\t".join(block["matches"]))
        function_match_count += 1

def line_process(block, line_number, trimmed_src_line):
    block["lines"] += 1
    for search in block["searches"]:
        match = re.search(search["search"], trimmed_src_line)
        lower_match = re.search(search["search"], trimmed_src_line.lower())

        if None != match or None != lower_match:
            block["matches"].append("%s - %s" % (line_number, trimmed_src_line))


if "outline" == mode:
    scan_file(file_name, outline_display, None)

if "findall" == mode:
    scan_file(file_name, search_display, line_process)

if "findany" == mode:
    scan_file(file_name, search_display, line_process)

#print("%s lines" % line_number)
#print("%s functions" % function_count)
print("%s functions matched" % function_match_count)
print("done")

