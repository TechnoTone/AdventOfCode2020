module Day03Tests exposing (tests)

import Day03
import Expect
import Test exposing (Test, describe, test)


testData : List String
testData =
    [ "..##......."
    , "#...#...#.."
    , ".#....#..#."
    , "..#.#...#.#"
    , ".#...##..#."
    , "..#.##....."
    , ".#.#.#....#"
    , ".#........#"
    , "#.##...#..."
    , "#...##....#"
    , ".#..#...#.#"
    ]


tests : Test
tests =
    describe "Day 03"
        [ test "Part 1 Example" <| always <| Expect.equal 7 <| Day03.part1 testData
        , test "Part 1" <| always <| Expect.equal 232 <| Day03.part1 Day03.input
        , test "Part 2 Example" <| always <| Expect.equal 336 <| Day03.part2 testData
        , test "Part 2" <| always <| Expect.equal 3952291680 <| Day03.part2 Day03.input
        ]
