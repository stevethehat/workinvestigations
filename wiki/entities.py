import os

print "search for entities"

root_folder  = "/Users/stevelamb/Development/ibcos/priceupdates"

def process(directory, extension, processor):
    for _, _, files in os.walk(directory):
        for file in files:
            if file.endswith(extension):
                processor(file)

def process_ddf(file_name):
    print file_name

process(root_folder, ".DDF", process_ddf)