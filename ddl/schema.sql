-- ================================================================
-- Team 14 - Theme Park
--
-- Conventions:
--'id' in column names is lower-case.
-- Everything else in column names is lower-case.
-- id(s) are varchar(64).
-- description(s) are varchar(128).
-- all contraints (including primary key) at the end of CREATE TABLE statement.
-- *keywords* are upper-case.
-- *datatypes* are lower-case.
--
-- formatted using: https://github.com/darold/pgFormatter
-- ================================================================

-- Users
-- --------------------------------
-- Section that focuses on users and details.

SET search_path TO program_planner;

CREATE TABLE users(
    id varchar(64) NOT NULL,
    username varchar(32) NOT NULL,
    email varchar(64) NOT NULL,
    password_salt varchar(32) NOT NULL,
    password_hash varchar(64) NOT NULL,
    PRIMARY KEY (id),
    UNIQUE (username),
    UNIQUE (email)
);

CREATE TABLE user_details (
    user_id varchar(64) NOT NULL,
    first_name varchar(32),
    last_name varchar(32),
    date_of_birth date,
    phone varchar(16),
    PRIMARY KEY (user_id),
    FOREIGN KEY (user_id) REFERENCES users (id)
);

-- Sessions
-- --------------------------------
-- Section that focuses on sessions.

CREATE TABLE session_types (
    id varchar(64) NOT NULL,
    session_type varchar(32) NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE sessions(
    id varchar(64) NOT NULL,
    title varchar(64) NOT NULL,
    date_held date,
    description varchar(512),
    organizer varchar(64) NOT NULL,
    session_type_id varchar(64),
    PRIMARY KEY (id),
    FOREIGN KEY (organizer) REFERENCES users (id),
    FOREIGN KEY (session_type_id) REFERENCES session_types (id)
);

-- Session attendees
-- --------------------------------
-- Section that focuses on session's that users are attending.

CREATE TABLE session_attendees (
    user_id varchar(64) NOT NULL,
    session_id varchar(64) NOT NULL,
    PRIMARY KEY (user_id, session_id),
    FOREIGN KEY (user_id) REFERENCES users (id),
    FOREIGN KEY (session_id) REFERENCES sessions (id)
);