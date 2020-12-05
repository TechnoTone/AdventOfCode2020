module.exports.part1 = function (input) {
  let passports = parseInput(input);
  return passports.filter(isValid1).length;
};

module.exports.part2 = function (input) {
  let passports = parseInput(input);
  return passports.filter(isValid2).length;
};

function parseInput(input) {
  return input
    .replace(/\r/g, "")
    .split(/\n\n/)
    .map((s) => s.replace(/\n/g, " ").trim());
}

function isValid1(line) {
  return line.split(" ").length === (line.indexOf("cid") >= 0 ? 8 : 7);
}

function isValid2(line) {
  return isValid1(line) && line.split(" ").every(validField);
}

function validField(field) {
  const split = field.split(":");
  const key = split[0];
  const value = split[1];

  switch (key) {
    case "byr":
      return isInt(value, 1920, 2002);
    case "iyr":
      return isInt(value, 2010, 2020);
    case "eyr":
      return isInt(value, 2020, 2030);
    case "hgt":
      return isHeight(value);
    case "hcl":
      return isHairColor(value);
    case "ecl":
      return isEyeColor(value);
    case "pid":
      return isId(value);
    case "cid":
      return true;
  }
}

function isInt(value, min, max) {
  const parsed = parseInt(value);
  return !isNaN(parsed) && parsed >= min && parsed <= max;
}

function isHeight(value) {
  return value.endsWith("cm")
    ? isInt(value.replace("cm", ""), 150, 193)
    : value.endsWith("in") && isInt(value.replace("in", ""), 59, 76);
}

function isHairColor(value) {
  return /^#([0-9|[a-f]){6}$/.test(value);
}

const validEyeColors = {
  amb: true,
  blu: true,
  brn: true,
  gry: true,
  grn: true,
  hzl: true,
  oth: true,
};

function isEyeColor(value) {
  return validEyeColors[value];
}

function isId(value) {
  return /^[0-9]{9}$/.test(value);
}
