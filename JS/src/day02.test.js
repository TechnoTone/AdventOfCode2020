const { test } = require("@jest/globals");
const { part1, part2 } = require("./day02");
const Input = require("./input");

test("Part 1 Example", () => {
  const input = ["1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc"];
  expect(part1(input)).toBe(2);
});

test("Part 1", () => {
  const input = new Input(2).fromLines().get();
  expect(part1(input)).toBe(506);
});

test("Part 2 Example", () => {
  const input = ["1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc"];
  expect(part2(input)).toBe(1);
});

test("Part 2", () => {
  const input = new Input(2).fromLines().get();
  expect(part2(input)).toBe(443);
});
