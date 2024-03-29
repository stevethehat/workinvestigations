import sys, os, io, re
import socket

class Debugger:
    def __init__(self):
        self.current_input = ""
        # "/Users/stevelamb/Development/ibcos/investigations
        self.root_directory = "/home/steve/winc/ibcos/Repositorys/gold/source"

    def init(self):
        self.socket = None

        self.output_file = open("out.log", "w")

        """        
        self.code = curses_util.Window(self.main_window, " Code...", 2, 2, 140, 50)
        self.variables = curses_util.Window(self.main_window, " Variables..", 2, 142, 80, 19)
        self.stack = curses_util.Window(self.main_window, " Stack..", 21, 142, 80, 15)
        self.output = curses_util.Window(self.main_window, " Output..", 36, 142, 80, 16)
        """

        self.code = []
        self.variables = []
        self.stack = []
        self.variables = []
        self.output = []

        self.init_matches()
        self.init_socket()

        response = self.send_request("se st ov")
        self.navigate(response)
        response = self.send_request("s")
        self.navigate(response)
        self.send_request("b WHGINE")
        self.update(103)

    def log_out(self, message):
        print(message)

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

        full_response = []
        try:
            self.socket.sendall(request)

            done = False
            while not(done):
                response = self.socket.recv(4096).split('\n')

                for line in response:
                    if output:
                        self.log_out(line)
                    full_response.append(line)
                    if "" != line:
                        self.output.append(line)   
                    if line.startswith("DBG>"):
                        done = True

        except Exception as e:
            self.error = "Connection closed.. %s" % e
            full_response = []

        return full_response     

    def find_file(self, file_name):
        result = None
        for root, dirs, files in os.walk(self.root_directory):
            for file in files:
                if file == file_name:
                    result = os.path.join(root, file)

        return result

    def load_file(self, file_name):
        self.log_out("load_file %s" % file_name)
        src_file = open(file_name, "rt")
        self.file_lines = src_file.readlines()
        src_file.close()

    def get_code(self, file_name, line):
        #self.code.title = " code - %s" % file_name
        if None != file_name:
            self.load_file(file_name)

            self.code = self.file_lines[line - 20 :line + 20]
        else:
            self.code = ["file %s not found.." % file_name]

    def get_stack(self):
        return
        response = self.send_request("tr", output = False)
        self.stack = response

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
        if key == 115 or key == 274 or key == "s":
            request = "s"
            response = self.send_request("s")

        # go
        if key == 269:
            needs_update = True

        if key == 10:
            request = self.process_current_input()
            needs_update = True

        """
        if needs_update:
            self.current_input = ""
        else:
            self.current_input = self.current_input + curses.keyname(key)
        """
        self.navigate(response)
        return needs_update
    
    def navigate(self, response):
        if None != response:
            for response_line in response:
                if "" != response_line:
                    for match in self.matches:
                        re_match = re.match(match["regex"], response_line)

                        if None != re_match:
                            self.log_out("nav match . %s %s" % (re_match.groups()[2], re_match.groups()[0]))
                            self.get_code(self.find_file(re_match.groups()[2]), int(re_match.groups()[0]))

    def update(self, key):
        needs_update = self.process_key(key)
        if None != self.error:
            self.statusbar = "{} Press 'q' to exit".format(self.error)
        else:
            self.statusbar = "Press 'q' to exit {} {}".format(key, self.current_input)

        #self.main_window.addstr(52, 0, self.statusbar, curses.color_pair(3))

        self.get_stack()

