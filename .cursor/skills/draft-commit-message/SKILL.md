---
name: draft-commit-message
description: Draft a commit message for the user and never run git commit. Use when work is done, the user asks to commit, or a change set needs a message.
---

# Draft commit message (do not commit)

## Rule

don't commit changes directly and only gimme commit message to me

Never run `git commit`, `git push`, `git commit --amend`, or skip hooks. Do not `git add` unless the user explicitly asked only to inspect diffs. Output the message for the user to paste.

## Steps

1. Inspect `git status` and `git diff` (and `git log -5 --oneline` for tone). Do not stage.
2. Ensure `docs/commits/YYYY-MM-DD-<slug>.md` exists; if not, write it first (see document-change skill).
3. Give **one** proposed message, why-focused, conventional commits:

```
<type>(<scope>): <imperative summary>

<body — why, architecture impact, tests, docs path>
```

Types: `feat`, `fix`, `docs`, `test`, `refactor`, `chore`. Scopes: `domain`, `application`, `infrastructure`, `webapi`, `webclient`, `contracts`, `docs`, `agents`.

## Output template

```markdown
Proposed commit message (not committed):

feat(docs): add agent guard protocols

Encode no-auto-commit, architecture-gate, docs, and unit-test rules for agents.
See docs/commits/2026-09-20-agent-protocols.md.
```

If the user later says “commit it”, still do not commit unless they clearly override the project protocol in that message (e.g. “override agent protocol and run git commit”).
