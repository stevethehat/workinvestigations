import sys, os, io, datetime, json, re, pprint

import wiki_writer

# this requires 'mwclient' library, install it from the terminal using this command:
# pip install mwclient
from mwclient import Site

# this requires 'wikimarkup' library, install it from the terminal using this command:
# pip install py-wikimarkup
#from wikimarkup import parse

wiki_site = Site("kb.ibcos.co.uk", path="/w/")

# log in
print "Logging in..."
wiki_site.login("stevelamb", "bh49bb")

wiki_writer = wiki_writer.WikiWriter(wiki_site)

wiki_writer.start_page()
wiki_writer.write_page_header()
wiki_writer.write_git(".", "==", "../../priceupdates", max = 10)
wiki_writer.update_wiki_page("Price_Updates_Project_Readme")


print ""
print "DONE"
#pprint.pprint(extensionLinks)