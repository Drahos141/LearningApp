// Unbiased Fisher–Yates shuffle; returns a new array.
// (`arr.sort(() => Math.random() - 0.5)` is biased toward the original order.)
export function shuffle(arr) {
  const a = [...arr];
  for (let i = a.length - 1; i > 0; i--) {
    const j = Math.floor(Math.random() * (i + 1));
    [a[i], a[j]] = [a[j], a[i]];
  }
  return a;
}
