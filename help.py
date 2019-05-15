import sys, re, argparse

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

src_file = open(file_name, "r")

line_number = 1
current_block = ""
current_block_mentions = []
function_count = 1
function_match_count = 0

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
        "lines": 0
    }

block = None

for src_line in src_file:
    trimmed_src_line = src_line.lstrip()

    block_start = False

    if trimmed_src_line.startswith("function") or trimmed_src_line.startswith(".function"):
        block_start = True
    if trimmed_src_line.startswith("subroutine") or trimmed_src_line.startswith(".subroutine"):
        block_start = True

    if block_start and current_block != "":
        function_count += 1
        if "outline" == mode:
            print("\n%s (%s)" % (current_block, block["lines"]))
        else:
            if len(current_block_mentions) > 0:
                print("\n%s (%s)" % (current_block, block["lines"]))
                current_block_mentions.insert(0, "")
                print("\n\t".join(current_block_mentions))
                current_block_mentions = []
                function_match_count += 1
        
    if block_start:
        block = new_block()
        current_block = "%s - %s" % (line_number, src_line)
        block["description"] = current_block

    if None != block:
        block["lines"] += 1
        for search in block["searches"]:
            match = re.search(search["search"], trimmed_src_line.lower())

            if None != match:
                current_block_mentions.append("%s - %s" % (line_number, trimmed_src_line))

    line_number += 1

src_file.close()

print("%s lines" % line_number)
print("%s functions" % function_count)
print("%s functions matched" % function_match_count)
print("done")

