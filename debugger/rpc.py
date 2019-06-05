# Echo client program
import socket
import sys, os, pickle

methods = {
    "class" : '{ "method" : "Net.Ibcos.GoldAPIServer.CoreControllers.UtilitiesCoreController.Ping", "params" : {}}\n\r',
    "cpq"   : '{ "method" : "Net.Ibcos.GoldAPIServer.CoreControllers.WholegoodsExportController.Export", "params" : { "Company": 1 }}\n\r',
    "cpq2"   : '{ "method" : "Net.Ibcos.GoldAPIServer.BusinessLayer.Manufacturers.AGCO.CPQ.MachineInventoryController.MachineInventory", "params" : { "Company": 1 }}\n\r'
}
#Net.Ibcos.GoldAPIServer.BusinessLayer.Manufacturers.AGCO.CPQ.MachineInventoryController.MachineInventory

def send_request(method):
    print "testing request %s..." % method
    print methods[method]
    HOST = 'localhost'          # The remote host
    PORT = 3333                 # The same port as used by the server
    message = methods[method]
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    s.connect((HOST, PORT))
    s.sendall(message)
    data = s.recv(1024)
    s.close()
    print 'Received', repr(data)

last_sent_file_name = ".rpc.txt"
method = ""

if len(sys.argv) > 1:
    # get method
    method = sys.argv[1]
    last_sent_file = open(last_sent_file_name, "wb")
    last_sent = pickle.dump([method], last_sent_file)
    last_sent_file.close()
else:
    # try to load method
    last_sent_file = open(last_sent_file_name, "rb")
    last_sent = pickle.load(last_sent_file)[0]
    last_sent_file.close()

send_request(method)