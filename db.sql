CREATE TABLE user(
  id uuid DEFAULT gen_random_uuid() PRIMARY KEY,
  username VARCHAR(32) UNIQUE,
  password VARCHAR(64),
  first_name VARCHAR(64),
  last_name VARCHAR(64),
  phone_number VARCHAR(32),
  email VARCHAR(64)
);

CREATE TABLE administrator(
  id uuid PRIMARY KEY,
  FOREIGN KEY (id) REFERENCES user (id) ON DELETE CASCADE
);

CREATE TABLE parent(
  id uuid PRIMARY KEY,
  amount_owed NUMERIC(10,5),
  FOREIGN KEY (id) REFERENCES user (id) ON DELETE CASCADE
);

CREATE TABLE trainer(
  id uuid PRIMARY KEY,
  date_of_birth DATE NOT NULL,
  info TEXT,
  verified BOOLEAN DEFAULT false,
  FOREIGN KEY (id) REFERENCES user (id) ON DELETE CASCADE 
);

CREATE TABLE child(
  id uuid PRIMARY KEY,
  parent_id uuid NOT NULL,
  first_name VARCHAR(64) NOT NULL,
  last_name VARCHAR(64) NOT NULL,
  date_of_birth DATE NOT NULL,
  phone_number VARCHAR(32),
  email VARCHAR(64),
  FOREIGN KEY (parent_id) REFERENCES parent (id) ON DELETE CASCADE
);

CREATE TABLE note(
  id SERIAL PRIMARY KEY,
  trainer_id uuid,
  child_id uuid,
  text TEXT,
  crated_time DATETIME WITH TIME ZONE,
  FOREIGN KEY (trainer_id) REFERENCES trainer (trainer_id),
  FOREIGN KEY (child_id) REFERENCES child (child_id)
);

CREATE TABLE sport(
  id SERIAL PRIMARY KEY,
  name VARCHAR(64) NOT NULL UNIQUE
);

CREATE TABLE child_training(
  training_id uuid NOT NULL,
  child_id uuid NOT NULL,
  verified BOOLEAN DEFAULT FALSE,
  PRIMARY KEY (training_id, child_id),
  FOREIGN KEY (training_id) REFERENCES training (id) ON DELETE CASCADE,
  FOREIGN KEY (child_id) REFERENCES child (id) ON DELETE CASCADE
);

CREATE TABLE training(
  id SERIAL PRIMARY KEY,
  trainer_id uuid,
  max_members INT,
  min_age INT,
  max_age INT,
  sport_id SERIAL NOT NULL,
  start_date DATE NOT NULL,
  start_time TIME WITH TIME ZONE NOT NULL,
  duration INTERVAL NOT NULL,
  end_date DATE,
  repeat_type VARCHAR(32),
  repeat_interval INT,
  FOREIGN KEY (group_id) REFERENCES sport_group (id) ON DELETE CASCADE,
  FOREIGN KEY (trainer_id) REFERENCES trainer (id),
  FOREIGN KEY (sport_id) REFERENCES sport (id) ON DELETE CASCADE
);

CREATE TABLE training_exception(
  training_id SERIAL NOT NULL,
  exception_date DATE NOT NULL,
  PRIMARY KEY (training_id, exception_date),
  FOREIGN KEY (training_id) REFERENCES training (training_id)
);
