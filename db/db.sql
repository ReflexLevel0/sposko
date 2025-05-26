CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
ALTER EXTENSION "uuid-ossp" SET SCHEMA public;

CREATE TABLE users(
  id UUID DEFAULT UUID_generate_v4() PRIMARY KEY,
  username VARCHAR(32) UNIQUE,
  password VARCHAR(64),
  first_name VARCHAR(64),
  last_name VARCHAR(64),
  phone_number VARCHAR(32),
  email VARCHAR(64)
);

CREATE TABLE administrators(
  id UUID PRIMARY KEY,
  FOREIGN KEY (id) REFERENCES users (id) ON DELETE CASCADE
);

CREATE TABLE parents(
  id UUID PRIMARY KEY,
  amount_owed NUMERIC(10,5),
  FOREIGN KEY (id) REFERENCES users (id) ON DELETE CASCADE
);

CREATE TABLE trainers(
  id UUID PRIMARY KEY,
  date_of_birth DATE NOT NULL,
  info TEXT,
  verified BOOLEAN DEFAULT false,
  FOREIGN KEY (id) REFERENCES users (id) ON DELETE CASCADE
);

CREATE TABLE children(
  id UUID PRIMARY KEY,
  parent_id UUID NOT NULL,
  first_name VARCHAR(64) NOT NULL,
  last_name VARCHAR(64) NOT NULL,
  date_of_birth DATE NOT NULL,
  phone_number VARCHAR(32),
  email VARCHAR(64),
  FOREIGN KEY (parent_id) REFERENCES parents (id) ON DELETE CASCADE
);

CREATE TABLE notes(
  id SERIAL PRIMARY KEY,
  trainer_id UUID NOT NULL,
  child_id UUID NOT NULL,
  text TEXT,
  crated_time TIMESTAMP WITH TIME ZONE,
  FOREIGN KEY (trainer_id) REFERENCES trainers (id),
  FOREIGN KEY (child_id) REFERENCES children (id)
);

CREATE TABLE sports(
  id SERIAL PRIMARY KEY,
  name VARCHAR(64) NOT NULL UNIQUE
);

CREATE TABLE sport_groups(
	id SERIAL PRIMARY KEY,
	name VARCHAR(64) NOT NULL,
	trainer_id UUID,
	sport_id INT NOT NULL,
	max_members INT,
	min_age INT,
	max_age INT,
	FOREIGN KEY (trainer_id) REFERENCES trainers (id),
    FOREIGN KEY (sport_id) REFERENCES sports (id) ON DELETE CASCADE
);

CREATE TABLE sport_trainings(
  id SERIAL PRIMARY KEY,
  group_id INT NOT NULL,
  start_date DATE NOT NULL,
  start_time INTERVAL NOT NULL,
  duration INTERVAL NOT NULL,
  end_date DATE,
  repeat_type VARCHAR(32),
  repeat_interval INT,
  cost DECIMAL(10,5) NOT NULL,
  FOREIGN KEY(group_id) REFERENCES sport_groups (id)
);

CREATE TABLE training_exceptions(
  training_id SERIAL NOT NULL,
  exception_date DATE NOT NULL,
  PRIMARY KEY (training_id, exception_date),
  FOREIGN KEY (training_id) REFERENCES sport_trainings (id)
);

CREATE TABLE child_group(
  child_id UUID NOT NULL,
  group_id INT NOT NULL,
  PRIMARY KEY (child_id, group_id),
  FOREIGN KEY (child_id) REFERENCES children (id) ON DELETE CASCADE,
  FOREIGN KEY (group_id) REFERENCES sport_groups (id) ON DELETE CASCADE
);

INSERT INTO sports(id, name) VALUES
	(1, 'Nogomet'),
	(2, 'Tenis'),
	(3, 'Plivanje');
INSERT INTO users(id, username, password, first_name, last_name, phone_number, email) VALUES
	('e4d064bd-f6b0-47f9-bb08-3330ffa8c2ab', 'admin', 'admin', 'Pero', 'Perić', '098456432', 'pero.peric@gmail.com'),
	('281147de-75bd-4e33-a27c-2a084abfee82', 'ante', 'ante.antic', 'Ante', 'Antić', '095053252', 'ante.antic@gmail.com');
INSERT INTO trainers(id, date_of_birth, info) VALUES
	('e4d064bd-f6b0-47f9-bb08-3330ffa8c2ab', '1993-03-14', 'Ja sam Pero i volim sportove!'),
	('281147de-75bd-4e33-a27c-2a084abfee82', '1975-08-23', 'Ja sam Ante i apsolutno mrzim sportove!!! >:[');
INSERT INTO sport_groups(id, name, trainer_id, sport_id, max_members, min_age, max_age) VALUES
	(1, 'Nogometna grupa 1', 'e4d064bd-f6b0-47f9-bb08-3330ffa8c2ab', 1, 16, 16,18),
	(2, 'Nogometna grupa 2', 'e4d064bd-f6b0-47f9-bb08-3330ffa8c2ab', 1, 24, 18, 19),
	(3, 'Teniska grupa', 'e4d064bd-f6b0-47f9-bb08-3330ffa8c2ab', 2, 8, 10, 14),
	(4, 'Plivački tim', 'e4d064bd-f6b0-47f9-bb08-3330ffa8c2ab', 3, 12, 16, 18),
	(5, 'Antinovićki nogometaši', '281147de-75bd-4e33-a27c-2a084abfee82', 1, 22, 12, 13);
INSERT INTO sport_trainings(group_id, start_date, start_time, duration, cost) VALUES
	(1, '2025-06-01', '16:00:00', '01:45:00', 5),
	(1, '2025-06-08', '14:00:00', '02:15:00', 7.5),
	(1, '2025-06-15', '16:00:00', '01:45:00', 5),
	(2, '2025-06-01', '12:30:00', '02:15:00', 7.5),
	(2, '2025-06-08', '10:30:00', '01:45:00', 5),
	(2, '2025-06-15', '12:30:00', '02:15:00', 7.5),
	(3, '2025-06-03', '12:00:00', '00:45:00', 4),
	(4, '2025-06-05', '13:30:00', '01:00:00', 6),
	(5, '2025-06-07', '11:15:00', '01:15:00', 4);
