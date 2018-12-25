# Unit Analysis and Design

This document serves as a purpose as an introductory proposal to unit design (including stats, skills, interactions). This document will also run over basic status/disabilities that may be included in the game.

## Notes

* All units will be balanced around a 100 HP basis.
* All units will be balanced around a 10 frame turn
* Skills cannot span turns (tentative)

# Status Alignments

Based of popular strategy titles such as `League of Legends`, `Dota` and `Starcraft`, Project Castle will also include debuffs which will diversify gameplay. Debuffs act as a tool players can use to setup a broader set of strategies while lowering the "guessing" factor. 

All status alignments will run on a `time-based factor` - that is, all debuffs are temporary and only last a specified number of frames which can span multiple turns. A debuff can be prematurely dropped by potential `cleansing spells`.

The following is a tentative list of debuffs that can potentially be included.

# List of Status Alignments

## Stun

Drawn across multiple sources, the definitive effect of a stun `prohibits a unit from all actions`. A unit will be locked from executing any actions during that turn until the stun duration is over. Once the stun is over, the unit may `execute any actions left in its queue before the turn ends`. A stunned unit may be stunned again but the counter is `not additive` - the highest stunned duration will be chosen.

> Any skills which may span to the next turn will be dropped

A `stun may also span multiple turns`, in which case `the player can still queue up actions for the unit`, but will only start `executing the actions after the stun debuff has been dropped`.

### Analysis

Stuns are the `most dangerous of status alignments` because of their ability to incapacitate a unit (temporarily removing it from the game). Stuns can easily lead to safe approach options, setups for the turn and even the following turn. 

The tool can potentially be abused - stuns which last too long/are easy to hit will act as a defensive tool rather than an offensive one, thus `promoting stalemate rather than ending one`.

## Snare

A lesser stun in which a snare only `prohibits a unit from moving`. A unit will be locked from executing any movement actions during that turn until the snare duration is over. During the snare, the unit will try to `execute actions as much as possible`. A snared unit may be snared again but the counter is `not additive` - the highest snared duration will be chosen.

A `snare may also span multiple turns`, in which case `the player can still queue up actions for the unit`, but will only `execute valid actions` while the snare is in effect.

### Analysis

Snares just like stuns are incredible for setups. Generally in most strategy games, snares `tend to last slightly longer than stuns` to compensate for the difference in effect. In essence, snares can potentially be even more dangerous than stuns because of its longer duration.

Snares can potentially be abused by defensive strategies. Snares often are used to `limit enemy advancement` or to `catch enemies out of position`, which may be hard to deal with.

## Silence

The other half of the snare, a silence `prohibits enemies from casting attacks`. A unit will be locked from executing attacking actions during that turn until the silence duration is over. During the silence, the unit will try to `execute actions as much as possible`. A silenced unit may be silenced again but the counter is `not additive` - the highest silenced duration will be chosen.

A `silence may also span multiple turns`, in which case `the player can still queue up actions for the unit`, but will only `execute valid actions` while the snare is in effect.

### Analysis

Silences are great for the offensive strategy. This debuff allows a player to `make openings in an opponent's defense` and break sieges that would normally hold out against the player. Similarily, silences `typically last longer or are easier to hit` than stuns to make up for the difference in effect. 

Silences can potentially cause fustrating battles. Overusing the effect can slow down battles and `deny attackers kill pressure too often`.

## Poison

Poison effect temporarily `applies small ticks of damage to a poisoned unit.` A poisoned unit will take damage (perhaps % or otherwise flat) every frame for the poisoned duration. A poisoned unit may be poisoned again but the counter is `not additive` - the highest poison duration will be chosen. 

### Analysis

Poison is a status generally employed by `hit and run strategies` which allow lesser units to topple over high health units over time. Many strategy games also support attacks which yield bonus effects when dealt to poisoned units.

Although hit and run strategies are valid gameplay strategies, overwhelming poison may make it difficult for an opponent to retaliate.

## Knockback

Knockbacks `displace units around the board`. Knockbacks can applied to both allied and enemy units, moving the target unit possibly multiple tiles during a single frame. 

### Analysis

