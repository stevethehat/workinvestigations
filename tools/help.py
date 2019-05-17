import sys, re, argparse, os, subprocess

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
 
            block["lines"] += 1

        line_number += 1
    
    block_end(block)
    src_file.close()

    return {
        "lines": line_number,
        "functions": function_count
    }


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

def format_description(block):
    return "\n%s (%s)" % (block["description"].replace("\n", ""), block["lines"])

def outline_display(block):
    print(format_description(block))

def search_display(block):
    global function_match_count
    if len(block["matches"]) > 0:
        print(format_description(block))
        block["matches"].insert(0, "")
        print("\n\t".join(block["matches"]))
        function_match_count += 1

def search_all_display(block):
    global function_match_count

    if len(block["matches"]) > 0:
        found_all = True
        for search in block["searches"]:
            if 0 == search["count"]:
                found_all = False

        if found_all:
            print(format_description(block))
            block["matches"].insert(0, "")
            print("\n\t".join(block["matches"]))
            function_match_count += 1

def line_process_search(block, line_number, trimmed_src_line):
    for search in block["searches"]:
        match = re.search(search["search"], trimmed_src_line)
        lower_match = re.search(search["search"], trimmed_src_line.lower())

        if None != match or None != lower_match:
            block["matches"].append("%s - %s" % (line_number, trimmed_src_line))
            search["count"] += 1
 
def line_process_lineno(block, line_number, trimmed_src_line):
    if line_number == int(block["searches"][0]["search"]):
        block["matches"].append("%s - %s" % (line_number, trimmed_src_line))

result = None

if "outline" == mode:
    result = scan_file(file_name, outline_display, None)

if "findall" == mode:
    result = scan_file(file_name, search_all_display, line_process_search)

if "findany" == mode:
    result = scan_file(file_name, search_display, line_process_search)

if "finddef" == mode:
    result = subprocess.call("grep -irn '.define\s%s' %s" % (searches[0], file_name), shell=True)

if "finduse" == mode:
    result = subprocess.call("grep -irn 'call\s%s\|xcall\s%s' %s" % (searches[0], searches[0], file_name), shell=True)

if "line" == mode:
    result = scan_file(file_name, search_display, line_process_lineno)

print("%s lines" % result["lines"])
print("%s functions" % result["functions"])
print("%s functions matched" % function_match_count)
print("done!!")

