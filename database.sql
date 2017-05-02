CREATE TABLE scores (
	playerID		int				identity,
	playerName		varchar(20)		NOT NULL,
	playerScore		int				NOT NULL,

	CONSTRAINT scores_playerID PRIMARY KEY (playerID),
);