import sys, os, io, re
import curses
import socket
import curses_util



class Debugger:
    def __init__(self):
        self.current_input = ""
        # "/Users/stevelamb/Development/ibcos/investigations
        self.root_directory = "/home/steve/winc/ibcos/Repositorys/gold/source"

    def init(self, stdscr):
        self.socket = None
        curses.start_color()
        curses.init_pair(1, curses.COLOR_WHITE, curses.COLOR_BLACK)
        curses.init_pair(2, curses.COLOR_WHITE, curses.COLOR_RED)
        curses.init_pair(3, curses.COLOR_BLACK, curses.COLOR_WHITE)

        self.output_file = open("out.log", "w")

        self.highlight = curses.color_pair(2)

        self.main_window = stdscr
        
        self.code = curses_util.Window(self.main_window, " Code...", 2, 2, 140, 50)
        self.variables = curses_util.Window(self.main_window, " Variables..", 2, 142, 80, 19)
        self.stack = curses_util.Window(self.main_window, " Stack..", 21, 142, 80, 15)
        self.output = curses_util.Window(self.main_window, " Output..", 36, 142, 80, 16)
        
        self.init_matches()
        self.init_socket()

        response = self.send_request("se st ov")
        self.navigate(response)
        response = self.send_request("s")
        self.navigate(response)
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

    def close(self):
        if None != self.socket:
            self.socket.close()     
        self.output_file.close()
    

    def init_socket(self):
        HOST = '172.16.128.21'          # The remote host
        #HOST = "127.0.0.1"
        PORT = 1024  # The same port as used by the server
        try:
            self.socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            self.socket.connect((HOST, PORT))
        except:
            self.error = "No connection..."

    def send_request(self, request, output = True):
        request = "%s\n" % request
        self.output_file.write(request)
        self.error = None

        try:
            self.socket.sendall(request)
            response = self.socket.recv(4096).split('\n')

            if output:
                for line in response:
                    if "" != line:
                        self.output.add_line(line)   
        except:
            self.error = "Connection closed.."
            response = []

        return response     

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
        self.code.title = " code - %s" % file_name
        if None != file_name:
            self.load_file(file_name)

            self.code.output_lines(self.file_lines[line - 20 :line + 20], line -20, [21])
        else:
            self.code.output_lines(["file %s not found.." % file_name])

    def show_stack(self):
        return
        response = self.send_request("tr", output = False)
        self.stack.output_lines(response)

    def show_variables(self):
        pass
        
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

        self.navigate(response)
        return needs_update
    
    def navigate(self, response):
        if None != response:
            for response_line in response:
                if "" != response_line:
                    for match in self.matches:
                        re_match = re.match(match["regex"], response_line)

                        if None != re_match:
                            self.show_code(self.find_file(re_match.groups()[2]), int(re_match.groups()[0]))

    def update(self, key):
        needs_update = self.process_key(key)
        if None != self.error:
            self.statusbar = "{} Press 'q' to exit".format(self.error)
        else:
            self.statusbar = "Press 'q' to exit {} {}".format(key, self.current_input)

        self.main_window.addstr(52, 0, self.statusbar, curses.color_pair(3))

        self.show_stack()
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
