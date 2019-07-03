from datetime import datetime

def parse_timestamp(timestamp):
    h = int(timestamp[0:2])
    m = int(timestamp[3:5])
    s = int(timestamp[6:8])
    ms = int(timestamp[9:13])

    d = datetime(2019,1,1,h,m,s,ms * 1000)
    print d
    return d

d1 = parse_timestamp("10:44:15.345")
d2 = parse_timestamp("10:44:16.245")

print d2 - d1

log = open("test.log", "r")

for line in log:
    print line

log.close()
