import sys, os, io, datetime, json, re, pprint

import wiki_writer

# this requires 'mwclient' library, install it from the terminal using this command:
# pip install mwclient
from mwclient import Site

# this requires 'wikimarkup' library, install it from the terminal using this command:
# pip install py-wikimarkup
#from wikimarkup import parse

#wiki_site = Site("kb.ibcos.co.uk", path="/w/")
wiki_site = Site(("http", "localhost"), path="/mediawiki/")

# log in
print "Logging in..."
wiki_site.login("steve", "V4l3n+!n4")

def get_property(properties, key, default = "NOT SET"):
    if properties.has_key(key):
        return properties[key]
    else:
        return default

def copy_properties(properties):
    pass

def merge_properties(properties):
    hierarchy = []
    hierarchy_path = ""
    result = {}
    property_level = properties

    hierarchy.append(properties)

    while property_level.has_key("template") or property_level.has_key("parent"):        
        if property_level.has_key("template"):
            hierarchy_path = "%s > %s" % (hierarchy_path, property_level["template"]["name"])
            property_level = property_level["template"]["structure"]["properties"]
        elif property_level.has_key("parent"):
            hierarchy_path = "%s > %s" % (hierarchy_path, property_level["parent"]["name"])
            property_level = property_level["parent"]["structure"]["properties"]

        hierarchy.append(property_level)

    for property_level in reversed(hierarchy):
        # = result.update(property_level)
        result = dict(result.items() + property_level.items())


    print result
    return (result, hierarchy_path)

wiki_writer = wiki_writer.WikiWriter(wiki_site)

wiki_writer.start_page()
wiki_writer.write_page_header()
wiki_writer.create_table_header(["File", "Types"])
wiki_writer.create_table_row({ "file": "PCG_FILE", "types": "[[DDFReference_PCGREC|PCGREC]]"}, ["file", "types"])
#wiki_writer.write_table_footer()
wiki_writer.update_wiki_page("DDFReference_Index")


def writer_record_type_definition(record_type):
    wiki_writer.start_page()
    wiki_writer.write_page_header()
    wiki_writer.write_header(2, "Fields")

    json_path = os.path.join("output", "%s.json" % record_type)
    json_path = "/Users/stevelamb/Development/ibcos/investigations/DDF/output/%s.json" % record_type
    with open(json_path) as json_definition:
        json_definition = json.load(json_definition)

    wiki_writer.create_table_header(["Name", "Description", "Type", "Size", "Inheritence" ])
    for field in json_definition["properties"]["fields"]:
        (properties, hierarchy_path) = merge_properties(field["properties"])

        wiki_writer.create_table_row(
            { 
                "name": get_property(properties, "name"), 
                "description": get_property(properties, "description"),
                "type": get_property(properties, "fieldtype"),
                "size": get_property(properties, "size"),
                "inheritance": hierarchy_path 
            }, ["name", "description", "type", "size", "inheritance"])

        if properties.has_key("longdescription"):
            wiki_writer.create_table_row({ "title": "Long Description", "value": "<br/>".join(properties["longdescription"])}, ["title", { "name": "value", "colspan": 2 }])

    wiki_writer.update_wiki_page("DDFReference_%s" % record_type.upper())


print ""
print "DONE"
#pprint.pprint(extensionLinks)