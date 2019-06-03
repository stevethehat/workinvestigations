import curses

class Window:
    def __init__(self, parent, title, top, left, width, height):
        self.width = width
        self.height = height
        self.title = title
        self.window = parent.subwin(height, width, top, left)
        self.window.box()

        self.normal = curses.color_pair(3)
        self.highlight = curses.color_pair(2)
        self.window.refresh()

    def output_line(self, line_no, line, colour):
        #line = line.encode("utf-8").decode("utf-8")

        line = line.rstrip("\n").rstrip("\r").rstrip("\t").rstrip(" ")
        

        #line = "{line: <{width}}".format(line=line, width=self.width - 20)
        if len(line) > self.width - 10:
            line = "@" + line[:self.width -10] 
        line = "%s- %s" % (len(line), line)
        self.window.addstr(line_no, 2, line, colour)
        

    def output_lines(self, lines, highlights = []):
        i = 2
        for line in lines:
            if i in highlights:
                self.output_line(i +1, line, self.highlight)
            else:
                self.output_line(i +1, line, self.normal)
                
            i += 1

        self.window.refresh()
