const { test } = require("@jest/globals");
const { part1, part2 } = require("./day01");
const Input = require("./input");

test("Part 1 Example", () => {
  const input = [1721, 979, 366, 299, 675, 1456];
  expect(part1(input)).toBe(514579);
});

test("Part 1", () => {
  const input = new Input(1).fromLines().asIntArray();
  expect(part1(input)).toBe(444019);
});

test("Part 2 Example", () => {
  const input = [1721, 979, 366, 299, 675, 1456];
  expect(part2(input)).toBe(241861950);
});

test("Part 2", () => {
  const input = new Input(1).fromLines().asIntArray();
  expect(part2(input)).toBe(29212176);
});
