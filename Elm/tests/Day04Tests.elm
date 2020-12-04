module Day04Tests exposing (tests)

import Day04
import Expect
import Test exposing (Test, describe, test)


testData : String
testData =
    """ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
nhcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in
"""


testInvalidData : String
testInvalidData =
    """eyr:1972 cid:100
hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926

iyr:2019
hcl:#602927 eyr:1967 hgt:170cm
ecl:grn pid:012533040 byr:1946

hcl:dab227 iyr:2012
ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277

hgt:59cm ecl:zzz
eyr:2038 hcl:74454a iyr:2023
pid:3556412378 byr:2007"""


testValidData : String
testValidData =
    """pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980
hcl:#623a2f

eyr:2029 ecl:blu cid:129 byr:1989
iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm

hcl:#888785
hgt:164cm byr:2001 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022

iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719"""


expectedParsedData : List Day04.Passport
expectedParsedData =
    [ Day04.Passport 1937 (Just "147") Day04.Gry 2020 "fffffd" (Day04.Cm 183) 2017 "860033327"
    , Day04.Passport 1931 Nothing Day04.Brn 2024 "ae17e1" (Day04.Cm 179) 2013 "760753108"
    ]


tests : Test
tests =
    describe "Day 04"
        [ test "Parser" <| always <| Expect.equal expectedParsedData <| Day04.parseInput testData
        , test "Part 1 Example" <| always <| Expect.equal 2 <| Day04.part1 testData
        , test "Part 1" <| always <| Expect.equal 235 <| Day04.part1 Day04.input
        , test "Part 2 Invalid Examples" <| always <| Expect.equal 0 <| Day04.part2 testInvalidData
        , test "Part 2 Valid Examples" <| always <| Expect.equal 4 <| Day04.part2 testValidData
        , test "Part 2" <| always <| Expect.equal 194 <| Day04.part2 Day04.input
        ]
