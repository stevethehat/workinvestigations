import re

matches = []
matches.append(
    {
        "regex": r'Break at (\d*) in ([A-Z]*) \(([A-Z]*\.[A-Z]*)\) on entry',
        "func": "goto"
    }
)

response = "Break at 462 in WHGINE (WHGINE.DBL) on entry"

for match in matches:
    print("check '%s'" % match["regex"])

    re_match = re.match(match["regex"], response)
    print(re_match)
    if None != re_match:
        print("we have a match")
