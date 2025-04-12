CREATE TABLE personal_info(
  id uuid DEFAULT gen_random_uuid() PRIMARY KEY, 
  first_name VARCHAR(64) NOT NULL,
  last_name VARCHAR(64) NOT NULL,
  phone_number VARCHAR(20) NOT NULL,
);

CREATE TABLE account(
  id uuid DEFAULT gen_random_uuid() PRIMARY KEY,
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

CREATE TABLE sport(
  id SERIAL PRIMARY KEY,
  name VARCHAR(64) NOT NULL UNIQUE
);

CREATE TABLE sport_group(
  id uuid DEFAULT gen_random_uuid() PRIMARY KEY,
  name VARCHAR(30) NOT NULL UNIQUE,
  sport_id SERIAL,
  FOREIGN KEY (sport_id) REFERENCES sport (id)
);

CREATE TABLE group_child(
  group_id uuid,
  child_id uuid,
  PRIMARY KEY (group_id, child_id),
  FOREIGN KEY (group_id) REFERENCES sport_group (id),
  FOREIGN KEY (child_id) REFERENCES child (id)
);

CREATE TABLE training_schedule(
  group_id uuid,
  training_start_date DATE,
  training_start_time TIME WITH TIME ZONE NOT NULL,
  training_end_time TIME WITH TIME ZONE NOT NULL,
  training_end_date DATE DEFAULT NULL,
  repeat_type VARCHAR(32) DEFAULT NULL,
  repeat_interval INT DEFAULT NULL,
  PRIMARY KEY(group_id, training_start_date, training_start_time),
  FOREIGN KEY (group_id) REFERENCES sport_group (id)
);
