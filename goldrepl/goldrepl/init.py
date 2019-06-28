import sys
sys.path.append("/home/steve/winc/ibcos/Repositorys/gold/api-server/Build/Debug")
clr.AddReference("Microsoft.Extensions.Options")
clr.AddReference("System.ComponentModel.DataAnnotations")
clr.AddReference("GoldApiServer.DataLayer")
clr.AddReference("Gold")

from Gold import Gold
from Net.Ibcos.GoldAPIServer.DataLayer.Models import *

gold = Gold("/home/steve/winc/ibcos/Repositorys/golddata/gold/data")


