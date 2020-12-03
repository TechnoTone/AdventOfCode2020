module.exports.part1 = function (input) {
  return input.filter((s) => {
    const parts = s.split(" ");
    const min = parseInt(parts[0].split("-")[0]);
    const max = parseInt(parts[0].split("-")[1]);
    const letter = parts[1][0];
    const password = parts[2];

    const count = charCount(password, letter);

    return count >= min && count <= max;
  }).length;
};

function charCount(s, c) {
  let count = 0;
  let pos = s.indexOf(c);
  while (pos >= 0) {
    count++;
    pos = s.indexOf(c, pos + 1);
  }
  return count;
}

module.exports.part2 = function (input) {
  return input.filter((s) => {
    const parts = s.split(" ");
    const p1 = parseInt(parts[0].split("-")[0]);
    const p2 = parseInt(parts[0].split("-")[1]);
    const letter = parts[1][0];
    const password = parts[2];

    return (password[p1 - 1] === letter) !== (password[p2 - 1] === letter);
  }).length;
};