Knockback is a versatile tool which allows players to play `keep-away` against certain enemy units, or quickly deploy slower moving units to the front line faster. 

Large knockback units may become incredibly fustrating to play against due to their keep-away potential. 

# Units

In-order to diversify Project Castle's combat potential, a diverse set of units exist to support each other. Each unit has their own cambat specialty which encourages players to mix and match a unique set of strategies.

## King

The King is the lifeline of the player. `Once the king is slain, the game ends`. The King doesn't provide a strong combat ability, but a good player will know when to use the King to sway the fight in their favor.

| HP  | Total AP |
|-----|----------|
| 200 | 5        |

### Actions

| Action | AP Cost | Area of Effect | Frame Description                                                                                          |
|--------|---------|----------------|------------------------------------------------------------------------------------------------------------|
| Move   | 2       | -              | [1] Frame - Moves one tile in a direction                                                                   |
| Lance  | 3       |➡️❎✳️			| [1, 2] Frame - Deals 40 Damage <br> [3, 4] Frame - Ending Lag <br><br>  Sweet Spot - Knockback enemy 1 tile |

## Knight

The Knight plays the role of a front-liner in the player's team. A knight is `bulky enough to take damage from enemy seige` and `deals enough damage to break through enemy defense`.

| HP  | Total AP |
|-----|----------|
| 175 | 6        |


### Actions

| Action | AP Cost | Area of Effect | Frame Description                                                                                          |
|--------|---------|----------------|------------------------------------------------------------------------------------------------------------|
| Move   | 1       | -              | [1] Frame - Moves one tile in a direction                                                                    |
| Slash  | 3       |➖❎<br>➡️❎<br>➖❎| [1, 2, 3] Frame - Deals 60 Damage <br> [4, 5] Frame - Ending Lag <br>|

## Archer

The Archer holds down narrow corridors against enemy advances. Although frail in health, the archer makes up for much of the `team's setup and damage`.

| HP  | Total AP |
|-----|----------|
| 90  | 4        |


### Actions

| Action | AP Cost | Area of Effect | Frame Description                                                                                          |
|--------|---------|----------------|------------------------------------------------------------------------------------------------------------|
| Move   | 1       | -              | [1] Frame - Moves one tile in a direction                                                                    |
| Power Shot | 1   |➡️✳️✳️✳️❎✴️✴️ | [1] Frame - Deals 30 Damage to first target hit <br> [2] Frame - Ending Lag <br><br> Sweet Spot - Knockback 1 tile. If enemy collides, also stun for 3 frames and an additional 30 damage <br> Sour Spot - Deals 15 less damage|

## Mage

The Mage is a powerful magic user who exercises consistent AOE damage at a range. The mage `capitalizes on enemy advancement and controls areas of battle`.

| HP  | Total AP |
|-----|----------|
| 100 | 8        |

### Actions

| Action | AP Cost | Area of Effect | Frame Description                                                                                          |
|--------|---------|----------------|------------------------------------------------------------------------------------------------------------|
| Move   | 2       | -              | [1] Frame - Moves one tile in a direction                                                                    |
| Incinerate | 6   | ✴️❎✴️<br>❎✳️❎<br>✴️❎✴️ | Can be casted at a range between [2, 4]. <br><br> [1] Frame - Sarting Lag <br> [2,3,4,5] Frame - Deals 60 Damage <br> [6] Frame - Ending Lag <br><br> Sweet Spot - Deals additional 40 Damage <br> Sour Spot - Deals 20 less damage|

## Rogue

The rogue is a highly-mobile and skillful unit which focuses on `weaving between battle and dealing damage to key units`.

| HP  | Total AP |
|-----|----------|
| 120 | 9        |

### Actions

| Action | AP Cost | Area of Effect | Frame Description                                                                                          |
|--------|---------|----------------|------------------------------------------------------------------------------------------------------------|
| Move   | 1       | -              | [1] Frame - Moves one tile in a direction                                                                    |
| Poison Jab | 2   | ➡️✳️❎ | [1] Frame - Deal 30 Damage <br><br> Sweet Spot - Apply 3 frames of poison |

# Items

