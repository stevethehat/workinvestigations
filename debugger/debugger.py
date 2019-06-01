import sys,os
import curses
import socket
import curses_util

class Debugger:
    def __init__(self):
        self.current_input = ""

    def init(self, stdscr):
        curses.start_color()
        curses.init_pair(1, curses.COLOR_WHITE, curses.COLOR_BLACK)
        curses.init_pair(2, curses.COLOR_WHITE, curses.COLOR_RED)
        curses.init_pair(3, curses.COLOR_BLACK, curses.COLOR_WHITE)

        self.highlight = curses.color_pair(2)


        self.main_window = stdscr
        self.s = socket.socket(
            socket.AF_INET, socket.SOCK_STREAM)
        self.s.connect(("127.0.0.1", 80))
        print("in init")
        self.current_input = ""

    def load_file(self, file_name):
        src_file = open(file_name, "rt")
        self.file_lines = src_file.readlines()
        src_file.close()

    def show_code(self, file_name, line):
        self.load_file(file_name)

        self.output_lines(self.code, self.file_lines[line - 20 :line + 20], [9, 11, 18  ])

    def send_request(self, request):
        return "a response"

    def process_current_input(self):
        return "arequset"

    def output_lines(self, window, lines, highlights = []):
        i = 2
        for line in lines:
            if i in highlights:
                window.addstr(i + 1, 2, line, self.highlight)
            else:
                window.addstr(i + 1, 2, line)
            i += 1

        
        window.addstr(i + 1, 1, "done.. %s lines.." % len(self.file_lines))
        window.refresh()

    def process_key(self, key):
        needs_update = False
        request = ""

        if key == 0:
            needs_update = True

        # step
        if key == 115 or key == 274:
            request = "s"
            needs_update = True

        # go
        if key == 269:
            needs_update = True

        if key == 10:
            request = self.process_current_input()
            needs_update = True


        if needs_update:
            self.current_input = ""
        else:
            self.current_input = self.current_input + curses.keyname(key)

        return needs_update

    
    def update(self, key):
        needs_update = self.process_key(key)
        self.statusbar = "Press 'q' to exit {} {}".format(key, self.current_input)

        if needs_update:
            pass

        self.show_code("/Users/stevelamb/Development/ibcos/investigations/WHGINE.DBL", 100)
            #self.output_lines(self.code, ["1", "hjhjhghjghjghj", "ygjhgjgjghj", "hgjhghgjhghj"])

        self.main_window.attron(curses.color_pair(3))
        self.main_window.addstr(52, 0, self.statusbar)
        #self.main_window.addstr(51, len(statusbarstr), " " * (width - len(statusbarstr) - 1))
        self.main_window.attroff(curses.color_pair(3))

        self.code.box()
        self.code.addstr(1, 1, "Code.", curses.color_pair(1))
        ##self.code.bkgd(' ', curses.color_pair(1))

        self.variables.box()

        self.variables.addstr(1, 1, "Variables.", curses.color_pair(1))
        self.variables.bkgd(' ', curses.color_pair(1))

        self.main_window.refresh()


def main_window(stdscr):
    k = 0
    cursor_x = 0
    cursor_y = 0

    stdscr.clear()
    stdscr.refresh()

    debugger = Debugger()
    debugger.init(stdscr)




    debugger.code = stdscr.subwin(50, 140, 2, 2)
    debugger.variables = stdscr.subwin(50, 80, 2, 142)

    # Loop where k is the last character pressed
    while (k != ord('q')):

        # Initialization
        stdscr.clear()
        height, width = stdscr.getmaxyx()

        if k == curses.KEY_DOWN:
            cursor_y = cursor_y + 1
        elif k == curses.KEY_UP:
            cursor_y = cursor_y - 1
        elif k == curses.KEY_RIGHT:
            cursor_x = cursor_x + 1
        elif k == curses.KEY_LEFT:
            cursor_x = cursor_x - 1

        cursor_x = max(0, cursor_x)
        cursor_x = min(width-1, cursor_x)

        cursor_y = max(0, cursor_y)
        cursor_y = min(height-1, cursor_y)

        # Declaration of strings
        statusbarstr = "Press 'q' to exit | STATUS BAR | Pos: {}, {}, Last Key {} {}".format(cursor_x, cursor_y, k, curses.keyname(k))
        if k == 0:
            keystr = "No key press detected..."[:width-1]

        start_y = int((height // 2) - 2)



        # Refresh the screen
        debugger.update(k)

        # Wait for next input
        k = stdscr.getch()

def main():
    print("starting")
    curses.wrapper(main_window)
    print("done")

if __name__ == "__main__":
    main()