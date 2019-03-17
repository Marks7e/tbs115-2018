BEGIN TRANSACTION;
DROP TABLE IF EXISTS "LevelSuccessTime";
CREATE TABLE IF NOT EXISTS "LevelSuccessTime" (
	"SuccessID"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"LevelID"	INTEGER NOT NULL UNIQUE,
	"SuccessTime"	INTEGER NOT NULL
);
DROP TABLE IF EXISTS "LevelData";
CREATE TABLE IF NOT EXISTS "LevelData" (
	"LevelID"	INTEGER PRIMARY KEY AUTOINCREMENT,
	"BestScore"	INTEGER NOT NULL DEFAULT 0,
	"RoundTime"	INTEGER NOT NULL DEFAULT 0,
	"PointMultiplier"	INTEGER NOT NULL DEFAULT 1,
	"UnlockLevelAt"	INTEGER NOT NULL DEFAULT 0,
	"TimesPlayed"	INTEGER
);
DROP TABLE IF EXISTS "PlayerData";
CREATE TABLE IF NOT EXISTS "PlayerData" (
	"PlayerID"	INTEGER PRIMARY KEY AUTOINCREMENT,
	"TotalScore"	INTEGER NOT NULL DEFAULT 0
);
DROP TABLE IF EXISTS "QuestionData";
CREATE TABLE IF NOT EXISTS "QuestionData" (
	"QuestionID"	INTEGER PRIMARY KEY AUTOINCREMENT,
	"RealmNumber"	INTEGER NOT NULL,
	"Question"	TEXT NOT NULL,
	"Answer"	TEXT NOT NULL
);
DROP TABLE IF EXISTS "GameOptions";
CREATE TABLE IF NOT EXISTS "GameOptions" (
	"OptionID"	INTEGER PRIMARY KEY AUTOINCREMENT,
	"Parameter"	TEXT NOT NULL,
	"PValue"	TEXT NOT NULL
);
COMMIT;