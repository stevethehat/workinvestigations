import sys, os, io, datetime, json, re

from mwclient import Site
# this requires 'gitpython' library, install it from the terminal using this command:
# pip install gitpython
import git

class WikiWriter:
    def __init__(self, site = None):
        self.site = site

        self.load()

    def load(self):
        pass

    def start_page(self):
        self.wiki_lines = []

    def update_wiki_page(self, page_name):
        # -------------------------------------------------------------------
        # concatenate all the wiki lines into a string, separated by newlines
        # -------------------------------------------------------------------
        #print self.wiki_lines
        wiki_text = "\n".join(self.wiki_lines)
        #wiki_text = "helo a generated page"

        #print wiki_text
        #print page_name

        # -------------------------------------------------------------------
        # write wiki text to a file named <controlname>.txt
        # -------------------------------------------------------------------
        # output_filename = "%s.txt" % control['objectName']
        # print "Writing wiki text for control '%s' to file '%s'..." % (control['objectName'], output_filename)

        # with io.open(output_filename, mode="wt", encoding="utf-8") as output_file:
        #     output_file.write(wiki_text)

        # -------------------------------------------------------------------
        # update the page in the wiki
        # -------------------------------------------------------------------
        page = self.site.pages[page_name]
        if page.text() == wiki_text:
            pass
            #print "Page content has not changed"
        else:
            #print "Updating Wiki page '%s'..." % page_name
            page.save(wiki_text, "Page auto-generated by script")
            #print "Page content updated"

        #self.write_tohtmlfile(wiki_text, "control_%s.html" % control_name)
        
    def write_page_footer(self, parent = None, source_file = None, source_documentation = None):
        # -------------------------------------------------------------------
        # create the auto-generated page warning
        # -------------------------------------------------------------------
        self.wiki_lines.append("''[This page was auto-generated by a script.  If you edit this page manually, your changes will be overwritten the next time the page is regenerated.]''")
        # -------------------------------------------------------------------
        # create the link back to the 'ZEN hierarchy' page
        # -------------------------------------------------------------------
        self.wiki_lines.append("")

      
    def create_table_header(self, cols):
        self.wiki_lines.append('{| class="wikitable"')

        for col in cols:
            self.wiki_lines.append('! %s' % col)

    def create_table_row(self, data, cols):
        self.wiki_lines.append('|-')

        for col in cols:
            value = ""
            if type(col) == dict:
                if data.has_key(col["name"]):           
                    value = data[col["name"]] 

                    #value = self.json_files.create_link_if_found(value)
                if col.has_key("colspan"):
                    self.wiki_lines.append('| colspan=%s | %s' % (col["colspan"], value))
                else:
                    self.wiki_lines.append('| %s' % value)
            else:
                if data.has_key(col):           
                    value = data[col] 

                    #value = self.json_files.create_link_if_found(value)
                self.wiki_lines.append('| %s' % value)

    def create_table_footer(self):
        self.wiki_lines.append('|}')
        
    def write_header(self, level, text, section_name = "autogenerated", add_qutogenerated_warning = True):
        self.wiki_lines.append("= %s = " % (text))
        self.wiki_lines.append("<!-- %s -->" % (section_name))
        self.wiki_lines.append("")
        if add_qutogenerated_warning:
            self.wiki_lines.append("<!-- ===== WARNING ===== -->")
            self.wiki_lines.append("<!-- This section is auto generated and is liable to be overwritten. -->")
            self.wiki_lines.append("<!-- To add content to this page create a new section and it will be preserved -->")
            self.wiki_lines.append("")


    def write_paragraphs(self, paragraphs):
        # create the example description paragraphs
        if type(paragraphs) == list:
            for paragraph in paragraphs:
                self.wiki_lines.append("%s" % self.create_link_if_found(paragraph))
                self.wiki_lines.append("")
        else:
            self.wiki_lines.append("%s" % self.create_link_if_found(paragraphs))
            self.wiki_lines.append("")
            
    def create_link_if_found(self, text):
        return text

    def write_table(self, columns, control, key, table_writer, row_writer, sort_key = "name"):
        self.create_table_header(columns)
        
        table_writer(control, False, key, row_writer, sort_key = sort_key)

        # end the table
        self.create_table_footer()

    def write_git_commit_table(self, header, repo_root, file_name, indent = "==", branch = "master", max = -1):
        repo = git.Repo(repo_root)
        commits = list(repo.iter_commits(branch, paths = "%s" % file_name ))

        commit_count = 0
        if len(commits) > 0:
            self.wiki_lines.append("%s %s %s" % (indent, header, indent))
            self.create_table_header(["Date and Time", "Author", "Message"])

            for commit in commits:
                formatted_date = datetime.datetime.fromtimestamp(commit.authored_date).strftime('%a %d %b %Y %H:%M:%S')
                self.wiki_lines.append("|-")
                self.wiki_lines.append("| %s" % formatted_date)
                self.wiki_lines.append("| %s" % commit.author)
                self.wiki_lines.append("| %s" % commit.summary)
                # print commit.summary

                if max != -1 and commit_count > max:
                    break

                commit_count = commit_count +1

            # end the table
            self.create_table_footer()

    def write_git(self, file_name, indent = "=", repo_root = "../", max = 10):
        self.write_git_commit_table("Git Commits", repo_root, file_name, indent, max = 10)

        

class Table:
    def __init__(self):
        self.lines = []

    def add_header(self, cols = None):
        self.lines.append('\n {| class="wikitable"')

        if cols:
            for col in cols:
                self.lines.append('! %s' % col)

    def add_row(self, data, cols = None):
        self.lines.append('|-')

        if cols:
            for col in cols:
                value = ""
                if type(col) == dict:
                    if data.has_key(col["name"]):           
                        value = data[col["name"]] 

                        #value = self.json_files.create_link_if_found(value)
                    if col.has_key("colspan"):
                        self.lines.append('| colspan=%s | %s' % (col["colspan"], value))
                    else:
                        self.lines.append('| %s' % value)
                else:
                    if data.has_key(col):           
                        value = data[col] 

                        #value = self.json_files.create_link_if_found(value)
                    self.lines.append('| %s' % value)
        else:
            for data_item in data:
                self.lines.append("|%s" % data_item)

    def add_footer(self):
        self.lines.append('|}')

    def write(self, wiki_writer):
        for line in self.lines:
            wiki_writer.wiki_lines.append(line)

    def to_string(self):
        return "\n".join(self.lines)