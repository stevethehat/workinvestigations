import sys, os, io, datetime, json, re, pprint, time

import wiki_writer

# this requires 'mwclient' library, install it from the terminal using this command:
# pip install mwclient
from mwclient import Site

# this requires 'wikimarkup' library, install it from the terminal using this command:
# pip install py-wikimarkup
#from wikimarkup import parse

print "Logging in..."

wiki_site = Site("kb.ibcos.co.uk", path="/w/")
wiki_site.login("stevelamb", "bh49bb")

#

# log in
#wiki_site = Site(("http", "localhost"), path="/mediawiki/")
#wiki_site.login("steve", "V4l3n+!n4")

# prerec, pmfrec, pcdrec, pcgrec, ctfrec, cmfrec
definitions = {
    "prerec": { },
    "pmfrec": { },
    "pcdrec": { },
    "pcgrec": { },
    "ctfrec": { },
    "cmfrec": { }
}


def get_property(properties, key, default = "NOT SET"):
    if properties.has_key(key):
        return properties[key]
    else:
        return default

def copy_properties(properties):
    pass

templates = {}
def add_template(name, properties):
    if not(templates.has_key(name)):
        templates[name] = properties

def merge_properties(properties):
    hierarchy = []
    hierarchy_path = []
    result = {}
    property_level = properties

    hierarchy.append(properties)

    while property_level.has_key("template") or property_level.has_key("parent"):        
        if property_level.has_key("template"):
            name = property_level["template"]["name"]
            hierarchy_path.append(name)
            property_level = property_level["template"]["structure"]["properties"]

            add_template(name, property_level)
        elif property_level.has_key("parent"):
            name = property_level["parent"]["name"]
            hierarchy_path.append(name)
            property_level = property_level["parent"]["structure"]["properties"]
            add_template(name, property_level)

        hierarchy.append(property_level)

    for property_level in reversed(hierarchy):
        # = result.update(property_level)
        result = dict(result.items() + property_level.items())


    #print result
    return (result, hierarchy_path)

def writer_record_type_definition(record_type):
    fields = {}
    wiki_writer.start_page()
    wiki_writer.write_page_header()
    wiki_writer.write_header(2, "Fields")

    json_path = os.path.join("output", "%s.json" % record_type)
    #json_path = "/Users/stevelamb/Development/ibcos/investigations/DDF/output/%s.json" % record_type
    with open(json_path) as json_definition:
        json_definition = json.load(json_definition)

    if json_definition["properties"].has_key("description"):
        definitions[record_type]["description"] = json_definition["properties"]["description"]
    else:
        definitions[record_type]["description"] = "UNKNOWN"

    wiki_writer.write_paragraphs(definitions[record_type]["description"])

    wiki_writer.create_table_header(["Name", "Description", "Type", "Size", "Inheritence" ])

    for field in json_definition["properties"]["fields"]:
        (properties, hierarchy_path) = merge_properties(field["properties"])
        field_name = get_property(properties, "name")

        fields[field_name] = properties

        path = ""
        for path_level in hierarchy_path:
            path = "%s > [[DDFReference_Template_%s|%s]]" % (path, path_level, path_level)
        wiki_writer.create_table_row(
            { 
                "name": "[[DDFReference_Field_%s|%s]]" % (field_name, field_name),
                "description": get_property(properties, "description"),
                "type": get_property(properties, "fieldtype"),
                "size": get_property(properties, "size"),
                "inheritance": path 
            }, ["name", "description", "type", "size", "inheritance"])

        if properties.has_key("longdescription"):
            wiki_writer.create_table_row({ "title": "Long Description", "value": "<br/>".join(properties["longdescription"])}, ["title", { "name": "value", "colspan": 4 }])
    
    wiki_writer.create_table_footer()
    wiki_writer.update_wiki_page("DDFReference_%s" % record_type.upper())
    time.sleep(2)

    for field in fields:
        write_template_definition(field, "Field", fields[field], "[[DDFReference_%s|<< %s]]" % (record_type.upper(), record_type.upper()))

def write_template_definition(name, output_type, properties, back_link = None):
    (properties, hierarchy_path) = merge_properties(properties)
    wiki_writer.start_page()
    wiki_writer.write_page_header()
    wiki_writer.write_header(2, "Properties")

    if properties.has_key("parent"):
        parent_name = properties["parent"]["name"]
        wiki_writer.write_paragraphs("Descended from [[DDFReference_Template_%s|%s]]." % (parent_name, parent_name))

    wiki_writer.create_table_header(["Property", "Value" ])
    if properties.has_key("methods"):
        del properties["methods"]
    if properties.has_key("type"):
        del properties["type"]
  
    if properties.has_key("parent"):
        properties["templatename"] = "[[DDFReference_Template_%s|%s]]" % (properties["parent"]["name"], properties["parent"]["name"])
        del properties["parent"]
    if properties.has_key("template"):
        properties["templatename"] = "[[DDFReference_Template_%s|%s]]" % (properties["template"]["name"], properties["template"]["name"])
        del properties["template"]

    for property_item in properties:
        wiki_writer.create_table_row({
            "property": property_item,
            "value": properties[property_item]
        }, ["property", "value"])

    wiki_writer.create_table_footer()
    if back_link != None:
        wiki_writer.write_paragraphs(back_link)

    wiki_writer.update_wiki_page("DDFReference_%s_%s" % (output_type, name))

    time.sleep(2)

wiki_writer = wiki_writer.WikiWriter(wiki_site)

for definition_name in definitions:
    writer_record_type_definition(definition_name)


wiki_writer.start_page()
wiki_writer.write_page_header()
wiki_writer.create_table_header(["File", "Types", "Description"])
for definition_name in definitions:
    definition_name = definition_name.upper()
    wiki_writer.create_table_row({ "file": "%s_FILE" % definition_name[:2], "types": "[[DDFReference_%s|%s]]" % (definition_name, definition_name), "description": definitions[definition_name.lower()]["description"]}, ["file", "types", "description"])
#wiki_writer.write_table_footer()
wiki_writer.update_wiki_page("DDFReference_Index")


for template in templates:
    write_template_definition(template, "Template", templates[template])



print ""
print "DONE"
#pprint.pprint(extensionLinks)