module Day07Tests exposing (tests)

import Day07
import Expect
import Test exposing (Test, describe, test)


testData : String
testData =
    """light red bags contain 1 bright white bag, 2 muted yellow bags.
       dark orange bags contain 3 bright white bags, 4 muted yellow bags.
       bright white bags contain 1 shiny gold bag.
       muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
       shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
       dark olive bags contain 3 faded blue bags, 4 dotted black bags.
       vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
       faded blue bags contain no other bags.
       dotted black bags contain no other bags."""


expectedParsingResult : List ( String, List ( Int, String ) )
expectedParsingResult =
    [ ( "light red", [ ( 1, "bright white" ), ( 2, "muted yellow" ) ] )
    , ( "dark orange", [ ( 3, "bright white" ), ( 4, "muted yellow" ) ] )
    , ( "bright white", [ ( 1, "shiny gold" ) ] )
    , ( "muted yellow", [ ( 2, "shiny gold" ), ( 9, "faded blue" ) ] )
    , ( "shiny gold", [ ( 1, "dark olive" ), ( 2, "vibrant plum" ) ] )
    , ( "dark olive", [ ( 3, "faded blue" ), ( 4, "dotted black" ) ] )
    , ( "vibrant plum", [ ( 5, "faded blue" ), ( 6, "dotted black" ) ] )
    , ( "faded blue", [] )
    , ( "dotted black", [] )
    ]


tests : Test
tests =
    describe "Day 07"
        [ test "Parsing" <| always <| Expect.equal expectedParsingResult <| Day07.parse testData
        , test "Part 1 Example" <| always <| Expect.equal 4 <| Day07.part1 testData
        , test "Part 1" <| always <| Expect.equal 316 <| Day07.part1 Day07.input
        , test "Part 2 Example" <| always <| Expect.equal 32 <| Day07.part2 testData
        , test "Part 2" <| always <| Expect.equal 11310 <| Day07.part2 Day07.input
        ]
