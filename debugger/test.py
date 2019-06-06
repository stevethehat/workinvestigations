import re

matches = []
matches.append(
    {
        "regex": r'Break at (\d*) in ([A-Z]*) \(([A-Z]*\.[A-Z]*)\) on entry',
        "func": "goto"
    }
)
matches.append(
    {
        "regex": r'Step to (\d*) in ([A-Z]*) \(([A-Z]*\.[A-Z]*)\)',
        "func": "goto"
    }
)

response = "Step to 463 in WHGINE (WHGINE.DBL)"
for match in matches:
    print("check '%s'" % match["regex"])

    re_match = re.match(match["regex"], response)
    print(re_match)
    if None != re_match:
        print("we have a match")
