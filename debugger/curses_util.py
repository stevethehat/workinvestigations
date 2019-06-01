import curses

class Window:
    def __init__(self, parent, top, left, width, height):
        #print("t:%s l:%s w:%s h:%s" % (top, left, width, height))
        self.window = parent.subwin(height, width, top, left)
        self.window.box()

        self.highlight = curses.color_pair(2)
        self.window.refresh()

    def output_lines(self, lines, highlights = []):
        i = 2
        for line in lines:
            if i in highlights:
                self.window.addstr(i + 1, 2, line, self.highlight)
            else:
                self.window.addstr(i + 1, 2, line)
            i += 1

        self.window.refresh()
