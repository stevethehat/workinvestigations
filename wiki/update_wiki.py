import sys, os, io, datetime, json, re, pprint

from queues import Queues
from controls import Controls
from actions import Actions
from jsonfiles import JSONFiles
from tables import Tables


# this requires 'mwclient' library, install it from the terminal using this command:
# pip install mwclient
from mwclient import Site

# this requires 'wikimarkup' library, install it from the terminal using this command:
# pip install py-wikimarkup
from wikimarkup import parse

wiki = Site('wiki.zenlogic.co.uk', path='/')

# log in
print "Logging in..."
wiki.login('Paul', 'il0ved0cumentati0n')



team_id = 'T3A2V6B25'
people = {
    "mike boyns" : "U3ALUFEE7",
    "Paul Hallett": "U3ALUFEE7",
    "Stuart Ross": "U3ALUFEE7",
    "Steve Lamb": "U3ALUFEE7",
    "Ant": "U3ALUFEE7",
    "Ant Cocking": "U3ALUFEE7",
    "Tony Feltham": "U3ALUFEE7"
}

    
extensionLinks = {
    "controls": {},
    "parameters": {},
    "actions": {},
    "tables": {}
}

class Links():
    def __init__(self):
        self.links = []

links = []

for person in people:
    links.append(
        {
            "name": person,
            "link": "slack://user?team=%s&id=%s" % (team_id, people[person]),
            "type": "user"
        }
    )


if len(sys.argv) > 1:
    options = sys.argv[1]
else:
    options = "qcavpt"

if options.find("t") != -1:
    tables = Tables(site = wiki, extensionLinks = extensionLinks)
    tables.process()

queues = Queues(links, extensionLinks, site = wiki)

if options.find("c") != -1:
    controls = Controls(links, extensionLinks, site = wiki)
    controls.process_inheritences()

if options.find("a") != -1:
    actions = Actions(links, extensionLinks, site = wiki)

if options.find("v") != -1:
    json_files = JSONFiles("../vcode.userportal%s" % "/app/views", "JSON Files", links)
    json_files.process()

if options.find("p") != -1:
    packages = JSONFiles("../vcode.userportal%s" % "/packages", "Packages", links)
    packages.process()

if options.find("c") != -1:
    controls.process()

if options.find("a") != -1:
    actions.process()


current = {}
if os.path.exists("extensionlinks.json"):
    with open("extensionlinks.json", "r") as extension_links_file:
        current = json.loads(extension_links_file.read())
    
    os.remove("extensionlinks.json")

if extensionLinks.has_key("tables") and len(extensionLinks["tables"]) > 0:
    current["tables"] = extensionLinks["tables"]

if extensionLinks.has_key("controls") and len(extensionLinks["controls"]) > 0:
    current["controls"] = extensionLinks["controls"]

if extensionLinks.has_key("views") and len(extensionLinks["views"]) > 0:
    current["views"] = extensionLinks["views"]

if extensionLinks.has_key("actions") and len(extensionLinks["actions"]) > 0:
    current["actions"] = extensionLinks["actions"]

if extensionLinks.has_key("parameters") and len(extensionLinks["parameters"]) > 0:
    current["parameters"] = extensionLinks["parameters"]

with open("extensionlinks.json", "w") as extension_links_file:
    extension_links_file.write(json.dumps(current))

print ""
print "DONE"
#pprint.pprint(extensionLinks)