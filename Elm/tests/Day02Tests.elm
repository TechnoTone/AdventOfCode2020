module Day02Tests exposing (tests)

import Day02 exposing (PasswordSpec)
import Expect
import Test exposing (Test, describe, test)


testData : List PasswordSpec
testData =
    [ PasswordSpec 1 3 'a' "abcde"
    , PasswordSpec 1 3 'b' "cdefg"
    , PasswordSpec 2 9 'c' "ccccccccc"
    ]


tests : Test
tests =
    describe "Day 02"
        [ test "Part 1 Example" <| always <| Expect.equal 2 <| Day02.part1 testData
        , test "Part 1" <| always <| Expect.equal 506 <| Day02.part1 Day02.input
        , test "Part 2 Example" <| always <| Expect.equal 1 <| Day02.part2 testData
        , test "Part 2" <| always <| Expect.equal 443 <| Day02.part2 Day02.input
        ]
