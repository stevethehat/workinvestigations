vat_repo = gold.GetRepo[Vatrec]()
vat = Vatrec()
vat.Id = 1
found_vat = vat_repo.Get(vat)

new_vat = Vatrec()
new_vat.Id = 99
new_vat.Description = "steves vat"

vat_repo.Add(new_vat)

print "found"
print found_vat

all_vat_rates = vat_repo.GetAll()

for vat in all_vat_rates:
    print "%s - %s" % (vat.Id, vat.Description)

filtered = all_vat_rates.Select(lambda rate : rate.Description)

print filtered

print "in test"