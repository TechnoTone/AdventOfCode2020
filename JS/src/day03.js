module.exports.part1 = function (input) {
  return traverse(input, 3, 1);
};

module.exports.part2 = function (input) {
  return (
    traverse(input, 1, 1) *
    traverse(input, 3, 1) *
    traverse(input, 5, 1) *
    traverse(input, 7, 1) *
    traverse(input, 1, 2)
  );
};

function traverse(input, dx, dy) {
  let x = 0;
  let y = 0;
  let width = input[0].length;
  let height = input.length;
  let trees = 0;

  while (y < height) {
    x = (x + dx) % width;
    y += dy;
    if (y < height && input[y][x] === "#") trees++;
  }

  return trees;
}
