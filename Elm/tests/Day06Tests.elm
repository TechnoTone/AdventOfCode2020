module Day06Tests exposing (tests)

import Day06
import Expect
import Test exposing (Test, describe, test)


testData : String
testData =
    "abc,,a,b,c,,ab,ac,,a,a,a,a,,b" |> String.replace "," "\n"


tests : Test
tests =
    describe "Day 06"
        [ test "Part 1 Example" <| always <| Expect.equal 11 <| Day06.part1 testData
        , test "Part 1" <| always <| Expect.equal 7027 <| Day06.part1 Day06.input
        , test "Part 2 Example" <| always <| Expect.equal 6 <| Day06.part2 testData
        , test "Part 2" <| always <| Expect.equal 3579 <| Day06.part2 Day06.input
        ]
