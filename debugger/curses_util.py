import curses

class Window:
    def __init__(self, parent, title, top, left, width, height):
        self.width = width
        self.height = height
        self.title = title        
        self.window = parent.subwin(height, width, top, left)
        self.window.box()

        self.current_lines = []
        self.normal_color = curses.color_pair(1)
        self.highlight_color = curses.color_pair(2)
        self.title_color = curses.color_pair(3)
        self.show_title()
        self.window.refresh()

    def show_title(self):

        self.output_line(1, self.title, self.title_color)

    def output_line(self, line_no, line, colour):
        line = line.rstrip("\n").rstrip("\b").rstrip("\r").rstrip("\t").rstrip(" ")

        line = "{line:{width}.{width}}".format(line=line, width=self.width -4)
        self.window.addstr(line_no, 2, line, colour)
        
    def add_line(self, line):
        self.current_lines.append(line)
        if len(self.current_lines) > self.height - 3:
            self.current_lines = self.current_lines[-self.height - 3:]
        self.output_lines(self.current_lines)
        
    def output_lines(self, lines, start_line = 1, highlights = []):
        self.show_title()
        self.current_lines = lines
        i = 2

        for line in lines:
            if i in highlights:
                self.output_line(i +1, "%s %s" % (start_line, line), self.highlight_color)
            else:
                self.output_line(i +1, "%s %s" % (start_line, line), self.normal_color)
                
            i += 1
            start_line += 1

        self.window.box()
        self.window.refresh()
