module Day05Tests exposing (tests)

import Day05
import Expect
import Test exposing (Test, describe, test)


testData : List String
testData =
    [ "FBFBBFFRLR", "BFFFBBFRRR", "FFFBBBFRRR", "BBFFBBFRLL" ]


tests : Test
tests =
    describe "Day 05"
        [ test "Part 1 seatId" <| always <| Expect.equal [ 357, 567, 119, 820 ] <| List.filterMap Day05.seatId testData
        , test "Part 1 Example" <| always <| Expect.equal 820 <| Day05.part1 <| String.join "\n" <| testData
        , test "Part 1" <| always <| Expect.equal 861 <| Day05.part1 Day05.input
        , test "Part 2" <| always <| Expect.equal 633 <| Day05.part2 Day05.input
        ]
