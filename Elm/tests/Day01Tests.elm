module Day01Tests exposing (tests)

import Day01
import Expect
import Test exposing (Test, describe, test)


tests : Test
tests =
    describe "Day 01"
        [ test "Part 1 Example" <| always <| Expect.equal 514579 <| Day01.part1 [ 1721, 979, 366, 299, 675, 1456 ]
        , test "Part 1" <| always <| Expect.equal 444019 <| Day01.part1 Day01.input
        , test "Part 2 Example" <| always <| Expect.equal 241861950 <| Day01.part2 [ 1721, 979, 366, 299, 675, 1456 ]
        , test "Part 2" <| always <| Expect.equal 29212176 <| Day01.part2 Day01.input
        ]
