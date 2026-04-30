# JSON Lesson Files

This folder contains lesson content for the **Psychology** category (category `_id: 7`, subcategory `_id: 25 — Foundations of Psychology`).

Each `.json` file represents one lesson. When `node seed.js` is run, all `.json` files in this folder are loaded automatically in alphabetical file-name order.

## Adding a lesson

1. Create a new `.json` file named `<lessonId>-<slug>.json` (e.g. `76-social-psychology.json`).
2. Use the structure below (all fields are required unless marked optional).
3. Re-run `node seed.js` to push the new lesson to the database.

```json
{
  "id": 76,
  "order": 4,
  "title": "Social Psychology",
  "content": "Main lesson text. Use \\n\\n to separate paragraphs.\n\nLines starting with '* ' render as bullet points.",
  "depths": [
    "Optional deeper-dive content shown when the user clicks 'Go Deeper — Level 1'."
  ],
  "questions": [
    ["Question text?", ["Option A", "Option B", "Option C", "Option D"], 0, "Explanation of the correct answer."]
  ],
  "flashcards": [
    ["Term", "Definition"]
  ]
}
```

### Field reference

| Field | Type | Notes |
|---|---|---|
| `id` | number | Must be unique across **all** lessons in the app |
| `order` | number | Display order within the subcategory (1, 2, 3…) |
| `title` | string | Lesson title |
| `content` | string | Main lesson body; `\n\n` = paragraph break; lines starting with `* ` = bullet points |
| `depths` | string[] | Each entry is one "Go Deeper" level (can be empty `[]`) |
| `questions` | array | Each entry: `[questionText, [4 options], correctIndex (0-based), explanation]` |
| `flashcards` | array | Each entry: `[front, back]` |
| `additionalInfo` | string? | Optional extra info shown in a collapsible section |
| `deepDive` | string? | Optional deep-dive shown in a collapsible section |

## Removing a lesson

Delete the corresponding `.json` file and re-run `node seed.js`.
