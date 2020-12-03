module.exports.part1 = function (input) {
  const pairs = uniquePairs(input);
  const pair = pairs.find((pair) => pair[0] + pair[1] == 2020);
  return pair[0] * pair[1];
};

module.exports.part2 = function (input) {
  const triplets = uniqueTriplets(input);
  const triplet = triplets.find(
    (triplet) => triplet[0] + triplet[1] + triplet[2] == 2020
  );
  return triplet[0] * triplet[1] * triplet[2];
};

function uniquePairs(input) {
  const pairs = [];
  for (let i = 0; i < input.length - 1; i++)
    for (let j = i; j < input.length; j++) {
      pairs.push([input[i], input[j]]);
    }
  return pairs;
}

function uniqueTriplets(input) {
  const triplets = [];
  for (let i = 0; i < input.length - 2; i++)
    for (let j = i; j < input.length - 1; j++)
      for (let k = j; k < input.length; k++) {
        triplets.push([input[i], input[j], input[k]]);
      }
  return triplets;
}
