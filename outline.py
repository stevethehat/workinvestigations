import sys

searches = []
if len(sys.argv) > 1:
    searches = sys.argv[1:]

print searches

src_file = open(sys.argv[1], "r")

line_number = 1
current_block = ""
current_block_mentions = []
function_count = 1

for src_line in src_file:
    trimmed_src_line = src_line.lstrip()

    block_start = False

    if trimmed_src_line.startswith("function") or trimmed_src_line.startswith(".function"):
        block_start = True
    if trimmed_src_line.startswith("subroutine") or trimmed_src_line.startswith(".subroutine"):
        block_start = True

    if block_start and current_block != "":
        function_count = function_count + 1
        if len(searches) == 0:
            print "\n%s" % current_block
        else:
            if len(current_block_mentions) > 0:
                print "\n%s" % current_block
                current_block_mentions.insert(0, "")
                print "\n\t".join(current_block_mentions)
                current_block_mentions = []
        
    if block_start:
        current_block = "%s - %s" % (line_number, src_line)
        #print "%s - %s" % (line_number, src_line)

    for search in searches:
        if trimmed_src_line.lower().find(search) != -1:
            current_block_mentions.append("%s - %s" % (line_number, trimmed_src_line))

    line_number = line_number +1

src_file.close()

print "%s functions" % function_count
print "done"