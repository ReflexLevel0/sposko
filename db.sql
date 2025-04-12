CREATE TABLE personal_info(
  id uuid DEFAULT gen_random_uuid() PRIMARY KEY, 
  first_name VARCHAR(64) NOT NULL,
  last_name VARCHAR(64) NOT NULL,
  phone_number VARCHAR(32),
  email VARCHAR(64)
);

CREATE TABLE account(
  id uuid PRIMARY KEY,
  username VARCHAR(32) NOT NULL UNIQUE,
  password VARCHAR(64) NOT NULL,
  FOREIGN KEY (id) REFERENCES personal_info (id) ON DELETE CASCADE
);

CREATE TABLE administrator(
  id uuid PRIMARY KEY,
  FOREIGN KEY (id) REFERENCES personal_info (id) ON DELETE CASCADE
);

CREATE TABLE parent(
  id uuid PRIMARY KEY,
  FOREIGN KEY (id) REFERENCES personal_info (id) ON DELETE CASCADE
);

CREATE TABLE trainer(
  id uuid PRIMARY KEY,
  verified BOOLEAN DEFAULT false,
  FOREIGN KEY (id) REFERENCES personal_info (id) ON DELETE CASCADE 
);

CREATE TABLE child(
  id uuid PRIMARY KEY,
  FOREIGN KEY (id) REFERENCES personal_info (id) ON DELETE CASCADE
);

CREATE TABLE note(
  id SERIAL PRIMARY KEY,
  trainer_id uuid,
  child_id uuid,
  text TEXT,
  FOREIGN KEY (trainer_id) REFERENCES trainer (trainer_id),
  FOREIGN KEY (child_id) REFERENCES child (child_id)
);

CREATE TABLE sport(
  id SERIAL PRIMARY KEY,
  name VARCHAR(64) NOT NULL UNIQUE
);

CREATE TABLE sport_group(
  id uuid DEFAULT gen_random_uuid() PRIMARY KEY,
  name VARCHAR(30) NOT NULL UNIQUE,
  sport_id SERIAL NOT NULL,
  max_members INT,
  min_age INT,
  max_age INT,
  FOREIGN KEY (sport_id) REFERENCES sport (id) ON DELETE CASCADE
);

CREATE TABLE child_group(
  group_id uuid,
  child_id uuid,
  PRIMARY KEY (group_id, child_id),
  FOREIGN KEY (group_id) REFERENCES sport_group (id) ON DELETE CASCADE,
  FOREIGN KEY (child_id) REFERENCES child (id) ON DELETE CASCADE
);

CREATE TABLE training(
  group_id uuid,
  start_date DATE NOT NULL,
  start_time TIME WITH TIME ZONE NOT NULL,
  duration INTERVAL NOT NULL,
  end_date DATE,
  repeat_type VARCHAR(32),
  repeat_interval INT,
  PRIMARY KEY(group_id, training_start_date, training_start_time),
  FOREIGN KEY (group_id) REFERENCES sport_group (id) ON DELETE CASCADE
);
