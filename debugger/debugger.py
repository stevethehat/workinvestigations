import sys, os, io, re
import curses
import socket
import curses_util



class Debugger:
    def __init__(self):
        self.current_input = ""
        # "/Users/stevelamb/Development/ibcos/investigations
        self.root_directory = "."

    def init(self, stdscr):
        self.socket = None
        curses.start_color()
        curses.init_pair(1, curses.COLOR_WHITE, curses.COLOR_BLACK)
        curses.init_pair(2, curses.COLOR_WHITE, curses.COLOR_RED)
        curses.init_pair(3, curses.COLOR_BLACK, curses.COLOR_WHITE)

        self.output = open("out.log", "w")

        self.highlight = curses.color_pair(2)


        self.main_window = stdscr
        self.code = curses_util.Window(self.main_window, "Code...", 2, 2, 140, 50)
        self.variables = curses_util.Window(self.main_window, "Variables..", 2, 142, 80, 50)
        self.init_matches()
        self.init_socket()
        self.send_request("se st ov")
        self.send_request("s")
        self.send_request("b WHGINE")
        self.update(103)

    def goto(self, match):
        pass

    def init_matches(self):
        self.matches = []
        self.matches.append(
            {
                "regex": r'Break at (\d*) in ([A-Z]*) \(([A-Z]*\.[A-Z]*)\)',
                "func": self.goto
            }
        )
        self.matches.append(
            {
                "regex": r'Step to (\d*) in ([A-Z]*) \(([A-Z]*\.[A-Z]*)\)',
                "func": self.goto
            }
        )

    def init_socket(self):
        HOST = '172.16.128.21'          # The remote host
        #HOST = "127.0.0.1"
        PORT = 1024                 # The same port as used by the server
        self.socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.socket.connect((HOST, PORT))

    def close(self):
        if None != self.socket:
            self.socket.close()     
        self.output.close()

    def find_file(self, file_name):
        result = None
        for root, dirs, files in os.walk(self.root_directory):
            for file in files:
                if file == file_name:
                    result = os.path.join(root, file)

        return result

    def load_file(self, file_name):
        src_file = open(file_name, "rt")
        self.file_lines = src_file.readlines()
        src_file.close()

    def show_code(self, file_name, line):
        if None != file_name:
            self.load_file(file_name)

            self.code.output_lines(self.file_lines[line - 20 :line + 20], line -20, [21])
        else:
            self.code.output_lines(["file %s not found.." % file_name])

    def send_request(self, request):
        request = "%s\n" % request
        self.output.write(request)

        self.socket.sendall(request)
        response = self.socket.recv(4096).split('\n')
        #response = self.socket.makefile().readlines()

        #self.output.write(response)
        for line in response:
            self.variables.add_line(line)   

        return response     

    def process_current_input(self):
        return "arequset"

    def process_key(self, key):
        needs_update = False
        response = ""
        
        if key == 0:
            needs_update = True

        if key == 103:
            needs_update = True
            response = self.send_request("g")

        # step
        if key == 115 or key == 274:
            request = "s"
            response = self.send_request("s")

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

        self.variables.add_line(type(response))
        if [] != response and None != response:
            for response_line in response:
                for match in self.matches:
                    re_match = re.match(match["regex"], response_line)

                    if None != re_match:
                        self.variables.add_line("we have a match %s " % re_match.groups()[0])
                        self.show_code("wgd/WHGINE.DBL", int(re_match.groups()[0]))
                        #self.variables.add_line("we have a match")

            #self.output("GO %s\n" % line_no)
            #self.show_code("wgd/WHGINE.DBL", line_no)    
        return needs_update
    
    def update(self, key):
        needs_update = self.process_key(key)
        self.statusbar = "Press 'q' to exit {} {}".format(key, self.current_input)

        if needs_update:
            pass

        code_file_name = self.find_file("WHGINE.DBL")
        #self.variables.output_lines(["v1 = 2", "v2 = 'hello'"])

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
    debugger.close()

def main():
    print("starting")
    curses.wrapper(main_window)
    print("done")

if __name__ == "__main__":
    main()
