import curses

class Window:
    def __init__(self, parent, title, top, left, width, height):
        self.width = width
        self.height = height
        self.title = title
        self.window = parent.subwin(height, width, top, left)
        self.window.box()

        self.current_lines = []
        self.normal = curses.color_pair(1)
        self.highlight = curses.color_pair(2)
        self.title = curses.color_pair(3)
        self.output_line(1, title, self.title)
        self.window.refresh()

    def output_line(self, line_no, line, colour):
        line = line.rstrip("\n").rstrip("\b").rstrip("\r").rstrip("\t").rstrip(" ")

        line = "{line:{width}.{width}}".format(line=line, width=self.width -4)
        self.window.addstr(line_no, 2, line, colour)
        
    def add_line(self, line):
        self.current_lines.append(line)
        self.output_lines(self.current_lines)
        
    def output_lines(self, lines, start_line = 1, highlights = []):
        self.current_lines = lines
        i = 2

        for line in lines:
            if i in highlights:
                self.output_line(i +1, "%s %s" % (start_line, line), self.highlight)
            else:
                self.output_line(i +1, "%s %s" % (start_line, line), self.normal)
                
            i += 1
            start_line += 1

        self.window.box()
        self.window.refresh()