As the game progresses, so should a unit's capabilities. Attaching these items to a unit may `grant a combination of stats or new skills`. Here are some sample items which may be included in Project Castle.

## Axe of Despair

Grants the Knight an axe ... of despair.

### Actions

| Action | AP Cost | Area of Effect | Frame Description                                                                                          |
|--------|---------|----------------|------------------------------------------------------------------------------------------------------------|
| Judgement |  3  | ❎❎✳️ <br> ❎➡️✳️ <br> ❎✴️✴️ | [1] Frame - Starting Lag <br> [2, 3] Frame - Deals 70 Damage <br> [4] - Ending Lag <br><br> Sweet Spot - Heals 20 HP <br> Sour Spot - Deals 20 less Damage |

## Storm's Edge

The Knight becomes a natural disaster. 

### Actions

| Action | AP Cost | Area of Effect | Frame Description                                                                                          |
|--------|---------|----------------|------------------------------------------------------------------------------------------------------------|
| Eye of The Storm |  5  | ❎❎🔽➖ <br> ➡️✳️✳️◀️ <br> ❎❎🔼➖ | [1] Frame - Starting Lag, Storm appears <br> [2, 3, 4] Frame - Deals 70 Damage <br> [5, 6] - Ending Lag, Storm dissipates <br><br> Sweet Spot - Stuns enemy for 5 frames <br> Storm - Pushes enemy in the specified direction by 1 tile |

## Athene's Bow

The Archer fires arrows which pierces all enemies. `Power Shot` now damages and applies effects on all enemies within the hit range. 

## Bomb Arrows

The Archer fires an explosive arrow which deals splash damage.

| Action | AP Cost | Area of Effect | Frame Description                                                                                          |
|--------|---------|----------------|------------------------------------------------------------------------------------------------------------|
| Explosive Shot |  2  | ➡️❎❎❎❎❎ <br><br> ❎❎❎<br>❎❎❎<br>❎❎❎ | [1] Frame - Deals 30 Damage <br> [2] - Ending Lag <br><br> On Impact - Explodes and deals 30 damage to all units within the explosion (including the unit hit) |

## Eye of the Oracle

The Mage obtains the ability to change the future.

| Action | AP Cost | Area of Effect | Frame Description                                                                                          |
|--------|---------|----------------|------------------------------------------------------------------------------------------------------------|
| Future Sight |  4  | ➖❎➖ <br> ❎❎❎<br>➖❎➖ | [1, 2] Frame - Starting Lag <br> [3, 4] Frame - Delayed 5 Frame Stun starting next turn <br> [5] Frame - Ending Lag |

## The Big Book About Transportation

The Mage obtains the ability to dematerialize and rematerialize at a nearby location.

| Action | AP Cost | Area of Effect | Frame Description                                                                                          |
|--------|---------|----------------|------------------------------------------------------------------------------------------------------------|
| Teleport |  2  | ➖➖🔯➖➖ <br> ➖🔯➖🔯➖ <br> 🔯➖➡️➖🔯 <br> ➖🔯➖🔯➖ <br> ➖➖🔯➖➖ | [1] Frame - Blinks to a nearby location two tiles away from the user |

## Kunai With Chain

The Rogue throws a kunai which drags any enemies hit towards him.

| Action | AP Cost | Area of Effect | Frame Description                                                                                          |
|--------|---------|----------------|------------------------------------------------------------------------------------------------------------|
| Chain Hook |  3  | ➡️✳️✳️✳️❎  | [1] Frame - Deals 30 Damage to first target <br> [2] Frame - Ending Lag <br><br> Sweet Spot - Pulls enemy in front of Rogue and snares for 5 frames

## Serpent's Tongue

The Rogue gains a pair of daggers (... like the forked ends of a snake tongue)!

| Action | AP Cost | Area of Effect | Frame Description                                                                                          |
|--------|---------|----------------|------------------------------------------------------------------------------------------------------------|
| Cross Step |  4  | ❎➖❎ <br> ➡️✳️🔯 <br> ❎➖❎ | [1, 2] Frame - Deals 40 Damage <br> [3] Frame - Ending Lag, Rogue is moved to target location if possible <br><br> Sweet Spot - Debuffed enemy takes additional 50 Damage


