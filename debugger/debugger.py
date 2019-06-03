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
        self.code = curses_util.Window(self.main_window, "Code...", 2, 2, 140, 50)
        self.variables = curses_util.Window(self.main_window, "Variables..", 2, 142, 80, 50)

        self.update(0)





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

        self.code.output_lines(self.file_lines[line - 20 :line + 20], [9, 11, 18  ])

    def send_request(self, request):
        return "a response"

    def process_current_input(self):
        return "arequset"

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
        self.variables.output_lines(["v1 = 2", "v2 = 'hello'"])

        self.main_window.addstr(52, 0, self.statusbar, curses.color_pair(3))

        self.main_window.refresh()


def main_window(stdscr):
    k = 0

    stdscr.clear()
    stdscr.refresh()

    debugger = Debugger()
    debugger.init(stdscr)

    # Loop where k is the last character pressed
    while (k != ord('q')):
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