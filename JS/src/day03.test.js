const { test } = require("@jest/globals");
const { part1, part2 } = require("./day03");
const Input = require("./input");

test("Part 1 Example", () => {
  const input = [
    "..##.......",
    "#...#...#..",
    ".#....#..#.",
    "..#.#...#.#",
    ".#...##..#.",
    "..#.##.....",
    ".#.#.#....#",
    ".#........#",
    "#.##...#...",
    "#...##....#",
    ".#..#...#.#",
  ];

  expect(part1(input)).toBe(7);
});

test("Part 1", () => {
  const input = new Input(3).fromLines().get();
  expect(part1(input)).toBe(232);
});

test("Part 2 Example", () => {
  const input = [
    "..##.......",
    "#...#...#..",
    ".#....#..#.",
    "..#.#...#.#",
    ".#...##..#.",
    "..#.##.....",
    ".#.#.#....#",
    ".#........#",
    "#.##...#...",
    "#...##....#",
    ".#..#...#.#",
  ];

  expect(part2(input)).toBe(336);
});

test("Part 2", () => {
  const input = new Input(3).fromLines().get();
  expect(part2(input)).toBe(3952291680);
});
