import sys, os, io, re
import socket
import coredebugger


def main():
    print("starting")
    debugger = coredebugger.Debugger()
    debugger.init()

    k = ''
    # Loop where
#   k = '' k is the last character pressed
    while (k != 'q'):
        # Refresh the screen
        debugger.update(k)

        print("\n".join(debugger.code))
        print(len(debugger.code))
        # Wait for next input
        k = raw_input("next...")

    debugger.close()
    print("done")

if __name__ == "__main__":
    main()
