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
  start_time TIME WITH TIME ZONE NOT NULL,
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
