import sys, os, io, datetime, json, re, pprint, time

import wiki_writer

# this requires 'mwclient' library, install it from the terminal using this command:
# pip install mwclient
from mwclient import Site

# this requires 'wikimarkup' library, install it from the terminal using this command:
# pip install py-wikimarkup
#from wikimarkup import parse

print "Logging in..."

#wiki_site = Site("kb.ibcos.co.uk", path="/w/")
#wiki_site.login("stevelamb", "bh49bb")

#

# log in
wiki_site = Site(("http", "localhost"), path="/mediawiki/")
wiki_site.login("steve", "V4l3n+!n4")

# prerec, pmfrec, pcdrec, pcgrec, ctfrec, cmfrec
definitions = {
    #"prerec": { },
    #"pmfrec": { },
    #"pcdrec": { },
    #"pcgrec": { },
    "ctfrec": { },
    #"cmfrec": { }
}


def get_property(properties, key, default = "NOT SET"):
    if properties.has_key(key):
        return properties[key]
    else:
        return default

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
        result = dict(result.items() + property_level.items())

    return (result, hierarchy_path)

def writer_record_type_definition(record_type):
    fields = {}
    wiki.start_page()

    wiki.write_header(2, "General", "general", False)
    json_path = "/Users/stevelamb/Development/ibcos/investigations/DDF/output/%s.json" % record_type
    with open(json_path) as json_definition:
        json_definition = json.load(json_definition)

    if json_definition["properties"].has_key("description"):
        definitions[record_type]["description"] = json_definition["properties"]["description"]
    else:
        definitions[record_type]["description"] = "UNKNOWN"

    wiki.write_paragraphs(definitions[record_type]["description"])

    wiki.write_header(2, "Fields", "fields")

    fields_table = wiki_writer.Table()
    fields_table.add_header(["Name", "Description", "Type", "Size", "Inheritence" ])

    for field in json_definition["properties"]["fields"]:
        (properties, hierarchy_path) = merge_properties(field["properties"])
        field_name = get_property(properties, "name")

        fields[field_name] = properties

        path = ""
        for path_level in hierarchy_path:
            path = "%s > [[DDFReference_Template_%s|%s]]" % (path, path_level, path_level)
        fields_table.add_row(
            { 
                "name": "[[DDFReference_Field_%s|%s]]" % (field_name, field_name),
                "description": get_property(properties, "description"),
                "type": get_property(properties, "fieldtype"),
                "size": get_property(properties, "size"),
                "inheritance": path 
            }, ["name", "description", "type", "size", "inheritance"])

        if properties.has_key("longdescription"):
            fields_table.add_row({ "title": "Long Description", "value": "<br/>".join(properties["longdescription"])}, ["title", { "name": "value", "colspan": 4 }])
    
    fields_table.add_footer()

    wiki.write_paragraphs(fields_table.to_string())
    wiki.write_paragraphs("[[DDFReference_Index||<< Index]]")

    wiki.update_wiki_page("DDFReference_%s" % record_type.upper())
    #time.sleep(2)

    for field in fields:
        write_template_definition(field, "Field", fields[field], "[[DDFReference_%s|<< %s]]" % (record_type.upper(), record_type.upper()))

def property_compare(o1, o2):
    if o1["name"] == "name":
        return -1
    else:
        if o1["name"] < o2["name"]:
            return -1
        elif o1["name"] > o2["name"]:
            return 1
        else:
            return 0

def properties_list(properties):
    result = []

    for property_item in properties:
        result.append({ "name": property_item, "value": properties[property_item]})

    #result.sort(cmp = lambda o1, o2: property_compare(o1, o2))
    result.sort(key = lambda o: o["name"])
    return result

def write_template_definition(name, output_type, properties, back_link = None):
    print "Updating Template/Field %s - %s" % (output_type, name)
    (properties, hierarchy_path) = merge_properties(properties)
    wiki.start_page()
    wiki.write_header(2, "Properties", "properties")

    if properties.has_key("description"):
        wiki.write_paragraphs(properties["description"])

    if properties.has_key("type"):
        del properties["type"]
  
    if properties.has_key("parent"):
        properties["templatename"] = "[[DDFReference_Template_%s|%s]]" % (properties["parent"]["name"], properties["parent"]["name"])
        del properties["parent"]
    if properties.has_key("template"):
        properties["templatename"] = "[[DDFReference_Template_%s|%s]]" % (properties["template"]["name"], properties["template"]["name"])
        del properties["template"]

    if properties.has_key("templatename"):
        wiki.write_paragraphs("Descended from %s." % (properties["templatename"]))

    properties_table = wiki_writer.Table()
    properties_table.add_header(["Property", "Value" ])
    for property_item in properties_list(properties):
        value = property_item["value"]

        if type(value) == dict:
            sub_table = wiki_writer.Table()
            sub_table.add_header()

            for sub_property_item in properties_list(value):
                sub_table.add_row([sub_property_item["name"], sub_property_item["value"]])
            sub_table.add_footer()

            properties_table.add_row({
                "property": property_item["name"],
                "value": sub_table.to_string()
            }, ["property", "value"])


        else:
            if type(value) == list:
                value = "<br/>".join(value)

            properties_table.add_row({
                "property": property_item["name"],
                "value": value
            }, ["property", "value"])

    properties_table.add_footer()

    wiki.write_paragraphs(properties_table.to_string())
    if back_link != None:
        wiki.write_paragraphs(back_link)

    wiki.update_wiki_page("DDFReference_%s_%s" % (output_type, name))

wiki = wiki_writer.WikiWriter(wiki_site)

for definition_name in definitions:
    writer_record_type_definition(definition_name)


wiki.start_page()
table = wiki_writer.Table()
table.add_header(["File", "Types", "Description"])
for definition_name in definitions:
    definition_name = definition_name.upper()
    table.add_row({ "file": "%s_FILE" % definition_name[:2], "types": "[[DDFReference_%s|%s]]" % (definition_name, definition_name), "description": definitions[definition_name.lower()]["description"]}, ["file", "types", "description"])
table.add_footer()

wiki.write_paragraphs(table.to_string())
wiki.update_wiki_page("DDFReference_Index")

for template in templates:
    write_template_definition(template, "Template", templates[template])



print ""
print "DONE"
#pprint.pprint(extensionLinks)